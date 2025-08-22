using BmgPromotionApi.Data;
using BmgPromotionApi.Models;
using Microsoft.Extensions.Logging;

namespace BmgPromotionApi.Services
{
    public class PromotionService
    {
        private readonly PromotionStore _promotionStore;
        private readonly ILogger<PromotionService> _logger;

        public PromotionService(PromotionStore promotionStore, ILogger<PromotionService> logger)
        {
            _promotionStore = promotionStore;
            _logger = logger;
        }

        // Tenta comprar um iPhone e retorna o resultado
        public PurchaseResult PurchaseIPhone()
        {
            _logger.LogInformation("Tentando comprar um iPhone.");

            if (_promotionStore.TryPurchase())
            {
                _logger.LogInformation("iPhone comprado com sucesso! Restantes: {Remaining}", _promotionStore.GetPromotion().CurrentAvailableQuantity);
                return new PurchaseResult { Success = true, Message = "iPhone purchased successfully!" };
            }
            else
            {
                _logger.LogWarning("Falha ao comprar iPhone. Sem estoque ou limite horário atingido.");
                return new PurchaseResult { Success = false, Message = "No iPhones available or hourly limit reached." };
            }
        }

        // Retorna o status atual da promoção
        public Promotion GetCurrentPromotionStatus()
        {
            return _promotionStore.GetPromotion();
        }
    }

    // Classe auxiliar para o resultado da compra
    public class PurchaseResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}