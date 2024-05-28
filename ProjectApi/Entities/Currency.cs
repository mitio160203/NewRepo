using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Entities
{
    public class Currency: BaseEntity
    {
        public Currency(string material, string type, int weight, double price, bool isItPure, bool inStock )
        {
            Material = material;
            Type = type;
            Weight = weight;
            Price = price;
            IsItPure = isItPure;
            InStock = inStock;
        }

        public Currency()
        {
            
        }

        [Display(Name="Material")]
        [StringLength(50)]
        [Required]
        public string Material { get; set; }

        [Display(Name = "Type")]
        [StringLength(50)]
        [Required]
        public string Type { get; set; }

        [Display(Name = "Weight")]
        [Required]
        public int Weight { get; set; }

        [Display(Name = "Price")]
        [Required]
        public double Price { get; set; }

        [Display(Name = "Is it Pure")]
        [Required]
        public bool IsItPure { get; set; }

        [Display(Name = "In stock")]
        [Required]
        public bool InStock { get; set; }

        public int TotalOrders;
    }
}
