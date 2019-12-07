using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseRequests.Chat;
using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Interfaces.UseCases.Chat;
using Web.Api.Core.Interfaces.UseCases.Offer;
using Web.Api.Presenters.Chat;
using Web.Api.Presenters.Offer;

namespace Web.Api.Controllers
{
    [ApiController]
    public class OfferController : ControllerBase
    {
        // create an offer
        private readonly IOfferCreateUseCase _offerCreateUseCase;
        // update an offer
        private readonly IOfferUpdateUseCase _offerUpdateUseCase;
        // get a single offer
        private readonly IOfferFetchUseCase _offerFetchUseCase;
        // get all offers
        private readonly IOfferFetchAllUseCase _offerFetchAllUseCase;
        // delete an offer
        private readonly IOfferDeleteUseCase _offerDeleteUseCase;
        // get all offers for request
        private readonly IOfferFetchAllByReqUseCase _offerFetchAllByReqUseCase;
        // send a message
        private readonly IChatSendUseCase _chatSendUseCase;
        // fetch a message
        private readonly IChatFetchUseCase _chatFetchUseCase;

        public OfferController(
            IOfferFetchUseCase offerFetchUseCase,
            IOfferFetchAllUseCase offerFetchAllUseCase,
            IOfferDeleteUseCase offerDeleteUseCase,
            IOfferCreateUseCase offerCreateUseCase,
            IOfferFetchAllByReqUseCase offerFetchAllByReqUseCase,
            IOfferUpdateUseCase offerUpdateUseCase,
            IChatSendUseCase chatSendUseCase,
            IChatFetchUseCase chatFetchUseCase
            )
        {
            _offerCreateUseCase = offerCreateUseCase;
            _offerFetchUseCase = offerFetchUseCase;
            _offerFetchAllUseCase = offerFetchAllUseCase;
            _offerDeleteUseCase = offerDeleteUseCase;
            _offerFetchAllByReqUseCase = offerFetchAllByReqUseCase;
            _offerUpdateUseCase = offerUpdateUseCase;
            _chatSendUseCase = chatSendUseCase;
            _chatFetchUseCase = chatFetchUseCase;
        }

        
        // GET : api/offer/{quoteId}/messages
        [HttpGet("api/offer/{quoteId}/messages")]
        public async Task<ActionResult> GetUnseenMessages(int quoteId, [FromQuery] Models.Request.Chat.ChatFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new ChatFetchPresenter();
            await _chatFetchUseCase.HandleAsync(new ChatFetchRequest(quoteId, request.Timestamp), presenter);
            return presenter.ContentResult;
        }
       
        // POST : api/offer/messages
        [HttpPost("api/offer/messages")]
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
                    request.Timestamp), presenter);
            return presenter.ContentResult;
        }

        // GET: api/offer/offersbyrequest/{requestId}
        [HttpGet("api/offer/offersbyrequest/{requestId}")]
        // Authorize
        public async Task<ActionResult> GetAllRequestOffers([FromRoute] Models.Request.Offer.OfferFetchAllByReqRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferFetchAllByReqPresenter();
            await _offerFetchAllByReqUseCase.HandleAsync(new OfferFetchAllByReqRequest(request.RequestId), presenter);
            return presenter.ContentResult;
        }

        // GET: api/offer/offersbyuser/userId
        [HttpGet("api/offer/offersbyuser/{userId}")]
        // Authorize
        public async Task<ActionResult> GetAllUserOffers([FromRoute] Models.Request.Offer.OfferFetchAllRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferFetchAllPresenter();
            await _offerFetchAllUseCase.HandleAsync(new OfferFetchAllRequest(request.UserId), presenter);
            return presenter.ContentResult;
        }


        // GET: api/offer/{id}
        [HttpGet("api/offer/{id}")]
        // Authorize
        public async Task<ActionResult> GetSingleOffer([FromRoute] Models.Request.Offer.OfferFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferFetchPresenter();
            await _offerFetchUseCase.HandleAsync(new OfferFetchRequest(request.Id), presenter);
            return presenter.ContentResult;
        }

        // PUT :api/offer/{id}
        [HttpPut("api/offer/{id}")]
        public async Task<ActionResult> EditSingleOffer([FromRoute] int id, [FromBody] Models.Request.Offer.OfferUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferUpdatePresenter();
            await _offerUpdateUseCase.HandleAsync(new OfferUpdateRequest(
                id,
                request.User_Id,
                request.Annual_Interest_Rate,
                request.Loan,
                request.Mensuality,
                request.Rate_Type,
                request.Contract_Duration,
                request.Loan_Duration,
                request.Payment_Frequency,
                request.Description,
                request.Submitted), presenter);
            return presenter.ContentResult;
        }


        // POST: api/offer
        [HttpPost("api/offer")]
        //[Authorize]
        public async Task<ActionResult> CreateOffer([FromBody] Models.Request.Offer.OfferCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferCreatePresenter();
            await _offerCreateUseCase.HandleAsync(
                new OfferCreateRequest(
                    request.User_Id,
                    request.Quote_Request_Id,
                    request.Submitted
                    ), presenter);
            return presenter.ContentResult;
        }

        // DELETE: api/offer/{id}
        [HttpDelete("api/offer/{id}")]
        public async Task<ActionResult> DeleteOffer([FromRoute] Models.Request.Offer.OfferDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferDeletePresenter();
            await _offerDeleteUseCase.HandleAsync(
                new OfferDeleteRequest(request.Id), presenter);
            return presenter.ContentResult;
        }

    }
}