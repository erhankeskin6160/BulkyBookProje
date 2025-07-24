namespace BulkyBook.Models.ViewModels
{
    using BulkyBook.Models; 
    public class ShopCartVm
    {
      public  IEnumerable<BulkyBook.Models.ShoppingCart> ListCart { get; set; }
        public double CartTotal { get; set; }
      
    }
}
