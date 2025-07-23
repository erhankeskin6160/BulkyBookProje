using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class Product 
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public string Title { get; set; }   

        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }    

        [Required]
        public string Author { get; set; }  

        [Required]
        [Range(1,100)]
        [Display(Name = "List Price")]
        public double ListPrice { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 1-50+")]
        public double Price { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 50-100")]
        public double Price50 { get; set; }



        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 100+")]

        public double Price100 { get; set; }

        [ValidateNever]

        public string ImageUrl { get; set; }
        [Display(Name = "Category")]

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [Display(Name="Cover Type")]
        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        [ValidateNever]//Validater model binding will not validate this property    
        public CoverType CoverType { get; set; }
    }
}
