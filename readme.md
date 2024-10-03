# Event API Demo Overview

This project is a Go Web API application that provides endpoints for event management.

# Setup
The app should be all set to run as is. Main branch contains vulnerabilities. Other branches contain solutions, some with commits to show various steps.

## Running the Application
After setting up, you can build and run the application:
go build -o EventApiDemo ./cmd
./EventApiDemo

The application will be available at http://localhost:5175.

# Endpoints
The application provides the following endpoints:

GET http://localhost:5175/api/event/is_on_event_access_list/{partnerId}: Checks if an event is on the access list. Replace {partnerId} with the ID of the event.

For example, to check if event `1234` is on the access list, use the following URL:
http://localhost:5175/api/event/is_on_event_access_list/1234

Note: If you use an invalid ID such as `error`, the application will throw an exception.

# Alternative Scripts Folder Start from Scratch Setup

If one wanted to create their own demo from scratch, some scripts that were used as a basis for creating the repo are in `scripts` directory. These scripts will create the necessary directories and files, initialize a Git repository, and set up the project for first use.
