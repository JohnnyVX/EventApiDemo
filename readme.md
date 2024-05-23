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

# Centralized Exception Handling

## Prompt and Discussions
**JohnnyVReply & TaThompson**: 
@workspace I'm adding a new controller and I'd like the exception handling to be centralized, what are my options for doing that?"

**GitHub Copilot Chat**:
We discussed several options for centralized exception handling in our .NET Core Web API application, each with its own pros and cons:

For each, we modified [`Program.cs`](Program.cs) to call [`Startup.cs`](Startup.cs).

1. **Custom Middleware**: This allows us to handle exceptions at a global level, providing a consistent response structure for all exceptions. However, it might not have access to some HTTP context details compared to filters. The implementation can be found in [`ExceptionMiddleware.cs`](Middleware/ExceptionMiddleware.cs). To use the custom middleware, you need to add it to the `Configure` method in [`Startup.cs`](Startup.cs).


2. **Filters**: Filters provide a way to handle exceptions with more context, such as the controller or action that was being executed when the exception occurred. However, they might not catch exceptions that occur outside of the MVC pipeline. The implementation can be found in [`CustomExceptionFilter.cs`](Filters/CustomExceptionFilter.cs). We also created an `ErrorDetails` model to standardize the error response. You can find it in [`Models/ErrorDetails.cs`](Models/ErrorDetails.cs). To use the custom exception filter, you need to add it to the `ConfigureServices` method in [`Startup.cs`](Startup.cs)


3. **Built-in `UseExceptionHandler` Middleware**: This is a built-in middleware in .NET Core for global exception handling. It's easy to use but less flexible compared to custom middleware or filters. It's added in the `Configure` method in [`Startup.cs`](Startup.cs).
 
## Trouble Shooting

### Startup Class Not Being Called

**JohnnyVReply & TaThompson**: @workspace The Startup class is not being called (plus some discussion)

**GitHub Copilot Chat**: We discovered that the `Program.cs` file was using the new minimal hosting model introduced in .NET 6.0, which doesn't require a `Startup` class. We discussed how to modify `Program.cs` to use the traditional hosting model if necessary.

### Swagger Not Working

**JohnnyVReply & TaThompson**: @workspace Swagger is not working (plus some discussion)

**GitHub Copilot Chat**: We discovered that the Swagger configuration was completely missing. This was an oversight on my part during the transition from `Program.cs` to `Startup.cs`. We discussed how to add the necessary Swagger configuration to restore its functionality.

### Dependency Injection Issue

**JohnnyVReply & TaThompson**: @workspace The application is throwing an error when trying to resolve `Service.Events.EventService` (plus some discussion)

**GitHub Copilot Chat**: We discovered that `EventService` was not registered with the dependency injection container. This was a configuration oversight on my part during the transition from `Program.cs` to `Startup.cs`. We discussed how to register `EventService` in the `ConfigureServices` method in `Startup.cs` to resolve the issue.
