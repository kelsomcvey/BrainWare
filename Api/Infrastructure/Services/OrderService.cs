using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using DataRepository.Repositories;


namespace Api.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBrainWareRepository _brainwareRepository;

        public OrderService(IBrainWareRepository brainwareRepository)
        {
            _brainwareRepository = brainwareRepository;
        }

        public async Task<List<Company>> GetCompanies()
        {
            var parameters = new { };
            var storedProcedure = "[dbo].[SPBW_GET_ALL_COMPANY_DETAILS]";

            try
            {
                var companies = await _brainwareRepository.ExecuteStoredProcedure<Company>(storedProcedure, parameters);
                return companies.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }

        private async Task<List<Order>> GetCompanyOrders(int companyId)
        {
            var parameters = new { CompanyId = companyId };
            var storedProcedure = "[dbo].[SPBW_GET_COMPANY_ORDERS]";

            try
            {
                var orders = await _brainwareRepository.ExecuteStoredProcedure<Order>(storedProcedure, parameters);
                return orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }

        private async Task<List<OrderProduct>> GetOrderProducts(int orderId)
        {
            var parameters = new { OrderId = orderId };
            var storedProcedure = "[dbo].[SPBW_GET_ORDER_PRODUCTS]";
            try
            {
                var orderProducts = await _brainwareRepository.ExecuteStoredProcedure<OrderProduct>(storedProcedure, parameters);
                return orderProducts.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }

        public async Task<List<Order>> GetOrdersWithProducts(int companyId)
        {
            try
            {
                var orders = await GetCompanyOrders(companyId);

                foreach (var order in orders)
                {
                    order.OrderProducts = await GetOrderProducts(order.OrderId);
                }
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }
    }
}
