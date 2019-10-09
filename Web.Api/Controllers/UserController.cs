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
        private readonly IUserRegisterUseCase _registerUserUseCase;
        private readonly UserRegisterPresenter _UserRegisterPresenter;

        public UserController(IUserRegisterUseCase registerUserUseCase, UserRegisterPresenter UserRegisterPresenter)
        {
            _registerUserUseCase = registerUserUseCase;
            _UserRegisterPresenter = UserRegisterPresenter;
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
        public async Task<ActionResult> Post([FromBody] Models.Request.UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            await _registerUserUseCase.Handle(new UserRegisterRequest(request.Id, request.FirstName, request.LastName, request.Email, request.UserType, request?.Phone, request?.PostalCode, request?.Province), _UserRegisterPresenter);
            return _UserRegisterPresenter.ContentResult;
        }
    }
}
