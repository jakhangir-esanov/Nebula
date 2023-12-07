using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;
using Nebula.Application.Commands.Attachments.UpdateAttachment;

namespace Nebula.Application.Commands.People.UpdateCustomerImage;

public record UpdateCustomerImageCommand : IRequest<Customer>
{
    public UpdateCustomerImageCommand(long customerId, AttachmentCreationDto dto)
    {
        this.customerId = customerId;
        this.dto = dto;
    }

    public long customerId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}

public class UpdateCustomerImageCommandHandler : IRequestHandler<UpdateCustomerImageCommand, Customer>
{
    private readonly IRepository<Customer> repository;
    private readonly IMediator mediator;
    public UpdateCustomerImageCommandHandler(IRepository<Customer> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<Customer> Handle(UpdateCustomerImageCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.SelectAsync(x => x.Id.Equals(request.customerId))
            ?? throw new NotFoundException("Customer was not found!");

        var customerAttachmentId = customer.AttachmentId
            ?? throw new NotFoundException("Attachment was not found!");

        var updatedAttachment = await this.mediator.Send(new UpdateAttachmentCommand("CustomerFile", customerAttachmentId, request.dto));
        customer.AttachmentId = updatedAttachment.Id;
        customer.Attachment = updatedAttachment;

        this.repository.Update(customer);
        await this.repository.SaveAsync();

        return customer;
    }
}
