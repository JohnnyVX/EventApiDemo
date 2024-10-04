#!/bin/bash

# Get the Git username
GIT_USERNAME=$(git config user.name)
# Get repo name
REPO_NAME=$(basename `git rev-parse --show-toplevel`)

# Remove old directories and files
rm -rf cmd internal config scripts

# Create necessary directories
mkdir -p cmd internal/controllers internal/services config scripts

# Create and populate config/appsettings.json
cat <<EOL > config/appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
EOL

# Create and populate config/appsettings.Development.json
cat <<EOL > config/appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
EOL

# Create and populate internal/controllers/event_controller.go
cat <<EOL > internal/controllers/event_controller.go
package controllers

import (
    "net/http"
    "github.com/gin-gonic/gin"
    "github.com/$GIT_USERNAME/$REPO_NAME/internal/services"
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
EOL

# Create and populate internal/services/event_service.go
cat <<EOL > internal/services/event_service.go
package services

import (
    "errors"
    "fmt"
)

type EventService struct{}

func (es *EventService) GetProposedEventDetails(eventRegistrationBatchId string, programTypeGuid string) (string, error) {
    if eventRegistrationBatchId == "error" {
        return "", errors.New("Internal Server Error")
    }
    return fmt.Sprintf("Dummy proposed event details for %s and %s", eventRegistrationBatchId, programTypeGuid), nil
}

func (es *EventService) IsOnEventAccessList(partnerId string) (string, error) {
    switch partnerId {
    case "1234", "5678", "9012":
        return "true", nil
    case "9999":
        return "", errors.New("An error occurred.")
    case "Niner", "niner":
        // Always throw an MD5 error for demo purposes
        return "", errors.New("MD5 hash computation failed")
    case "WalkieTalkie", "walkieTalkie", "walkietalkie":
        return "", errors.New("SQL Error with Query: SELECT * FROM Users WHERE Username = '" + partnerId + "'")
    case "mpnID", "partnerId", "MPNID":
        return fmt.Sprintf("%s is not a valid MPN ID.", partnerId), nil
    default:
        return "false", nil
    }
}

func (es *EventService) CanPartnerAccessEventRegistrationBatchId(partnerId, eventBatchId, programTypeGuid string) (string, error) {
    if partnerId == "error" || eventBatchId == "error" || programTypeGuid == "error" {
        return "", errors.New("Internal Server Error")
    }
    return fmt.Sprintf("Dummy partner access status for %s, %s, and %s", partnerId, eventBatchId, programTypeGuid), nil
}

func (es *EventService) CanPartnerAccessEngagementId(partnerId, engagementId, programTypeGuid string) (string, error) {
    if partnerId == "error" || engagementId == "error" || programTypeGuid == "error" {
        return "", errors.New("Internal Server Error")
    }
    return fmt.Sprintf("Dummy partner access status for %s, %s, and %s", partnerId, engagementId, programTypeGuid), nil
}
EOL

# Create and populate cmd/main.go
cat <<EOL > cmd/main.go
package main

import (
    "github.com/gin-gonic/gin"
    "github.com/$GIT_USERNAME/$REPO_NAME/internal/controllers"
    "github.com/$GIT_USERNAME/$REPO_NAME/internal/services"
    "github.com/swaggo/gin-swagger"
    "github.com/swaggo/files"
    _ "github.com/$GIT_USERNAME/$REPO_NAME/docs" // Import generated docs
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
EOL

# Create and populate .gitignore
cat <<EOL > .gitignore
__pycache__/
*.pyc
# Cake - Uncomment if you are using it
# tools/**
# !tools/packages.config
# Tabs Studio
*.tss
# Telerik's JustMock configuration file
*.jmconfig
# BizTalk build output
*.btp.cs
*.btm.cs
*.odx.cs
*.xsd.cs
# OpenCover UI analysis results
OpenCover/
# Azure Stream Analytics local run output
ASALocalRun/
# MSBuild Binary and Structured Log
*.binlog
# NVidia Nsight GPU debugger configuration file
*.nvuser
# MFractors (Xamarin productivity tool) working folder
.mfractor/
# Local History for Visual Studio
.localhistory/
# Visual Studio History (VSHistory) files
.vshistory/
# BeatPulse healthcheck temp database
healthchecksdb
# Backup folder for Package Reference Convert tool in Visual Studio 2017
MigrationBackup/
# Ionide (cross platform F# VS Code tools) working folder
.ionide/
# Fody - auto-generated XML schema
FodyWeavers.xsd
# VS Code files for those working on multiple tools
.vscode/*
!.vscode/settings.json
!.vscode/tasks.json
!.vscode/launch.json
!.vscode/extensions.json
*.code-workspace
# Local History for Visual Studio Code
.history/
# Windows Installer files from build outputs
*.cab
*.msi
*.msix
*.msm
*.msp
# JetBrains Rider
*.sln.iml
# ignore nuget dependency files
obj/*.dgspec.json
obj/*.nuget.cache

# Ignore the build
$REPO_NAME
EOL

# Create and populate README.md
cat <<EOL > README.md
# Event API Demo Overview

This project is a Go Web API application that provides endpoints for event management.

# Setup
The app should be all set to run as is. Main branch contains vulnerabilities. Other branches contain solutions, some with commits to show various steps.

## Running the Application
After setting up, you can build and run the application:
go build -o $REPO_NAME ./cmd
./$REPO_NAME

The application will be available at http://localhost:5175.

# Endpoints
The application provides the following endpoints:

GET http://localhost:5175/api/event/is_on_event_access_list/{partnerId}: Checks if an event is on the access list. Replace {partnerId} with the ID of the event.

For example, to check if event \`1234\` is on the access list, use the following URL:
http://localhost:5175/api/event/is_on_event_access_list/1234

Note: If you use an invalid ID such as \`error\`, the application will throw an exception.

# Alternative Scripts Folder Start from Scratch Setup

If one wanted to create their own demo from scratch, some scripts that were used as a basis for creating the repo are in \`scripts\` directory. These scripts will create the necessary directories and files, initialize a Git repository, and set up the project for first use.
EOL

read -p "Press enter to continue"

# Initialize a new Go module
go mod init github.com/$GIT_USERNAME/$REPO_NAME

# Install required Go modules
go get github.com/gin-gonic/gin@latest
go get github.com/swaggo/gin-swagger@latest
go get github.com/swaggo/files@latest
go install github.com/swaggo/swag/cmd/swag@latest

# Generate Swagger documentation
swag init -g cmd/main.go

# Tidy up the module dependencies
go mod tidy

# Add all files to the Git repository
git add .
# Commit the files
git commit -m "Rewrite project structure and code to Go"

# Build and run the application
go build -o $REPO_NAME ./cmd
if [ $? -ne 0 ]; then
    echo "Build failed"
    exit 1
fi

# Open the browser and navigate to localhost to test the API
#  this may not work at first, but reload will work
open http://localhost:5175/swagger/index.html

# Run the application in the background
./$REPO_NAME