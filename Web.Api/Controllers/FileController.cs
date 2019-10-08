using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Interfaces.UseCases;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileUploadUseCase _fileUploadUseCase;
        private readonly FileUploadPresenter _fileUploadPresenter;

        public FileController( IFileUploadUseCase fileUploadUseCase, FileUploadPresenter fileUploadPresenter) {
            _fileUploadUseCase = fileUploadUseCase;
            _fileUploadPresenter = fileUploadPresenter;
        }


        // POST: api/file/upload
        [HttpPost("upload")]
        //[Authorize]
        public async Task<ActionResult> Post([FromForm] Models.Request.FileUploadRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _fileUploadUseCase.Handle(
                new FileUploadRequest(
                    request.File,
                    request.UserId,
                    request.DocumentTypeId,
                    request.Visible
                    ), _fileUploadPresenter);
            return _fileUploadPresenter.ContentResult;
        }
    }
}
