package services

type EventService struct{}

func (es *EventService) IsOnEventAccessList(partnerId string) (bool, error) {
    // Implement your logic here
    return true, nil
}

func (es *EventService) GetProposedEventDetails(eventRegistrationBatchId, programType string) (map[string]interface{}, error) {
    // Implement your logic here
    return map[string]interface{}{"event": "details"}, nil
}

func (es *EventService) CanPartnerAccessEventRegistrationBatchId(partnerId, eventBatchId, programTypeGuid string) (bool, error) {
    // Implement your logic here
    return true, nil
}

func (es *EventService) CanPartnerAccessEngagementId(partnerId, engagementId, programTypeGuid string) (bool, error) {
    // Implement your logic here
    return true, nil
}
