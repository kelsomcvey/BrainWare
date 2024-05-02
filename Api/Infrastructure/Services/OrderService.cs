namespace Api.Infrastructure.Services
{
    using System.Data;
    using DataRepository.Repositories;
    using Models;

    public class OrderService : IOrderService
    {
        private IBrainWareRepository brainwareRepository;

        public OrderService(IBrainWareRepository brainwareRepository)
        {
            this.brainwareRepository = brainwareRepository;
        }

        private async Task<List<Order>> GetCompanyOrders(int companyId)
        {
            var parameter = new { CompanyId = companyId }; 
            var storedProcedure = "[dbo].[SPBW_GET_COMPANY_ORDERS]";

            var orderProducts = await brainwareRepository.ExecuteStoredProcedure<Order>(storedProcedure, parameter);
            return orderProducts.ToList();
        }

        private async Task<List<OrderProduct>> GetOrderProducts(int orderId)
        {
            var parameter = new { OrderId = orderId }; 
            var storedProcedure = "[dbo].[SPBW_GET_ORDER_PRODUCTS]";
            try
            {
                var orderProducts = await brainwareRepository.ExecuteStoredProcedure<OrderProduct>(storedProcedure, parameter);
                return orderProducts.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine($"Error executing stored procedure: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Order>> GetOrdersWithProducts()
        {
            var orders = await GetCompanyOrders(1);
        
            foreach (var order in orders)
            {
               order.OrderProducts = await GetOrderProducts(order.OrderId);               
            }
            return orders;
        }


    }
}