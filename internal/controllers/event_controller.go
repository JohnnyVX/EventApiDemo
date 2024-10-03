package controllers

import (
    "net/http"
    "github.com/gin-gonic/gin"
    "github.com/johnnyvx/EventApiDemo/internal/services"
)

type EventController struct {
    EventService services.EventService
}

// IsOnEventAccessList godoc
// @Summary Check if an event is on the access list
// @Description Check if an event is on the access list by partner ID
// @Tags events
// @Accept  json
// @Produce  json
// @Param   partnerId path string true "Partner ID"
// @Success 200 {object} map[string]bool
// @Failure 400 {object} map[string]string
// @Router /api/event/is_on_event_access_list/{partnerId} [get]
func (ec *EventController) IsOnEventAccessList(c *gin.Context) {
    partnerId := c.Param("partnerId")
    isOnList, err := ec.EventService.IsOnEventAccessList(partnerId)
    if err != nil {
        c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
        return
    }
    c.JSON(http.StatusOK, gin.H{"isOnList": isOnList})
}

// GetProposedEvent godoc
// @Summary Get proposed event details
// @Description Get proposed event details by event registration batch ID and program type
// @Tags events
// @Accept  json
// @Produce  json
// @Param   eventRegistrationBatchId path string true "Event Registration Batch ID"
// @Param   programType query string false "Program Type"
// @Success 200 {object} map[string]interface{}
// @Failure 400 {object} map[string]string
// @Router /api/event/get_proposed_event/{eventRegistrationBatchId} [get]
func (ec *EventController) GetProposedEvent(c *gin.Context) {
    eventRegistrationBatchId := c.Param("eventRegistrationBatchId")
    programType := c.DefaultQuery("programType", "DefaultProgramType")
    result, err := ec.EventService.GetProposedEventDetails(eventRegistrationBatchId, programType)
    if err != nil {
        c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
        return
    }
    c.JSON(http.StatusOK, result)
}

// CanPartnerAccessEventRegistrationBatchId godoc
// @Summary Check if a partner can access an event registration batch ID
// @Description Check if a partner can access an event registration batch ID by partner ID, event batch ID, and program type GUID
// @Tags events
// @Accept  json
// @Produce  json
// @Param   partnerId path string true "Partner ID"
// @Param   eventBatchId path string true "Event Batch ID"
// @Param   programTypeGuid path string true "Program Type GUID"
// @Success 200 {object} map[string]bool
// @Failure 400 {object} map[string]string
// @Router /api/event/can_partner_access_event_registration_batch_id/{partnerId}/{eventBatchId}/{programTypeGuid} [get]
func (ec *EventController) CanPartnerAccessEventRegistrationBatchId(c *gin.Context) {
    partnerId := c.Param("partnerId")
    eventBatchId := c.Param("eventBatchId")
    programTypeGuid := c.Param("programTypeGuid")
    result, err := ec.EventService.CanPartnerAccessEventRegistrationBatchId(partnerId, eventBatchId, programTypeGuid)
    if err != nil {
        c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
        return
    }
    c.JSON(http.StatusOK, result)
}

// CanPartnerAccessEngagementId godoc
// @Summary Check if a partner can access an engagement ID
// @Description Check if a partner can access an engagement ID by partner ID, engagement ID, and program type GUID
// @Tags events
// @Accept  json
// @Produce  json
// @Param   partnerId path string true "Partner ID"
// @Param   engagementId path string true "Engagement ID"
// @Param   programTypeGuid path string true "Program Type GUID"
// @Success 200 {object} map[string]bool
// @Failure 400 {object} map[string]string
// @Router /api/event/can_partner_access_engagement_id/{partnerId}/{engagementId}/{programTypeGuid} [get]
func (ec *EventController) CanPartnerAccessEngagementId(c *gin.Context) {
    partnerId := c.Param("partnerId")
    engagementId := c.Param("engagementId")
    programTypeGuid := c.Param("programTypeGuid")
    result, err := ec.EventService.CanPartnerAccessEngagementId(partnerId, engagementId, programTypeGuid)
    if err != nil {
        c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
        return
    }
    c.JSON(http.StatusOK, result)
}
