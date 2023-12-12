using Nebula.Application.Commands.People.CreateCustomer;
using Nebula.Application.Commands.People.UpdateCustomer;

namespace Nebula.UnitTest.Controller.People;

public class CustomerControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly CustomerController controller;
    public CustomerControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new CustomerController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddCustomerSuccessfully()
    {
        //Arrange
        var customer = new CreateCustomerCommand("firstName", "lastName", "email@mail.com", "998991234567", 
            "password", DateTime.UtcNow, "address", "124233231", DateTime.UtcNow);

        //Act
        var result = await this.controller.CreateAsync(customer);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdaetCustomerSuccessfully()
    {
        //Arrange
        var updatedCustomer = new UpdateCustomerCommand(1, "updatedName", "updateLastName", "updated@mail.com", "998972357632", 
            "password", DateTime.UtcNow, "updateAddress", "2345223432", DateTime.UtcNow);

        //Act
        var result = await this.controller.UpdateAsync(updatedCustomer);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteCustomerSuccessfully()
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
    public async Task GetByIdCustomerSuccessfully()
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
    public async Task GetAllCustomersSuccessfully()
    {
        //Arrange

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
