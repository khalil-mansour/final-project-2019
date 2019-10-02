﻿using System;
using System.Collections.Generic;
using System.Text;
using Web.Api.Core.Dto.UseCaseResponses;
using Web.Api.Core.Interfaces;

namespace Web.Api.Core.Dto.UseCaseRequests
{
    public class LoginUserRequest : IUseCaseRequest<LoginUserResponse>
    {
        public string Email { get; }
        public string Password { get; }

        public LoginUserRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}