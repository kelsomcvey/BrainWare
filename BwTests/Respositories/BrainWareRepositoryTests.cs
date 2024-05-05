using Dapper;
using DataRepository.Repositories;
using Microsoft.AspNetCore.Routing;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace BwTests.Respositories
{
    public class BrainWareRepositoryTests
    {
        [Theory]
        [InlineData("StoredProcedure1", new object[] { 1, 2, 3 })]
        ////[InlineData("StoredProcedure2", new object[] { 4, 5, 6 })]
        ////[InlineData("StoredProcedure3", new object[] { 7, 8, 9 })]
        public async Task ExecuteStoredProcedure_ReturnsExpectedResult_WhenSuccessful(string storedProcedure, object[] parameters)
        {
            // Arrange
            var expectedResult = new int[] { 2 };
            var mockRepository = new Mock<IBrainWareRepository>();
            mockRepository.Setup(repo => repo.ExecuteStoredProcedure<int>(storedProcedure, parameters))
                          .ReturnsAsync(expectedResult);
            var repository = mockRepository.Object;

            // Act
            var result = await repository.ExecuteStoredProcedure<int>(storedProcedure, parameters);

            // Assert
            Assert.Equal(expectedResult, result);
            // Add further assertions based on the expected result
        }


        [Fact]
        public void ExecuteStoredProcedure_ThrowsDataException_WhenExceptionOccurs()
        {
            // Arrange
            string storedProcedure = "InvalidStoredProcedure";
            object[] parameters = new object[] { 1, 2, 3 };
           

            var mockRepository = new Mock<IBrainWareRepository>();
            mockRepository.Setup(repo => repo.ExecuteStoredProcedure<int>(storedProcedure, parameters))
                          .ThrowsAsync(new Exception("Mocked exception"));

            var repository = mockRepository.Object;

            // Act & Assert
            Assert.ThrowsAsync<DataException>(() => repository.ExecuteStoredProcedure<int>(storedProcedure, parameters));
        }
    }
}

