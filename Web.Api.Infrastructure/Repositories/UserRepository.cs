using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System.Linq;
using System;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayReponses.Repositories;

[assembly: InternalsVisibleTo("Web.Api.Core.UnitTests")]
namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<LoginUserResponse> FindById(int id)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var select_query = $@"SELECT * FROM public.users WHERE id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    return new LoginUserResponse(conn.Query<User>(select_query, id).FirstOrDefault(), true);
                }
                catch (NpgsqlException e)
                {
                    return new LoginUserResponse(null, false, new Error(e.ErrorCode.ToString(), e.Message));
                }
            }
        }

        public async Task<CreateUserResponse> Create(User user)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var add_query = $@"INSERT INTO public.users (id, name, surname, email, user_type_id, phone, postalcode, province)
                               VALUES (@id, @firstname, @lastname, @email, @usertype, @phone, @postalcode, @province);";

            var select_query = $@"SELECT * FROM public.users WHERE id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // add user
                    var success = Convert.ToBoolean(conn.Execute(add_query, user));

                    // return the response
                    return new CreateUserResponse(conn.Query<User>(select_query, new { user.Id }).FirstOrDefault(), success);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new CreateUserResponse(null, false, new Error(e.ErrorCode.ToString(), e.Message));
                }
            }
        }
    }
}
