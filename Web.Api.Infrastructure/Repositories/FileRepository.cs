using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto.UseCaseResponses;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class FileRepository : IFileRepository
    {
        private IConfiguration _configuration;
        
        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FileUploadResponse> Create(int userId, string uploadedFileID)
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
