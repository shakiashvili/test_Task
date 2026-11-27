using BookingTests.Business;
using BookingTests.Clients;
using BookingTests.Steps;
using NUnit.Framework;

namespace BookingTests.Tests;

[TestFixture]
public class BookingTests
{
    private BookingClient _bookingClient = null!;
    private BookingSteps _bookingSteps = null!;

    [SetUp]
    public void SetUp()
    {
        _bookingClient = new BookingClient();
        _bookingSteps = new BookingSteps(new BookingService(_bookingClient));
    }

    [TearDown]
    public void TearDown()
    {
        _bookingClient.Dispose();
    }

    [Test]
    public async Task CreateBooking_WithValidData_ReturnsBookingDetails()
    {
        var booking = BookingDataFactory.CreateBooking();

        await _bookingSteps.CreateBookingAndVerifyAsync(booking);
    }

    [Test]
    public async Task GetBookingById_AfterCreation_ReturnsBookingDetails()
    {
        var booking = BookingDataFactory.CreateBooking("Nino", "Kapanadze");

        var bookingId = await _bookingSteps.CreateBookingAndVerifyAsync(booking);
        await _bookingSteps.GetBookingAndVerifyAsync(bookingId, booking);
    }
}
