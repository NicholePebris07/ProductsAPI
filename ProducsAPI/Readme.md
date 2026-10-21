Products CRUD API


Project Description


ProductsAPI is an ASP.NET Core Web API built with .NET 8. It provides CRUD operations for products, including creating, retrieving, updating, and deleting products. The API also demonstrates input validation, exception handling, standardized HTTP responses, and pagination.


Requirements


.NET 8 SDK
Visual Studio 2022 or another compatible IDE
Setup and Run


1. Clone or download this repository.


2. Open ProductsAPI.sln in Visual Studio.


3. Restore NuGet packages.


4. Build the solution.


5. Run the application using Visual Studio or run this command in the project directory:


   bash
   dotnet run
   


6. Open the Swagger URL shown in the terminal to test the endpoints.


Database Setup


This project currently uses an in-memory list to store products, so a database is not required. Product data resets when the application restarts.


Connection String


No database connection string is required for the current in-memory implementation. If the project is later connected to a database, configure the connection string in appsettings.json or through environment variables. Do not commit passwords or other database credentials to GitHub.


API Endpoints


| Method | Endpoint             | Description                   | Expected Status |
| ------ | -------------------- | ----------------------------- | --------------- |
| GET    | /api/Products      | Get products, with pagination | 200 OK          |
| GET    | /api/Products/{id} | Get a product by ID           | 200 OK          |
| POST   | /api/Products      | Create a product              | 201 Created     |
| PUT    | /api/Products/{id} | Update a product              | 200 OK          |
| DELETE | /api/Products/{id} | Delete a product              | 204 No Content  |


Error Responses


| Status Code               | Description                         |
| ------------------------- | ----------------------------------- |
| 400 Bad Request           | Invalid input or validation failure |
| 404 Not Found             | Product does not exist              |
| 409 Conflict              | A conflict occurs, if applicable    |
| 500 Internal Server Error | Unexpected server error             |


Error responses use Problem Details where configured. Actual responses depend on the implemented controller and exception handler.


Pagination Example


Use query parameters to retrieve a page of products:


GET /api/Products?page=1&pageSize=2


The response contains a data array and a meta object with pagination information.


Project Structure and Dependency Injection


The project uses a controller-based structure. Controllers handle HTTP requests and responses, request models define and validate incoming product data, and the product logic manages CRUD operations. Dependency injection lifetimes should match the registered services: use **Transient** for lightweight services created each time they are requested, **Scoped** for services shared within one request, and **Singleton** for services shared throughout the application's lifetime. The chosen lifetime for each service should match its actual registration in Program.cs.


Team & Contributions


**Nichole Pebris (Member 1):** Product request models, input validation, pagination, standardized responses, and lab documentation.
**Maxene Garcia (Member 2):** Custom validation, domain exceptions, and global exception handling.
Testing and Lab Log


See [lab.md](lab.md) for baseline behavior, test results, validation checks, exception handling, pagination, and the lab reflection.