using Entitiy.Concrete.Dtos;
using FluentValidation;

namespace ShoppingWebUI.ValidationRules.FluentValidation
{
    public class ShippingDetailValidation:AbstractValidator<ShippingDetail>
    {
        public ShippingDetailValidation()
        {
            RuleFor(s=>s.FirstName).NotEmpty();
            RuleFor(s=>s.LastName).NotEmpty();
            RuleFor(s=>s.City).NotEmpty();
            RuleFor(s=>s.Email).NotEmpty();
            RuleFor(s=>s.ShippingAddress).NotEmpty();
        }
    }
}
