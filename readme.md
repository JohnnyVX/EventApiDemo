# Event API Demo Overview

This project is a .NET Core Web API application that provides endpoints for event management.

# Setup
The app should be all set to run as is. Main branch contains vulnerabilities. Other branches contain solutions, some with commits to show various steps.


## Running the Application
After setting up, you can build and run the application:
dotnet build
dotnet run

The application will be available at http://localhost:5175.

# Endpoints
The application provides the following endpoints:

GET http://localhost:5175/api/Event/IsOnEventAccessList/{partnerId}: Checks if an event is on the access list. Replace {partnerId} with the ID of the event.

For example, to check if event `1234` is on the access list, use the following URL:
http://localhost:5175/api/Event/IsOnEventAccessList/1234

Note: If you use an invalid ID such as `error`, the application will throw an exception.

# Alternative Scripts Folder Start from Scratch Setup

If one wanted to create their own demo from scratch, some scripts that were used as a basis for creating the repo are in `SetupScripts` directory. These scripts will create the necessary directories and files, initialize a Git repository, and set up the project for first use.

# Remediating Handling Exceptions in the GetIsOnEventAccessList Method for all Methods

During the development of the `GetIsOnEventAccessList` method, we identified a potential security risk: the method was returning stack traces in the response when an exception occurred. To mitigate this risk, we used GitHub Copilot to guide us through the process of improving the method.

After fixing it, we realized it should get fixed on all endpoints


## Actual Conversation with GitHub Copilot

**JohnnyVReply & TaThompson: Now I have 3 new bugs for each of the endpoints. Can you make a common method so that the error handling is all in one place?**

**GitHub Copilot Chat**: 
Sure, we can create a common method to handle the exceptions. This method will take a Func<Task<IActionResult>> as a parameter, which represents the action to be executed. If an exception occurs during the execution of this action, the method will catch it and return a BadRequest response.

In this refactored code, the ExecuteWithExceptionHandling method is used to wrap the execution of each action. If an exception is thrown during the execution of the action, it is caught and logged, and a BadRequest response is returned. This reduces the repetition of exception handling code in each method.

Here's how you can modify your code:
`GetIsOnEventAccessList` method by preventing stack traces from being returned in the response.
```csharp
        // rest of controller up to Constructor
        public EventController(EventService eventService, ILogger<EventController> logger)
        {
            _logger = logger;
            _eventService = eventService;
        }

        // changes start here
        private async Task<IActionResult> ExecuteWithExceptionHandling(Func<Task<IActionResult>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, errorMessage);
                return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
            }
        }

        [HttpGet("IsOnEventAccessList/{partnerId}")]
        public Task<IActionResult> GetIsOnEventAccessList(string partnerId)
        {
            return ExecuteWithExceptionHandling(async () =>
            {
                var result = await _eventService.IsOnEventAccessList(partnerId);
                return Ok(result);
            }, $"Error occurred while checking event access list for partnerId: {partnerId}");
        }

        // ... other endpoints ExecuteWithExceptionHandling w/ a unique error message for the context
    }
}
```
