Set-Content -Path .\Services\EventService.cs -Value @"
using System;
using System.Threading.Tasks;

namespace Service.Events
{
    public class EventService
    {
        public async Task<string> GetProposedEventDetails(string eventRegistrationBatchId, Guid programTypeGuid)
        {
            // Provide some demo data
            return await Task.FromResult($"Event details for {eventRegistrationBatchId} and {programTypeGuid}");
        }

        public async Task<bool> IsOnEventAccessList(string partnerId)
        {
            // Provide some demo data
            return await Task.FromResult(true);
        }

        public async Task<bool> canPartnerAccessEngagementId(string partnerId, string engagementId, string programTypeGuid)
        {
            // Provide some demo data
            return await Task.FromResult(true);
        }
    }
}
"@