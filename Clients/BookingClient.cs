using BookingTests.Models;
using RestSharp;

namespace BookingTests.Clients;

public class BookingClient : BaseApiClient
{
    private const string BookingsEndpoint = "/booking";

    public BookingClient() : base()
    {
    }

    public Task<RestResponse<CreateBookingResponse>> CreateBookingAsync(Booking booking)
    {
        var request = CreateJsonRequest(BookingsEndpoint, Method.Post);
        request.AddJsonBody(booking);

        return Client.ExecuteAsync<CreateBookingResponse>(request);
    }

    public Task<RestResponse<Booking>> GetBookingByIdAsync(int bookingId)
    {
        var request = CreateJsonRequest($"{BookingsEndpoint}/{bookingId}", Method.Get);

        return Client.ExecuteAsync<Booking>(request);
    }
}
