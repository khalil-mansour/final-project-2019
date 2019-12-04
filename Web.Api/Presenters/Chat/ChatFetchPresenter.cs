using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.Chat;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters.Chat
{
    public class ChatFetchPresenter : IOutputPort<ChatPostResponse>, IOutputPort<ChatFetchResponse>
    {
        public JsonContentResult ContentResult { get; }

        public ChatFetchPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(ChatPostResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? Models.Response.ChatResponse.ToJson(response.Chat) : JsonConvert.SerializeObject(response));
        }

        public void Handle(ChatFetchResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? Models.Response.ChatResponse.ToJson(response.Chats) : JsonConvert.SerializeObject(response));
        }
    }
}
