using FluentValidation;
using ProjectUser.WebAPI.Models;

namespace ProjectUser.WebAPI.FluentValidation
{
    public class UpdateUseParameterValidator : AbstractValidator<UpdateUserParameter>
    {
        public UpdateUseParameterValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("姓名不可為空！")
                .Length(3, 5).WithMessage("長度最少為3，最多為5字元！");

            RuleFor(user => user.UserBirthDay)
                .NotEmpty().WithMessage("生日不可為空！");

            RuleFor(user => user.UserSex)
                .IsInEnum()
                .WithMessage("性別為M、F！");

            RuleFor(user => user.UserMobilePhone)
                .NotEmpty().WithMessage("手機不可為空！")
                .Length(10).WithMessage("長度需為10碼！");
        }
    }
}