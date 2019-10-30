using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters.QuoteRequest
{
    public class HouseQuoteRequestPresenter : IOutputPort<HouseQuoteCreateResponse>, IOutputPort<HouseQuoteGetAllRequestResponse>

    {
        public JsonContentResult ContentResult { get; }

        public HouseQuoteRequestPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(HouseQuoteCreateResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? HouseQuoteRequestResponse.ToJson(response.HouseQuoteRequest) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(HouseQuoteGetAllRequestResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? HouseQuoteRequestResponse.ToJson(response.HouseQuoteRequests) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}