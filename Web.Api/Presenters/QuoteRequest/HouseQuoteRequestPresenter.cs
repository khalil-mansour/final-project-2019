using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Api.Core.Dto.GatewayResponses.Repositories.QuoteRequest;

namespace Web.Api.Presenters.QuoteRequest
{
    public class HouseQuoteRequestPresenter
    {
        public JsonContentResult ContentResult { get; }

        public HouseQuoteRequestPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(HouseQuoteRequesteCreateRepoResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? JsonConvert.SerializeObject(response.HouseQuoteRequest, Formatting.Indented) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
