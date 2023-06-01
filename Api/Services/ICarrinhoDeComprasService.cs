using Api.DTOs;

namespace Api.Services
{
    public interface ICarrinhoDeComprasService
    {
        ItemDTO AdicionarItem(ItemDTO item);
        double CalcularTotal();
        CheckoutDTO RealizarCheckout();
        List<ItemDTO> GetItens();
    }
}