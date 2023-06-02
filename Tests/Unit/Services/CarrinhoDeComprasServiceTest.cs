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

        [Fact]
        public void Caso_3()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.25, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.25, Valor = 100 },
                new ItemDTO { Nome = "Item 2", Peso = 0.25, Valor = 100 },
                new ItemDTO { Nome = "Item 3", Peso = 0.25, Valor = 100 },
                new ItemDTO { Nome = "Item 4", Peso = 1, Valor = 99.99 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().Be(499.99);
        }

        [Fact]
        public void Caso_4()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.25, Valor = 100},
                new ItemDTO { Nome = "Item 2", Peso = 0.25, Valor = 100 },
                new ItemDTO { Nome = "Item 3", Peso = 0.25, Valor = 100 },
                new ItemDTO { Nome = "Item 4", Peso = 0.25, Valor = 100 },
                new ItemDTO { Nome = "Item 5", Peso = 1, Valor = 101 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().Be(450.90);
        }

        [Fact]
        public void Caso_5()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.2, Valor = 200},
                new ItemDTO { Nome = "Item 2", Peso = 0.2, Valor = 200 },
                new ItemDTO { Nome = "Item 3", Peso = 0.2, Valor = 200 },
                new ItemDTO { Nome = "Item 4", Peso = 0.2, Valor = 200 },
                new ItemDTO { Nome = "Item 5", Peso = 0.2, Valor = 201 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().Be(800.80);
        }

        [Fact]
        public void Caso_6()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 50 },
                new ItemDTO { Nome = "Item 2", Peso = 0.4, Valor = 100 },
                new ItemDTO { Nome = "Item 3", Peso = 0.4, Valor = 100 },
                new ItemDTO { Nome = "Item 4", Peso = 0.2, Valor = 100.01 },
                new ItemDTO { Nome = "Item 5", Peso = 0.2, Valor = 50 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(460.01, 2);
        }
    }
}