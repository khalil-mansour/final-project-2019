using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Interfaces.Gateways.Repositories;
using System.Linq;
using Web.Api.Core.Dto.GatewayResponses.Repositories.File;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class FileRepository : IFileRepository
    {
        private IConfiguration _configuration;
        private readonly string _connectionString;

        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public async Task<FileUploadRepoResponse> Create(File file)
        {
            var add_query = $@"INSERT INTO public.document (user_id, document_type_id, user_file_name, storage_file_id, created_date, visible)
                               VALUES (@userid, @documenttype, @filename, @storageid, @createddate, @visible)
                               RETURNING id;";

            var select_query = $@"SELECT
                                  id as { nameof(File.Id) },
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // add user
                    var fileID = conn.Query<int>(add_query, file).Single();
                    // get added user
                    var response = conn.Query<File>(select_query, new { id = fileID }).FirstOrDefault();
                    // return the response
                    return new FileUploadRepoResponse(response, true);
                }
                catch (Exception e)
                {
                    return new FileUploadRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }

            }
        }

        public async Task<FileFetchRepoResponse> Fetch(int id)
        {
            var select_query = $@"SELECT 
                                  id as { nameof(File.Id) },
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE id = @id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // return the response
                    return new FileFetchRepoResponse(conn.Query<File>(select_query, new { id }).FirstOrDefault(), true);
                }
                catch (NpgsqlException e)
                {
                    // return the response
                    return new FileFetchRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        public async Task<FileDeleteRepoResponse> Delete(int id)
        {
            var select_document_query = $@"SELECT
                                  id as { nameof(File.Id) },
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE id = @id";

            var delete_document_query = $@"DELETE FROM public.document WHERE id = @id";
            var delete_quote_request_doc_query = $@"DELETE FROM public.quote_request_document WHERE document_id = @document_id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    // fetch document
                    var response = conn.Query<File>(select_document_query, new { id }).FirstOrDefault();
                    // delete document
                    var success = Convert.ToBoolean(conn.Execute(delete_document_query, new { id }));
                    // delete quote_request_document
                    var success2 = conn.Execute(delete_quote_request_doc_query, new { document_id = id });


                    transaction.Commit();

                    // return the response
                    return new FileDeleteRepoResponse(response, success);
                }
                catch (NpgsqlException e)
                {
                    transaction.Rollback();
                    // return the response
                    return new FileDeleteRepoResponse(null, false, new[] { new Error(e.ErrorCode.ToString(), e.Message) });
                }
            }
        }

        public async Task<FileFetchAllRepoResponse> FetchAll(string userId)
        {
            var select_all_query = $@"SELECT 
                                  id as { nameof(File.Id) },
                                  user_id as { nameof(File.UserId) },
                                  document_type_id as { nameof(File.DocumentType) },
                                  user_file_name as { nameof(File.FileName) },
                                  storage_file_id as { nameof(File.StorageId) },
                                  created_date as { nameof(File.CreatedDate) },
                                  visible as { nameof(File.Visible) }
                                  FROM public.document
                                  WHERE user_id = @userid";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
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
                                  id as {nameof (File.Id)},
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