namespace ProjectApi.CommConstants
{
    #region Customers Request
    public class CreateCustomerRequest : DataRequest
    {
        public CreateCustomerRequest(string firstName, string lastName, string socialSctyNum, double accountBalance, bool vipAccount, DateTime registeredOn) 
            : base()
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSctyNum = socialSctyNum;
            AccountBalance = accountBalance;
            VipAccount = vipAccount;
            RegisteredOn = registeredOn;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string SocialSctyNum { get; }
        public double AccountBalance { get; }
        public bool VipAccount { get; }
        public DateTime RegisteredOn { get; }
    }

    public class UpdateCustomerRequest : DataRequest
    {
        public UpdateCustomerRequest(int id ,string firstName, string lastName, string socialScrityNum, double accountBalance, bool vipAccount, DateTime registeredOn)
            : base()
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            SocialScrityNum = socialScrityNum;
            AccountBalance = accountBalance;
            VipAccount = vipAccount;
            RegisteredOn = registeredOn;
        }
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string SocialScrityNum { get; }
        public double AccountBalance { get; }
        public bool VipAccount { get; }
        public DateTime RegisteredOn { get; }
    }
    #endregion

    #region Currency Request
    public class CreateCurrencyRequest : DataRequest
    {
        public CreateCurrencyRequest(string material, string type, int weight, double price, bool isItPure, bool inStock)
            : base()
        {
            Material = material;
            Type = type;
            Weight = weight;
            Price = price;
            IsItPure = isItPure;
            InStock = inStock;
        }
        public string Material { get; }
        public string Type { get; }
        public int Weight { get; }
        public double Price { get; }
        public bool IsItPure { get; }
        public bool InStock { get; }
    }

    public class UpdateCurrencyRequest : DataRequest
    {
        public UpdateCurrencyRequest(int id, string material, string type, int weight, double price, bool isItPure, bool inStock)
            : base()
        {
            Id = id;
            Material = material;
            Type = type;
            Weight = weight;
            Price = price;
            IsItPure = isItPure;
            InStock = inStock;
        }
        public int Id { get; }
        public string Material { get; }
        public string Type { get; }
        public int Weight { get; }
        public double Price { get; }
        public bool IsItPure { get; }
        public bool InStock { get; }
    }
    #endregion

    #region Order Request
    public class CreateOrderRequest : DataRequest
    {
        public CreateOrderRequest(string info, int quantity, double total, bool forAGift, bool wantCustomDesign, DateTime dateIssued, int customer_ID, int currency_ID)
            : base()
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
        public string Info { get; set; }
        public int Quantity { get; set; }
        public bool ForAGift { get; set; }
        public double Total { get; set; }
        public bool WantCustomDesign { get; set; }
        public DateTime DateIssued { get; set; }
        public int Customer_ID { get; set; }
        public int Currency_ID { get; set; }
    }

    public class UpdateOrderRequest : DataRequest
    {
        public UpdateOrderRequest(int id, string info, int quantity, double total, bool forAGift, bool wantCustomDesign, DateTime dateIssued, int customer_ID, int currency_ID)
            : base()
        {
            Id = id;
            Info = info;
            Quantity = quantity;
            ForAGift = forAGift;
            Total = total;
            WantCustomDesign = wantCustomDesign;
            DateIssued = dateIssued;
            Customer_ID = customer_ID;
            Currency_ID = currency_ID;
        }

        public int Id { get; set; }
        public string Info { get; set; }
        public int Quantity { get; set; }
        public bool ForAGift { get; set; }
        public double Total { get; set; }
        public bool WantCustomDesign { get; set; }
        public DateTime DateIssued { get; set; }
        public int Customer_ID { get; set; }
        public int Currency_ID { get; set; }
    }
    #endregion
}
