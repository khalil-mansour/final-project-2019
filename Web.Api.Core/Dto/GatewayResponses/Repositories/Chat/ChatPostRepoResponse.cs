using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.Chat
{
    public sealed class ChatPostRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.Chat Chat { get; }

        public ChatPostRepoResponse(Domain.Entities.Chat chat = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Chat = chat;
        }
    }
}
