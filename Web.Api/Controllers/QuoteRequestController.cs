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
        private readonly IHouseQuoteRequestGetQuotesRequestUseCase _houseQuoteRequestGetQuotesRequestUseCase;
        private readonly IHouseQuoteRequestGetDetailRequestUseCase _houseQuoteRequestGetDetailRequestUseCase;

        public QuoteRequestController(
            IHouseQuoteRequestCreateUseCase houseQuoteRequestCreateUseCase,
            IHouseQuoteRequestGetQuotesRequestUseCase houseQuoteRequestGetQuotesRequestUseCase,
            IHouseQuoteRequestGetDetailRequestUseCase houseQuoteRequestGetDetailRequestUseCase)
        {
            _houseQuoteRequestCreateUseCase = houseQuoteRequestCreateUseCase;
            _houseQuoteRequestGetQuotesRequestUseCase = houseQuoteRequestGetQuotesRequestUseCase;
            _houseQuoteRequestGetDetailRequestUseCase = houseQuoteRequestGetDetailRequestUseCase;
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Models.Request.HouseQuoteCreateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new HouseQuoteRequestPresenter();

            await _houseQuoteRequestCreateUseCase.Handle(
                new HouseQuoteCreateRequest(request.UserId, request.HouseType,
                new HouseLocationRequest(request.Location.PostalCode
                , request.Location.City, request.Location.ProvinceId,
                request.Location.Address, request.Location.ApartmentUnit),
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

        [HttpGet("{QuoteId}")]
        public async Task<ActionResult> GetQuoteRequestDetails(int quoteId)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestGetDetailRequestUseCase.Handle(new HouseQuoteRequestGetDetailRequest(quoteId), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("fetchAll/{UserId}")]
        public async Task<ActionResult> GetQuoteRequest([FromRoute]  Models.Request.HouseQuoteRequestFetchAllRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new HouseQuoteRequestPresenter();
            await _houseQuoteRequestGetQuotesRequestUseCase.Handle(new HouseQuoteRequestGetAllRequest(request.UserId), presenter);
            return presenter.ContentResult;
        }

    }
}