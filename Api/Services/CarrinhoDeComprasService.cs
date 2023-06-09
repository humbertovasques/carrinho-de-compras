using Api.DTOs;
using Api.Repository;

namespace Api.Services
{
    public class CarrinhoDeComprasService : ICarrinhoDeComprasService
    {
        private readonly IItemRepository _itemRepository;
        public double _total = 0;
        public double _frete = 0;

        public CarrinhoDeComprasService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ItemDTO AdicionarItem(ItemDTO item)
        {
            _itemRepository.AddItem(item);
            return item;
        }

        public double CalcularTotal()
        {
            double pesoTotal = 0;
            double valorItens = 0;
            int quantidadeItensMesmoTipo = 0;
            bool descontoFreteAplicado = false;


            foreach (var item in _itemRepository.GetAllItems())
            {
                pesoTotal += item.Peso;
                valorItens += item.Valor;

                //verificação para garantir que o desconto do frete seja aplicado uma única vez
                if (!descontoFreteAplicado)
                {
                    // Verifica a quantidade de itens do mesmo tipo
                    foreach (var outroItem in _itemRepository.GetAllItems())
                    {
                        if (outroItem.Nome == item.Nome)
                        {
                            quantidadeItensMesmoTipo++;
                            descontoFreteAplicado = true;

                        }
                    }

                    // Aplica desconto de 5% no frete para carrinho de compras que possuem mais de dois itens do mesmo tipo
                    if (quantidadeItensMesmoTipo > 2)
                    {
                        _frete -= 0.05 * item.Valor;
                    }

                    quantidadeItensMesmoTipo = 0;
                }
            }

            // Calcula o valor do frete com base no peso total
            if (pesoTotal > 2 && pesoTotal < 10)
            {
                _frete = pesoTotal * 2.0;
            }
            else if (pesoTotal >= 10 && pesoTotal < 50)
            {
                _frete = pesoTotal * 4.0;
            }
            else if (pesoTotal >= 50)
            {
                _frete = pesoTotal * 7.0;
            }

            // Aplica acréscimo de R$ 10 no frete para carrinho de compras que tenha mais de 5 itens
            if (_itemRepository.GetAllItems().Count > 5)
            {
                _frete += 10.0;
            }

            // Aplica desconto de 10% para carrinhos de compras que custam mais de R$ 500,00
            if (valorItens > 500 && valorItens <= 1000)
            {
                valorItens -= (0.1 * valorItens);
            }
            // Aplica desconto de 20% para carrinhos de compras que custam mais de R$ 1000,00
            else if (valorItens > 1000)
            {
                valorItens -= (0.2 * valorItens);
            }

            _total = valorItens + _frete;
            return _total;
        }

        public List<ItemDTO> GetItens()
        {
            return _itemRepository.GetAllItems();
        }

        public CheckoutDTO RealizarCheckout()
        {
            var checkout = new CheckoutDTO
            {
                Itens = _itemRepository.GetAllItems(),
                Frete = _frete,
                Total = _total
            };

            return checkout;
        }
    }
}
