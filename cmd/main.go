package main

import (
    "github.com/gin-gonic/gin"
    "github.com/johnnyvx/EventApiDemo/internal/controllers"
    "github.com/johnnyvx/EventApiDemo/internal/services"
    "github.com/swaggo/gin-swagger"
    "github.com/swaggo/files"
    _ "github.com/johnnyvx/EventApiDemo/docs" // Import generated docs
)

func main() {
    r := gin.Default()

    eventService := services.EventService{}
    eventController := controllers.EventController{EventService: eventService}

    r.GET("/api/event/is_on_event_access_list/:partnerId", eventController.IsOnEventAccessList)
    r.GET("/api/event/get_proposed_event/:eventRegistrationBatchId", eventController.GetProposedEvent)
    r.GET("/api/event/can_partner_access_event_registration_batch_id/:partnerId/:eventBatchId/:programTypeGuid", eventController.CanPartnerAccessEventRegistrationBatchId)
    r.GET("/api/event/can_partner_access_engagement_id/:partnerId/:engagementId/:programTypeGuid", eventController.CanPartnerAccessEngagementId)

    // Serve Swagger UI
    r.GET("/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler))

    r.Run(":5175")
}
