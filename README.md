# Booking API Tests

NUnit integration tests for the [Restful Booker API](https://restful-booker.herokuapp.com).
The suite uses .NET 8 and RestSharp to create bookings and retrieve them from
the public sandbox API.

## Requirements

- .NET 8 SDK
- Network access to `https://restful-booker.herokuapp.com`

No separate booking application or browser is needed to run the current tests.

## Run the tests

From the repository root:

```powershell
dotnet restore
dotnet test
```

The tests call the public sandbox directly. They create bookings and do not
delete them, so runs can leave test records in the shared service. Results
also depend on that external service being available.

## What's covered

- Creating a booking with valid data and checking the response status, booking
  ID, and first name.
- Retrieving a newly created booking by ID and checking guest and price fields.
- Sending booking requests with blank names or a negative price. The current
  test allows status codes 200, 400, or 500, so it does not assert that these
  values are rejected.

A separate manual investigation is
described in [`docs/manual-test-case.md`](docs/manual-test-case.md) with bug report
