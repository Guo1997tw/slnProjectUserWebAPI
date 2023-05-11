using ProjectUser.Common.Enums;

namespace ProjectUser.WebAPI.Models
{
    public class CreateUserParameter
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public Gender UserSex { get; set; }
        public DateTime? UserBirthDay { get; set; }
        public string? UserMobilePhone { get; set; }
    }
}