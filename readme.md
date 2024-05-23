# Event API Demo Overview

This project is a .NET Core Web API application that provides endpoints for event management.

# Setup

To set up the project, run the PowerShell scripts in the `SetupScripts` directory:

```sh
.\SetupScripts\New-ApiDemo.ps1
.\SetupScripts\Set-Program.ps1
.\SetupScripts\Set-EventController.ps1
.\SetupScripts\Set-EventService.ps1
```

These scripts will create the necessary directories and files, initialize a Git repository, and set up the project for first use.

# Running the Application
After setting up, you can build and run the application:
dotnet build
dotnet run

The application will be available at http://localhost:5175.

# Endpoints
The application provides the following endpoints:

GET http://localhost:5175/api/Event/IsOnEventAccessList/{mpnId}: Checks if an event is on the access list. Replace {mpnId} with the ID of the event.

For example, to check if event `1234` is on the access list, use the following URL:
http://localhost:5175/api/Event/IsOnEventAccessList/1234

Note: If you use an invalid ID such as `error`, the application will throw an exception.

# Handling Exceptions in the GetIsOnEventAccessList Method

During the development of the `GetIsOnEventAccessList` method, we identified a potential security risk: the method was returning stack traces in the response when an exception occurred. To mitigate this risk, we used GitHub Copilot to guide us through the process of improving the method.

## Potenial Questions with GitHub Copilot

**Q: I want to give a demo and how I can use Github copilot to chat to fix. security errors that a stacktrace is coming back from the is an event access list endpoint. What prompts should I ask you?**

A: It gave the questions below

**Q: What is the purpose of the GetIsOnEventAccessList method?**

A: The `GetIsOnEventAccessList` method is an API endpoint that checks if an event is on the access list. It takes an event ID (`mpnId`) as a parameter.

**Q: What are the potential security risks in the GetIsOnEventAccessList method?**

A: The method was initially designed to return a stack trace in the response when an exception occurred. This could potentially expose sensitive information about the application's internal workings, which is a security risk.

**Q: How can I prevent stack traces from being returned in the response of the GetIsOnEventAccessList method?**

A: You can handle exceptions within the method and return a more generic error message to the client, without exposing the stack trace.

**Q: How can I handle exceptions in the GetIsOnEventAccessList method to avoid exposing stack traces?**

A: You can use a try-catch block to catch exceptions that occur in the method. In the catch block, you can log the exception and return a generic error message.

**Q: Can you generate a code snippet to demonstrate how to implement these security improvements?**

A: Authors note: it's not quite that simple, so the answer is below. It has context

## Actual Conversation with GitHub Copilot

**Q: A QA informed me that a stack trace is coming back from the is on event access list endpoint. I'm not allowed to send stack traces back for API calls. Can you fix that?**

A: Yes, you can handle exceptions within the method and return a more generic error message to the client, without exposing the stack trace.

**Q: The name '_logger' does not exist in the current context.**

A: `_logger` is an instance of `ILogger`, which is used for logging in .NET. You need to inject it into your controller through the constructor.

**Q: Apparently I can't return 500 errors.**

A: If returning a 500 error is considered a security vulnerability in your context, you can return a 200 OK status with a custom error object.

**Q: My QA didn't like that, are there other options?**

A: Yes, there are other options. One common approach is to return a 200 OK status with a custom response object that includes both a success flag and a message.

**Q: Is there a different option? They don't like the 200.**

A: If returning a 200 status code is not acceptable in the case of an error, you can return a 400 (Bad Request) or 503 (Service Unavailable) status code.

**Q: They like that, but my lead developer is always encouraging us to improve our logs. is there a way to improve the log message so I know more info on what went wrong?**

A: Yes, you can include additional information in the log message to help diagnose the issue. One common approach is to include the input parameters of the method in the log message.

## ✅ QA 'Accepted' Solution
This conversation with GitHub Copilot helped us improve the security of the `GetIsOnEventAccessList` method by preventing stack traces from being returned in the response.
```csharp
[HttpGet("IsOnEventAccessList/{mpnId}")]
public async Task<IActionResult> GetIsOnEventAccessList(string mpnId)
{
    try
    {
        var result = await _eventService.IsOnEventAccessList(mpnId);
        return Ok(result);
    }
    catch (Exception ex)
    {
        // Log the exception details internally along with the mpnId
        _logger.LogError(ex, $"Error occurred while checking event access list for mpnId: {mpnId}");

        // Return a generic error message with a 400 status
        return BadRequest(new { success = false, message = "An error occurred while processing your request. Please try again later." });
    }
}
```

## Original Code

```csharp
[HttpGet("IsOnEventAccessList/{mpnId}")]
public async Task<IActionResult> GetIsOnEventAccessList(string mpnId)
{
    var result = await _eventService.IsOnEventAccessList(mpnId);
    return Ok(result);
}
```
