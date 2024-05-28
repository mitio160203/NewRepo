using MyApplication.Models;
using MyApplication.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyApplication.Services;
using System.Text;
using ProjectApi.CommConstants;
using ProjectApi.Entities;

namespace MyApplication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await OrderService.Instance.GetAllAsync<List<OrderResponse>>();
                if (response == null)
                    return BadRequest("Couldn't load orders. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allOrders = response.Select(orderResponse => GenerateOrder(orderResponse)).ToList();

                vm.Orders = allOrders;
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
        [HttpGet]
        public async Task<IActionResult> Add()
        {
           
            var customersResponse = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

            if (customersResponse == null)
                return BadRequest("Couldn't load customers. Responce message from the server is null");

           
            var currencyResponse = await CurrenciesService.Instance.GetAllAsync<List<CurrencyResponse>>();

            if (currencyResponse == null)
                return BadRequest("Couldn't load currency. Responce message from the server is null");

            AddVM vm = new AddVM();
            vm.Customers = customersResponse.Select(customerResponse => GenerateCustomer(customerResponse)).ToList();
            vm.Currencies = currencyResponse.Select(currencyResponse => GenerateCurrency(currencyResponse)).ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await OrderService.Instance.PostAsync<OkResult>(new CreateOrderRequest(addVM.Info, addVM.Quantity, addVM.Total, addVM.ForAGift, addVM.CustomDesign, DateTime.Now, addVM.Customer_ID, addVM.Currency_ID));

                if (response == null)
                    return BadRequest("Couldn't add order. Responce message from the server is null");

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
            var orderResponse = await OrderService.Instance.GetAsync<OrderResponse>(id.ToString());

            EditVM vm = new EditVM();
            vm.Order = GenerateOrder(orderResponse);


            //Retreive all customers
            var customersResponse = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

            if (customersResponse == null)
                return BadRequest("Couldn't load customers which are needed for editing orders. Responce message from the server is null");

            var currencyResponse = await CurrenciesService.Instance.GetAllAsync<List<CurrencyResponse>>();

            if (currencyResponse == null)
                return BadRequest("Couldn't load currencies which are needed for editing orders. Responce message from the server is null");

            vm.Customers = customersResponse.Select(customerResponse => GenerateCustomer(customerResponse)).ToList();
            vm.Currencies = currencyResponse.Select(CurrencyResponse => GenerateCurrency(CurrencyResponse)).ToList();

            
            var optionsHtml = new StringBuilder();
            foreach (var currency in vm.Currencies)
            {
                var selected = currency.Id == vm.Order.Currency_ID ? "selected=\"selected\"" : "";
                optionsHtml.Append($"<option {selected} value=\"{currency.Id}\">{currency.Material} {currency.Type}</option>");
            }
            ViewBag.CurrencyOptions = optionsHtml.ToString();

            optionsHtml = new StringBuilder();
            foreach (var customer in vm.Customers)
            {
                var selected = customer.Id == vm.Order.Customer_ID ? "selected=\"selected\"" : "";
                optionsHtml.Append($"<option {selected} value=\"{customer.Id}\">{customer.FirstName} {customer.LastName}</option>");
            }
            ViewBag.CustomerOptions = optionsHtml.ToString();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await OrderService.Instance.PutAsync<OkResult>(vm.Order.Id, new UpdateOrderRequest(vm.Order.Id, vm.Order.Info, vm.Order.Quantity, vm.Order.Total, vm.Order.ForAGift, vm.Order.WantCustomDesign, vm.Order.DateIssued, vm.Order.Customer_ID, vm.Order.Currency_ID));

                if (response == null)
                    return BadRequest("Couldn't edit Order. Responce message from the server is null");

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
                var response = await OrderService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Couldn't delete order. Responce message from the server is null");

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
        public async Task<IActionResult> Search(string firstName)
        {
            try
            {
                var responseList = await OrderService.Instance.GetSearchAsync<List<OrderResponse>>(firstName);

                if (responseList == null)
                    return BadRequest("Couldn't search for orders. Responce message from the server is null");

                SearchVM vm = new SearchVM();
                var ordersList = responseList.Select(response => GenerateOrder(response)).ToList();

                vm.Orders = ordersList;
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

        private Orders GenerateOrder(OrderResponse responce)
        {
            return new Orders()
            {
                Id = responce.Id,
                Info = responce.Info,
                ForAGift = responce.ForAGift,
                Total = responce.Total,
                WantCustomDesign = responce.WantCustomDesign,
                DateIssued = responce.DateIssued,
                Quantity = responce.Quantity,

                //Foreign Keys
                Customer = responce.Customer,
                Customer_ID = responce.Customer.Id,
                Currency = responce.Currency,
                Currency_ID = responce.Currency.Id,
            };
        }

        private Customer GenerateCustomer(CustomerResponse customerResponse)
        {
            return new Customer()
            {
                Id = customerResponse.Id,
                FirstName = customerResponse.FirstName,
                LastName = customerResponse.LastName,
                SocialSctyNum = customerResponse.SocialScrityNum,
                Balance = customerResponse.Balance,
                VipAccount = customerResponse.VipAccount,
                RegisteredOn = customerResponse.RegisteredOn,
            };
        }

        private Currency GenerateCurrency(CurrencyResponse currencyResponse)
        {
            return new Currency()
            {
                Id = currencyResponse.Id,
                Material = currencyResponse.Material,
                Type = currencyResponse.Type,
                Weight = currencyResponse.Weight,
                Price = currencyResponse.Price,
                IsItPure = currencyResponse.IsItPure,
                InStock = currencyResponse.InStock,
            };
        }


    }
}
