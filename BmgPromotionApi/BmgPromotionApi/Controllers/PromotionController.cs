using BmgPromotionApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BmgPromotionApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base para este controlador (ex: /api/Promotion)
    public class PromotionController : ControllerBase
    {
        private readonly PromotionService _promotionService;
        private readonly ILogger<PromotionController> _logger;

        public PromotionController(PromotionService promotionService, ILogger<PromotionController> logger)
        {
            _promotionService = promotionService;
            _logger = logger;
        }

        // Endpoint para comprar um iPhone
        [HttpPost("purchase-iphone")]
        public IActionResult PurchaseIPhone()
        {
            _logger.LogInformation("Requisição de compra de iPhone recebida.");
            var result = _promotionService.PurchaseIPhone();

            if (result.Success)
            {
                return Ok(result); // Retorna 200 OK em caso de sucesso
            }
            else
            {
                return BadRequest(result); // Retorna 400 Bad Request em caso de falha
            }
        }

        // Endpoint para obter o status da promoção
        [HttpGet("status")]
        public IActionResult GetPromotionStatus()
        {
            var status = _promotionService.GetCurrentPromotionStatus();
            return Ok(status); // Retorna 200 OK com o status da promoção
        }
    }
}