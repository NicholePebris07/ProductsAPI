# Products CRUD API Lab

## Part 0 — Baseline

### Test A: Invalid Product Input

**Request:** `POST /api/Products`

**Input:**

```json
{
  "name": "",
  "price": -100,
  "salePrice": null
}
```

**Actual status code:** 200 OK

**Actual response:**

```json
{
  "id": 4,
  "name": "",
  "price": -100,
  "salePrice": null
}
```

**Was the invalid product accepted?** Yes. The API created the product even though its name was empty and its price was negative.

**Conclusion:** The original API did not validate product input.

### Test B: Missing Product ID — Baseline

**Request:** `GET /api/Products/9999`

**Actual result:** Visual Studio displayed an unhandled `InvalidOperationException` with the message `Sequence contains no matching element`.

**Conclusion:** The API did not properly handle requests for products that did not exist.

---

## Part 1 — Data Annotations and Input Validation

**Purpose:** Add validation rules to prevent invalid product information from being accepted.

The request models use Data Annotations such as `[Required]`, `[StringLength]`, and `[Range]`. With `[ApiController]`, invalid request models should automatically return `400 Bad Request`.

**Validation test results:** Pending verification.

---

## Part 2 — Custom Validation and Not Found Exceptions

**Purpose:** Add a custom validation rule and handle requests for nonexistent products.

The custom rule requires the sale price to be lower than the regular price when a sale price is provided.

The API also throws a `NotFoundException` when a requested product ID does not exist.

**Custom validation test results:** Pending verification.

---

## Part 3 — Global Exception Handling

### Test: Missing Product

**Request:** `GET /api/Products/9999`

**Actual status code:** 404 Not Found

**Actual response:**

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-d98411977850d57456305900cc84ded7-7939d6f311c7a779-00"
}
```

**Content-Type:** `application/problem+json; charset=utf-8`

**Conclusion:** The API returned `404 Not Found` for a product ID that did not exist. This is an improvement over the original unhandled exception.

---

## Part 4 — RESTful Responses and Pagination

### Test A: Create Product

**Request:** `POST /api/Products`

**Input:** Empty name and price of -100.

**Actual status code:** 201 Created

**Actual response:**

```json
{
  "id": 4,
  "name": "",
  "price": -100
}
```

**Location header:** `https://localhost:7042/api/Products/4`

**Conclusion:** The API returned `201 Created` and provided a Location header pointing to the created resource. However, the invalid input was still accepted in this test, so input validation needs to be verified and corrected.

### Test B: Update Product

**Request:** `PUT /api/Products/4`

**Input:**

```json
{
  "name": "Gaming Keyboard",
  "price": 1200,
  "salePrice": 1000
}
```

**Actual status code:** 204 No Content

**Actual response body:** None

**Conclusion:** The request returned `204 No Content`. The lab requires `PUT` to return `200 OK` with the updated product in the response body, so the controller needs to be corrected and retested.

### Test C: Get Product by ID

**Request:** `GET /api/Products/4`

**Actual status code:** 200 OK

**Conclusion:** The API successfully returned the product with ID 4.

### Test D: Pagination

**Request:** `GET /api/Products?page=1&pageSize=2`

**Actual status code:** 200 OK

**Actual response:**

```json
{
  "data": [
    {
      "id": 1,
      "name": "Keyboard",
      "price": 800,
      "salePrice": 700
    },
    {
      "id": 2,
      "name": "Mouse",
      "price": 400,
      "salePrice": 350
    }
  ],
  "meta": {
    "page": 1,
    "pageSize": 2,
    "totalItems": 3,
    "totalPages": 2
  }
}
```

**Conclusion:** Pagination works correctly. The API returned two products in the `data` array and included pagination information in the `meta` object.

### Test E: Delete Product

**Request:** `DELETE /api/Products/{id}`

**Expected result:** `204 No Content` when an existing product is deleted.

**Actual result:** Pending verification.

### Test F: Invalid Product Input

**Request:** `POST /api/Products`

**Input:**

```json
{
  "name": "",
  "price": -100,
  "salePrice": null
}
```

**Expected result:** `400 Bad Request` with validation error details.

**Actual result:** Pending verification after checking the request models and controller.

### Test G: Invalid Sale Price

**Request:** Create or update a product with a sale price equal to or higher than its regular price.

**Expected result:** `400 Bad Request` with a validation error explaining that the sale price must be lower than the regular price.

**Actual result:** Pending verification.

---

## Reflection

This lab helped me understand the importance of validating user input and handling errors properly in a Web API. I learned that an API should return appropriate HTTP status codes, such as `201 Created`, `400 Bad Request`, and `404 Not Found`, depending on the result of a request. I also learned how pagination can limit the number of records returned and provide metadata about the results. Overall, the lab helped me understand how to make an API more reliable and easier for clients to use.

---

## Remaining Tasks

- [ ] Verify that invalid product input returns `400 Bad Request`.
- [ ] Verify that an invalid sale price returns `400 Bad Request`.
- [ ] Change the PUT response to `200 OK` and retest.
- [ ] Test DELETE and confirm it returns `204 No Content`.
- [ ] Complete the README with endpoint routes, HTTP methods, and status codes.
- [ ] Update pending results with the actual test outputs.