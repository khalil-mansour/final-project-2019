using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.UseCases.QuoteRequest;
using Web.Api.Presenters.QuoteRequest;

namespace Web.Api.Controllers
{
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


        [HttpPost("api/quoterequest")]
        public async Task<ActionResult> Create([FromBody] Models.Request.QuoteRequest.HouseQuoteRequestCreateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new HouseQuoteRequestPresenter();

            await _houseQuoteRequestCreateUseCase.HandleAsync(
                new HouseQuoteRequestCreateRequest(
                    request.User_Id,
                    request.House_Type_Id,
                    new HouseLocationRequest(
                        request.House_Location.Postal_Code,
                        request.House_Location.City,
                        request.House_Location.Province_Id,
                        request.House_Location.Address,
                        request.House_Location.Apartment_Unit),
                    request.Listing_Price,
                    request.Down_Payment,
                    request.Offer,
                    request.First_House,
                    request.Description,
                    request.Documents_Id,
                    request.Municipal_Evaluation_Url),
                presenter);

            return presenter.ContentResult;
        }

        [HttpGet("api/quoterequest/{quoteRequestId}")]
        public async Task<ActionResult> GetQuoteRequestDetails(int quoteRequestId)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestGetDetailRequestUseCase.HandleAsync(new HouseQuoteRequestGetDetailRequest(quoteRequestId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("/api/quoterequest/fetchall/{UserId}")]
        public async Task<ActionResult> GetQuoteRequest([FromRoute] Models.Request.QuoteRequest.HouseQuoteRequestFetchAllRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestFetchAllUseCase.HandleAsync(new HouseQuoteRequestFetchAllRequest(request.UserId), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("api/quoterequest/{quoteRequestId}")]
        public async Task<ActionResult> UpdateQuoteRequest([FromRoute] int quoteRequestId, [FromBody] Models.Request.QuoteRequest.HouseQuoteRequestUpdateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestUpdateUseCase.HandleAsync(
                new HouseQuoteRequestUpdateRequest(
                    quoteRequestId,
                    request.User_Id,
                    request.House_Type_Id,
                    new HouseLocationRequest(
                        request.House_Location.Postal_Code,
                        request.House_Location.City,
                        request.House_Location.Province_Id,
                        request.House_Location.Address,
                        request.House_Location.Apartment_Unit),
                    request.Listing_Price,
                    request.Down_Payment,
                    request.Offer,
                    request.First_House,
                    request.Description,
                    request.Documents_Id,
                    request.Municipal_Evaluation_Url),
                presenter);
            return presenter.ContentResult;

        }

        [HttpDelete("api/quoterequest/{HouseQuoteRequestId}")]
        public async Task<ActionResult> DeleteQuoteRequest([FromRoute] Models.Request.QuoteRequest.HouseQuoteRequestDeleteRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestDeleteUseCase.HandleAsync(new HouseQuoteRequestDeleteRequest(request.HouseQuoteRequestId), presenter);
            return presenter.ContentResult;

        }
    }
}