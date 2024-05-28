using ProjectApi.Entities;
using System.ComponentModel;

namespace MyApplication.ViewModels.Orders
{
	public class EditVM
	{
        public ProjectApi.Entities.Orders Order { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Currency> Currencies { get; set; } = new List<Currency>();
    }
}
