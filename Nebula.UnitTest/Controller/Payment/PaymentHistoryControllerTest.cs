using Nebula.Application.Commands.Payments.CreatePaymentHistory;
using Nebula.Application.Commands.Payments.UpdatePaymentHistory;

namespace Nebula.UnitTest.Controller.Payment;

public class PaymentHistoryControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly PaymentHistoryController controller;
    public PaymentHistoryControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new PaymentHistoryController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddPaymentHistorySuccessfully()
    {
        //Arrange 
        var paymentHistory = new CreatePaymentHistoryCommand(DateTime.UtcNow, 234, Domain.Enums.PaymentType.crypto, 1, 1, 1);

        //Act
        var result = await this.controller.CreateAsync(paymentHistory);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePaymentHistorySuccessfully()
    {
        //Arrange
        var updatePaymentHistory = new UpdatePaymentHistoryCommand(1, DateTime.UtcNow, 132, Domain.Enums.PaymentType.card, 2, 2, 2);

        //Act
        var result = await this.controller.UpdateAsync(updatePaymentHistory);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeletePaymentHistorySuccessfully()
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
    public async Task GetByIdPaymentHistorySuccessfully()
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
    public async Task GetAllPaymentHistoriesSuccessfully()
    {
        //Arrange 

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
