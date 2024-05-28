using MyApplication.Models;
using MyApplication.ViewModels.Currencies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyApplication.Services;
using ProjectApi.CommConstants;
using ProjectApi.Entities;


namespace MyApplication.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly ILogger<CurrenciesController> _logger;

        public CurrenciesController(ILogger<CurrenciesController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await CurrenciesService.Instance.GetAllAsync<List<CurrencyResponse>>();

                if (response == null)
                    return BadRequest("Couldn't load currency. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allCurrencies = response.Select(currencyResponse => new Currency()
                {
                    Id = currencyResponse.Id,
                    Material = currencyResponse.Material,
                    Type = currencyResponse.Type,
                    Weight = currencyResponse.Weight,
                    Price = currencyResponse.Price,
                    IsItPure = currencyResponse.IsItPure,
                    InStock = currencyResponse.InStock,
                    TotalOrders = currencyResponse.TotalOrders,
                }).ToList();

                vm.Curencies = allCurrencies;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await CurrenciesService.Instance.PostAsync<CurrencyResponse>(new CreateCurrencyRequest(addVM.Material, addVM.Type, addVM.Weight, addVM.Price, addVM.IsItPure, addVM.InStock));

                if (response == null)
                    return BadRequest("Couldn't add currency. Responce message from the server is null");    

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await CurrenciesService.Instance.GetAsync<CurrencyResponse>(id.ToString());

            Currency currency = new Currency()
            {
                Id = response.Id,
                Material = response.Material,
                Type = response.Type,
                Weight = response.Weight,
                Price = response.Price,
                IsItPure = response.IsItPure,
                InStock = response.InStock
            };
            EditVM vm = new EditVM();
            vm.Currency = currency;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await CurrenciesService.Instance.PutAsync<OkResult>(vm.Currency.Id, new UpdateCurrencyRequest(vm.Currency.Id, vm.Currency.Material, vm.Currency.Type, vm.Currency.Weight, vm.Currency.Price, vm.Currency.IsItPure, vm.Currency.InStock));

                if (response == null)
                    return BadRequest("Couldn't edit currency. Responce message from the server is null");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await CurrenciesService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Couldn't edit currency. Responce message from the server is null");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(string material)
        {
            try
            {
                var responseList = await CurrenciesService.Instance.GetSearchAsync<List<CurrencyResponse>>(material);

                if (responseList == null)
                    return BadRequest("Couldn't add currency. Responce message from the server is null");

                SearchVM vm = new SearchVM();
                var CurrencyList = responseList.Select(response => new Currency()
                {
                    Id = response.Id,
                    Material = response.Material,
                    Type = response.Type,
                    Weight = response.Weight,
                    Price = response.Price,
                    IsItPure = response.IsItPure,
                    InStock = response.InStock,
                }).ToList();

                vm.Currencies = CurrencyList;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
