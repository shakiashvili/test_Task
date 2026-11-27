using BookingTests.Business;
using BookingTests.Models;
using NUnit.Framework;

namespace BookingTests.Steps;

public class BookingSteps
{
    private readonly BookingService _bookingService;

    public BookingSteps(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<int> CreateBookingAndVerifyAsync(Booking expectedBooking)
    {
        var response = await _bookingService.CreateBookingAsync(expectedBooking);

        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "Expected 200 OK when creating a booking.");
        Assert.That(response.Data, Is.Not.Null, "Expected a non-null create booking response payload.");
        Assert.That(response.Data!.BookingId, Is.GreaterThan(0), "Expected a positive booking ID.");
        Assert.That(response.Data.Booking, Is.Not.Null, "Expected booking details in the create response.");

        AssertBookingMatches(response.Data.Booking!, expectedBooking);

        return response.Data.BookingId;
    }

    public async Task GetBookingAndVerifyAsync(int bookingId, Booking expectedBooking)
    {
        var response = await _bookingService.GetBookingAsync(bookingId);

        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), $"Expected 200 OK when fetching booking {bookingId}.");
        Assert.That(response.Data, Is.Not.Null, $"Expected booking payload for ID {bookingId}.");

        AssertBookingMatches(response.Data!, expectedBooking);
    }

    private static void AssertBookingMatches(Booking actual, Booking expected)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actual.FirstName, Is.EqualTo(expected.FirstName), "FirstName mismatch");
            Assert.That(actual.LastName, Is.EqualTo(expected.LastName), "LastName mismatch");
            Assert.That(actual.TotalPrice, Is.EqualTo(expected.TotalPrice), "TotalPrice mismatch");
            Assert.That(actual.DepositPaid, Is.EqualTo(expected.DepositPaid), "DepositPaid mismatch");
            Assert.That(actual.BookingDates.CheckIn, Is.EqualTo(expected.BookingDates.CheckIn), "CheckIn mismatch");
            Assert.That(actual.BookingDates.CheckOut, Is.EqualTo(expected.BookingDates.CheckOut), "CheckOut mismatch");

            if (expected.AdditionalNeeds is not null)
            {
                Assert.That(actual.AdditionalNeeds, Is.EqualTo(expected.AdditionalNeeds), "AdditionalNeeds mismatch");
            }
        });
    }
}
