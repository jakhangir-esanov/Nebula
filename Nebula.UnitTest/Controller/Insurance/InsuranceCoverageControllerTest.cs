using Nebula.Application.Commands.Insurances.CreateInsuranceCoverage;
using Nebula.Application.Commands.Insurances.UpdateInsuranceCoverage;

namespace Nebula.UnitTest.Controller.Insurance;

public class InsuranceCoverageControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly InsuranceCoverageController controller;
    public InsuranceCoverageControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new InsuranceCoverageController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddInsuranceCoverageSuccessfully()
    {
        //Arrange
        var insuranceCoverage = new CreateInsuranceCoverageCommand("name", "description", 120);

        //Act
        var result = await this.controller.CreateAsync(insuranceCoverage);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateInsuranceCoverageSucccessfully()
    {
        //Arrange
        var updatedInsuranceCoverage = new UpdateInsuranceCoverageCommand(1, "updateName", "updateDescription", 130);

        //Act
        var result = await this.controller.UpdateAsync(updatedInsuranceCoverage);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);  
    }

    [Fact]
    public async Task DeleteInsuranceCoverageSuccessfully()
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
    public async Task GetByIdInsuranceCoverageSuccessfully()
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
    public async Task GetAllInsuranceCoveragesSuccessfully()
    {
        //Arrange

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
