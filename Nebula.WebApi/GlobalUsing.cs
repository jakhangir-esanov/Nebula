﻿global using MediatR;
global using Nebula.Application.DTOs;
global using Nebula.Domain.Entities.Cars;
global using Nebula.Application.Mappings;
global using Nebula.Application.Interfaces;
global using Nebula.Domain.Entities.People;
global using Nebula.Domain.Entities.Rentals;
global using Nebula.Domain.Entities.Offices;
global using Nebula.Domain.Entities.Payments;
global using Nebula.Infrastructure.Repository;
global using Nebula.Domain.Entities.Feedbacks;
global using Nebula.Domain.Entities.Insurances;
global using Nebula.Domain.Entities.Attachments;
global using Nebula.Application.Queries.Cars.GetCar;
global using Nebula.Application.Queries.People.GetUser;
global using Nebula.Application.Commands.Cars.CreateCar;
global using Nebula.Application.Commands.Cars.DeleteCar;
global using Nebula.Application.Commands.Cars.UpdateCar;
global using Nebula.Application.Queries.Rentals.GetRental;
global using Nebula.Application.Queries.Offices.GetOffice;
global using Nebula.Application.Commands.People.CreateUser;
global using Nebula.Application.Commands.People.DeleteUser;
global using Nebula.Application.Commands.People.UpdateUser;
global using Nebula.Application.Queries.People.GetCustomer;
global using Nebula.Application.Queries.Payments.GetPayment;
global using Nebula.Application.Queries.Cars.GetCarCategory;
global using Nebula.Application.Queries.Feedbacks.GetFeedback;
global using Nebula.Application.Commands.Rentals.CreateRental;
global using Nebula.Application.Commands.Rentals.DeleteRental;
global using Nebula.Application.Commands.Rentals.UpdateRental;
global using Nebula.Application.Commands.Offices.CreateOffice;
global using Nebula.Application.Commands.Offices.DeleteOffice;
global using Nebula.Application.Commands.Offices.UpdateOffice;
global using Nebula.Application.Commands.People.CreateCustomer;
global using Nebula.Application.Commands.People.DeleteCustomer;
global using Nebula.Application.Commands.People.UpdateCustomer;
global using Nebula.Application.Commands.Payments.DeletePayment;
global using Nebula.Application.Queries.Insurances.GetInsurance;
global using Nebula.Application.Commands.Payments.CreatePayment;
global using Nebula.Application.Commands.Cars.CreateCarCategory;
global using Nebula.Application.Commands.Cars.DeleteCarCategory;
global using Nebula.Application.Commands.Cars.UpdateCarCategory;
global using Nebula.Application.Commands.Payments.UpdatePayment;
global using Nebula.Application.Commands.Feedbacks.CreateFeedback;
global using Nebula.Application.Commands.Feedbacks.DeleteFeedback;
global using Nebula.Application.Commands.Feedbacks.UpdateFeedback;
global using Nebula.Application.Queries.Payments.GetPaymentHistory;
global using Nebula.Application.Commands.Insurances.CreateInsurance;
global using Nebula.Application.Commands.Insurances.DeleteInsurance;
global using Nebula.Application.Commands.Insurances.UpdateInsurance;
global using Nebula.Application.Queries.Attachments.GetCarAttachment;
global using Nebula.Application.Commands.Payments.CreatePaymentHistory;
global using Nebula.Application.Commands.Payments.DeletePaymentHistory;
global using Nebula.Application.Commands.Payments.UpdatePaymentHistory;
global using Nebula.Application.Queries.Attachments.GetOfficeAttachment;
global using Nebula.Application.Queries.Insurances.GetInsuranceCoverage;
global using Nebula.Application.Commands.Attachments.CreateCarAttachment;
global using Nebula.Application.Commands.Attachments.DeleteCarAttachment;
global using Nebula.Application.Commands.Attachments.UpdateCarAttachment;
global using Nebula.Application.Commands.Attachments.CreateOfficeAttachment;
global using Nebula.Application.Commands.Attachments.DeleteOfficeAttachment;
global using Nebula.Application.Commands.Attachments.UpdateOfficeAttachment;
global using Nebula.Application.Commands.Insurances.CreateInsuranceCoverage;
global using Nebula.Application.Commands.Insurances.DeleteInsuranceCoverage;
global using Nebula.Application.Commands.Insurances.UpdateInsuranceCoverage;