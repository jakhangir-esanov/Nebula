using Nebula.Application.DTOs;
using Nebula.Domain.Entities.People;
using Nebula.Application.Commands.Attachments.CreateAttachment;

namespace Nebula.Application.Commands.People.UploadCustomerImage;

public record UploadCustomerImageCommand : IRequest<Customer>
{
    public UploadCustomerImageCommand(long customerId, AttachmentCreationDto dto)
    {
        this.customerId = customerId;
        this.dto = dto;
    }

    public long customerId { get; set; }
    public AttachmentCreationDto dto { get; set; }
}


public class UploadCustomerImageCommandHandler : IRequestHandler<UploadCustomerImageCommand, Customer>
{
    private readonly IRepository<Customer> repository;
    private readonly IMediator mediator;
    public UploadCustomerImageCommandHandler(IRepository<Customer> repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    public async Task<Customer> Handle(UploadCustomerImageCommand request, CancellationToken cancellationToken)
    {
        var customer = await this.repository.SelectAsync(x => x.Id.Equals(request.customerId))
            ?? throw new NotFoundException("Customer was not found!");

        if (customer.AttachmentId is not null)
            throw new AlreadyExistException("Attachment is already exist!");

        var createdAttachment = await this.mediator.Send(new CreateAttachmentCommand("CustomerFile", request.dto));
        customer.AttachmentId = createdAttachment.Id;
        customer.Attachment = createdAttachment;    

        this.repository.Update(customer);
        await this.repository.SaveAsync();

        return customer;
    }
}
