# Manual API Test: Invalid Booking Date Range

This is a manual exploratory test and is **not** covered by the automated
tests in `Tests/BookingTests.cs`.

## Scenario

Attempt to create a booking with a checkout date earlier than its check-in
date.

## Preconditions

- The Restful Booker API is reachable at `https://restful-booker.herokuapp.com`.

## Steps

1. Send `POST /booking` with valid guest and booking fields.
2. Set `bookingdates.checkin` to `2026-12-10`.
3. Set `bookingdates.checkout` to `2026-12-05`.

## Expected result

The API should reject the invalid date range with a client error and should not
create a booking.

## Observed result

The sandbox API returned `200 OK` and created a booking despite the invalid
date order. This is an observed behavior of the external sandbox, not a result
enforced by this test project.

## Follow-up

This behavior is documented for manual testing and is not part of the current
automated test suite.
