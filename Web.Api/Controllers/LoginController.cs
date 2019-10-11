using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Presenters;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserLoginUseCase _UserLoginUseCase;
        private readonly UserLoginPresenter _UserLoginPresenter;

        public LoginController(IUserLoginUseCase UserLoginUseCase, UserLoginPresenter UserLoginPresenter)
        {
            _UserLoginPresenter = UserLoginPresenter;
            _UserLoginUseCase = UserLoginUseCase;
        }

        // POST: api/user/Login
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Models.Request.UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _UserLoginUseCase.Handle(new UserLoginRequest(request.ID), _UserLoginPresenter);
            return _UserLoginPresenter.ContentResult;
        }
    }
}
