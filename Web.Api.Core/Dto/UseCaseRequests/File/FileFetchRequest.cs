using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class FileFetchRequest : IUseCaseRequest<FileFetchResponse>
    {
        public string UploadedFileId { get; }

        public FileFetchRequest(string uploadedFileId)
        {
            UploadedFileId = uploadedFileId;
        }
    }
}
