using Core.Entities.Concrete;
using Entitiy.Concrete.Dtos;
using FluentValidation;

namespace ShoppingWebUI.ValidationRules.FluentValidation
{
    public class UserDetailValidation:AbstractValidator<UserDetail>
    {
        public UserDetailValidation()
        {
            RuleFor(s=>s.FirstName).NotEmpty();
            RuleFor(s=>s.LastName).NotEmpty();
            RuleFor(s=>s.Email).NotEmpty();
            RuleFor(s=>s.PasswordHash).NotEmpty();
        }
    }
}
