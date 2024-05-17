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