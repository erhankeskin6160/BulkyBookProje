using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class Company 
    {

        public int Id {get;set; }
        [Required]
        public string Name { get; set; }

        public string StreetAddress { get; set; }   

        public string City { get; set; }    


        public string State { get; set; }

        public string PostalCode { get; set; }  

        public string PhoneNumber { get; set; }
    }

    public class ShoppingCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey ("ProductId")]
        public Product Product{get;set;}
        [Range(1,100,ErrorMessage ="Please enter a value between 1 and 100")]

        public int Count { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public  ApplicationUser ApplicationUser { get; set; }

    }
}
