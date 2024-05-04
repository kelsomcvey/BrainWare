namespace Api.Controllers
{
    using Api.Infrastructure.Services;
    using DataRepository.Repositories;
    using Infrastructure;
  using Microsoft.AspNetCore.Mvc;
  using Models;

  [ApiController]
  [Route("api")]
  public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService) {
            this._orderService = orderService;
        }

        [HttpGet]
        [Route("order/companies")]
        public async Task<ActionResult<IEnumerable<Company>>> GetOrderCompanies()
        {
            try
            {
                var companies = await _orderService.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("order/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int id = 1)
        {
            try
            {
                var orders = await _orderService.GetOrdersWithProducts(id);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
}
