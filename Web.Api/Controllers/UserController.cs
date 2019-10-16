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


        public UserController(IUserRegisterUseCase userRegisterUseCase, UserRegisterPresenter userRegisterPresenter)
        {
            _userRegisterUseCase = userRegisterUseCase;
            _userRegisterPresenter = userRegisterPresenter;
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
        /*
        // GET : api/user/fetchall/
        [HttpGet("fetchall")]
        public async Task<ActionResult> GetAllUsers([FromRoute] Models.Request.UserFetchAllRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userFetchAllUseCase.Handle(new UserFetchAllRequest(request), _userFetchAllPresenter);
            return _fileFetchAllPresenter.ContentResult;
        }

        // GET : api/user/fetch/
        [HttpGet("fetch/{userId}")]
        public async Task<ActionResult> GetUser([FromRoute] Models.Request.UserFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userFetchUseCase.Handle(new UserFetchRequest(request), _userFetchPresenter);
            return _fileFetchPresenter.ContentResult;
        }
        */
    }
}
