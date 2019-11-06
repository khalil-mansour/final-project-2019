using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters
{
    public class FileFetchAllPresenter : IOutputPort<FileFetchAllResponse>
    {
        public JsonContentResult ContentResult { get; }

        public FileFetchAllPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(FileFetchAllResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? FileResponse.ToJson(response.Files) : JsonConvert.SerializeObject(response.Error));
        }
    }
}
