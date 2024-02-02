using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nebula.Application.Commands.Cars.CreateCarCategory;
using Nebula.Application.Commands.Cars.UpdateCarCategory;
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
        var carCategory = new CreateCarCategoryCommand("name", 25, 250, "description", 250);

        //Act
        var result = await controller.CreateAsync(carCategory);

        //Asseert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCarCategorySuccessfully()
    {
        //Arrrange 
        var updateCarCategory = new UpdateCarCategoryCommand(1, "updateName", 21, 240, "updateDescription", 0);

        //Act
        var result = await this.controller.UpdateAsync(updateCarCategory);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCarCategorySuccessfully()
    {
        //Arrange 
        long id = 1;

        //Act
        var result = await this.controller.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetByIdCarCategorySuccessfully()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await this.controller.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAllCarCategoriesSuccessfully()
    {
        //Arrange 

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
