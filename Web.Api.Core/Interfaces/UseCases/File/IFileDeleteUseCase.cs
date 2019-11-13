using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseRequests.File;
using Web.Api.Core.Dto.UseCaseResponses.File;

namespace Web.Api.Core.Interfaces.UseCases.File
{
    public interface IFileDeleteUseCase : IUseCaseRequestHandler<FileDeleteRequest, FileDeleteResponse>
    {
    }
}
