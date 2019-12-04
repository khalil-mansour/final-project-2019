using System;
using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters.Chat
{
    public class ChatSendPresenter : IOutputPort<ChatPostResponse>
    {
        public JsonContentResult ContentResult { get; }

        public ChatSendPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(ChatPostResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? Models.Response.ChatResponse.ToJson(response.Chat) : JsonConvert.SerializeObject(response));
        }
    }
}
