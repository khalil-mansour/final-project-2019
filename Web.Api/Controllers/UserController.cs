using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Presenters;
using Web.Api.Core.Dto.UseCaseRequests;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserUseCase _registerUserUseCase;
        private readonly RegisterUserPresenter _registerUserPresenter;

        public UserController(IRegisterUserUseCase registerUserUseCase, RegisterUserPresenter registerUserPresenter)
        {
            _registerUserUseCase = registerUserUseCase;
            _registerUserPresenter = registerUserPresenter;            
        }

        // POST api/user/register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Models.Request.RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            await _registerUserUseCase.Handle(new RegisterUserRequest(request.Id, request.FirstName, request.LastName, request.Email), _registerUserPresenter);

            return _registerUserPresenter.ContentResult;
        }
    }
}
