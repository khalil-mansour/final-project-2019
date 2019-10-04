using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.UseCases;
using Web.Api.Models.Request;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {   
        private IConfiguration _configuration;
        private readonly FileUploadUseCase _fileUploadUseCase;
        private readonly FileUplaodPresenter _fileUploadPresenter;

        public FileController(IConfiguration configuration) {
            _configuration = configuration;
        }


        // POST: api/file
        [HttpPost("file")]
        [Authorize]
        public async Task<ActionResult> Login(IFormFile file, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _fileUploadUseCase.Handle(new FileUploadRequest(file, id, _configuration.GetSection("BucketName").Value), _fileUploadPresenter);
            return _fileUploadPresenter.ContentResult;
        }
    }
}
