using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="1 ile 100 arasýnda olmalý")]
        public string DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
