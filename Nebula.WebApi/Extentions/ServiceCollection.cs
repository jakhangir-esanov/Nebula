using Nebula.Application.Commands.Attachments.DeleteAttachment;
using Nebula.Application.Commands.Attachments.UpdateAttachment;
using Nebula.Application.Commands.Cars.DeleteCarCategoryImage;
using Nebula.Application.Commands.Cars.DeleteCarImage;
using Nebula.Application.Commands.Cars.UpdateCarCategoryImage;
using Nebula.Application.Commands.Cars.UpdateCarImage;
using Nebula.Application.Commands.Cars.UploadCarCategoryImage;

namespace Nebula.WebApi.Extentions;

public static class ServiceCollection
{

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateAttachmentCommand, Attachment>, CreateAttachmentCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteAttachmentCommand, bool>, DeleteAttachmentCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateAttachmentCommand, Attachment>, UpdateAttachmentCommandHandler>();

        services.AddTransient<IRequestHandler<CreateCarAttachmentCommand, CarAttachment>, CreateCarAttachmentCommandHandler>();
        services.AddTransient<IRequestHandler<CreateOfficeAttachmentCommand, OfficeAttachment>, CreateOfficeAttachmentCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteCarAttachmentCommand, bool>, DeleteCarAttachmentCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteOfficeAttachmentCommand, bool>, DeleteOfficeAttachmentCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateCarAttachmentCommand, CarAttachment>, UpdateCarAttachmentCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateOfficeAttachmentCommand, OfficeAttachment>, UpdateOfficeAttachmentCommandHandler>();

        services.AddTransient<IRequestHandler<GetAllCarAttachmentsQuery, IEnumerable<AttachmentResultDto>>, GetAllCarAttachmentsQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllOfficeAttachmentsQuery, IEnumerable<AttachmentResultDto>>, GetAllOfficeAttachmentsQueryHandler>();

        services.AddTransient<IRequestHandler<CreateCarCommand, Car>, CreateCarCommandHandler>();
        services.AddTransient<IRequestHandler<UploadCarImageCommand, CarResultDto>, UploadCarImageCommandHandler>();
        services.AddTransient<IRequestHandler<UploadCarCategoryImageCommand, CarCategory>, UploadCarCategoryImageCommandHandler>();
        services.AddTransient<IRequestHandler<CreateCarCategoryCommand, CarCategory>, CreateCarCategoryCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteCarCommand, bool>, DeleteCarCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteCarImageСommand, bool>, DeleteCarImageCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteCarCategoryImageCommand, bool>, DeleteCarCategoryImageCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteCarCategoryCommand, bool>, DeleteCarCategoryCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateCarCommand, Car>, UpdateCarCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateCarImageCommand, CarResultDto>, UpdateCarImageCommadHandler>();
        services.AddTransient<IRequestHandler<UpdateCarCategoryImageCommand, CarCategory>, UpdateCarCategoryImageCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateCarCategoryCommand, CarCategory>, UpdateCarCategoryCommandHandler>();

        services.AddTransient<IRequestHandler<GetCarQuery, CarResultDto>, GetCarQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllCarsQuery, IEnumerable<CarCategoryResultDto>>, GetAllCarsQueryHandler>();

        services.AddTransient<IRequestHandler<GetCarCategoryQuery, CarCategoryResultDto>, GetCarCategoryQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllCarCategoriesQuery, IEnumerable<CarCategoryResultDto>>, GetAllCarCategoriesQueryHandler>();

        services.AddTransient<IRequestHandler<CreateFeedbackCommand, Feedback>, CreateFeedbackCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteFeedbackCommand, bool>, DeleteFeedbackCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateFeedbackCommand, Feedback>, UpdateFeedbackCommandHandler>();

        services.AddTransient<IRequestHandler<GetFeedbackQuery, FeedbackResultDto>, GetFeedbackQueryHandler>();

        services.AddTransient<IRequestHandler<GetAllFeedbacksQuery, IEnumerable<FeedbackResultDto>>, GetAllFeedbacksQueryHandler>();

