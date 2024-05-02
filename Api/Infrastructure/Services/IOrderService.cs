using Api.Models;

namespace Api.Infrastructure.Services
{
    public interface IOrderService
    {      
        Task<List<Order>> GetOrdersWithProducts();
     
    }
}
