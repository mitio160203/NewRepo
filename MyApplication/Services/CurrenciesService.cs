namespace MyApplication.Services
{
    public class CurrenciesService : BaseService
    {
        public static CurrenciesService Instance { get; } = new CurrenciesService();

        public CurrenciesService() : base("Currencies")
        {

        }
    }
}
