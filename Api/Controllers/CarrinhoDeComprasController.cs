using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarrinhoDeComprasController : ControllerBase
    {
        private readonly ICarrinhoDeComprasService _carrinhoDeComprasService;

        public CarrinhoDeComprasController(ICarrinhoDeComprasService carrinhoDeComprasService)
        {
            _carrinhoDeComprasService = carrinhoDeComprasService;
        }
        
        [HttpGet("GetItens")]
        public IActionResult GetItens()
        {
            var itens = _carrinhoDeComprasService.GetItens();
            return Ok(itens);
        }

        [HttpPost("AdicionarItem")]
        public IActionResult AdicionarItem(ItemDTO item)
        {
            var result = _carrinhoDeComprasService.AdicionarItem(item);
            return Ok(result);
        }

        [HttpGet("CalcularTotal")]
        public IActionResult CalcularTotal()
        {
            double total = _carrinhoDeComprasService.CalcularTotal();
            return Ok(total);
        }

        [HttpGet("RealizarCheckout")]
        public IActionResult RealizarCheckout()
        {
            CheckoutDTO checkout = _carrinhoDeComprasService.RealizarCheckout();
            return Ok(checkout);
        }

    }
