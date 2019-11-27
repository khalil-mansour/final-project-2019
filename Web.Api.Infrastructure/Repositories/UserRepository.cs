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
using Web.Api.Core.Dto.GatewayResponses.Repositories.User;

[assembly: InternalsVisibleTo("Web.Api.Core.UnitTests")]
namespace Web.Api.Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private IConfiguration _configuration;
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection("ConnectionString").Value;
        }

        public async Task<UserLoginRepoResponse> FindById(string id)
        {
            try
            {
                var returnedUser = new UserLoginRepoResponse(FindUserById(id), true);
                if (returnedUser.User == null)
                {
                    return new UserLoginRepoResponse(null, false, new[] { new Error("auth/user-not-found", "user not found") });
                }

                return returnedUser;
            }
            catch (Exception e)
            {
                // return the response
                return new UserLoginRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
            }

        }

        public async Task<UserFetchRepoResponse> GetUser(string userId)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // return the response
                    return new UserFetchRepoResponse(FindUserById(userId), true);
                }
                catch (Exception e)
                {
                    // return the response
                    return new UserFetchRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<UserRegisterRepoResponse> Create(User user)
        {
            var add_query = $@"INSERT INTO public.users (id, name, surname, email, user_type_id, phone, postalcode, province, birthday)
                               VALUES (@id, @firstname, @lastname, @email, @usertype, @phone, @postalcode, @province, @birthday);";

            var add_profession = $@"INSERT INTO public.profession_profile (user_id) VALUES (@id)";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    // add user
                    var success = Convert.ToBoolean(conn.Execute(add_query, user));

                    // add empty profession profile if user is broker
                    if (user.UserType == 2)
                        conn.Execute(add_profession, new { id = user.Id });

                    // return the response
                    return new UserRegisterRepoResponse(FindUserById(user.Id), success);
                }
                catch (Exception e)
                {
                    // return the response
                    return new UserRegisterRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<UserUpdateRepoResponse> UpdateUser(string userId, User user)
        {
            var client_query = $@"UPDATE public.users 
                                    SET
                                        name = @lastname,
                                        surname = @firstname,
                                        phone = @phone,
                                        postalcode = @postalcode,
                                        province = @province,
                                        birthday = @birthday
                                    WHERE
                                        id = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();

                try
                {
                    // update client type user
                    conn.Execute(client_query,
                        new
                        {
                            id = userId,
                            lastname = user.LastName,
                            firstname = user.FirstName,
                            phone = user.Phone,
                            postalcode = user.PostalCode,
                            province = user.Province,
                            birthday = user.Birthday
                        });
                    // commit
                    transaction.Commit();

                    // return the response
                    return new UserUpdateRepoResponse(FindUserById(userId), true);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // return the response
                    return new UserUpdateRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }
            }
        }

        public async Task<UserProfileUpdateRepoResponse> UpdateUserProfile(string userId, Profile profile)
        {
            var broker_query = $@"UPDATE public.profession_profile
                                    SET
                                        gender = @gender,
                                        photo = @photo,
                                        business_name = @businessname,
                                        business_phone = @businessphone,
                                        business_email = @businessemail,
                                        description = @description
                                    WHERE
                                        user_id = @id;";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction();

                try
                {
                    conn.Execute(broker_query,
                        new
                        {
                            id = userId,
                            gender = profile.Sex,
                            photo = profile.AvatarImage,
                            businessname = profile.BusinessName,
                            businessphone = profile.BusinessPhone,
                            businessemail = profile.BusinessEmail,
                            description = profile.Bio
                        });
                    // commit
                    transaction.Commit();

                    // return the response
                    return new UserProfileUpdateRepoResponse(FindProfileById(userId), true);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // return the response
                    return new UserProfileUpdateRepoResponse(null, false, new[] { new Error(e.HResult.ToString(), e.Message) });
                }

            }
        }
        public async Task<UserProfileUpdateRepoResponse> GetProfile(string userId)
        {
            return new UserProfileUpdateRepoResponse(FindProfileById(userId), true);
        }

        // HELPERS

        private Profile FindProfileById(string id)
        {
            var select_query = $@"SELECT   user_id as { nameof(Profile.UserId) },
                                           gender as { nameof(Profile.Sex) },                                           
                                           business_name as { nameof(Profile.BusinessName) },
                                           business_phone as { nameof(Profile.BusinessPhone) },
                                           business_email as { nameof(Profile.BusinessEmail) },
                                           description as { nameof(Profile.Bio) },
                                           photo as { nameof(Profile.AvatarImage) }
                                    FROM public.profession_profile where user_id=@id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                return conn.Query<Profile>(select_query, new { id }).FirstOrDefault();
            }

        }

        private int GetUserType(string userId)
        {
            var select_user_type = $@"SELECT (user_type_id) FROM public.users WHERE id=@id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                try
                {
                    // fetch user type
                    var userType = conn.Query<int>(select_user_type, new { id = userId }).Single();
                    return userType;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        private User FindUserById(string id)
        {
            // get type
            var userType = GetUserType(id);

            var select_query = $@"SELECT id AS {nameof(User.Id)},
                                    surname AS {nameof(User.FirstName)},
                                    name AS {nameof(User.LastName)},
                                    email AS {nameof(User.Email)},
                                    user_type_id AS {nameof(User.UserType)},
                                    phone AS {nameof(User.Phone)},
                                    postalcode AS {nameof(User.PostalCode)},
                                    province AS {nameof(User.Province)},
                                    birthday AS {nameof(User.Birthday)}
                                  FROM public.users WHERE id=@id";

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                return conn.Query<User>(select_query, new { id }).FirstOrDefault();
            }
        }


    }
}
