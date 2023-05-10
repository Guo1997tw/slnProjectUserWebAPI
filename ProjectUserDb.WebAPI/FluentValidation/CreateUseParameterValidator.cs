using FluentValidation;
using ProjectUser.Common.Enums;
using ProjectUser.WebAPI.Models;

namespace ProjectUser.WebAPI.FluentValidation
{
    public class CreateUseParameterValidator : AbstractValidator<CreateUserParameter>
    {
        public CreateUseParameterValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("姓名不可為空！")
                .Length(50).WithMessage("長度需為50字元！");

            RuleFor(user => user.UserBirthDay)
                .NotEmpty().WithMessage("生日不可為空！");

            RuleFor(user => user.UserSex)
                .IsInEnum()
           .    WithMessage("");

            RuleFor(user => user.UserMobilePhone)
              
                .NotEmpty().WithMessage("手機不可為空！")
                .Length(10).WithMessage("長度需為10碼！");
        }
    }
}