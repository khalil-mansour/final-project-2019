using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Api.Models.Request;
using Web.Api.Core.Dto.UseCaseRequests.QuoteRequest;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters.QuoteRequest;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteRequestController : ControllerBase
    {
        private readonly IHouseQuoteRequestCreateUseCase _houseQuoteRequestCreateUseCase;
        private readonly HouseQuoteRequestPresenter _houseQuoteRequestPresenter;

        public QuoteRequestController(IHouseQuoteRequestCreateUseCase houseQuoteRequestCreateUseCase, HouseQuoteRequestPresenter houseQuoteRequestPresenter) {
            _houseQuoteRequestCreateUseCase = houseQuoteRequestCreateUseCase;
            _houseQuoteRequestPresenter = houseQuoteRequestPresenter;
        }   


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Models.Request.HouseQuoteCreateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            await _houseQuoteRequestCreateUseCase.Handle(new Core.Dto.UseCaseRequests.QuoteRequest.HouseQuoteCreateRequest(request.HouseType, new HouseLocationRequest( request.Location.PostalCode, request.Location.CityId, request.Location.ProvinceId, request.Location.Street, request.Location.AppartementUnits), request.ListingPrice, request.DownPayment, request.Offer, request.FirstHouse, request.Description, request.MunicipalEvaluationUrl), _houseQuoteRequestPresenter);
            return _houseQuoteRequestPresenter.ContentResult;
        }

    }
}