using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models.ViewModels
{
    public class ShoppingCart 
    {

        public Product Product { get; set; }

        [Range(1,100,ErrorMessage ="Please Enter a value beetwen 1 and 100")]
        public int Count { get; set; }  
    }
}
