using Nebula.Application.Commands.People.CreateUser;
using Nebula.Application.Commands.People.UpdateUser;

namespace Nebula.UnitTest.Controller.People;

public class UserControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly UserController controller;
    public UserControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new UserController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddUserSuccessfully()
    {
        //Arrange 
        var user = new CreateUserCommand("firstName", "lastName", "username", "email.@mail.com", "998912345687", 
            "password", DateTime.UtcNow, Domain.Enums.UserRole.admin, 1);

        //Act
        var result = await this.controller.CreateAsync(user);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateUserSuccessfully()
    {
        //Arrange 
        var updateUser = new UpdateUserCommand(1, "updateName", "updatedLastName", "updateUsername", "updated@mail.com",
            "998976543722", "password", DateTime.UtcNow, Domain.Enums.UserRole.superAdmin, 2);

        //Act
        var result = await this.controller.UpdateAsync(updateUser);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUserSuccessfully()
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
    public async Task GetByIdUserSuccessfully()
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
    public async Task GetAllUsersSuccessfully()
    {
        //Arrange 

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
