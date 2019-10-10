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
        private readonly IUserFetchAllUseCase _userFetchAllUseCase;
        private readonly IUserFetchUseCase _userFetchUseCase;

        private readonly UserRegisterPresenter _userRegisterPresenter;
        private readonly UserFetchAllPresenter _userFetchAllPresenter;
        private readonly UserFetchPresenter _userFetchPresenter;


        public UserController(
            IUserRegisterUseCase userRegisterUseCase,
            IUserFetchAllUseCase userFetchAllUseCase,
            IUserFetchUseCase userFetchUseCase,
            UserRegisterPresenter userRegisterPresenter,
            UserFetchAllPresenter userFetchAllPresenter,
            UserFetchPresenter userFetchPresenter)
        {
            _userRegisterUseCase = userRegisterUseCase;
            _userFetchAllUseCase = userFetchAllUseCase;
            _userFetchUseCase = userFetchUseCase;
            _userFetchPresenter = _userFetchPresenter;
            _userRegisterPresenter = userRegisterPresenter;
        }


        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///        "id": 14423,
        ///        "firstname": "lala",
        ///        "lastname": "vboub",
        ///        "email": "dsadsa",
        ///        "user_type_id": 1"id": 1,
        ///     }
        ///
        /// </remarks>
        /// <param></param>
        /// <returns>A newly created user</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] Models.Request.UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            await _registerUserUseCase.Handle(new UserRegisterRequest(request.Id, request.FirstName, request.LastName, request.Email, request.UserType, request?.Phone, request?.PostalCode, request?.Province), _UserRegisterPresenter);
            return _userRegisterPresenter.ContentResult;
        }

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

    }
}
