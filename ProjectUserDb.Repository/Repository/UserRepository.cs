using Dapper;
using System.Data;
using ProjectUser.Repository.Helpers;
using ProjectUser.Repository.Models;
using ProjectUser.Repository.Interface;

namespace ProjectUser.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseHelper _databaseHelper;
        public UserRepository(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        public async Task<List<UserModel>> GetListAsync()
        {
            var ucs = _databaseHelper.GetConnection();

            //SQL comment
            var selectSQL = @"SELECT * FROM UserTable";
                
            var result = await ucs.QueryAsync<UserModel>(selectSQL);

            return result.ToList();
        }

        public async Task<UserModel> GetAsync(int id)
        {
            var ucs = _databaseHelper.GetConnection();

            //SQL comment
            var searchSQL = @"SELECT * FROM UserTable WHERE UserId = @UserId";
            
            var result = await ucs.QueryFirstOrDefaultAsync<UserModel>(searchSQL, new { UserId = id, DbType.Int64});

            return result;
        }

        public async Task CreateAsync(UserModel user)
        {
            var ucs = _databaseHelper.GetConnection();

            //SQL comment
            var insertSQL = @"INSERT INTO [dbo].[UserTable] (UserName, UserSex, UserBirthDay, UserMobilePhone)
                              VALUES (@UserName, @UserSex, @UserBirthDay, @UserMobilePhone)";

            //DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("UserName", user.UserName, DbType.String);
            parameters.Add("UserSex", user.UserSex, DbType.String);
            parameters.Add("UserBirthDay", user.UserBirthDay, DbType.DateTime);
            parameters.Add("UserMobilePhone", user.UserMobilePhone, DbType.String);

            await ucs.ExecuteAsync(insertSQL, parameters);
        }

        public async Task UpdateAsync(UserModel user)
        {
            var ucs = _databaseHelper.GetConnection();

            //SQL comment
            var updateSQL = @"UPDATE [dbo].[UserTable]
                            SET [UserName] = @UserName,
                                [UserSex] = @UserSex,
                                [UserBirthDay] = @UserBirthDay,
                                [UserMobilePhone] = @UserMobilePhone
                            WHERE [UserId] = @UserId";

            //DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("UserId", user.UserId, DbType.Int64);
            parameters.Add("UserName", user.UserName, DbType.String);
            parameters.Add("UserSex", user.UserSex, DbType.String);
            parameters.Add("UserBirthDay", user.UserBirthDay, DbType.DateTime);
            parameters.Add("UserMobilePhone", user.UserMobilePhone, DbType.String);

            await ucs.ExecuteAsync(updateSQL, parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var ucs = _databaseHelper.GetConnection();

            //SQL comment
            const string sqlCommand = @"DELETE FROM [dbo].[UserTable]
                                        WHERE UserId = @UserId";

            await ucs.ExecuteAsync(sqlCommand, new { UserId = id });
        }
    }
}