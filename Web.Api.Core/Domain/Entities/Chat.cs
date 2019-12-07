using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Api.Core.Domain.Entities
{
    public class Chat
    {
        public int Id { get; }

        public string UserId { get; }

        public int QuoteId { get; }

        public string Message { get; }

        public DateTimeOffset TimeStamp { get; }
        
        public Chat(string userId, int quoteId, string message, string timeStamp)
        {
            UserId = userId;
            QuoteId = quoteId;
            Message = message;
            TimeStamp = DateTimeOffset.Parse(timeStamp);
        }

        public Chat(int id, string userId, int quoteId, string message, DateTime timeStamp)
        {
            Id = id;
            UserId = userId;
            QuoteId = quoteId;
            Message = message;
            TimeStamp = DateTimeOffset.Parse(timeStamp.ToString());
        }
    }
}
