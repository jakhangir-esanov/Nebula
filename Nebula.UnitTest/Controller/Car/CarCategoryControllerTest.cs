using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nebula.Application.Commands.Cars.CreateCarCategory;
using Nebula.WebApi.Controllers;

namespace Nebula.UnitTest.Controller.Car;

public class CarCategoryControllerTest
{
    private Mock<IMediator> mediatr;
    private CarCategoryController controller;
    public CarCategoryControllerTest()
    {
        mediatr = new Mock<IMediator>();
        controller = new CarCategoryController(mediatr.Object);
    }

    [Fact]
    public async Task AddCarCategorySuccessfully()
    {
        //Arrange 
        var carCategory = new CreateCarCategoryCommand("name", 25, "description", 250);

        //Act
        var result = await controller.CreateAsync(carCategory);

        //Asseert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
