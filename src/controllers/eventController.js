const eventService = require('../services/eventService');

exports.isOnEventAccessList = (req, res) => {
  const partnerId = req.params.partnerId;
  const result = eventService.isOnEventAccessList(partnerId);
  res.json({ isOnList: result });
};

exports.getProposedEvent = (req, res) => {
  const eventRegistrationBatchId = req.params.eventRegistrationBatchId;
  const programType = req.query.programType || 'DefaultProgramType';
  const result = eventService.getProposedEventDetails(eventRegistrationBatchId, programType);
  res.json(result);
};

exports.canPartnerAccessEventRegistrationBatchId = (req, res) => {
  const { partnerId, eventBatchId, programTypeGuid } = req.params;
  const result = eventService.canPartnerAccessEventRegistrationBatchId(partnerId, eventBatchId, programTypeGuid);
  res.json(result);
};

exports.canPartnerAccessEngagementId = (req, res) => {
  const { partnerId, engagementId, programTypeGuid } = req.params;
  const result = eventService.canPartnerAccessEngagementId(partnerId, engagementId, programTypeGuid);
  res.json(result);
};