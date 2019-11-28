using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Api.Core.Domain.Entities;
using Web.Api.Core.Dto;
using Web.Api.Core.Dto.GatewayResponses.Repositories.Chat;
using Web.Api.Core.Interfaces.Gateways.Repositories;

namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class ChatRepository : IChatRepository
    {
        private IConfiguration _configuration;
        private readonly string _connectionString;

        public ChatRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public async Task<ChatRepoResponse> FetchMessage(int quoteId, DateTime time)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatRepoResponse> SendMessage(Chat chat)
        {
            var add_chat = $@"INSERT INTO public.chat (user_id, quote_id, message, sent)
                              VALUES (@userid, @quoteid, @message, @timestamp)
                              RETURNING id;";

            var select_query = $@"SELECT
                                  id as { nameof(Chat.Id) },
                                  user_id as { nameof(Chat.UserId) },
                                  quote_id as { nameof(Chat.QuoteId) },
                                  message as { nameof(Chat.Message) },
                                  sent as { nameof(Chat.TimeStamp) }
                                  FROM public.chat where id=@id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();
                try
                {
                    // add chat message
                    var chatID = conn.Query<int>(add_chat, chat).Single();
                    var response = conn.Query<Chat>(select_query, new { id = chatID }).Single();

                    // commit
                    transaction.Commit();

                    // return the response
                    return new ChatRepoResponse(response, true);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ChatRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }
    }
}
