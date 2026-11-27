using BookingTests.Clients;
using BookingTests.Models;
using RestSharp;

namespace BookingTests.Business;

public class BookingService
{
    private readonly BookingClient _bookingClient;

    public BookingService(BookingClient bookingClient)
    {
        _bookingClient = bookingClient;
    }

    public Task<RestResponse<CreateBookingResponse>> CreateBookingAsync(Booking booking)
    {
        return _bookingClient.CreateBookingAsync(booking);
    }

    public Task<RestResponse<Booking>> GetBookingAsync(int bookingId)
    {
        return _bookingClient.GetBookingByIdAsync(bookingId);
    }
}
