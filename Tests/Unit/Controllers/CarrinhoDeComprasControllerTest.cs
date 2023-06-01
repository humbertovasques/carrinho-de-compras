using Api.Controllers;
using Api.DTOs;
using Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Unit.Controllers
{
    public class CarrinhoDeComprasControllerTest
    {
        CarrinhoDeComprasController Sut;
        Mock<ICarrinhoDeComprasService> MockCarrinhoDeComprasService = new Mock<ICarrinhoDeComprasService>();

        public CarrinhoDeComprasControllerTest()
        {
            Sut = new CarrinhoDeComprasController(MockCarrinhoDeComprasService.Object);
        }

        [Fact]
        public void GetItens_RetornaStatusCode200()
        {
            // Configuração
            MockCarrinhoDeComprasService.Setup(carrinhoDeComprasService => carrinhoDeComprasService.GetItens()).Returns(new List<ItemDTO>());

            // Ação
            var result = Sut.GetItens() as OkObjectResult;

            // Verificação
            result?.StatusCode.Should().Be(200);
        }
        
        [Fact]
        public void AdicionarItem_RetornaStatusCode200()
        {
            // Configuração
            MockCarrinhoDeComprasService.Setup(carrinhoDeComprasService => carrinhoDeComprasService.AdicionarItem(It.IsAny<ItemDTO>())).Returns(new ItemDTO());

            // Ação
            var result = Sut.AdicionarItem(It.IsAny<ItemDTO>()) as OkObjectResult;

            // Verificação
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public void CalcularTotal_RetornaStatusCode200()
        {
            // Configuração
            MockCarrinhoDeComprasService.Setup(carrinhoDeComprasService => carrinhoDeComprasService.CalcularTotal()).Returns(It.IsAny<double>);

            // Ação
            var result = Sut.CalcularTotal() as OkObjectResult;

            // Verificação
            result?.StatusCode.Should().Be(200);
        }

        [Fact]
        public void RealizarCheckout_RetornaStatusCode200()
        {
            // Configuração
            MockCarrinhoDeComprasService.Setup(carrinhoDeComprasService => carrinhoDeComprasService.RealizarCheckout()).Returns(It.IsAny<CheckoutDTO>);

            // Ação
            var result = Sut.RealizarCheckout() as OkObjectResult;

            // Verificação
            result?.StatusCode.Should().Be(200);
        }
    }
}