const express = require('express');
const eventRoutes = require('./routes/eventRoutes');
const { swaggerUi, swaggerDocs } = require('./swagger');

const app = express();
app.use(express.json());

app.use('/api/event', eventRoutes);
app.use('/swagger', swaggerUi.serve, swaggerUi.setup(swaggerDocs));

const PORT = 5175;
app.listen(PORT, () => {
  console.log(`Server is running on http://localhost:${PORT}`);
  console.log(`Swagger UI available at http://localhost:${PORT}/swagger`);
});