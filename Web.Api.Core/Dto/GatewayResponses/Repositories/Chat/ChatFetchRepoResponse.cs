using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Dto.GatewayResponses.Repositories.Chat
{
    public sealed class ChatFetchRepoResponse : BaseGatewayResponse
    {
        public IEnumerable<Domain.Entities.Chat> Chats { get; }

        public ChatFetchRepoResponse(IEnumerable<Domain.Entities.Chat> chats = null, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
        {
            Chats = chats;
        }
    }
}
