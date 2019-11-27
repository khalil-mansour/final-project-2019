using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Core.Dto.UseCaseRequests.User;
using Web.Api.Core.Interfaces.UseCases.User;

namespace Web.Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRegisterUseCase _userRegisterUseCase;
        private readonly IUserLoginUseCase _userLoginUseCase;
        private readonly IUserUpdateUseCase _userUpdateUseCase;
        private readonly IUserProfileUpdateUseCase _userProfileUpdateUseCase;
        private readonly IUserFetchUseCase _userFetchUseCase;
        private readonly IUserProfileFetchUseCase _userProfileFetchUseCase;

        public UserController(
            IUserRegisterUseCase userRegisterUseCase,
            IUserLoginUseCase userLoginUseCase,
            IUserUpdateUseCase userUpdateUseCase,
            IUserProfileUpdateUseCase professionalUpdateUseCase,
            IUserFetchUseCase userFetchUseCase,
            IUserProfileFetchUseCase userProfileFetchUseCase
            )
        {
            _userRegisterUseCase = userRegisterUseCase;
            _userLoginUseCase = userLoginUseCase;
            _userUpdateUseCase = userUpdateUseCase;
            _userProfileUpdateUseCase = professionalUpdateUseCase;
            _userFetchUseCase = userFetchUseCase;
            _userProfileFetchUseCase = userProfileFetchUseCase;
        }

        [HttpPost("api/user")]
        public async Task<ActionResult> Register([FromBody] Models.Request.UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new UserRegisterPresenter();
            await _userRegisterUseCase.HandleAsync(
                new UserRegisterRequest(
                    request.User_Id,
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.User_Type_Id,
                    request?.Phone,
                    request?.Postal_Code,
                    request?.Birthday,
                    request?.Province), presenter);
            return presenter.ContentResult;
        }

        [HttpPost("api/user/login")]
        public async Task<ActionResult> Login([FromBody] Models.Request.UserLoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var presenter = new UserLoginPresenter();
            await _userLoginUseCase.HandleAsync(new UserLoginRequest(request.User_Id), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("api/user/{id}")]
        public async Task<ActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new UserFetchPresenter();
            await _userFetchUseCase.HandleAsync(new UserFetchRequest(id), presenter);
            return presenter.ContentResult;
        }

        [HttpGet("api/user/profile/{id}")]
        public async Task<ActionResult> GetProfile([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new UserUpdatePresenter();
            await _userProfileFetchUseCase.HandleAsync(new UserProfileFetchRequest(id), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("api/user/{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] string id, [FromBody] Models.Request.UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new UserUpdatePresenter();
            await _userUpdateUseCase.HandleAsync(
                new UserUpdateRequest(
                    id,
                    request.FirstName,
                    request.LastName,
                    request?.Phone,
                    request?.Postal_Code,
                    request?.Province_Id,
                    request?.Birthday), presenter);
            return presenter.ContentResult;
        }

        [HttpPut("api/user/profile/{id}")]
        public async Task<ActionResult> UpdateProfessional([FromRoute] string id, [FromBody] Models.Request.User.UserProfileUpdateRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            var presenter = new UserUpdatePresenter();
            await _userProfileUpdateUseCase.HandleAsync(
                    new UserProfileUpdateRequest(
                        id,
                        request.Sex,
                        request.Business_Name,
                        request.Business_Phone,
                        request.Business_Email,
                        request.Bio,
                        request.Avatar_Image
                        ), presenter);
            return presenter.ContentResult;
        }
    }
}
