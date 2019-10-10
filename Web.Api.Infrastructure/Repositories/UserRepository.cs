using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Linq;
using System;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Dto;

[assembly: InternalsVisibleTo("Web.Api.Core.UnitTests")]
namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public async Task<UserLoginRepoResponse> FindById(string id)
        {
            try
                {
                    return new UserLoginRepoResponse(FindUserById(id), true);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new UserLoginRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message)});
                }

        }

        public async Task<UserRegisterRepoResponse> Create(User user)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var add_query = $@"INSERT INTO public.users (id, name, surname, email, user_type_id, phone, postalcode, province)
                               VALUES (@id, @firstname, @lastname, @email, @usertype, @phone, @postalcode, @province);";


            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // add user
                    var success = Convert.ToBoolean(conn.Execute(add_query, user));

                    // return the response
                    return new UserRegisterRepoResponse(FindUserById(user.Id), success);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new UserRegisterRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message)});
                }
            }
        }

        private User FindUserById(string id)
        {

            var select_query = $@"SELECT id AS {nameof(User.Id)},
                                    surname AS {nameof(User.FirstName)},
                                    name AS {nameof(User.LastName)},
                                    email AS {nameof(User.Email)},
                                    user_type_id AS {nameof(User.UserType)},
                                    phone AS {nameof(User.Phone)},
                                    postalcode AS {nameof(User.PostalCode)},
                                    province AS {nameof(User.Province)}
                                  FROM public.users WHERE id=@id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                var query = conn.Query<User>(select_query, new { id } ).FirstOrDefault();
                return query;
            }
        }
    }
}
