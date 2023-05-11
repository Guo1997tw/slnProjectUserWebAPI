using System.Data;


namespace ProjectUser.Repository.Helpers
{
    /// <summary>
    ///   <br />
    /// </summary>
    public interface IDatabaseHelper
    {
        IDbConnection GetConnection();
    }
}