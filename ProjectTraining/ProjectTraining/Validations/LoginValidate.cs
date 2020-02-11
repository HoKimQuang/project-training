using FluentValidation;
using ProjectTraining.Common.Resources;
using ProjectTraining.ViewModels;

namespace ProjectTraining.Validations
{
    /// <summary>
    /// This class contain validation for Login
    /// </summary>
    public class LoginValidate : AbstractValidator<LoginViewModel>
    {
        public LoginValidate()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage(MessageResource.UserNameNotNull);
            RuleFor(x => x.UserName).MaximumLength(30).WithMessage(MessageResource.UserNameMaxLegth);
            RuleFor(x => x.Pass).NotNull().WithMessage(MessageResource.PasswordNotNull);
            RuleFor(x => x.Pass).MinimumLength(5).WithMessage(MessageResource.PasswordMinLength);
            RuleFor(x => x.Pass).MaximumLength(30).WithMessage(MessageResource.PasswordMaxlength);
        }
    }
}