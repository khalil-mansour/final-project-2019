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

        private readonly FileUploadPresenter _fileUploadPresenter;
        private readonly FileFetchPresenter _fileFetchPresenter;
        private readonly FileFetchAllPresenter _fileFetchAllPresenter;
        private readonly FileDeletePresenter _fileDeletePresenter;


        public FileController(
            IFileUploadUseCase fileUploadUseCase,
            IFileFetchUseCase fileFetchUseCase,
            IFileFetchAllUseCase fileFetchAllUseCase,
            IFileDeleteUseCase fileDeleteUseCase,
            FileUploadPresenter fileUploadPresenter,
            FileFetchPresenter fileFetchPresenter,
            FileFetchAllPresenter fileFetchAllPresenter,
            FileDeletePresenter fileDeletePresenter)
        {
            _fileUploadUseCase = fileUploadUseCase;
            _fileFetchAllUseCase = fileFetchAllUseCase;
            _fileFetchUseCase = fileFetchUseCase;
            _fileDeleteUseCase = fileDeleteUseCase;
            _fileDeletePresenter = fileDeletePresenter;
            _fileUploadPresenter = fileUploadPresenter;
            _fileFetchAllPresenter = fileFetchAllPresenter;
            _fileFetchPresenter = fileFetchPresenter;
        }

        // GET: api/file/fetchall/userId
        [HttpGet("api/file/fetchall/{userId}")]
        // Authorize
        public async Task<ActionResult> GetAllUserFiles([FromRoute] Models.Request.FileFetchAllRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileFetchAllUseCase.Handle(new FileFetchAllRequest(request.UserId), _fileFetchAllPresenter);
            return _fileFetchAllPresenter.ContentResult;
        }

        // GET: api/file/fetch/{id}
        [HttpGet("api/file/fetch/{id}")]
        // Authorize
        public async Task<ActionResult> GetSingleFile([FromRoute] Models.Request.FileFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileFetchUseCase.Handle(new FileFetchRequest(request.Id), _fileFetchPresenter);
            return _fileFetchPresenter.ContentResult;
        }

        // POST: api/file/upload
        [HttpPost("api/file/upload")]
        //[Authorize]
        public async Task<ActionResult> CreateFile([FromForm] Models.Request.FileUploadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _fileUploadUseCase.Handle(
                new FileUploadRequest(
                    request.File,
                    request.UserId,
                    request.DocumentTypeId,
                    request.Visible
                    ), _fileUploadPresenter);
            return _fileUploadPresenter.ContentResult;
        }

        // DELETE: api/file/remove/{id}
        [HttpDelete("api/file/remove/{id}")]
        public async Task<ActionResult> DeleteFile([FromRoute] Models.Request.File.FileDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileDeleteUseCase.Handle(
                new FileDeleteRequest(request.Id), _fileDeletePresenter);
            return _fileDeletePresenter.ContentResult;
        }
    }
}
