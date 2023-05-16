using ProjectUser.Common.Enums;

namespace ProjectUser.Services.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? UserBirthDay { get; set; }
        public string? UserMobilePhone { get; set; }
    }
}