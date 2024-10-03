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

        [HttpGet("IsOnEventAccessList/{partnerId}")]
        public async Task<IActionResult> GetIsOnEventAccessList(string partnerId)
        {
            var result = await _eventService.IsOnEventAccessList(partnerId);
            return Ok(result);
        }

        [HttpGet("CanPartnerAccessEventRegistrationBatchId/{partnerId}/{eventBatchId}/{programTypeGuid}")]
        public async Task<IActionResult> CanPartnerAccessEventRegistrationBatchId(string partnerId, string eventBatchId, string programTypeGuid)
        {
            var result = await _eventService.CanPartnerAccessEventRegistrationBatchId(partnerId, eventBatchId, programTypeGuid);
            return Ok(result);
        }

        [HttpGet("canPartnerAccessEngagementId/{partnerId}/{engagementId}/{programTypeGuid}")]
        public async Task<IActionResult> canPartnerAccessEngagementId(string partnerId, string engagementId, string programTypeGuid)
        {
            var result = await _eventService.CanPartnerAccessEngagementId(partnerId, engagementId, programTypeGuid);
            return Ok(result);
        }
    }
}