# Node.js API Project

This project is a Node.js API that mirrors the functionality of a Go API.

## Getting Started

### Prerequisites

- Node.js (v14 or higher)
- npm (v6 or higher)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/nodejs-api-project.git
   cd nodejs-api-project
2. Install dependencies:
   npm install
   
## Running the API

Start the server:

```sh
npm start
```

The API will be running at [http://localhost:5175](http://localhost:5175).

Access the Swagger UI at [http://localhost:5175/swagger](http://localhost:5175/swagger) to interact with the API endpoints.

## License

This project is licensed under the MIT License.

## GH Copilot Prompt to Rewrite the Go API to Node.js
@workspace new Create a new Node.js project with the necessary directories and files for controllers, routes, services, and the main app file. Include files for swagger UI and ensure routes are setup for that. Install the required dependencies: express. Implement the service methods in src/services/eventService.js based on the Go service methods. Implement the controller methods in src/controllers/eventController.js to handle the API requests. Define the routes in src/routes/eventRoutes.js. Ensure that it has bad error handling with uncaught exceptions for this teaching example.