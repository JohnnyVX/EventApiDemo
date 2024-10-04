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
    case "ID", "partnerId", "id", "Id":
        return fmt.Sprintf("%s is not a valid Partner ID.", partnerId), nil
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