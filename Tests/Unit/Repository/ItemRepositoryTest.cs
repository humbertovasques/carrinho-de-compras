using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Repository;
using FluentAssertions;

namespace Tests.Unit.Repository
{
    public class ItemRepositoryTest
    {
        ItemRepository sut = new ItemRepository();

        [Fact]
        public void AddItem_AdicionaUmItemNaLista()
        {
            // Configuração
            ItemDTO item = new ItemDTO { Nome = "Item 1", Peso = 0.25, Valor = 100 };

            // Ação
            sut.AddItem(item);

            // Verificação
            sut.GetAllItems().Count.Should().Be(1);
        }

        [Fact]
        public void GetAllItems_RetornaUmaListadeItems()
        {
            // Configuração
            var itemList = new List<ItemDTO>
            {   
                new ItemDTO {Nome = "Item 1", Peso = 0.25, Valor = 100 },
                new ItemDTO {Nome = "Item 2", Peso = 0.25, Valor = 100 },
                new ItemDTO {Nome = "Item 3", Peso = 0.25, Valor = 100 },
            };

            foreach (var item in itemList)
            {
                sut.AddItem(item);
            }

            // Ação
            var result = sut.GetAllItems();

            // Verificação
            result.Count.Should().Be(itemList.Count);
            result.Should().BeOfType<List<ItemDTO>>();

        }
    }
}