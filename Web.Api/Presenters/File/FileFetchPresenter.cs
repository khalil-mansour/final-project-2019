using Newtonsoft.Json;
using System.Net;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Presenters
{
    public class FileFetchPresenter : IOutputPort<FileFetchResponse>
    {
        public JsonContentResult ContentResult { get; }

        public FileFetchPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(FileFetchResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonConvert.SerializeObject(response);
        }
    }
}
