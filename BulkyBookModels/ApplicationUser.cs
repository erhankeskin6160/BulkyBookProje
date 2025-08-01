using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class ApplicationUser :IdentityUser
    {
        [Required]
        public string FName { get; set; }    

        public string? StreetAddress { get; set; }

        public string? City { get; set; }    

        public string? State { get; set; }   

        public string? PostalCode { get; set; }

        [ForeignKey ("CompanyId")]
        [ValidateNever]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

    }
}
