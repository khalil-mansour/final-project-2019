using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.Api.Core.UseCases;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {   
        private IConfiguration _configuration;
        private readonly FileUploadUseCase _fileUploadUseCase;
        private readonly FileUploadPresenter _fileUploadPresenter;

        public FileController(IConfiguration configuration) {
            _configuration = configuration;
        }


        // POST: api/file
        [HttpPost("file")]
        [Authorize]
        // AJOUTER MODEL REQUEST POUR FILE ?? //
        public async Task<ActionResult> Post(IFormFile file, [FromBody] Models.Request.FileUploadRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _fileUploadUseCase.Handle(
                new FileUploadRequest(
                    file,
                    request.Id,
                    request.UserId,
                    request.DocumentTypeId,
                    request.Name,
                    request.LastModified,
                    request.Url,
                    request.Visible,
                    _configuration.GetSection("BucketName").Value), _fileUploadPresenter);
            return _fileUploadPresenter.ContentResult;
        }
    }
}
