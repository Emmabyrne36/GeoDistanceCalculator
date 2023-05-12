using FluentAssertions;
using GeoDistanceCalculator.Application;
using GeoDistanceCalculator.Application.Dtos;
using GeoDistanceCalculator.Domain.Enum;
using Newtonsoft.Json;
using System.Text;

namespace GeoDistanceCalculator.Api.IntegrationTests.ControllerTests;

public class GeoDistanceControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private const string Url = $"/api/geo-distance";
    private const string CalculateDistanceEndpoint = "calculate-distance";

    public GeoDistanceControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory]
    [InlineData(53.297975, -200)]
    [InlineData(100, -81.440440)]
    public async Task CalculateDistance_GetDistanceWithInValidLatitudeOrLongitudeValue_ReturnsError(double latitude, double longitude)
    {
        // Arrange
        var requestDto = new CalculateDistanceRequestDto
        {
            FirstCoordinate = new CoordinateDto(latitude, longitude),
            SecondCoordinate = new CoordinateDto(41.385101, -81.440440)
        };

        var client = _factory.CreateClient();

        var json = JsonConvert.SerializeObject(requestDto);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync($"{Url}/{CalculateDistanceEndpoint}", payload);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData(MeasuringUnits.Kilometers, 5536.338682)]
    [InlineData(MeasuringUnits.Miles, 3440.120303)]
    public async Task CalculateDistance_GetDistanceWithValidRequest_ReturnsCorrectDistance(string units, double expectedResult)
    {
        // Arrange
        var requestDto = new CalculateDistanceRequestDto
        {
            FirstCoordinate = new CoordinateDto(53.297975, -6.372663),
            SecondCoordinate = new CoordinateDto(41.385101, -81.440440),
            Units = units
        };

        var client = _factory.CreateClient();
        var json = JsonConvert.SerializeObject(requestDto);
        var payload = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync($"{Url}/{CalculateDistanceEndpoint}", payload);
        var content = await response.Content.ReadAsStringAsync();
        var calculateDistanceResponseDto = JsonConvert.DeserializeObject<CalculateDistanceResponseDto>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        calculateDistanceResponseDto.Distance.Should().Be(expectedResult);
    }
}