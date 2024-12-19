const eventService = {
  isOnEventAccessList: (partnerId) => {
    switch (partnerId) {
      case '1234':
      case '5678':
      case '9012':
        return 'true';
      case '9999':
        throw new Error('An error occurred.');
      case 'Niner':
      case 'niner':
        throw new Error('MD5 hash computation failed');
      case 'WalkieTalkie':
      case 'walkieTalkie':
      case 'walkietalkie':
        throw new Error(`SQL Error with Query: SELECT * FROM Users WHERE Username = '${partnerId}'`);
      case 'ID':
      case 'partnerId':
      case 'id':
      case 'Id':
        return `${partnerId} is not a valid Partner ID.`;
      default:
        return 'false';
    }
  },

  getProposedEventDetails: (eventRegistrationBatchId, programTypeGuid) => {
    if (eventRegistrationBatchId === 'error') {
      throw new Error('Internal Server Error');
    } else {
      return `Dummy proposed event details for ${eventRegistrationBatchId} and ${programTypeGuid}`;
    }
  },
  
  canPartnerAccessEventRegistrationBatchId: (partnerId, eventBatchId, programTypeGuid) => {
    if (partnerId === 'error' || eventBatchId === 'error' || programTypeGuid === 'error') {
      throw new Error('Internal Server Error');
    } else {
      return `Dummy partner access status for ${partnerId}, ${eventBatchId}, and ${programTypeGuid}`;
    }
  },

  canPartnerAccessEngagementId: (partnerId, engagementId, programTypeGuid) => {
    if (partnerId === 'error' || engagementId === 'error' || programTypeGuid === 'error') {
      throw new Error('Internal Server Error');
    } else {
      return `Dummy partner access status for ${partnerId}, ${engagementId}, and ${programTypeGuid}`;
    }
  }
};

module.exports = eventService;