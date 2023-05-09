using Dapper;
using ProjectUser.Common.Interface;
using ProjectUser.Repository.Models;
using ProjectUser.Repository.Interface;
using System.Data;

namespace ProjectUser.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDbCommon _userDbCommon;

        public UserRepository(IUserDbCommon userDbCommon)
        {
            _userDbCommon = userDbCommon;
        }

        public async Task<List<UserDTO>> GetAllUserAsync()
        {
            using (var ucs = _userDbCommon.GetConnection())
            {
                var selectSQL = @"SELECT * FROM UserTable";
                
                var result = await ucs.QueryAsync<UserDTO>(selectSQL);

                return result.ToList();
            }
        }

        public async Task<UserDTO> GetDetailUserAsync(string name)
        {
            using (var ucs =  _userDbCommon.GetConnection())
            {
                var searchSQL = @"SELECT * FROM UserTable WHERE UserName = @userName";

                var result = await ucs.QueryFirstOrDefaultAsync<UserDTO>(searchSQL, new { userName = name, DbType.String});

                return result;
            }
        }

        public async Task CreateUserAsync(string userName, string userSex, DateTime userBirthDay, string userMobilePhone)
        {
            var ucs = _userDbCommon.GetConnection();

            var insertSQL = @"INSERT INTO UserTable (UserName, UserSex, UserBirthDay, UserMobilePhone)
                            VALUES (@userName, @userSex, @userBirthDay, @userMobilePhone)";

            /*var parameters = new DynamicParameters();
            parameters.Add("userName", userDTO.UserName, DbType.String);
            parameters.Add("userSex", userDTO.UserSex, DbType.String);
            parameters.Add("userBirthDay", userDTO.UserBirthDay, DbType.DateTime);
            parameters.Add("userMobilePhone", userDTO.UserMobilePhone, DbType.String);*/

            await ucs.ExecuteAsync(insertSQL, new { UserName = userName, UserSex = userSex, UserBirthDay = userBirthDay, UserMobilePhone = userMobilePhone });
        }
    }
}