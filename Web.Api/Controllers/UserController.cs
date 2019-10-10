using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Models.Request;
using Web.Api.Core.Interfaces.UseCases.User;
using Web.Api.Presenters.User;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRegisterUseCase _userRegisterUseCase;
        private readonly IUserFetchUseCase _userFetchUseCase;

        private readonly UserRegisterPresenter _userRegisterPresenter;
        private readonly UserFetchPresenter _userFetchPresenter;


        public UserController(
            IUserRegisterUseCase userRegisterUseCase,
            IUserFetchUseCase userFetchUseCase,
            UserRegisterPresenter userRegisterPresenter,
            UserFetchPresenter userFetchPresenter)
        {
            _userRegisterUseCase = userRegisterUseCase;
            _userFetchUseCase = userFetchUseCase;
            _userFetchPresenter = userFetchPresenter;
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
            await _userRegisterUseCase.Handle(new Core.Dto.UseCaseRequests.UserRegisterRequest(request.Id, request.FirstName, request.LastName, request.Email, request.UserType, request?.Phone, request?.PostalCode, request?.Province), _userRegisterPresenter);
            return _userRegisterPresenter.ContentResult;
        }


        // GET : api/user
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetUser([FromRoute] Models.Request.UserFetchRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userFetchUseCase.Handle(request, _userFetchPresenter);
            return _userFetchPresenter.ContentResult;
        }

    }
}
