using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Dto.UseCaseRequests.Offer;
using Web.Api.Core.Interfaces.UseCases.Offer;
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

        public OfferController(
            IOfferFetchUseCase offerFetchUseCase,
            IOfferFetchAllUseCase offerFetchAllUseCase,
            IOfferDeleteUseCase offerDeleteUseCase,
            IOfferCreateUseCase offerCreateUseCase,
            IOfferFetchAllByReqUseCase offerFetchAllByReqUseCase,
            IOfferUpdateUseCase offerUpdateUseCase
            )
        {
            _offerCreateUseCase = offerCreateUseCase;
            _offerFetchUseCase = offerFetchUseCase;
            _offerFetchAllUseCase = offerFetchAllUseCase;
            _offerDeleteUseCase = offerDeleteUseCase;
            _offerFetchAllByReqUseCase = offerFetchAllByReqUseCase;
            _offerUpdateUseCase = offerUpdateUseCase;

        }

        // GET: api/offer/offersbyrequest/{requestId}
        [HttpGet("api/offer/offersbyrequest/{requestId}")]
        // Authorize
        public async Task<ActionResult> GetAllRequestOffers([FromRoute] Models.Request.Offer.OfferFetchAllByReqRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferFetchAllByReqPresenter();
            await _offerFetchAllByReqUseCase.Handle(new OfferFetchAllByReqRequest(request.RequestId), presenter);
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
            await _offerFetchAllUseCase.Handle(new OfferFetchAllRequest(request.UserId), presenter);
            return presenter.ContentResult;
        }


        // GET: api/offer/fetch/{id}
        [HttpGet("api/offer/fetch/{id}")]
        // Authorize
        public async Task<ActionResult> GetSingleOffer([FromRoute] Models.Request.Offer.OfferFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferFetchPresenter();
            await _offerFetchUseCase.Handle(new OfferFetchRequest(request.Id), presenter);
            return presenter.ContentResult;
        }

        // PUT :api/offer/edit/{id}
        [HttpPut("api/offer/edit/{quoteRequestId}")]
        public async Task<ActionResult> EditSingleOffer([FromRoute] int quoteRequestId, [FromForm] Models.Request.Offer.OfferUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferUpdatePresenter();
            await _offerUpdateUseCase.Handle(new OfferUpdateRequest(
                quoteRequestId,
                request.UserId,
                request.QuoteRequestId,
                request.AnnualInterestRate,
                request.Loan,
                request.Mensuality,
                request.RateType,
                request.ContractDuration,
                request.LoanDuration,
                request.PaymentFrequency,
                request.Description,
                request.Submitted), presenter);
            return presenter.ContentResult;
        }


        // POST: api/offer/create
        [HttpPost("api/offer/create")]
        //[Authorize]
        public async Task<ActionResult> CreateOffer([FromForm] Models.Request.Offer.OfferCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferCreatePresenter();
            await _offerCreateUseCase.Handle(
                new OfferCreateRequest(
                    request.UserId,
                    request.QuoteRequestId,
                    request.Submitted
                    ), presenter);
            return presenter.ContentResult;
        }

        // DELETE: api/offer/remove/{id}
        [HttpDelete("api/offer/remove/{id}")]
        public async Task<ActionResult> DeleteOffer([FromRoute] Models.Request.Offer.OfferDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new OfferDeletePresenter();
            await _offerDeleteUseCase.Handle(
                new OfferDeleteRequest(request.Id), presenter);
            return presenter.ContentResult;
        }

    }
}