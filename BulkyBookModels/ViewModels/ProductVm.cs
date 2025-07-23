using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
    public class ProductVm
    {
        public Product Product { get; set; }
        [ValidateNever] // Validater model binding will not validate this property
        public   IEnumerable<SelectListItem> CategoryList { get; set; }

        [ValidateNever] // Validater model binding will not validate this property
        public IEnumerable<SelectListItem> ConverTypeList { get; set; }
    }
}
