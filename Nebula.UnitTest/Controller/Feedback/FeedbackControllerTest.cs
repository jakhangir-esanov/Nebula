using Nebula.Application.Commands.Feedbacks.CreateFeedback;
using Nebula.Application.Commands.Feedbacks.UpdateFeedback;

namespace Nebula.UnitTest.Controller.Feedback;

public class FeedbackControllerTest
{
    private readonly Mock<IMediator> mediatr;
    private readonly FeedbackController controller;
    public FeedbackControllerTest()
    {
        this.mediatr = new Mock<IMediator>();
        this.controller = new FeedbackController(this.mediatr.Object);
    }

    [Fact]
    public async Task AddFeedbackSuccessfully()
    {
        //Arrange
        var feedback = new CreateFeedbackCommand(Domain.Enums.Rating.excellent, "comment", DateTime.UtcNow, 1, 1);

        //Act
        var result = await this.controller.CreateAsync(feedback);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task UpdateFeedbackSuccessfully()
    {
        //Arrange 
        var updatedFeedback = new UpdateFeedbackCommand(1, Domain.Enums.Rating.poor, "updateComment", DateTime.UtcNow, 2, 2);

        //Act
        var result = await this.controller.UpdateAsync(updatedFeedback);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteFeedbackSuccessfully()
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
    public async Task GetByIdFeedbackSuccessfully()
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
    public async Task GetAllFeedbacksSuccesfully()
    {
        //Arrange

        //Act
        var result = await this.controller.GetAll();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}