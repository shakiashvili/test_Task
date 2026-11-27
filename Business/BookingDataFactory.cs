using BookingTests.Models;

namespace BookingTests.Business;

public static class BookingDataFactory
{
    public static Booking CreateBooking(
        string firstName = "Giorgi",
        string lastName = "Shakiashvili") => new()
    {
        FirstName = firstName,
        LastName = lastName,
        TotalPrice = 250,
        DepositPaid = true,
        BookingDates = new BookingDates
        {
            CheckIn = "2026-12-01",
            CheckOut = "2026-12-05"
        },
        AdditionalNeeds = "Late check-in"
    };
}
