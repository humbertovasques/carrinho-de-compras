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
        public void AdicionarItem_RetornaUmItem()
        {
            // Ação
            var result = Sut.AdicionarItem(new ItemDTO());

            // Verificação
            result.Should().BeOfType<ItemDTO>();
        }

        [Fact]
        public void GetItens_RetornaUmaListaDeItens()
        {
            // Configuração
            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(new List<ItemDTO>());

            // Ação
            var result = Sut.GetItens();

            // Verificação
            result.Should().BeOfType<List<ItemDTO>>();
        }

        [Fact]
        public void RealizarCheckout_RetornaUmCheckOutDTO()
        {
            // Configuração
            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(new List<ItemDTO>());

            // Ação
            var result = Sut.RealizarCheckout();

            // Verificação
            result.Should().BeOfType<CheckoutDTO>();
        }

        [Fact]
        public void CalcularTotal_Caso_01()
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
        public void CalcularTotal_Caso_02()
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
            Sut._total.Should().BeApproximately(0.01, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_03()
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
            Sut._total.Should().BeApproximately(499.99, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_04()
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
            Sut._total.Should().BeApproximately(450.90, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_05()
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
            Sut._total.Should().BeApproximately(800.80, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_06()
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
            Sut._total.Should().BeApproximately(460.01, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_07()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 100},
                new ItemDTO { Nome = "Item 2", Peso = 0.3, Valor = 200 },
                new ItemDTO { Nome = "Item 3", Peso = 0.3, Valor = 200 },
                new ItemDTO { Nome = "Item 4", Peso = 0.3, Valor = 200.01 },
                new ItemDTO { Nome = "Item 5", Peso = 0.3, Valor = 200 },               
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(810.01, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_08()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.3, Valor = 200},
                new ItemDTO { Nome = "Item 1", Peso = 0.3, Valor = 200},
                new ItemDTO { Nome = "Item 1", Peso = 0.3, Valor = 200},
                new ItemDTO { Nome = "Item 2", Peso = 0.3, Valor = 100 },
                new ItemDTO { Nome = "Item 3", Peso = 0.4, Valor = 100 },
                new ItemDTO { Nome = "Item 4", Peso = 0.4, Valor = 200.01 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(800.00, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_09()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.3, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.3, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.3, Valor = 100},
                new ItemDTO { Nome = "Item 2", Peso = 0.3, Valor = 50 },
                new ItemDTO { Nome = "Item 3", Peso = 0.4, Valor = 50 },
                new ItemDTO { Nome = "Item 4", Peso = 0.4, Valor = 99 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(504.00, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_10()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 1, Valor = 200},
                new ItemDTO { Nome = "Item 1", Peso = 1, Valor = 200},
                new ItemDTO { Nome = "Item 1", Peso = 1, Valor = 200},
                new ItemDTO { Nome = "Item 2", Peso = 1, Valor = 200 },
                new ItemDTO { Nome = "Item 3", Peso = 1, Valor = 201 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(810.80, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_11()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 200},
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 200},
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 200},
                new ItemDTO { Nome = "Item 2", Peso = 0.3, Valor = 200},
                new ItemDTO { Nome = "Item 3", Peso = 0.3, Valor = 100},
                new ItemDTO { Nome = "Item 4", Peso = 0.3, Valor = 100},
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(914.20, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_12()
        {
            // Configuração
            var items = new List<ItemDTO>()
            {
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 100},
                new ItemDTO { Nome = "Item 1", Peso = 0.4, Valor = 100},
                new ItemDTO { Nome = "Item 2", Peso = 0.5, Valor = 100},
                new ItemDTO { Nome = "Item 3", Peso = 0.5, Valor = 99 },
            };

            MockItemRepository.Setup(repo => repo.GetAllItems()).Returns(items);

            // Ação
            var result = Sut.CalcularTotal();

            // Verificação
            Sut._total.Should().BeApproximately(503.40, 0.01);
        }

        [Fact]
        public void CalcularTotal_Caso_13()
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
            Sut._total.Should().BeApproximately(469.81, 0.01);
        }
        [Fact]
        public void CalcularTotal_Caso_14()
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
            Sut._total.Should().BeApproximately(940.00, 0.01);
        }
        [Fact]
        public void CalcularTotal_Caso_15()
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
            Sut._total.Should().BeApproximately(840.48, 0.01);
        }
        [Fact]
        public void CalcularTotal_Caso_16()
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
            Sut._total.Should().BeApproximately(708.60, 0.01);
        }
        [Fact]
        public void CalcularTotal_Caso_17()
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
            Sut._total.Should().BeApproximately(800.09, 0.01);
        }
        [Fact]
        public void CalcularTotal_Caso_18()
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
            Sut._total.Should().BeApproximately(1150.70, 0.01);
        }
    }
}