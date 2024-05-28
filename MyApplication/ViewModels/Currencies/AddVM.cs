using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyApplication.ViewModels.Currencies
{
    public class AddVM
    {

        [DisplayName("Material: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Material { get; set; }

        [DisplayName("Type: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Type { get; set; }

        [DisplayName("Weight: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Weight { get; set; }

        [DisplayName("Price: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Price { get; set; }

        [DisplayName("Is It Pure: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public bool IsItPure { get; set; }

        [Display(Name = "In Stock")]
        [Required(ErrorMessage = "This field is Required!")]
        public bool InStock { get; set; }
    }
}
