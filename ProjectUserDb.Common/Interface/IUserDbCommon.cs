using System.Data;

namespace ProjectUser.Common.Interface
{
    public interface IUserDbCommon
    {
        IDbConnection GetConnection();
    }
}