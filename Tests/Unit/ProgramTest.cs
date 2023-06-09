using Api.Services;
using Api.Repository;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.AspNetCore.Builder;

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
            serviceProvider.Should().NotBeNull();
            serviceProvider.GetService<ICarrinhoDeComprasService>().Should().NotBeNull();
            serviceProvider.GetService<IItemRepository>().Should().NotBeNull();
        }

        // [Fact]
        // public void Main_ShouldRunApp()
        // {
        //     // Arrange
        //     var args = new string[] { };

        //     // Act
        //     Program.Main(args);

        //     // No assertions needed, as we are verifying that the code runs without errors
        // }

        // [Fact]
        // public void CreateApp_ShouldConfigureSwaggerInDevelopmentEnvironment()
        // {
        //     // Arrange
        //     var args = new string[] { };

        //     // Act
        //     var app = Program.CreateApp(args);
        //     var builder = new ApplicationBuilder(app.Services);

        //     // Call the configuration methods
        //     app.Configure(builder);

        //     // Assert
        //     app.Environment.IsDevelopment().Should().BeTrue();
        //     // Additional assertions for Swagger configuration can be added here
        // }
    }
}
