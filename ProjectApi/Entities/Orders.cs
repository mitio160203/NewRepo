using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Entities
{
    public class Orders : BaseEntity
    {
        public Orders(string info, int quantity, double total, bool forAGift, bool wantCustomDesign, DateTime dateIssued, int customer_ID, int currency_ID)
        {
            Info = info;
            Quantity = quantity;
            ForAGift = forAGift;
            Total = total;
            WantCustomDesign = wantCustomDesign;
            DateIssued = dateIssued;
            Customer_ID = customer_ID;
            Currency_ID = currency_ID;
        }

        public Orders()
        {
            
        }

        public string Info { get; set; }

        public int Quantity { get; set; }

        public bool ForAGift { get; set; }

        public double Total { get; set; }

        public bool WantCustomDesign { get; set; }

        public DateTime DateIssued { get; set; }

        #region Foreign Keys
        public int Customer_ID { get; set; }

        public int Currency_ID { get; set; }

        [ForeignKey("Customer_ID")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("Currency_ID")]
        public virtual Currency? Currency { get; set; }
        #endregion
    }
}
