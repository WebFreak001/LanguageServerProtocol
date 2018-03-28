using LanguageServer.Parameters;
using LanguageServer.Parameters.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LanguageServer.Client
{
    public sealed class ClientProxy
    {
        private readonly Connection _connection;

        internal ClientProxy(Connection connection)
        {
            _connection = connection;
        }

        public async Task<VoidResult<ResponseError>> RegisterCapability(RegistrationParams @params)
        {
            return Message.ToResult(await _connection.SendRequest<VoidResponseMessage<ResponseError>>(
                new RequestMessage<RegistrationParams>
                {
                    id = IdGenerator.Instance.Next(),
                    method = "client/registerCapability",
                    @params = @params
                }));
        }

        public async Task<VoidResult<ResponseError>> UnregisterCapability(UnregistrationParams @params)
        {
            return Message.ToResult(await _connection.SendRequest<VoidResponseMessage<ResponseError>>(
                new RequestMessage<UnregistrationParams>
                {
                    id = IdGenerator.Instance.Next(),
                    method = "client/unregisterCapability",
                    @params = @params
                }));
        }
    }
}
