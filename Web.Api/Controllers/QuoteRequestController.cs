using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;
using Web.Api.Presenters.QuoteRequest;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteRequestController : ControllerBase
    {
        private readonly IHouseQuoteRequestCreateUseCase _houseQuoteRequestCreateUseCase;
        private readonly IHouseQuoteRequestFetchAllUseCase _houseQuoteRequestFetchAllUseCase;
        private readonly IHouseQuoteRequestGetDetailRequestUseCase _houseQuoteRequestGetDetailRequestUseCase;

        // delete a house quote request
        private readonly IHouseQuoteRequestDeleteUseCase _houseQuoteRequestDeleteUseCase;

        // update a house quote request
        private readonly IHouseQuoteRequestUpdateUseCase _houseQuoteRequestUpdateUseCase;

        public QuoteRequestController(
            IHouseQuoteRequestCreateUseCase houseQuoteRequestCreateUseCase,
            IHouseQuoteRequestFetchAllUseCase houseQuoteRequestFetchAllUseCase,
            IHouseQuoteRequestGetDetailRequestUseCase houseQuoteRequestGetDetailRequestUseCase,
            IHouseQuoteRequestDeleteUseCase houseQuoteRequestDeleteUseCase,
            IHouseQuoteRequestUpdateUseCase houseQuoteRequestUpdateUseCase
            )
        {
            _houseQuoteRequestCreateUseCase = houseQuoteRequestCreateUseCase;
            _houseQuoteRequestFetchAllUseCase = houseQuoteRequestFetchAllUseCase;
            _houseQuoteRequestGetDetailRequestUseCase = houseQuoteRequestGetDetailRequestUseCase;
            _houseQuoteRequestDeleteUseCase = houseQuoteRequestDeleteUseCase;
            _houseQuoteRequestUpdateUseCase = houseQuoteRequestUpdateUseCase;
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Models.Request.QuoteRequest.HouseQuoteRequestCreateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new HouseQuoteRequestPresenter();

            await _houseQuoteRequestCreateUseCase.Handle(
                new HouseQuoteRequestCreateRequest(
                    request.UserId,
                    request.HouseType,
                    new HouseLocationRequest(
                        request.Location.PostalCode,
                        request.Location.City,
                        request.Location.ProvinceId,
                        request.Location.Address,
                        request.Location.ApartmentUnit),
                    request.ListingPrice,
                    request.DownPayment,
                    request.Offer,
                    request.FirstHouse,
                    request.Description,
                    request.DocumentsId,
                    request.MunicipalEvaluationUrl),
                presenter);

            return presenter.ContentResult;
        }

        [HttpGet("{quoteRequestId}")]
        public async Task<ActionResult> GetQuoteRequestDetails(int quoteRequestId)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestGetDetailRequestUseCase.Handle(new HouseQuoteRequestGetDetailRequest(quoteRequestId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("fetchall/{UserId}")]
        public async Task<ActionResult> GetQuoteRequest([FromRoute] Models.Request.QuoteRequest.HouseQuoteRequestFetchAllRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestFetchAllUseCase.Handle(new HouseQuoteRequestFetchAllRequest(request.UserId), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("{quoteRequestId}")]
        public async Task<ActionResult> UpdateQuoteRequest([FromRoute] int quoteRequestId, [FromBody] Models.Request.QuoteRequest.HouseQuoteRequestUpdateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestUpdateUseCase.Handle(
                new HouseQuoteRequestUpdateRequest(
                    quoteRequestId,
                    request.UserId,
                    request.HouseType,
                    new HouseLocationRequest(
                        request.Location.PostalCode,
                        request.Location.City,
                        request.Location.ProvinceId,
                        request.Location.Address,
                        request.Location.ApartmentUnit),
                    request.ListingPrice,
                    request.DownPayment,
                    request.Offer,
                    request.FirstHouse,
                    request.Description,
                    request.DocumentsId,
                    request.MunicipalEvaluationUrl),
                presenter);
            return presenter.ContentResult;

        }
        [HttpDelete("remove/{HouseQuoteRequestId}")]
        public async Task<ActionResult> DeleteQuoteRequest([FromRoute] Models.Request.QuoteRequest.HouseQuoteRequestDeleteRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestDeleteUseCase.Handle(new HouseQuoteRequestDeleteRequest(request.HouseQuoteRequestId), presenter);
            return presenter.ContentResult;

        }
    }
}