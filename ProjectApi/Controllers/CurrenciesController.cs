using ProjectApi.Repositories;
using ProjectApi.CommConstants;
using ProjectApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        public CurrenciesController()
        {
        }

        [HttpPost]
        public IActionResult CreateCurrency(CreateCurrencyRequest request)
        {
            try
            {
                // Save to database
                CurrenciesRepository currencyRepo = new CurrenciesRepository();
                Currency currency = new Currency(request.Material, request.Type, request.Weight, request.Price, request.IsItPure, request.InStock);
                currencyRepo.Save(currency);

                // Generate response
                var response = GenerateResponse(currency);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCurrency(int id)
        {
            try
            {
                // Retrieve from database
                CurrenciesRepository repos = new CurrenciesRepository();
                Currency currency = repos.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (currency == null)
                {
                    return NotFound();
                }

                // Generate response
                var response = GenerateResponse(currency);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            try
            {
                CurrenciesRepository currencyRepo = new CurrenciesRepository();
                OrdersRepository ordersRepo = new OrdersRepository();
                List<Currency> allCurrencies = currencyRepo.GetAll(i => true);
                List<Orders> allOrders = ordersRepo.GetAll(i => true);

                foreach (var currency in allCurrencies)
                {
                    int currentCount = 0;
                    foreach (var order in allOrders)
                    {
                        if (order.Currency_ID == currency.Id)
                        {
                            currentCount++;
                        }
                    }

                    currency.TotalOrders = currentCount;
                }

                var response = allCurrencies.Select(currency => GenerateResponse(currency)).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCurrency(int id, UpdateCurrencyRequest request)
        {
            try
            {
                CurrenciesRepository repo = new CurrenciesRepository();

                
                var existingCurrency = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (existingCurrency == null)
                {
                    return NotFound();
                }

                
                existingCurrency.Material = request.Material;
                existingCurrency.Type = request.Type;
                existingCurrency.Weight = request.Weight;
                existingCurrency.Price = request.Price;
                existingCurrency.IsItPure = request.IsItPure;
                existingCurrency.InStock = request.InStock;

                // Save changes to the database
                repo.Save(existingCurrency);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCurrency(int id)
        {
            try
            {
                CurrenciesRepository repo = new CurrenciesRepository();

                Currency currency = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (currency == null)
                {
                    return NotFound();
                }

                repo.Delete(currency);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("search/{searchWord}")]
        public IActionResult SearchCurrencyByMaterial(string searchWord)
        {
            try
            {
                CurrenciesRepository repo = new CurrenciesRepository();
                List<Currency> currencySearchResult = repo.GetAll(n => n.Material.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

                var response = currencySearchResult.Select(currency => GenerateResponse(currency)).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private CurrencyResponse GenerateResponse(Currency currency)
        {
            var response = new CurrencyResponse
            {
                Id = currency.Id,
                Material = currency.Material,
                Type = currency.Type,
                Weight = currency.Weight,
                Price = currency.Price,
                IsItPure = currency.IsItPure,
                InStock = currency.InStock,
                TotalOrders = currency.TotalOrders
            };

            return response;
        }
    }
}