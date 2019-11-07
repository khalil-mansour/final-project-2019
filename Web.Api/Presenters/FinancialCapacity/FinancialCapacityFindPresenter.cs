using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters
{
    public class FinancialCapacityFindPresenter : IOutputPort<FinancialCapacityFindResponse>
    {
        public JsonContentResult ContentResult { get; }

        public FinancialCapacityFindPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(FinancialCapacityFindResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            ContentResult.Content = response.Success ? FinancialCapacityResponse.ToJson(response.FinancialCapacity) : JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }
    }
}
