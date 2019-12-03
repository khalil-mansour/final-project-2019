using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseResponses.Chat
{
    public class ChatFetchResponse : UseCaseResponseMessage
    {
        public IEnumerable<Domain.Entities.Chat> Chats { get; }
        public IEnumerable<Error> Errors { get; }

        public ChatFetchResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ChatFetchResponse(IEnumerable<Domain.Entities.Chat> chats, bool success = false, string message = null) : base(success, message)
        {
            Chats = chats;
        }
    }
}
