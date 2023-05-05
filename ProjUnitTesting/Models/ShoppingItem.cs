using System.ComponentModel.DataAnnotations;

namespace ProjUnitTesting.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
    }
}
