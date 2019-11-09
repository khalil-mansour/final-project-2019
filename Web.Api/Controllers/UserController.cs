using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRegisterUseCase _userRegisterUseCase;
        private readonly UserRegisterPresenter _userRegisterPresenter;
        private readonly IUserLoginUseCase _userLoginUseCase;
        private readonly UserLoginPresenter _userLoginPresenter;


        public UserController(
            IUserRegisterUseCase userRegisterUseCase,
            UserRegisterPresenter userRegisterPresenter,
            IUserLoginUseCase userLoginUseCase, 
            UserLoginPresenter userLoginPresenter)
        {
            _userRegisterUseCase = userRegisterUseCase;
            _userRegisterPresenter = userRegisterPresenter;
            _userLoginPresenter = userLoginPresenter;
            _userLoginUseCase = userLoginUseCase;

        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Models.Request.UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            await _userRegisterUseCase.Handle(new UserRegisterRequest(request.Id, request.FirstName, request.LastName, request.Email, request.UserType, request?.Phone, request?.PostalCode, request?.Province), _userRegisterPresenter);
            return _userRegisterPresenter.ContentResult;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Models.Request.UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userLoginUseCase.Handle(new UserLoginRequest(request.Id), _userLoginPresenter);
            return _userLoginPresenter.ContentResult;
        }
    }
}
