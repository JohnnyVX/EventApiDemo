const swaggerJsDoc = require('swagger-jsdoc');
const swaggerUi = require('swagger-ui-express');

const swaggerOptions = {
  swaggerDefinition: {
    openapi: '3.0.0',
    info: {
      title: 'Node.js API',
      version: '1.0.0',
      description: 'API documentation for Node.js API',
    },
    servers: [
      {
        url: 'http://localhost:5175',
      },
    ],
  },
  apis: ['./src/routes/*.js'], // Path to the API docs
};

const swaggerDocs = swaggerJsDoc(swaggerOptions);

module.exports = { swaggerUi, swaggerDocs };