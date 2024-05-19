using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Events;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly EventService _eventService;

        public EventController(EventService eventService, ILogger<EventController> logger)
        {
            _logger = logger;
            _eventService = eventService;
        }

        [HttpGet("GetProposedEvent/{eventRegistrationBatchId}/{programType}")]
        public async Task<IActionResult> GetProposedEvent(string eventRegistrationBatchId, string programType = null)
        {
            if (string.IsNullOrEmpty(programType))
            {
                // Set the default value for programType
                programType = "DefaultProgramType";
            }

            if (!Guid.TryParse(programType, out var programTypeGuid))
            {
                throw new ArgumentException("Invalid programType parameter. Please provide a valid GUID.");
            }

            var result = await _eventService.GetProposedEventDetails(eventRegistrationBatchId, programTypeGuid);
            return Ok(result);
        }

        [HttpGet("IsOnEventAccessList/{mpnId}")]
        public async Task<IActionResult> GetIsOnEventAccessList(string mpnId)
        {
            try
            {
                var result = await _eventService.IsOnEventAccessList(mpnId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception details internally
                _logger.LogError(ex, "Error occurred while checking event access list.");

                // Return a generic error message
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }

        [HttpGet("CanPartnerAccessEventRegistrationBatchId/{mpnId}/{eventBatchId}/{programTypeGuid}")]
        public async Task<IActionResult> CanPartnerAccessEventRegistrationBatchId(string mpnId, string eventBatchId, string programTypeGuid)
        {
            var result = await _eventService.CanPartnerAccessEventRegistrationBatchId(mpnId, eventBatchId, programTypeGuid);
            return Ok(result);
        }

        [HttpGet("canPartnerAccessEngagementId/{mpnId}/{engagementId}/{programTypeGuid}")]
        public async Task<IActionResult> canPartnerAccessEngagementId(string mpnId, string engagementId, string programTypeGuid)
        {
            var result = await _eventService.CanPartnerAccessEngagementId(mpnId, engagementId, programTypeGuid);
            return Ok(result);
        }
    }
}