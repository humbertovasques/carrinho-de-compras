using Api.Services;
using Api.Repository;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Tests
{
    public class ProgramTest
    {
        [Fact]
        public void CreateApp_ShouldRegisterServices()
        {
            // Arrange
            var args = new string[] { };
            var services = new ServiceCollection();

            // Act
            var app = Program.CreateApp(args);
            var serviceProvider = app.Services;

            // Assert
            app.Environment.IsDevelopment().Should().Be(false);
            serviceProvider.Should().NotBeNull();
            serviceProvider.GetService<ICarrinhoDeComprasService>().Should().NotBeNull();
            serviceProvider.GetService<IItemRepository>().Should().NotBeNull();
        }

        
    }
}
