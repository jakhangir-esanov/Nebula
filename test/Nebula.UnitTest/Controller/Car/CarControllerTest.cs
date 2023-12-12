using Nebula.Application.Commands.Cars.CreateCar;
using Nebula.Application.Commands.Cars.UpdateCar;

namespace Nebula.UnitTest.Controller.Car;

public class CarControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly CarController controller;
    public CarControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new CarController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddCarSuccessfully()
    {
        //Arrange
        var car = new CreateCarCommand("Venom", DateTime.UtcNow, "red", "A777AA", 3342342, true, 1);

        //Act
        var result = await this.controller.CreateAsync(car);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateCarSuccessfully()
    {
        //Arrange
        var updatedCar = new UpdateCarCommand(1, "UpdatedModel", DateTime.UtcNow, "UpdatedColor", "A555AA", 2342123, false, 2);

        //Act
        var result = await this.controller.UpdateAsync(updatedCar);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCarSuccessfully()
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
    public async Task GetByIdCarSuccessfully()
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
    public async Task GetAllCarsSuccessfully()
    {
        //Arrange

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
