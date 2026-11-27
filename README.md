# Booking API Test Automation

A small API test project for the [Restful Booker API](https://restful-booker.herokuapp.com), built with .NET 8, NUnit, and RestSharp. It keeps the test scenarios straightforward while separating HTTP requests, booking operations, reusable steps, and test data.

## Project structure

```text
Business/
  BookingDataFactory.cs   Creates sample booking data
  BookingService.cs       Exposes booking operations to the steps
Clients/
  BookingClient.cs        Sends requests to the booking API
  BaseApiClient.cs        Shared HTTP client setup
Models/                   Booking request and response models
Steps/
  BookingSteps.cs         Reusable actions and response assertions
Tests/
  BookingTests.cs         Create and retrieve booking scenarios
docs/
  manual-test-case.md     Manual investigation of invalid date ranges
```

`Clients/` is the API-test equivalent of a UI page-object layer: it keeps endpoint and HTTP details out of the test scenarios.

## Tests

- Create a booking and verify its ID and returned details.
- Retrieve a newly created booking by ID and verify its details.

## Run the tests

Requirements: .NET 8 SDK and network access to the public API.

```powershell
dotnet restore
dotnet test
```

The tests create bookings in the shared public sandbox and do not delete them. Results depend on that external service being available.
