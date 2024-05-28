using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyApplication.ViewModels.Customers
{
    public class AddVM
    {
        [DisplayName("First Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }

        [DisplayName("Last Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }

        [DisplayName("Social Security Number: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string SocialSctyNum { get; set; }

        [DisplayName("Account Balance: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Balance { get; set; }

        [DisplayName("Vip Account: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public bool VipAccount { get; set; }
    }
}
