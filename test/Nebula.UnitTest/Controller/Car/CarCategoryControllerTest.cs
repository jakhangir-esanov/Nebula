using Nebula.Application.Commands.Cars.CreateCarCategory;
using Nebula.Application.Commands.Cars.UpdateCarCategory;

namespace Nebula.UnitTest.Controller.Car;

public class CarCategoryControllerTest
{
    private Mock<IMediator> mediatr;
    private CarCategoryController controller;
    public CarCategoryControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new CarCategoryController(this.mediatr.Object);
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

    [Fact]
    public async Task UpdateCarCategorySuccessfully()
    {
        //Arrange
        var updatedCarCategory = new UpdateCarCategoryCommand(1, "updatedName", 150, "updatedDescription", 20);

        //Act
        var result = await this.controller.UpdateAsync(updatedCarCategory);

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
