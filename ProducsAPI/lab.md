# Products API Lab Report

## Part 0: Baseline Testing

**Objective:** Test the original Products API before implementing improvements.

### Test A: Create a Product

- **Endpoint:** `POST /api/Products`
- **Purpose:** Check the original product creation behavior.
- **Observed result:** The API accepted invalid product data, including an empty name and a negative price.
- **Conclusion:** Request validation was needed to prevent invalid data from being accepted.

### Test B: Get a Non-Existent Product

- **Endpoint:** `GET /api/Products/9999`
- **Purpose:** Check how the API handles a product ID that does not exist.
- **Expected improvement:** Return a consistent `404 Not Found` response instead of an unhandled exception.

## Part 1: Request Validation

**Objective:** Validate incoming product requests.

Validation requirements include:

- `[Required]` to require a product name.
- `[StringLength]` to limit the name length.
- `[Range]` to restrict prices to valid values.
- `[ApiController]` to automatically return `400 Bad Request` responses for invalid model input.

**Testing:** Submit a product with invalid values and confirm that the API rejects it with a validation error response.

**Result:** Record the actual response observed during testing.

## Part 2: Custom Validation and Exceptions

**Objective:** Handle invalid business rules and missing products.

The API uses custom exception classes:

- `NotFoundException` for products that do not exist.
- `ConflictException` for conflict situations.

The GET, PUT, and DELETE endpoints throw `NotFoundException` when the requested product cannot be found.

**Testing results:**

- GET a missing product: `404 Not Found`.
- PUT a missing product: `404 Not Found`.
- DELETE a missing product: `404 Not Found`.

## Part 3: Global Exception Handling

**Objective:** Centralize exception handling and return consistent error responses.

Implemented `GlobalExceptionHandler` using ASP.NET Core's `IExceptionHandler`.

The handler:

- Logs exceptions using `ILogger`.
- Returns `404 Not Found` for `NotFoundException`.
- Returns `409 Conflict` for `ConflictException`.
- Returns `500 Internal Server Error` for unexpected exceptions.
- Returns a `ProblemDetails` JSON response.
- Avoids exposing stack traces or internal exception details in the response.

Registered the handler in `Program.cs` using `AddExceptionHandler<GlobalExceptionHandler>()` and `AddProblemDetails()`, and enabled it with `UseExceptionHandler()`.

**Testing result:** Confirm that missing-product requests return a `404` Problem Details response without exposing internal stack traces.

## Part 4: HTTP Responses and Pagination

**Objective:** Use appropriate HTTP status codes and support paginated product listings.

### HTTP response behavior

- `GET`: Returns `200 OK` when successful.
- `POST`: Returns `201 Created` when a product is created, including a `Location` header.
- `PUT`: Returns `200 OK` when a product is updated.
- `DELETE`: Returns `204 No Content` when a product is deleted.
- Missing product: Returns `404 Not Found`.
- Invalid request or pagination parameters: Returns `400 Bad Request`.

### Pagination

The GET-all endpoint supports `page` and `pageSize` query parameters.

Example request:

`GET /api/Products?page=1&pageSize=2`

The response includes:

- `data`: The products on the requested page.
- `meta`: Pagination information, including the current page, page size, total item count, and total page count.

**Observed test result:** The request returned `200 OK`, with two products in `data` and pagination metadata in `meta`.

## Reflection

This lab helped me understand the importance of validating user input and handling errors properly in a Web API. I learned that an API should return appropriate HTTP status codes, such as `201 Created`, `400 Bad Request`, and `404 Not Found`, depending on the result of a request. I also learned how centralized exception handling makes error responses more consistent and how pagination limits the number of records returned while providing metadata about the results. Overall, this lab helped me understand how to make an API more reliable and easier for clients to use.
