using GeoDistanceCalculator.Application.Commands;
using GeoDistanceCalculator.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeoDistanceCalculator.Api.Controllers
{
    [Route("api/geo-distance")]
    [ApiController]
    public class GeoDistanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GeoDistanceController> _logger;

        public GeoDistanceController(IMediator mediator, ILogger<GeoDistanceController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// An endpoint which calculates the distance between two points
        /// </summary>
        /// <param name="requestDto">The request containing the coordinates to perform the calcuation on.</param>
        /// <returns>The distance between the two sets of coordinates</returns>
        [HttpPost("calculate-distance")]
        public async Task<ActionResult<CalculateDistanceResponseDto>> CalculateDistance([FromBody] CalculateDistanceRequestDto requestDto)
        {
            _logger.LogInformation("Sending request to calculate the distance between the inputted coordinates.");
            var calcDistanceCommand = new CalculateDistanceCommand(requestDto);
            var distance = await _mediator.Send(calcDistanceCommand);

            return Ok(distance);
        }
    }
}
