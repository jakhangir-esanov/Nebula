
using Nebula.Application.Commands.Attachments.DeleteAttachment;
using Nebula.Domain.Entities.People;

namespace Nebula.Application.Commands.People.DeleteCustomerImage;

public record DeleteCustomerImageCommand : IRequest<bool>
{
    public DeleteCustomerImageCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}

public class DeleteCustomerImageCommandHandler : IRequestHandler<DeleteCustomerImageCommand, bool>
{
    private readonly IRepository<Customer> repository;
    private readonly IMediator mediator;
    public DeleteCustomerImageCommandHandler(IRepository<Customer> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<bool> Handle(DeleteCustomerImageCommand request, CancellationToken cancellationToken)
    {
        var customer = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Customer was not found!");

        var attachmentId = customer.AttachmentId
            ?? throw new NotFoundException("Attachment was not found!");

        await this.mediator.Send(new DeleteAttachmentCommand(attachmentId));
        customer.AttachmentId = null;
        customer.Attachment = null;

        this.repository.Update(customer);
        await this.repository.SaveAsync();

        return true;
    }
}
