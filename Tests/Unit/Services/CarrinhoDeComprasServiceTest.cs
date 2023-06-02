using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Repository;
using Api.Services;
using FluentAssertions;
using Moq;

namespace Tests.Unit.Services
{
    public class CarrinhoDeComprasServiceTest
    {
        private readonly CarrinhoDeComprasService Sut;
        private Mock<IItemRepository> MockItemRepository = new Mock<IItemRepository>();

        public CarrinhoDeComprasServiceTest()
        {
            Sut = new CarrinhoDeComprasService(MockItemRepository.Object);
        }

        [Fact]
        public void Caso_1()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO (),
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().Be(0);
        }

        [Fact]
        public void Caso_2()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.5, Valor = 0.01 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().Be(0.01);
        }
    }
}