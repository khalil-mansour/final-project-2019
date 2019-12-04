using System;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Chat
{
    public class ChatFetchRequest : IUseCaseRequest<ChatFetchResponse>
    {
        public int QuoteId { get; }

        public string Timestamp { get; }

        public ChatFetchRequest(int quoteId, string timestamp)
        {
            QuoteId = quoteId;
            Timestamp = timestamp;
        }
    }
}
