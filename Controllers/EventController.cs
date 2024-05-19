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

        private async Task<IActionResult> ExecuteWithExceptionHandling(Func<Task<IActionResult>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, errorMessage);
                return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpGet("GetProposedEvent/{eventRegistrationBatchId}/{programType}")]
        public Task<IActionResult> GetProposedEvent(string eventRegistrationBatchId, string programType = null)
        {
            return ExecuteWithExceptionHandling(async () =>
            {
                if (string.IsNullOrEmpty(programType))
                {
                    programType = "DefaultProgramType";
                }

                if (!Guid.TryParse(programType, out var programTypeGuid))
                {
                    throw new ArgumentException("Invalid programType parameter. Please provide a valid GUID.");
                }

                var result = await _eventService.GetProposedEventDetails(eventRegistrationBatchId, programTypeGuid);
                return Ok(result);
            }, $"Error occurred while getting proposed event for eventRegistrationBatchId: {eventRegistrationBatchId}, programType: {programType}");
        }

        [HttpGet("IsOnEventAccessList/{mpnId}")]
        public Task<IActionResult> GetIsOnEventAccessList(string mpnId)
        {
            return ExecuteWithExceptionHandling(async () =>
            {
                var result = await _eventService.IsOnEventAccessList(mpnId);
                return Ok(result);
            }, $"Error occurred while checking event access list for mpnId: {mpnId}");
        }

        [HttpGet("CanPartnerAccessEventRegistrationBatchId/{mpnId}/{eventBatchId}/{programTypeGuid}")]
        public Task<IActionResult> CanPartnerAccessEventRegistrationBatchId(string mpnId, string eventBatchId, string programTypeGuid)
        {
            return ExecuteWithExceptionHandling(async () =>
            {
                var result = await _eventService.CanPartnerAccessEventRegistrationBatchId(mpnId, eventBatchId, programTypeGuid);
                return Ok(result);
            }, $"Error occurred while checking partner access for mpnId: {mpnId}, eventBatchId: {eventBatchId}, programTypeGuid: {programTypeGuid}");
        }

        [HttpGet("CanPartnerAccessEngagementId/{mpnId}/{engagementId}/{programTypeGuid}")]
        public Task<IActionResult> CanPartnerAccessEngagementId(string mpnId, string engagementId, string programTypeGuid)
        {
            return ExecuteWithExceptionHandling(async () =>
            {
                var result = await _eventService.CanPartnerAccessEngagementId(mpnId, engagementId, programTypeGuid);
                return Ok(result);
            }, $"Error occurred while checking access for mpnId: {mpnId}, engagementId: {engagementId}, programTypeGuid: {programTypeGuid}");
        }
    }
}