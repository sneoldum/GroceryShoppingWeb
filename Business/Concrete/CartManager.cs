using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.DomainModels;
using Entitiy.Concrete.Dtos;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        public IResult AddtoCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine != null)
            {
                cartLine.Quantity++;
                cartLine.Price += Convert.ToInt32(cartLine.Product.UnitPrice);
                cart.Total += Convert.ToInt32(cartLine.Product.UnitPrice);
                return new SuccessResult(Messages.CartAdded);
            }
            else
            {
                CartLine test = new CartLine{
                    Product = product,
                    Quantity = 1,
                    Price = Convert.ToInt32(product.UnitPrice)

                };
                cart.CartLines.Add(test);

                cart.Total += (cart.CartLines.FirstOrDefault(x=>x.Product.ProductId == product.ProductId).Price);
                return new SuccessResult(Messages.CartAdded);
            }
        }

        public IResult RemoveFromCart(Cart cart, int productId)
        {

            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            cart.Total -= Convert.ToInt32(cartLine.Price);
            cartLine.Price -= Convert.ToInt32(cartLine.Product.UnitPrice * cartLine.Quantity);
            cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId));
            return new SuccessResult(Messages.CartDeleted);
        }

        public IResult ReduceFromCart(Cart cart, int productId)
        {

            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            if (cartLine.Quantity != 1)
            {
                cartLine.Quantity--;
                cartLine.Price -= Convert.ToInt32(cartLine.Product.UnitPrice);
                cart.Total -= Convert.ToInt32(cartLine.Product.UnitPrice);

                return new SuccessResult(Messages.CartUpdated);
            }
            else
            {
                //var count = cart.CartLines.FirstOrDefault(x=>x.Product.ProductId ==productId);
                cartLine.Price -= Convert.ToInt32(cartLine.Product.UnitPrice * cartLine.Quantity);
                cart.Total -= Convert.ToInt32(cartLine.Product.UnitPrice);

                cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId));

            return new SuccessResult(Messages.CartUpdated);
            }
        }
        public IResult IncreaseFromCart(Cart cart, int productId)
        {

            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId);
            cartLine.Quantity++;
            cartLine.Price += Convert.ToInt32(cartLine.Product.UnitPrice);
            cart.Total += Convert.ToInt32(cartLine.Product.UnitPrice);

            return new SuccessResult(Messages.CartUpdated);
        }

        public IDataResult<List<CartLine>> List(Cart cart)
        {
            return new SuccessDataResult<List<CartLine>>(cart.CartLines);
        }
    }
}
