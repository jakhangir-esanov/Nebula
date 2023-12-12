using Nebula.Application.Commands.Offices.CreateOffice;
using Nebula.Application.Commands.Offices.UpdateOffice;

namespace Nebula.UnitTest.Controller.Office;

public class OfficeControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly OfficeController controller;
    public OfficeControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new OfficeController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddOfficeSuccuessfully()
    {
        //Arrange
        var office = new CreateOfficeCommand("name", "address", "city", "state", "190614", "country", 
            "998991234567", "email@mail.com", "websit.com", "description");

        //Act
        var result = await this.controller.CreateAsync(office);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateOfficeSuccessfully()
    {
        //Arrange
        var updatedOffice = new UpdateOfficeCommand(1, "updateName", "updateAdress", "updatedCity", "updateState", 
            "190768", "updatedCountry", "998976753245", "updaetemail.com", "updatewebsit.com", "updateDescription");

        //Act
        var result = await this.controller.UpdateAsync(updatedOffice);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteOfficeSuccessfully()
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
    public async Task GetByIdOfficeSuccessfully()
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
    public async Task GetAllOfficesSuccessfully()
    {
        //Arrange 

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
