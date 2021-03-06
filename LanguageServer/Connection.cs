﻿using LanguageServer.Json;
using LanguageServer.Parameters.General;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LanguageServer
{
    public class Connection
    {
        private Stream input;
        private Stream output;
        private readonly object outputLock = new object();
        private readonly Handlers handlers = new Handlers();

        internal Handlers Handlers => handlers;

        public Connection(Stream input, Stream output)
        {
            this.input = input;
            this.output = output;
        }

        public async Task Listen()
        {
            while (await ReadAndHandle())
            {
            }
        }

        public async Task<bool> ReadAndHandle()
        {
            var json = await Read();
            var messageTest = (MessageTest)Serializer.Instance.Deserialize(typeof(MessageTest), json);
            if (messageTest == null)
            {
                return false;
            }
            if (messageTest.IsRequest)
            {
                HandleRequest(messageTest.method, messageTest.id, json);
            }
            else if (messageTest.IsResponse)
            {
                HandleResponse(messageTest.id, json);
            }
            else if (messageTest.IsCancellation)
            {
                HandleCancellation(json);
            }
            else if (messageTest.IsNotification)
            {
                HandleNotification(messageTest.method, json);
            }
            return true;
        }

        private async void HandleRequest(string method, NumberOrString id, string json)
        {
            if (handlers.TryGetRequestHandler(method, out var handler))
            {
                try
                {
                    var tokenSource = new CancellationTokenSource();
                    handlers.AddCancellationTokenSource(id, tokenSource);
                    var request = Serializer.Instance.Deserialize(handler.RequestType, json);
                    var requestResponse = (ResponseMessageBase)await handler.Handle(request, this, tokenSource.Token);
                    handlers.RemoveCancellationTokenSource(id);
                    requestResponse.id = id;
                    SendMessage(requestResponse);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                    var requestErrorResponse = Reflector.CreateErrorResponse(handler.ResponseType, ex.ToString());
                    SendMessage(requestErrorResponse);
                }
            }
            else
            {
                SendMessage(new VoidResponseMessage<ResponseError>
                {
                    id = id,
                    error = Message.MethodNotFound()
                });
            }
        }

        private void HandleResponse(NumberOrString id, string json)
        {
            if (handlers.TryRemoveResponseHandler(id, out var handler))
            {
                var response = Serializer.Instance.Deserialize(handler.ResponseType, json);
                handler.Handle(response);
            }
        }

        private void HandleCancellation(string json)
        {
            var cancellation = (NotificationMessage<CancelParams>)Serializer.Instance.Deserialize(typeof(NotificationMessage<CancelParams>), json);
            var id = cancellation.@params.Id;
            if (handlers.TryRemoveCancellationTokenSource(id, out var tokenSource))
            {
                tokenSource.Cancel();
            }
        }

        private void HandleNotification(string method, string json)
        {
            if (handlers.TryGetNotificationHandler(method, out var handler))
            {
                var notification = Serializer.Instance.Deserialize(handler.NotificationType, json);
                handler.Handle(notification, this);
            }
        }

        public async Task<TResponse> SendRequest<TResponse>(RequestMessageBase request)
            where TResponse : ResponseMessageBase
        {
            var tcs = new TaskCompletionSource<TResponse>();
            var handler = new ResponseHandler(request.id, typeof(TResponse), o => tcs.SetResult((TResponse)o));
            handlers.AddResponseHandler(handler);
            SendMessage(request);
            return await tcs.Task;
        }


        public void SendNotification<TNotification>(TNotification notification)
          where TNotification : NotificationMessageBase
        {
            SendMessage(notification);
        }

        public void SendCancellation(NumberOrString id)
        {
            var message = new NotificationMessage<CancelParams> { method = "$/cancelRequest", @params = new CancelParams { Id = id } };
            SendMessage(message);
        }

        private void SendMessage(MessageBase message)
        {
            Write(Serializer.Instance.Serialize(typeof(MessageBase), message));
        }

        private void Write(string json)
        {
            var utf8 = Encoding.UTF8.GetBytes(json);
            lock (outputLock)
            {
                using (var writer = new StreamWriter(output, Encoding.ASCII, 1024, true))
                {
                    writer.Write($"Content-Length: {utf8.Length}\r\n");
                    writer.Write("\r\n");
                    writer.Flush();
                }
                output.Write(utf8, 0, utf8.Length);
                output.Flush();
            }
        }

        private byte[] gbuffer = new byte[1024 * 1024];
        private int pos = 0;

        private async Task<string> Read()
        {
            var contentLength = 0;
            int len;
            while (true)
            {
                bool end = false;
                len = await input.ReadAsync(gbuffer, pos, Math.Min(1024, gbuffer.Length - pos));
                pos += len;
                while (true)
                {
                    var index = Array.IndexOf(gbuffer, (byte)'\n', 0, len);
                    if (index < 1 || gbuffer[index - 1] != (byte)'\r')
                        break;
                    var headerLine = Encoding.ASCII.GetString(gbuffer, 0, index - 1);
                    Array.Copy(gbuffer, index + 1, gbuffer, 0, gbuffer.Length - index - 1);
                    pos -= index + 1;
                    var position = headerLine.IndexOf(": ");
                    if (position >= 0)
                    {
                        var name = headerLine.Substring(0, position);
                        var value = headerLine.Substring(position + 2);
                        if (string.Equals(name, "Content-Length", StringComparison.Ordinal))
                        {
                            int.TryParse(value, out contentLength);
                        }
                    }
                    if (headerLine == "")
                    {
                        end = true;
                        break;
                    }
                }
                if (end)
                    break;
            }
            if (contentLength == 0)
            {
                return "";
            }
            if (contentLength > gbuffer.Length)
                Array.Resize(ref gbuffer, contentLength);

            while (pos < contentLength)
            {
                len = await input.ReadAsync(gbuffer, pos, contentLength - pos);
                pos += len;
            }
            string ret = Encoding.UTF8.GetString(gbuffer, 0, contentLength);
            Array.Copy(gbuffer, contentLength, gbuffer, 0, gbuffer.Length - contentLength);
            pos -= contentLength;
            return ret;
        }
    }
}
