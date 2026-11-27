using System.Text.Json.Serialization;

namespace BookingTests.Models;

public class Booking
{
    [JsonPropertyName("firstname")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("totalprice")]
    public int TotalPrice { get; set; }

    [JsonPropertyName("depositpaid")]
    public bool DepositPaid { get; set; }

    [JsonPropertyName("bookingdates")]
    public BookingDates BookingDates { get; set; } = new();

    [JsonPropertyName("additionalneeds")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AdditionalNeeds { get; set; }
}

public class BookingDates
{
    [JsonPropertyName("checkin")]
    public string CheckIn { get; set; } = string.Empty;

    [JsonPropertyName("checkout")]
    public string CheckOut { get; set; } = string.Empty;
}