        services.AddTransient<IRequestHandler<CreateInsuranceCommand, Insurance>, CreateInsuranceCommandHandler>();
        services.AddTransient<IRequestHandler<CreateInsuranceCoverageCommand, InsuranceCoverage>, CreateInsuranceCoverageCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteInsuranceCommand, bool>, DeleteInsuranceCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteInsuranceCoverageCommand, bool>, DeleteInsuranceCoverageCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateInsuranceCommand, Insurance>, UpdateInsuranceCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateInsuranceCoverageCommand, InsuranceCoverage>, UpdateInsuranceCoverageCommandHandler>();

        services.AddTransient<IRequestHandler<GetInsuranceQuery, InsuranceResultDto>, GetInsuranceQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllInsurancesQuery, IEnumerable<InsuranceResultDto>>, GetAllInsurancesQueryHandler>();

        services.AddTransient<IRequestHandler<GetInsuranceCoverageQuery, InsuranceCoverageResultDto>, GetInsuranceCoverageQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllInsuranceCoveragesQuery, IEnumerable<InsuranceCoverageResultDto>>, GetAllInsuranceCoverageQueryHandler>();

        services.AddTransient<IRequestHandler<CreateOfficeCommand, Office>, CreateOfficeCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteOfficeCommand, bool>, DeleteOfficeCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateOfficeCommand, Office>, UpdateOfficeCommandHandler>();

        services.AddTransient<IRequestHandler<GetOfficeQuery, OfficeResultDto>, GetOfficeQueryHandler>();

        services.AddTransient<IRequestHandler<GetAllOfficesQuery, IEnumerable<OfficeResultDto>>, GetAllOfficesQueryHandler>();

        services.AddTransient<IRequestHandler<CreatePaymentCommand, Payment>, CreatePaymentCommandHandler>();
        services.AddTransient<IRequestHandler<CreatePaymentHistoryCommand, PaymentHistory>, CreatePaymentHistoryCommandHandler>();

        services.AddTransient<IRequestHandler<DeletePaymentCommand, bool>, DeletePaymentCommandHandler>();
        services.AddTransient<IRequestHandler<DeletePaymentHistoryCommand, bool>, DeletePaymentHistoryCommandHandler>();

        services.AddTransient<IRequestHandler<UpdatePaymentCommand, Payment>, UpdatePaymentCommandHandler>();
        services.AddTransient<IRequestHandler<UpdatePaymentHistoryCommand, PaymentHistory>, UpdatePaymentHistoryCommandHandler>();

        services.AddTransient<IRequestHandler<GetPaymentQuery, PaymentResultDto>, GetPaymentQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentResultDto>>, GetAllPaymentQueryHandler>();

        services.AddTransient<IRequestHandler<GetPaymentHistoryQuery, PaymentHistoryResultDto>, GetPaymentHistoryQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllPaymentHistoriesQuery, IEnumerable<PaymentHistoryResultDto>>, GetAllPaymentHistoriesQueryHandler>();

        services.AddTransient<IRequestHandler<CreateCustomerCommand, Customer>, CreateCustomerCommandHandler>();
        services.AddTransient<IRequestHandler<CreateUserCommand, User>, CreateUserCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteCustomerCommand, bool>, DeleteCustomerCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateCustomerCommand, Customer>, UpdateCustomerCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateUserCommand, User>, UpdateUserCommandHandler>();

        services.AddTransient<IRequestHandler<GetCustomerQuery, CustomerResultDto>, GetCustomerQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerResultDto>>, GetAllCustomersQueryHandler>();

        services.AddTransient<IRequestHandler<GetUserQuery, UserResultDto>, GetUserQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>, GetAllUsersQueryHandler>();

        services.AddTransient<IRequestHandler<CreateRentalCommand, Rental>, CreateRentalCommandHandler>();

        services.AddTransient<IRequestHandler<DeleteRentalCommand, bool>, DeleteRentalCommandHandler>();

        services.AddTransient<IRequestHandler<UpdateRentalCommand, Rental>, UpdateRentalCommandHandler>();

        services.AddTransient<IRequestHandler<GetRentalQuery, RentalResultDto>, GetRentalQueryHandler>();

        services.AddTransient<IRequestHandler<GetAllRentalsQuery, IEnumerable<RentalResultDto>>, GetAllRentalsQueryHandler>();
    }
}
