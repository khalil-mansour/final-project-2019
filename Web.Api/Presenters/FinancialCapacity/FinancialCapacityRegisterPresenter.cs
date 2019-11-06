using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters
{
    public class FinancialCapacityRegisterPresenter : IOutputPort<FinancialCapacityRegisterRepoResponse>
    {
        public JsonContentResult ContentResult { get; }

        public FinancialCapacityRegisterPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(FinancialCapacityRegisterRepoResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
             
            ContentResult.Content = response.Success ? 
                FinancialCapacityResponse.ToJson(response.FinancialCapacity) : 
                JsonConvert.SerializeObject(response.Errors, Formatting.Indented);
        }

    }
}