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
        [Route("order/{id}")]

        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int id = 1)
        {
            var check = await _orderService.GetOrdersWithProducts();
          //  var returnVal = await _orderService.GetOrdersForCompany(id);

            return new OkObjectResult(check);
        }
    }
}
