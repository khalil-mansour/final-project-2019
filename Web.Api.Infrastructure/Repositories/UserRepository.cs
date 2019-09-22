using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using Web.Api.Core.Dto;

[assembly: InternalsVisibleTo("Web.Api.Core.UnitTests")]
namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
        public UserRepository(IConfiguration configuration) {
            _configuration = configuration;
        }
        public async Task<CreateUserResponse> Create(User user)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;
            /*string connstring = "Server=localhost;Port=8083;" + 
                  "User Id=postgres;Password=postgres;Database=postgres;";*/
            var query = $@"SELECT id AS {nameof(User.Id)}, 
                                  firstname AS {nameof(User.FirstName)},
                                  lastname AS {nameof(User.LastName)},
                                  email AS {nameof(User.Email)}
                        FROM public.{"user"}";
            List<User> results = new List<User>();
            
            using (var conn = new NpgsqlConnection(connectionString))
            {
                var success = true;
                CreateUserResponse response;
                Exception exception = new Exception();
                try
                {
                    conn.Open();
                    results = conn.Query<User>(query).ToList();
                }
                catch (Exception e)
                {
                    success = false;
                    exception = e;
                }
                finally {
                    var myUser = results.First();
                    response = new CreateUserResponse(myUser.Id, success, new Error(exception.HResult.ToString(), exception.Message));
                }

                return response;
            }
        }
    }
}
