using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters.Offer
{
    public class OfferUpdatePresenter : IOutputPort<OfferUpdateResponse>
    {
        public JsonContentResult ContentResult { get; }

        public OfferUpdatePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(OfferUpdateResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? OfferResponse.ToJson(response.Offer) : JsonConvert.SerializeObject(response));
        }
    }
}
