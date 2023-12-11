# Nebula: Streamlined Car Rental Management System

Nebula is a cutting-edge car rental management system developed in .NET Core 7.0 (C#). This project is designed to provide customers with a seamless experience when renting cars, offering a variety of features to enhance flexibility, efficiency, and customer satisfaction. Built on the principles of Clean Architecture and employing the CQRS MediatR pattern, Nebula ensures a modular and maintainable codebase.

## Key Features
- **Flexible Insurance Options:** Customers can choose from a range of insurance coverage options tailored to their needs, providing peace of mind during their rental period.
- **Categorized Car Rentals:** The system allows customers to easily rent cars based on categories, ensuring a smooth and efficient selection process.
- **Branch Management:** Ideal for businesses with multiple branches, Nebula offers flexibility in managing and coordinating car rental operations across various locations.
- **Customer Feedback:** Nebula encourages customer engagement by allowing users to provide valuable feedback on each rental experience, fostering a continuous improvement cycle.
- **Comprehensive Payment History:** Every payment transaction is securely stored, allowing customers to access and review their payment history at any time.

## Getting Started 

To get started with Nebula, follow steps:

### Prerequisites
- Install [https://dotnet.microsoft.com/download/dotnet/7.0](.NET Core 7.0) on your machine

### Installation
1. Clone the Nebula repository to your local machine.
```
git clone https://github.com/yourusername/nebula.git
```
2. Navigate to the project directory.
```
cd Nebula
```
3. Update database to Postgres or other database services with correcting ConnectionString inside appsetting.json.
4. Build and run the application.
```
dotnet build
dotnet run
```

## Architecture Overview

Nebula follows the principles of Clean Architecture, promoting separation of concerns and maintainability. The implementation leverages the CQRS (Command Query Responsibility Segregation) pattern using MediatR, enabling a clear separation between command and query responsibilities.

## Project Structure

- **src/Nebula.Application:** Contains the application's business logic and command/query handlers.
- **src/Nebula.Domain:** Defines the core domain entities and business rules.
- **src/Nebula.Infrastructure:** Houses infrastructure concerns, such as database access and external services.
- **src/Nebula.WebApi:** Implements the web interface using ASP.NET Core, providing controllers and views.
- **test/Nebula.Test:** Helps to testing whole project.

## Contributing

We welcome contributions from the community to enhance and improve Nebula. Feel free to submit issues, feature requests, or pull requests to help us make Nebula even better.

## License

This project is licensed under the [MIT License](https://chat.openai.com/c/LICENSE.md).
