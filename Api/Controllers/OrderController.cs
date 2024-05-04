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
            var comp = await _orderService.GetCompanies();            

            return new OkObjectResult(comp);
        }


        [HttpGet]
        [Route("order/{id}")]

        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int id = 1)
        {
           // var comp = await _orderService.GetCompanies();
            var check = await _orderService.GetOrdersWithProducts(id);
          //  var returnVal = await _orderService.GetOrdersForCompany(id);

            return new OkObjectResult(check);
        }
    }
}
