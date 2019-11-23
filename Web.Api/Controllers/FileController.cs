using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Interfaces.UseCases.File;
using Web.Api.Presenters.File;
using Web.Api.Core.Dto.UseCaseRequests.File;

namespace Web.Api.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileUploadUseCase _fileUploadUseCase;
        private readonly IFileFetchUseCase _fileFetchUseCase;
        private readonly IFileFetchAllUseCase _fileFetchAllUseCase;
        private readonly IFileDeleteUseCase _fileDeleteUseCase;


        public FileController(
            IFileUploadUseCase fileUploadUseCase,
            IFileFetchUseCase fileFetchUseCase,
            IFileFetchAllUseCase fileFetchAllUseCase,
            IFileDeleteUseCase fileDeleteUseCase)
        {
            _fileUploadUseCase = fileUploadUseCase;
            _fileFetchAllUseCase = fileFetchAllUseCase;
            _fileFetchUseCase = fileFetchUseCase;
            _fileDeleteUseCase = fileDeleteUseCase;
        }

        // GET: api/file/fetchall/userId
        [HttpGet("api/file/fetchall/{userId}")]
        // Authorize
        public async Task<ActionResult> GetAllUserFiles([FromRoute] Models.Request.FileFetchAllRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new FileFetchAllPresenter();
            await _fileFetchAllUseCase.Handle(new FileFetchAllRequest(request.UserId), presenter);
            return presenter.ContentResult;
        }

        // GET: api/file/fetch/{id}
        [HttpGet("api/file/fetch/{id}")]
        // Authorize
        public async Task<ActionResult> GetSingleFile([FromRoute] Models.Request.FileFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new FileFetchPresenter();
            await _fileFetchUseCase.Handle(new FileFetchRequest(request.Id), presenter);
            return presenter.ContentResult;
        }

        // POST: api/file/upload
        [HttpPost("api/file/upload")]
        //[Authorize]
        public async Task<ActionResult> CreateFile([FromForm] Models.Request.FileUploadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new FileUploadPresenter();
            await _fileUploadUseCase.Handle(
                new FileUploadRequest(
                    request.File,
                    request.UserId,
                    request.DocumentTypeId,
                    request.Visible
                    ), presenter);
            return presenter.ContentResult;
        }

        // DELETE: api/file/remove/{id}
        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> DeleteFile([FromRoute] Models.Request.File.FileDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var presenter = new FileDeletePresenter();
            await _fileDeleteUseCase.Handle(
                new FileDeleteRequest(request.Id), presenter);
            return presenter.ContentResult;
        }
    }
}
