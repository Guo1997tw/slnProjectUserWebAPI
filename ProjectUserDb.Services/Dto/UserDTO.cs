using ProjectUser.Common.Enums;

namespace ProjectUser.Repository.Models
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public Gender? UserSex { get; set; }
        public DateTime? UserBirthDay { get; set; }
        public string? UserMobilePhone { get; set; }
    }
}