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

        public async Task<List<Company>> GetCompanies()
        {
            var parameter = new { };
            var storedProcedure = "[dbo].[SPBW_GET_ALL_COMPANY_DETAILS]";

            var orderProducts = await brainwareRepository.ExecuteStoredProcedure<Company>(storedProcedure, parameter);
            return orderProducts.ToList();
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

        public async Task<List<Order>> GetOrdersWithProducts(int companyId)
        {
            var orders = await GetCompanyOrders(companyId);
        
            foreach (var order in orders)
            {
               order.OrderProducts = await GetOrderProducts(order.OrderId);               
            }
            return orders;
        }


    }
}