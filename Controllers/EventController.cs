using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Events;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
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
            var result = await _eventService.IsOnEventAccessList(mpnId);
            return Ok(result);
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