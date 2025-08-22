namespace BmgPromotionApi.Models
{
    public class Promotion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "iPhone Black Friday";
        public int HourlyLimit { get; set; } = 100; // Limite máximo de iPhones por hora
        public int CurrentAvailableQuantity { get; set; } // Quantidade disponível na hora atual
        public DateTime LastResetTime { get; set; } // Quando a quantidade foi resetada pela última vez
    }
}