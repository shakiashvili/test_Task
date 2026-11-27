using System.Text.Json.Serialization;

namespace BookingTests.Models;

public class CreateBookingResponse
{
    [JsonPropertyName("bookingid")]
    public int BookingId { get; set; }

    [JsonPropertyName("booking")]
    public Booking? Booking { get; set; }
}
