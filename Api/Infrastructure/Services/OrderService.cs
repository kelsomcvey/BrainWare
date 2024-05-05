using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers;
using Api.Models;
using DataRepository.Repositories;


namespace Api.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBrainWareRepository _brainwareRepository;
        ILogger<OrderService> _logger;

        public OrderService(IBrainWareRepository brainwareRepository, ILogger<OrderService> logger)
        {
            _brainwareRepository = brainwareRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Company>> GetCompanies()
        {
            var parameters = new { };
            var storedProcedure = "[dbo].[SPBW_GET_ALL_COMPANY_DETAILS]";

            // SP should return companies with exisitng orders
            try
            {
                var companies = await _brainwareRepository.ExecuteStoredProcedure<Company>(storedProcedure, parameters);
                return companies.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }

        private async Task<List<Order>> GetCompanyOrders(int companyId)
        {
            var parameters = new { CompanyId = companyId };
            var storedProcedure = "[dbo].[SPBW_GET_COMPANY_ORDERS]";
            
            // sp takes in a param for CompanyId and SP returns all orders for company
            try
            {
                var orders = await _brainwareRepository.ExecuteStoredProcedure<Order>(storedProcedure, parameters);
                return orders.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error executing stored procedure: {ex.Message}");
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }

        private async Task<List<OrderProduct>> GetOrderProducts(int orderId)
        {
            var parameters = new { OrderId = orderId };
            var storedProcedure = "[dbo].[SPBW_GET_ORDER_PRODUCTS]";

            // sp takes in param for orderId and gets all products for that order
            try
            {
                var orderProducts = await _brainwareRepository.ExecuteStoredProcedure<OrderProduct>(storedProcedure, parameters);
                return orderProducts.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error executing stored procedure: {ex.Message}");
                throw new Exception($"Error executing stored procedure: {ex.Message}");
            }
        }

        public async Task<List<Order>> GetOrdersWithProducts(int companyId)
        {
            try
            {
                var orders = await GetCompanyOrders(companyId);


                // for each order get the products and add these to OrderProducts property
                foreach (var order in orders)
                {
                    order.OrderProducts = await GetOrderProducts(order.OrderId);
                }
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting company orders: {ex.Message}");
                throw new Exception($"Error getting company orders: {ex.Message}");
            }
        }
    }
}
