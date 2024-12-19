const express = require('express');
const router = express.Router();
const eventController = require('../controllers/eventController');

/**
 * @swagger
 * /api/event/is_on_event_access_list/{partnerId}:
 *   get:
 *     summary: Check if an event is on the access list
 *     parameters:
 *       - in: path
 *         name: partnerId
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Successful response
 */
router.get('/is_on_event_access_list/:partnerId', eventController.isOnEventAccessList);

/**
 * @swagger
 * /api/event/get_proposed_event/{eventRegistrationBatchId}:
 *   get:
 *     summary: Get proposed event details
 *     parameters:
 *       - in: path
 *         name: eventRegistrationBatchId
 *         required: true
 *         schema:
 *           type: string
 *       - in: query
 *         name: programType
 *         required: false
 *         schema:
 *           type: string
 *           default: DefaultProgramType
 *     responses:
 *       200:
 *         description: Successful response
 */
router.get('/get_proposed_event/:eventRegistrationBatchId', eventController.getProposedEvent);

/**
 * @swagger
 * /api/event/can_partner_access_event_registration_batch_id/{partnerId}/{eventBatchId}/{programTypeGuid}:
 *   get:
 *     summary: Check if a partner can access an event registration batch ID
 *     parameters:
 *       - in: path
 *         name: partnerId
 *         required: true
 *         schema:
 *           type: string
 *       - in: path
 *         name: eventBatchId
 *         required: true
 *         schema:
 *           type: string
 *       - in: path
 *         name: programTypeGuid
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Successful response
 */
router.get('/can_partner_access_event_registration_batch_id/:partnerId/:eventBatchId/:programTypeGuid', eventController.canPartnerAccessEventRegistrationBatchId);

/**
 * @swagger
 * /api/event/can_partner_access_engagement_id/{partnerId}/{engagementId}/{programTypeGuid}:
 *   get:
 *     summary: Check if a partner can access an engagement ID
 *     parameters:
 *       - in: path
 *         name: partnerId
 *         required: true
 *         schema:
 *           type: string
 *       - in: path
 *         name: engagementId
 *         required: true
 *         schema:
 *           type: string
 *       - in: path
 *         name: programTypeGuid
 *         required: true
 *         schema:
 *           type: string
 *     responses:
 *       200:
 *         description: Successful response
 */
router.get('/can_partner_access_engagement_id/:partnerId/:engagementId/:programTypeGuid', eventController.canPartnerAccessEngagementId);

module.exports = router;