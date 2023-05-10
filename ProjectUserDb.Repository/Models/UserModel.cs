﻿namespace ProjectUser.Repository.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserSex { get; set; }
        public DateTime? UserBirthDay { get; set; }
        public string? UserMobilePhone { get; set; }
    }
}