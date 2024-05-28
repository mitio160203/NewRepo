using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string firstName, string lastName, string ssn, double balance, bool VIP, DateTime registeredOn)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSctyNum = ssn;
            Balance = balance;
            VipAccount = VIP;
            RegisteredOn = registeredOn;
        }
        public Customer()
        {
            
        }

        [Display(Name = "First Name")]
        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Social Security Number")]
        [StringLength(10)]
        [Required]
        public string SocialSctyNum { get; set; }

        [Display(Name = "Account Balance")]
        [Required]
        public double Balance { get; set; }

        [Display(Name = "VIP Account")]
        public bool VipAccount { get; set; }

        [Display(Name = "Registered on")]
        [Required]
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders;
    }
}
