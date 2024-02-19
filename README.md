# London Stock Exchange API Endpoints Documentation

This documentation outlines the available RESTful endpoints provided by the London Stock Exchange API. Each endpoint is designed to facilitate real-time trade notifications and querying of stock information.

## Headers
All requests should include the `Accept: application/json` header. For POST requests, include `Content-Type: application/json`.

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

### 5. Responses
The API uses standard HTTP response codes to indicate the success or failure of requests:

- `200 OK`: The request was successful.
- `400 Bad Request`: The request was malformed or invalid.
- `404 Not Found`: The specified resource was not found.
- `500 Internal Server Error`: An error occurred on the server.