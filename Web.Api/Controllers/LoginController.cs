﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Core.Interfaces.UseCases;
using Web.Api.Core.Dto.UseCaseRequests;
using Web.Api.Presenters;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginUserUseCase _loginUserUseCase;
        private readonly LoginUserPresenter _loginUserPresenter;

        public LoginController(ILoginUserUseCase loginUserUseCase, LoginUserPresenter loginUserPresenter)
        {
            _loginUserPresenter = loginUserPresenter;
            _loginUserUseCase = loginUserUseCase;
        }

        // GET: api/Login
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Models.Request.LoginUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _loginUserUseCase.Handle(new LoginUserRequest(request.Email, request.Password), _loginUserPresenter);
            return _loginUserPresenter.ContentResult;
        }
    }
}