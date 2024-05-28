using ProjectApi.Repositories;
using ProjectApi.CommConstants;
using ProjectApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersRepository _ordersRepo;
        private readonly CustomersRepository _customersRepo;
        private readonly CurrenciesRepository _currencyRepo;

        public OrdersController()
        {
            _ordersRepo = new OrdersRepository();
            _customersRepo = new CustomersRepository();
            _currencyRepo = new CurrenciesRepository();
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateOrders(CreateOrderRequest request)
        {
            try
            {
                var order = new Orders(request.Info, request.Quantity, request.Total, request.ForAGift, request.WantCustomDesign, request.DateIssued, request.Customer_ID, request.Currency_ID);
                _ordersRepo.Save(order);

                var response = GenerateResponse(order);
                return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, response); // Return 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Retrieve by ID
        [HttpGet("{id}")]
        public IActionResult GetOrders(int id)
        {
            try
            {
                var orders = _ordersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (orders == null)
                {
                    return NotFound();
                }

                var response = GenerateResponse(orders);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Retrieve all
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var allOrders = _ordersRepo.GetAll(i => true);
                var response = allOrders.Select(order => GenerateResponse(order)).ToList();
                return Ok(response); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult UpdateOrders(int id, UpdateOrderRequest request)
        {
            try
            {
                var order = _ordersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                order.Info = request.Info;
                order.Quantity = request.Quantity;
                order.ForAGift = request.ForAGift;
                order.Total = request.Total;
                order.WantCustomDesign = request.WantCustomDesign;
                order.DateIssued = request.DateIssued;
                order.Customer_ID = request.Customer_ID;
                order.Currency_ID = request.Currency_ID;

                _ordersRepo.Save(order);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var order = _ordersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (order == null)
                {
                    return NotFound(); // Return 404 if not found
                }

                _ordersRepo.Delete(order);
                return Ok(); // Return 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Search by details
        [HttpGet("search/{details}")]
        public IActionResult SearchOrdersByDetails(string details)
        {
            try
            {
                var ordersSearchResult = _ordersRepo.GetAll(n => n.Info.ToUpper().Replace(" ", "").Contains(details.ToUpper()));
                var response = ordersSearchResult.Select(order => GenerateResponse(order)).ToList();
                return Ok(response); // Return 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private OrderResponse GenerateResponse(Orders order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                Info = order.Info,
                ForAGift = order.ForAGift,
                Total = order.Total,
                WantCustomDesign = order.WantCustomDesign,
                DateIssued = order.DateIssued,
                Quantity = order.Quantity,
                Customer = _customersRepo.GetAll(n => n.Id == order.Customer_ID).Find(i => i.Id == order.Customer_ID),
                Currency = _currencyRepo.GetAll(n => n.Id == order.Currency_ID).Find(i => i.Id == order.Currency_ID)
            };
        }
    }
}
