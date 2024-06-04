using System.ComponentModel.DataAnnotations;

namespace Crud_MVC.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        
        public decimal Gst {  get; set; }
        [Required]

        public decimal Weight { get; set; }
        [Required]
        public string Description { get; set; }
        public IEnumerable<LocationModel> Location { get; set; }
    }

    
}
