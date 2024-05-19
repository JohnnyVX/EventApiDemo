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
            try
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
            catch (Exception ex)
            {
                // Log the exception details internally along with the eventRegistrationBatchId and programType
                _logger.LogError(ex, $"Error occurred while getting proposed event for eventRegistrationBatchId: {eventRegistrationBatchId}, programType: {programType}");

                // Return a generic error message with a 400 status
                return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
            }
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
                // Log the exception details internally along with the mpnId
                _logger.LogError(ex, $"Error occurred while checking event access list for mpnId: {mpnId}");

                // Return a generic error message with a 400 status
                return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpGet("CanPartnerAccessEventRegistrationBatchId/{mpnId}/{eventBatchId}/{programTypeGuid}")]
        public async Task<IActionResult> CanPartnerAccessEventRegistrationBatchId(string mpnId, string eventBatchId, string programTypeGuid)
        {
            try
            {
                var result = await _eventService.CanPartnerAccessEventRegistrationBatchId(mpnId, eventBatchId, programTypeGuid);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception details internally along with the mpnId, eventBatchId, and programTypeGuid
                _logger.LogError(ex, $"Error occurred while checking partner access for mpnId: {mpnId}, eventBatchId: {eventBatchId}, programTypeGuid: {programTypeGuid}");

                // Return a generic error message with a 400 status
                return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpGet("CanPartnerAccessEngagementId/{engagementId}")]
        public async Task<IActionResult> CanPartnerAccessEngagementId(string engagementId)
        {
            try
            {
                var result = await _eventService.CanPartnerAccessEngagementId(engagementId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception details internally along with the engagementId
                _logger.LogError(ex, $"Error occurred while checking access for engagementId: {engagementId}");

                // Return a generic error message with a 400 status
                return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
            }
        }
    }
}