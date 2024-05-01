using Api.Models;

namespace Api.Infrastructure.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetMyOrders();
        Task<List<Order>> GetOrdersForCompany(int CompanyId);
    }
}
