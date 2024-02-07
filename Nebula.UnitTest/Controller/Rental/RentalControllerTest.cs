using Nebula.Application.Commands.Rentals.CreateRental;
using Nebula.Application.Commands.Rentals.UpdateRental;

namespace Nebula.UnitTest.Controller.Rental;

public class RentalControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly RentalController controller;
    public RentalControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new RentalController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddRentalSuccessfully()
    {
        //Arrange 
        var rental = new CreateRentalCommand(1, DateTime.UtcNow, DateTime.UtcNow);

        //Act
        var result = await this.controller.CreateAsync(rental);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateRentalSuccessfully()
    {
        //Arrange 
        var updatedRental = new UpdateRentalCommand(1, 2, DateTime.UtcNow, DateTime.UtcNow);

        //Act
        var result = await this.controller.UpdateAsync(updatedRental);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteRentalSuccessfully()
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
    public async Task GetByIdRentalSuccessfully()
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
    public async Task GetAllRentalsSuccessfully()
    {
        //Arrange 

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
