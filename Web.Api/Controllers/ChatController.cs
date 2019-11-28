using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseRequests.Chat;
using Web.Api.Core.Interfaces.UseCases.Chat;
using Web.Api.Presenters.Chat;

namespace Web.Api.Controllers
{
    [ApiController]
    public class ChatController : ControllerBase
    {
        // send a message
        private readonly IChatSendUseCase _chatSendUseCase;
        // fetch a message
        private readonly IChatFetchUseCase _chatFetchUseCase;


        public ChatController(
            IChatSendUseCase chatSendUseCase,
            IChatFetchUseCase chatFetchUseCase
            )
        {
            _chatSendUseCase = chatSendUseCase;
            _chatFetchUseCase = chatFetchUseCase;
        }

        // GET : api/chat/{quoteId}
        [HttpGet("api/chat/{quoteId}")]
        public async Task<ActionResult> GetMessage(int quoteId, Models.Request.Chat.ChatFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new ChatFetchPresenter();
            await _chatFetchUseCase.HandleAsync(new ChatFetchRequest(quoteId, Convert.ToDateTime(request.Timestamp)), presenter);
            return presenter.ContentResult;
        }

        // POST : api/chat
        [HttpPost("api/chat")]
        public async Task<ActionResult> SendMessage([FromBody] Models.Request.Chat.ChatSendRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new ChatSendPresenter();
            await _chatSendUseCase.HandleAsync(
                new ChatSendRequest(
                    request.User_Id,
                    request.Quote_Id,
                    request.Message,
                    Convert.ToDateTime(request.Timestamp)), presenter);
            return presenter.ContentResult;
        }
    }
}