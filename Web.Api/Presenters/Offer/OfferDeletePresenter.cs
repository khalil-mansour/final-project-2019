using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.Offer;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters.Offer
{
    public class OfferDeletePresenter : IOutputPort<OfferDeleteResponse>
    {
        public JsonContentResult ContentResult { get; }

        public OfferDeletePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(OfferDeleteResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? OfferResponse.ToJson(response.Offer) : JsonConvert.SerializeObject(response));
        }
    }
}
