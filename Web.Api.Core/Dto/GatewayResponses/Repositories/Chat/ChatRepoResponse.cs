using System.Collections.Generic;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.Chat
{
    public sealed class ChatRepoResponse : BaseGatewayResponse
    {
        public Domain.Entities.Chat Chat { get; }

        public ChatRepoResponse(Domain.Entities.Chat chat = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Chat = chat;
        }
    }
}
