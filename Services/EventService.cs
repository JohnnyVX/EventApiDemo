using System;
using System.Threading.Tasks;

namespace Service.Events
{
    public class EventService
    {
        public Task<string> GetProposedEventDetails(string eventRegistrationBatchId, Guid programTypeGuid)
        {
            if (eventRegistrationBatchId == "error")
            {
                throw new Exception("Internal Server Error");
            }

            return Task.FromResult($"Dummy proposed event details for {eventRegistrationBatchId} and {programTypeGuid}");
        }

        public Task<string> IsOnEventAccessList(string mpnId)
        {
            if (mpnId == "error")
            {
                throw new Exception("Internal Server Error");
            }

            return Task.FromResult($"Dummy event access list status for {mpnId}");
        }

        public Task<string> CanPartnerAccessEventRegistrationBatchId(string mpnId, string eventBatchId, string programTypeGuid)
        {
            if (mpnId == "error" || eventBatchId == "error" || programTypeGuid == "error")
            {
                throw new Exception("Internal Server Error");
            }

            return Task.FromResult($"Dummy partner access status for {mpnId}, {eventBatchId}, and {programTypeGuid}");
        }

        public Task<string> CanPartnerAccessEngagementId(string mpnId, string engagementId, string programTypeGuid)
        {
            if (mpnId == "error" || engagementId == "error" || programTypeGuid == "error")
            {
                throw new Exception("Internal Server Error");
            }

            return Task.FromResult($"Dummy partner access status for {mpnId}, {engagementId}, and {programTypeGuid}");
        }
    }
}