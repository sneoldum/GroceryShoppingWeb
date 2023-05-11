using Business.Constants;
using Core.Utilities.Results;
using Entities.DomainModels;
using ShoppingWebUI.Extensions;
using IResult = Core.Utilities.Results.IResult;

namespace ShoppingWebUI.Helpers
{
    public class CartSessionHelper : ICartSessionHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public CartSessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IDataResult<Cart> GetCart(string key)
        {
            Cart cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>(key);
            if (cartToCheck == null)
            {
                SetCart(key, new Cart());
                cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>(key);
            }
            return new SuccessDataResult<Cart>(cartToCheck);
        }

        public IResult SetCart(string key, Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObject(key, cart);
            return new SuccessResult(Messages.CartSet);
        }

        public IResult Clear()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return new SuccessResult(Messages.CartCleared);
        }
    }
}
