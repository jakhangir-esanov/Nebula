using Nebula.Application.Commands.Insurances.CreateInsurance;
using Nebula.Application.Commands.Insurances.UpdateInsurance;

namespace Nebula.UnitTest.Controller.Insurance;

public class InsuranceControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly InsuranceController controller;
    public InsuranceControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new InsuranceController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddInsuranceSuccessfully()
    {
        //Arrange
        var insurance = new CreateInsuranceCommand("name", 1, 1);

        //Act
        var result = await this.controller.CreateAsync(insurance);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateInsuranceSuccessfully()
    {
        //Arrange
        var updateInsurance = new UpdateInsuranceCommand(1, "name", 1, 1);

        //Act
        var result = await this.controller.UpdateAsync(updateInsurance);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteInsuranceSuccessfully()
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
    public async Task GetByIdInsuranceSuccessfully()
    {
        //Arrange
        long id = 1;

        //Assert
        var result = await this.controller.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAllInsurancesSuccessfully()
    {
        //Arrange

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
