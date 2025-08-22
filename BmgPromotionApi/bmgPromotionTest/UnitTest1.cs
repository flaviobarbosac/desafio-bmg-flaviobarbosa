using BmgPromotionApi.Data;
using BmgPromotionApi.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace bmgPromotionTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var promotionStore = new PromotionStore();
            // Usa NullLogger em vez de mock - não produz logs reais
            var logger = NullLogger<PromotionService>.Instance;
            var service = new PromotionService(promotionStore, logger);

            int numberOfConcurrentRequests = 500;
            int successfulPurchases = 0;
            var tasks = new List<Task>();

            // Act
            for (int i = 0; i < numberOfConcurrentRequests; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    var result = service.PurchaseIPhone();
                    if (result.Success)
                    {
                        Interlocked.Increment(ref successfulPurchases);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            // Assert
            Assert.Equal(100, successfulPurchases); // Exatamente 100 compras bem-sucedidas
            Assert.Equal(0, promotionStore.GetPromotion().CurrentAvailableQuantity); // Estoque zerado
        }
    }
}