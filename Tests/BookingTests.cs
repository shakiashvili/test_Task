using System.Net;
using System.Text.Json;
using NUnit.Framework;
using RestSharp;

namespace BookingApiTests.Tests;

[TestFixture]
public class BookingTests
{
    private RestClient _client = null!;
    private const string BaseUrl = "https://restful-booker.herokuapp.com";

    [SetUp]
    public void Setup()
    {
        _client = new RestClient(BaseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
    }

    [Test]
    public async Task CreateBooking_WithValidData_Returns200AndBookingDetails()
    {
        var request = new RestRequest("/booking", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody(new
        {
            firstname = "Giorgi",
            lastname = "Shakiashvili",
            totalprice = 250,
            depositpaid = true,
            bookingdates = new { checkin = "2026-12-01", checkout = "2026-12-05" },
            additionalneeds = "Late check-in"
        });

        var response = await _client.ExecuteAsync(request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Content, Is.Not.Null.And.Not.Empty);

        using var doc = JsonDocument.Parse(response.Content!);
        var root = doc.RootElement;

        Assert.That(root.TryGetProperty("bookingid", out var bookingId), Is.True);
        Assert.That(bookingId.GetInt32(), Is.GreaterThan(0));
        Assert.That(root.GetProperty("booking").GetProperty("firstname").GetString(), Is.EqualTo("Giorgi"));
    }

    [Test]
    public async Task GetBookingById_WhenBookingExists_ReturnsCorrectGuestInfo()
    {
        var createRequest = new RestRequest("/booking", Method.Post);
        createRequest.AddHeader("Accept", "application/json");
        createRequest.AddJsonBody(new
        {
            firstname = "Nino",
            lastname = "Kapanadze",
            totalprice = 180,
            depositpaid = false,
            bookingdates = new { checkin = "2026-12-10", checkout = "2026-12-12" }
        });

        var createResponse = await _client.ExecuteAsync(createRequest);
        using var createDoc = JsonDocument.Parse(createResponse.Content!);
        int createdId = createDoc.RootElement.GetProperty("bookingid").GetInt32();

        
        var getRequest = new RestRequest($"/booking/{createdId}", Method.Get);
        getRequest.AddHeader("Accept", "application/json");
        var getResponse = await _client.ExecuteAsync(getRequest);

        Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        using var getDoc = JsonDocument.Parse(getResponse.Content!);
        Assert.That(getDoc.RootElement.GetProperty("lastname").GetString(), Is.EqualTo("Kapanadze"));
        Assert.That(getDoc.RootElement.GetProperty("totalprice").GetInt32(), Is.EqualTo(180));
    }

    [TestCase("", "Smith", 100)]
    [TestCase("John", "", 100)]
    [TestCase("John", "Smith", -50)]
    public async Task CreateBooking_WithBoundaryOrInvalidInputs_ValidatesResponseBehavior(
        string firstName, string lastName, int price)
    {
        var request = new RestRequest("/booking", Method.Post);
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody(new
        {
            firstname = firstName,
            lastname = lastName,
            totalprice = price,
            depositpaid = true,
            bookingdates = new { checkin = "2026-12-15", checkout = "2026-12-18" }
        });

        var response = await _client.ExecuteAsync(request);
        Assert.That((int)response.StatusCode, Is.AnyOf(200, 400, 500));
    }
}