namespace ProjectUser.Services.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public int UserId { get; }

        public UserNotFoundException(int userId)
        {
            UserId = userId;
        }
    }
}