using BmgPromotionApi.Models;

namespace BmgPromotionApi.Data
{
    public class PromotionStore
    {
        private readonly Promotion _promotion;
        private readonly object _lock = new object(); // Objeto para o lock de sincronização

        public PromotionStore()
        {
            _promotion = new Promotion
            {
                CurrentAvailableQuantity = 100, // Começa com 100 iPhones disponíveis
                LastResetTime = DateTime.UtcNow // Define o tempo de reset inicial
            };
        }

        // Retorna uma cópia do objeto Promotion para evitar modificações externas sem o lock
        public Promotion GetPromotion()
        {
            lock (_lock)
            {
                return new Promotion
                {
                    Id = _promotion.Id,
                    Name = _promotion.Name,
                    HourlyLimit = _promotion.HourlyLimit,
                    CurrentAvailableQuantity = _promotion.CurrentAvailableQuantity,
                    LastResetTime = _promotion.LastResetTime
                };
            }
        }

        // Tenta realizar uma compra, controlando o limite horário e a concorrência
        public bool TryPurchase()
        {
            lock (_lock) // Garante que apenas uma thread possa modificar o estado por vez
            {
                // Verifica se uma hora se passou desde o último reset
                if ((DateTime.UtcNow - _promotion.LastResetTime).TotalHours >= 1)
                {
                    _promotion.CurrentAvailableQuantity = _promotion.HourlyLimit; // Reseta a quantidade
                    _promotion.LastResetTime = DateTime.UtcNow; // Atualiza o tempo de reset
                }

                if (_promotion.CurrentAvailableQuantity > 0)
                {
                    _promotion.CurrentAvailableQuantity--; // Decrementa a quantidade
                    return true; // Compra bem-sucedida
                }
                return false; // Não há iPhones disponíveis ou limite horário atingido
            }
        }
    }
}