using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters.Offer
{
    public class OfferFetchAllByReqPresenter : IOutputPort<OfferFetchAllByReqResponse>
    {
        public JsonContentResult ContentResult { get; }

        public OfferFetchAllByReqPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(OfferFetchAllByReqResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? OfferResponse.ToJson(response.Offers) : JsonConvert.SerializeObject(response));
        }
    }
}
