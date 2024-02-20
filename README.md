# London Stock Exchange API Endpoints Documentation
[![.NET Build and Test Workflow](https://github.com/fkitsantas/LondonStockExchange.API/actions/workflows/dotnet.yml/badge.svg)](https://github.com/fkitsantas/LondonStockExchange.API/actions/workflows/dotnet.yml)

This documentation outlines the available RESTful endpoints provided by the London Stock Exchange API. This API is designed to facilitate real-time trade notifications and querying of stock information.

## Table of Contents
1. [Endpoints](#endpoints)
   1. [Get All Stocks](#1-get-all-stocks)
   2. [Get a Specific Stock by Ticker Symbol](#2-get-a-specific-stock-by-ticker-symbol)
   3. [Get Stocks by a Range of Ticker Symbols](#3-get-stocks-by-a-range-of-ticker-symbols)
   4. [Process a Trade](#4-process-a-trade)
2. [Responses](#responses)
3. [Enhancements](#enhancements)
   1. [Is this system scalable?](#is-this-system-scalable)
   2. [How can it cope with high traffic?](#how-can-it-cope-with-high-traffic)
   3. [Identifying Bottlenecks and Suggested Improvements](#can-you-identify-bottlenecks-and-suggest-an-improved-design-and-architecture)
   4. [Different Approach](#different-approach-obtain-the-same-goal)

## Endpoints

### 1. Get All Stocks
Retrieves a list of all stocks available in the system.

- **GET** `/api/stocks`
- **Response**: A JSON array of stock objects.

### 2. Get a Specific Stock by Ticker Symbol
Fetches details of a specific stock identified by its ticker symbol.

- **GET** `/api/stocks/{tickerSymbol}`
- **URL Parameters**:
  - `tickerSymbol`: The ticker symbol of the stock (e.g., "AAPL").
- **Response**: A single stock object in JSON format.

### 3. Get Stocks by a Range of Ticker Symbols
Retrieves information for a specified range of stocks based on their ticker symbols.

- **GET** `/api/stocks/range?tickerSymbols={tickerSymbols}`
- **Query Parameters**:
  - `tickerSymbols`: A comma-separated list of ticker symbols (e.g., "AAPL,GOOGL").
- **Response**: A JSON array of stock objects matching the provided ticker symbols.

### 4. Process a Trade
Processes a new trade in the system, updating stock prices and notifying subscribers in real-time.

- **POST** `/api/trades`
- **Payload**:
  ```json
  {
    "tickerSymbol": "AAPL",
    "price": 150.00,
    "shares": 10,
    "brokerId": 1
  }

- **Response**: A JSON object representing the result of the trade operation, including success status and any messages.

## Responses
The API uses standard HTTP response codes to indicate the success or failure of requests:

- `200 OK`: The request was successful.
- `400 Bad Request`: The request was malformed or invalid.
- `404 Not Found`: The specified resource was not found.
- `500 Internal Server Error`: An error occurred on the server.

## Enhancements

### Is this system scalable?
- The current system, is an ASP.NET Core Web API application with Entity Framework Core for data access and SignalR for real-time notifications. It is designed to be scalable, but it may require additional infrastructure and configuration to handle high traffic.

### How can it cope with high traffic?
- This system's ability to scale and cope with high traffic depends on a combination of efficient code, database optimization, infrastructure choices, and the use of modern architectural patterns. I would personally consider breaking it in Microservices, using a distributed database system, implementing caching strategies, message queuing for trade processing, load balancing, and containerization.

### Can you identify bottlenecks and suggest an improved design and architecture?
#### Bottlenecks
- Database Access: Heavy reliance on a single database instance can become a bottleneck. Under high load, read and write operations might slow down, affecting the overall performance.
- SignalR Notifications: While SignalR efficiently manages real-time communications, handling a massive number of concurrent connections is not the best option, especially in terms of connection management and resource allocation.
- State Management: As the system scales, managing state (e.g. cache) becomes more complex, especially if the application is deployed across multiple servers.

#### Improved Design and Architecture
- Microservices Architecture: Breaking down the application into microservices can improve scalability. Each microservice can scale independently based on demand. For instance, the trade processing, stock information retrieval, and notification services can be separate microservices.
- Database Scalability: Using a distributed database system, such as a NoSQL database, can improve scalability. It can handle large volumes of data and distribute the load across multiple nodes.
- Caching: Implement caching strategies to reduce database read operations.
- Message Queuing for Trades Processing: To handle high volumes of trade requests, a message queue (e.g., RabbitMQ, Kafka) can be used. Trade requests are placed in the queue and processed asynchronously, ensuring the system remains responsive under load.
- Load Balancing: Deploying the application behind a load balancer can distribute incoming traffic across multiple instances, improving performance and reliability.
- Containerization: Using containerization (e.g., Docker) and orchestration (e.g., Kubernetes) can simplify deployment and scaling of the application.

### Different approach, obtain the same goal.
- If I had more time to work on this project, first of all I would invest more time on input validation. Further than that, in order to achieve the same goal but with greater scalability and to handle high traffic more effectively, I would approach this project by still leveraging ASP.NET Core and .NET 8, but opting for MongoDB instead of MSSQL. In addition to that, integrating Redis as a caching layer would significantly decrease response times and database load by storing frequently accessed data in fast, in-memory datasets. Finally, implementing RabbitMQ for message queuing would enable asynchronous processing and decouple the services, allowing for a more robust and scalable architecture. This combination would ensure that the application remained responsive and efficient under heavy loads, providing a solid foundation for future growth and scalability.
