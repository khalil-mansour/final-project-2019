using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFileFetchUseCase _fileFetchUseCase;
        private readonly IFileFetchAllUseCase _fileFetchAllUseCase;

        private readonly FileUploadPresenter _fileUploadPresenter;
        private readonly FileFetchPresenter _fileFetchPresenter;
        private readonly FileFetchAllPresenter _fileFetchAllPresenter;


        public FileController(
            IFileUploadUseCase fileUploadUseCase,
            IFileFetchUseCase fileFetchUseCase,
            IFileFetchAllUseCase fileFetchAllUseCase,
            FileUploadPresenter fileUploadPresenter,
            FileFetchPresenter fileFetchPresenter,
            FileFetchAllPresenter fileFetchAllPresenter)
        {
            _fileUploadUseCase = fileUploadUseCase;
            _fileFetchAllUseCase = fileFetchAllUseCase;
            _fileFetchUseCase = fileFetchUseCase;
            _fileUploadPresenter = fileUploadPresenter;
            _fileFetchAllPresenter = fileFetchAllPresenter;
            _fileFetchPresenter = fileFetchPresenter;
        }

        // GET: api/file/userId
        [HttpGet("fetchall/{userId}")]
        // Authorize
        public async Task<ActionResult> GetAllUserFiles([FromRoute] Models.Request.FileFetchAllRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileFetchAllUseCase.Handle(new FileFetchAllRequest(request.UserId), _fileFetchAllPresenter);
            return _fileFetchAllPresenter.ContentResult;
        }

        // GET: api/file/uploadedFileId
        [HttpGet("fetch/{uploadedFileId}")]
        // Authorize
        public async Task<ActionResult> GetSingleFile([FromRoute] Models.Request.FileFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileFetchUseCase.Handle(new FileFetchRequest(request.UploadedFileId), _fileFetchPresenter);
            return _fileFetchPresenter.ContentResult;
        }

        // POST: api/file/upload
        [HttpPost("upload")]
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
    }
}
