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
        [Fact]
        public void Caso_13()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 2, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 2, Valor = 100 },
                new ItemDTO { Nome = "Item 1", Peso = 2, Valor = 100 },
                new ItemDTO { Nome = "Item 2", Peso = 1.5, Valor = 100 },
                new ItemDTO { Nome = "Item 3", Peso = 2.4, Valor = 100.02 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(468.83, 2);
        }
        [Fact]
        public void Caso_14()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 10, Valor = 1000},
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(940, 2);
        }
        [Fact]
        public void Caso_15()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 10.1, Valor = 1000.1},
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(840.48, 2);
        }
        [Fact]
        public void Caso_16()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 10, Valor = 100},
                new ItemDTO { Nome = "Item 2", Peso = 5, Valor = 100 },
                new ItemDTO { Nome = "Item 3", Peso = 5, Valor = 50 },
                new ItemDTO { Nome = "Item 4", Peso = 5, Valor = 99 },
                new ItemDTO { Nome = "Item 5", Peso = 14.9, Valor = 50 },
                new ItemDTO { Nome = "Item 6", Peso = 10, Valor = 100 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(708.6, 2);
        }
        [Fact]
        public void Caso_17()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 25, Valor = 250.05},
                new ItemDTO { Nome = "Item 1", Peso = 25, Valor = 250.05 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(650, 2);
        }
        [Fact]
        public void Caso_18()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 50.1, Valor = 1000.01},
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(1150.78, 2);
        }
    }
}