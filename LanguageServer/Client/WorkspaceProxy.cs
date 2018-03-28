using LanguageServer.Parameters;
using LanguageServer.Parameters.Workspace;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LanguageServer.Client
{
    public class WorkspaceProxy
    {
        private readonly Connection _connection;

        internal WorkspaceProxy(Connection connection)
        {
            _connection = connection;
        }

        public async Task<Result<ApplyWorkspaceEditResponse, ResponseError>> ApplyEdit(ApplyWorkspaceEditParams @params)
        {
            return Message.ToResult(
                await _connection.SendRequest<ResponseMessage<ApplyWorkspaceEditResponse, ResponseError>>(
                new RequestMessage<ApplyWorkspaceEditParams>
                {
                    id = IdGenerator.Instance.Next(),
                    method = "workspace/applyEdit",
                    @params = @params
                }));
        }
    }
}
