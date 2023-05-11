using Entity.Concrete;

namespace ShoppingWebUI.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }
        public int CurrentCategory { get; set; }
    }
}
