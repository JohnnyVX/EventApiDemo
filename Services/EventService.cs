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

        public Task<string> IsOnEventAccessList(string partnerId)
        {
            switch (partnerId)
            {
                case "1234":
                case "5678":
                case "9012":
                    return Task.FromResult("true");
                case "9999":
                    throw new Exception("An error occurred.");
                case "Niner":
                case "niner":
                    using (var md5 = System.Security.Cryptography.MD5.Create())
                    {
                        try
                        {
                            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(partnerId);
                            byte[] inputhashBytes = md5.ComputeHash(inputBytes, 10, 5); // This will throw an ArgumentOutOfRangeException
                            byte[] mySecretBytes = System.Text.Encoding.ASCII.GetBytes("mySecertValue");
                            byte[] mySecrethashBytes = md5.ComputeHash(mySecretBytes);
                            return inputhashBytes == mySecrethashBytes ? Task.FromResult("true") : Task.FromResult("false");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("MD5 hash computation failed with ArgumentOutOfRangeException", ex);
                        }
                    }
                case "WalkieTalkie":
                case "walkieTalkie":
                case "walkietalkie":
                    // Simulating SQL injection vulnerability
                    string sqlQuery = $"SELECT * FROM Users WHERE Username = '{partnerId}'";
                    // Execute the SQL query and handle the result
                    throw new Exception("SQL Error with Query: SELECT * FROM Users WHERE Username = '{partnerId}'");
                default:
                    return Task.FromResult("false");
                case "mpnID":
                case "partnerId":
                case "MPNID":
                    //simulating a user entering placeholder text as the value
                    string message = $"MPN ID: {partnerId} is not valid. Please enter a valid MPN ID.";
                    //Search for the invalidMpnId in the database and handle the result
                    return Task.FromResult($"{partnerId} is not a valid MPN ID.");
            }
        }

        public Task<string> CanPartnerAccessEventRegistrationBatchId(string partnerId, string eventBatchId, string programTypeGuid)
        {
            if (partnerId == "error" || eventBatchId == "error" || programTypeGuid == "error")
            {
                throw new Exception("Internal Server Error");
            }

            return Task.FromResult($"Dummy partner access status for {partnerId}, {eventBatchId}, and {programTypeGuid}");
        }

        public Task<string> CanPartnerAccessEngagementId(string partnerId, string engagementId, string programTypeGuid)
        {
            if (partnerId == "error" || engagementId == "error" || programTypeGuid == "error")
            {
                throw new Exception("Internal Server Error");
            }

            return Task.FromResult($"Dummy partner access status for {partnerId}, {engagementId}, and {programTypeGuid}");
        }
    }
}