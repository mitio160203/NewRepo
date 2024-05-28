using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ProjectApi.Entities;

namespace MyApplication.ViewModels.Orders
{
    public class AddVM
    {

        public AddVM()
        {
            Customers = new List<Customer>();
            Currencies = new List<Currency>();
        }

        [DisplayName("Info: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Info { get; set; }

        [DisplayName("Quantity: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Quantity { get; set; }

        [DisplayName("Total (BGN): ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Total { get; set; }

        [DisplayName("For a gift: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public bool ForAGift { get; set; }

        [DisplayName("Custom Design: ")]
        public bool CustomDesign { get; set; }

        [DisplayName("Customer: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Customer_ID { get; set; }

        public List<Customer> Customers { get; set; }

        [DisplayName("Currency: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Currency_ID { get; set; }

        public List<Currency> Currencies { get; set; }
    }
}
