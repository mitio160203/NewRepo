using ProjectApi.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.CommConstants
{
    #region Base Response
    public interface IResponse
    {
        bool Successfull { get; }
    }

    public abstract class Response : IResponse
    {
        public Response(bool isSuccessfull)
        {
            Successfull = isSuccessfull;
        }
        public bool Successfull { get; }
    }
    #endregion

    #region Customers Request
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SocialScrityNum { get; set; }

        public double Balance { get; set; }

        public bool VipAccount { get; set; }

        public DateTime RegisteredOn { get; set; }

        public int TotalOrders { get; set; }
    }

    public class CurrencyResponse
    {
        public int Id { get; set; }

        public string Material { get; set; }

        public string Type { get; set; }

        public int Weight { get; set; }

        public double Price { get; set; }

        public bool IsItPure { get; set; }

        public bool InStock { get; set; }
        public int TotalOrders { get; set; }
    }

    public class OrderResponse
    {
        public int Id { get; set; }

        public string Info { get; set; }

        public int Quantity { get; set; }

        public bool ForAGift { get; set; }

        public double Total { get; set; }

        public bool WantCustomDesign { get; set; }

        public DateTime DateIssued { get; set; }

        #region Foreign Keys

        public virtual Customer Customer { get; set; }

        public virtual Currency Currency { get; set; }
        #endregion
    }
    #endregion
}
