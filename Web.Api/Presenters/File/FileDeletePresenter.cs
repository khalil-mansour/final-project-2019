using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses.File;
using Web.Api.Core.Interfaces;
using Web.Api.Models.Response;

namespace Web.Api.Presenters.File
{
    public class FileDeletePresenter : IOutputPort<FileDeleteResponse>
    {
        public JsonContentResult ContentResult { get; }

        public FileDeletePresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(FileDeleteResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = (response.Success ? FileResponse.ToJson(response.File) : JsonConvert.SerializeObject(response));
        }
    }
}
