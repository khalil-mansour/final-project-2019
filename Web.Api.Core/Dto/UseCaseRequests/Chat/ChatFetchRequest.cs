using System;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests.Chat
{
    public class ChatFetchRequest : IUseCaseRequest<ChatResponse>
    {
        public int QuoteId { get; }

        public DateTime TimeStamp { get; }

        public ChatFetchRequest(int quoteId, DateTime timeStamp)
        {
            QuoteId = quoteId;
            TimeStamp = timeStamp;
        }
    }
}
