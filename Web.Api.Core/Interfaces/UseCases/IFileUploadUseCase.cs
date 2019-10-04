﻿using Web.Api.Core.Dto.UseCaseResponses;

namespace Web.Api.Core.Interfaces.UseCases
{
    interface IFileUploadUseCase : IUseCaseRequestHandler<FileUploadRequest,FileUploadResponse>
    {
    }
}
