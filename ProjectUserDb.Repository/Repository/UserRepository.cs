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

        public async Task<List<UserModel>> GetListAsync()
        {
            var ucs = _userDbCommon.GetConnection();

            //SQL comment
            var selectSQL = @"SELECT * FROM UserTable";
                
            var result = await ucs.QueryAsync<UserModel>(selectSQL);

            return result.ToList();
        }

        public async Task<UserModel> GetAsync(int _id)
        {
            var ucs = _userDbCommon.GetConnection();

            //SQL comment
            var searchSQL = @"SELECT * FROM UserTable WHERE UserId = @UserId";
            
            var result = await ucs.QueryFirstOrDefaultAsync<UserModel>(searchSQL, new { UserId = _id, DbType.Int64});

            return result;
        }

        public async Task CreateAsync(UserModel _userModel)
        {
            var ucs = _userDbCommon.GetConnection();

            //SQL comment
            var insertSQL = @"INSERT INTO [dbo].[UserTable] (UserName, UserSex, UserBirthDay, UserMobilePhone)
                              VALUES (@UserName, @UserSex, @UserBirthDay, @UserMobilePhone)";

            //DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("UserName", _userModel.UserName, DbType.String);
            parameters.Add("UserSex", _userModel.UserSex, DbType.String);
            parameters.Add("UserBirthDay", _userModel.UserBirthDay, DbType.DateTime);
            parameters.Add("UserMobilePhone", _userModel.UserMobilePhone, DbType.String);

            await ucs.ExecuteAsync(insertSQL, parameters);
        }

        public async Task UpdateAsync(UserModel _userModl)
        {
            var ucs = _userDbCommon.GetConnection();

            //SQL comment
            var updateSQL = @"UPDATE [dbo].[UserTable]
                            SET [UserName] = @UserName,
                                [UserSex] = @UserSex,
                                [UserBirthDay] = @UserBirthDay,
                                [UserMobilePhone] = @UserMobilePhone
                            WHERE [UserId] = @UserId";

            //DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("UserId", _userModl.UserId, DbType.Int64);
            parameters.Add("UserName", _userModl.UserName, DbType.String);
            parameters.Add("UserSex", _userModl.UserSex, DbType.String);
            parameters.Add("UserBirthDay", _userModl.UserBirthDay, DbType.DateTime);
            parameters.Add("UserMobilePhone", _userModl.UserMobilePhone, DbType.String);

            await ucs.ExecuteAsync(updateSQL, parameters);
        }

        public async Task DeleteAsync(int _id)
        {
            var ucs = _userDbCommon.GetConnection();

            //SQL comment
            var deleteSQL = @"DELETE FROM [dbo].[UserTable]
                              WHERE UserId = @UserId";

            await ucs.ExecuteAsync(deleteSQL, new { UserId = _id });
        }
    }
}