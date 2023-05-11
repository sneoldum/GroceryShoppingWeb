using Core.Utilities.Results;
using Entities.DomainModels;
using IResult = Core.Utilities.Results.IResult;

namespace ShoppingWebUI.Helpers
{
    public interface ICartSessionHelper
    {
        IDataResult<Cart> GetCart(string key);
        IResult SetCart(string key, Cart cart);
        IResult Clear();
    }
}
