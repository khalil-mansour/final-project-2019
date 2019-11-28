using System.Collections.Generic;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Chat
{
    public class ChatResponse : UseCaseResponseMessage
    {
        public Domain.Entities.Chat Chat { get; }
        public IEnumerable<Error> Errors { get; }

        public ChatResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ChatResponse(Domain.Entities.Chat chat, bool success = false, string message = null) : base(success, message)
        {
            Chat = chat;
        }
    }
}
