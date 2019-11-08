using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using System.Linq;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class FileRepository : IFileRepository
    {
        private IConfiguration _configuration;

        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FileUploadRepoResponse> Create(File file)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var add_query = $@"INSERT INTO public.document (user_id, document_type_id, user_file_name, storage_file_id, created_date, visible)
                               VALUES (@userid, @documenttype, @filename, @storageid, @createddate, @visible);";

            var select_query = $@"SELECT 
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE storage_file_id = @storageid";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // add user
                    var success = Convert.ToBoolean(conn.Execute(add_query, file));

                    // return the response
                    return new FileUploadRepoResponse(conn.Query<File>(select_query, new { file.StorageId }).FirstOrDefault(), success);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new FileUploadRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        public async Task<FileFetchRepoResponse> Fetch(string storageId)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var select_query = $@"SELECT 
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE storage_file_id = @storageid";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // return the response
                    return new FileFetchRepoResponse(conn.Query<File>(select_query, new { storageId }).FirstOrDefault(), true);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new FileFetchRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        public async Task<FileFetchAllRepoResponse> FetchAll(string userId)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var select_all_query = $@"SELECT 
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE user_id = @userid";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // return the response
                    return new FileFetchAllRepoResponse(conn.Query<File>(select_all_query, new { userId }).ToList(), true);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new FileFetchAllRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        public File GetFile(int id)
        {
            var connectionString = _configuration.GetSection("ConnectionString").Value;

            var select_query = $@"SELECT 
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE id = @id";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    return conn.Query<File>(select_query, new { id }).FirstOrDefault();
                }
                catch (NpgsqlException e)
                {
                    throw (e);
                }
            }
        }
    }
}