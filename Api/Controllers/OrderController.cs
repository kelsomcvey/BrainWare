namespace Api.Controllers
{
  using Api.Infrastructure.Services; 
  using Microsoft.AspNetCore.Mvc;
  using Models;

  [ApiController]
  [Route("api")]
  public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("order/companies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetOrderCompanies()
        {          
            try
            {
                // get all companies with orders held in DB
                var companies = await _orderService.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("order/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int id)
        {            
            try
            {      
                // return all orders for specific companyId
                var orders = await _orderService.GetOrdersWithProducts(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}

