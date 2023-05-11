using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.DomainModels;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface ICartService
    {
        IResult AddtoCart(Cart cart, Product product);
        IResult RemoveFromCart(Cart cart, int productId);
        IResult ReduceFromCart(Cart cart, int productId);
        IResult IncreaseFromCart(Cart cart, int productId);
        // IResult CompleteCart(Cart cart);
        IDataResult<List<CartLine>> List(Cart cart);
    }
}
