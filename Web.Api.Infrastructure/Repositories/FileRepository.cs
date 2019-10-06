using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class FileRepository : IFileRepository
    {
        private IConfiguration _configuration;
        
        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FileUploadResponse> Create(File file, string uploadedFileID)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var add_query = $@"INSERT INTO public.document (id, user_id, document_type_id, name, last_modified, url, visible)
                               VALUES (@id, @userid, @documenttype, @name, @lastmodified, @url, @visible);";

            var select_query = $@"SELECT * FROM public.document WHERE id=@id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // add user
                    var success = Convert.ToBoolean(conn.Execute(add_query));

                    // return the response
                    return new FileUploadResponse(conn.Query<File>(select_query, new { user.Id }).FirstOrDefault(), success);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new FileUploadResponse(null, false, new Error(e.ErrorCode.ToString(), e.Message));
                }
            }
        }

        public Task<FetchFileResponse> Fetch(int user_id, int doc_type, string name)
        {
            throw new NotImplementedException();
        }
    }
}
