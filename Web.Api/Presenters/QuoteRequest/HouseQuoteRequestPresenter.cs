using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.QuoteRequest;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters.QuoteRequest
{
    public class HouseQuoteRequestPresenter :
        IOutputPort<HouseQuoteRequestCreateResponse>,
        IOutputPort<HouseQuoteRequestFetchAllResponse>,
        IOutputPort<HouseQuoteRequestGetDetailResponse>,
        IOutputPort<HouseQuoteRequestDeleteResponse>,
        IOutputPort<HouseQuoteRequestUpdateResponse>
    {
        public JsonContentResult ContentResult { get; }

        public HouseQuoteRequestPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(HouseQuoteRequestCreateResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ?
                Models.Response.HouseQuoteRequestCreateResponse.ToJson(response.HouseQuoteRequest)
                :
                JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(HouseQuoteRequestFetchAllResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ?
                Models.Response.HouseQuoteRequestCreateResponse.ToJson(response.HouseQuoteRequests)
                :
                JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(HouseQuoteRequestGetDetailResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ?
                Models.Response.HouseQuoteRequestCreateResponse.ToJson(response.HouseQuoteRequest)
                :
                JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(HouseQuoteRequestDeleteResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ?
                Models.Response.HouseQuoteRequestCreateResponse.ToJson(response.HouseQuoteRequest)
                :
                JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

        public void Handle(HouseQuoteRequestUpdateResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ?
                Models.Response.HouseQuoteRequestCreateResponse.ToJson(response.HouseQuoteRequest)
                :
                JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}