using Nebula.Application.Commands.Payments.CreatePayment;
using Nebula.Application.Commands.Payments.UpdatePayment;

namespace Nebula.UnitTest.Controller.Payment;

public class PaymentControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly PaymentController controller;
    public PaymentControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new PaymentController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddPaymentSuccessfully()
    {
        //Arrange
        var payment = new CreatePaymentCommand(230, Domain.Enums.PaymentType.cash, 1, 1, 
            Domain.Enums.PaymentStatus.inProgress);

        //Act
        var result = await this.controller.CreateAsync(payment);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePaymentSuccessfully()
    {
        //Arrange
        var updatePayment = new UpdatePaymentCommand(1, 130, Domain.Enums.PaymentType.cash, 1, 1, 
            Domain.Enums.PaymentStatus.success);

        //Act
        var result = await this.controller.UpdateAsync(updatePayment);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeletePaymentSuccessfully()
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
    public async Task GetByIdPaymentSuccessfully()
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
    public async Task GetAllPaymentsSuccessfully()
    {
        //Arrange 

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
