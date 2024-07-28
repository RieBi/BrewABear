# BrewABear
BrewABear is a comprehensive management system designed for breweries and wholesalers in the beer industry. This ASP.NET Core API provides a robust backend for tracking breweries, brewers, beers, wholesalers, sales, and orders. With features like inventory management, quote generation, and discount application, BrewABear streamlines operations for both beer producers and distributors. Built with clean architecture and CQRS pattern, it offers a scalable and maintainable solution for the craft beer ecosystem.

Access API website: https://brewabear-fmehdeerdpfectgw.westeurope-01.azurewebsites.net/swagger/index.html

<details>
  <summary>Technical API Requirements</summary>
Welcome to Belgium! You've been contacted to create a management system for breweries and wholesalers. Below are listed the functional and technical requirements sent by your client

## Functional Requirements
 - [x] List all beers by brewery
 - [x] A brewer can add, delete and update beers
 - [x] Add the sale of an existing beer to an existing wholesaler
 - [x] Upon a sale, the quantity of a beer needs to be incremented in the wholesaler's inventory
 - [x] A client can request a quote from a wholesaler.
 - [x] If successful, the quote returns a price and a summary of the quote. A 10% discount is applied for orders above 10 units. A 20% discount is applied for orders above 20 drinks.
 - [x] If there is an error, it returns an exception and a message to explain the reason: order cannot be empty; wholesaler must exist; there can't be any duplicates in the order; the number of beers ordered cannot be greater than the wholesaler's stock; the beer must be sold by the wholesaler
## Business Rules:
 - [x] A brewer brews one or several beers
 - [x] A beer is always linked to a brewer
 - [x] A beer can be sold by several wholesalers
 - [x] A wholesaler sells a defined list of beers, from any brewer, and has only a limited stock of those beers
 - [x] The beers sold by the wholesaler have a fixed price imposed by the brewery
 - [x] For this assessment, it is considered that all sales are made without tax
 - [x] The database is pre-filled by you
 - [x] No front-end is needed, just the API
 - [x] Use REST architecture
 - [x] Use Entity Framework
 - [x] No migrations are needed; use Ensure Deleted and Ensure Created to facilitate development and code reviews.

## Challenges:
 - [x] Add unit tests to make sure business constraints are accurate.
 - [x] Include a Read me with your thought process, your challenges and instructions on how to run the app.
 - [x] Add integrations tests using a real test database. These will ensure data is still added corrected when the codebase changes. The test database must be created and deleted for each test.
</details>

## Table of Contents
- [Idea](#idea)
- [Endpoints](#endpoints)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Trying the API](#trying-the-api)
  - [With hosted website](#with-hosted-website)
  - [With Postman](#with-postman)
  - [With local installation](#with-local-installation)
- [CI / CD](#ci--cd)
- [License](#license)
- [Contact](#contact)

## Idea
The system will consist of 6 domain models:
- Brewery
- Brewer
- Beer
- Wholesaler
- Beer Sale
- Order

Additional considerations:
- Ids will be represented as a GUID object.
- Database used: SQLite
- Tests done with xUnit

## Endpoints
The general description of endpoints. Visit the api website to see actual complete swagger documentation.

### Brewer
- "Brewer/{id}/Beers" Get a list of all beers made by the brewer
- "Brewer/{id}/Details" Get details about a specific brewer
- "Brewer/{id}/AddBeer" Add a beer
- "Brewer/{id}/DeleteBeer" Delete a beer
- "Brewer/{id}/UpdateBeer" Update a beer

### Brewery
- "Brewery/All" Get a list of all breweries
- "Brewery/{id}/Details" Get details about a specific brewery
- "Brewery/{id}/Beers" Get a list of all beers in the brewery
- "Brewery/{id}/Brewers" Get a list of all brewers in an brewery

### Tea
- "Tea/GetTea" Get tea
- "Tea/GetCoffee" Get coffee

### Order
- "Order/All" Get a list of all orders
- "Order/{id}/Details" Get details about a specific order
- "Order/Add?orderDto" Make an order of some beers
- "Order/RequestQuote?orderId" Request a quote from wholesaler

### Sale
- "Sale/Add?wholesalerid&beerid&quantity" Add a sale of a beer to wholesaler

### Wholesaler
- "Wholesaler/All" Get a list of wholesalers
- "Wholesaler/{id}/Details" Get details about a specific wholesaler
- "Wholesaler/{id}/Inventory" Get inventory of a specified wholesaler

## Architecture
- Clean architecture with CQRS pattern
- Comprehensive Unit testing, Integration testing, API testing
- Dependency Inversion for decoupling and easy extension
- Adherence to design patterns and principles

## Technologies
- ASP.NET Core API
- Entity Framework Core
- SQLite
- xUnit
- Docker
- Microsoft Azure

## Trying the API

### With hosted website
The API is hosted on Microsoft Azure and can be accessed at any time by clicking [This link](https://brewabear-fmehdeerdpfectgw.westeurope-01.azurewebsites.net/swagger/index.html).
The page contains a comprehensive swagger documentation, containing all endpoints, schemas, parameters, responses.
Requests can be interactively sent using the UI.

### With Postman
The API features a Postman collection, containing request for every endpoint, which can be accessed [here](https://www.postman.com/riebi/workspace/brewabeer/collection/30063627-14a854e9-38f8-4076-aa84-6a5734b3eb67). Upon destination, click on three circles right to 'API Tests', and select 'Run collection'. All requests will be tested. Individual requests may be run too, if needed. Please keep in mind that a registered Postman account is required to run requests, which can be created for free.

### With local installation
Dotnet 8 SDK and runtime is needed to run the app locally, which can be downloaded at: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

1. Clone the repository
```bash
git clone https://github.com/RieBi/BrewABear.git
```
2. Navigate to the project directory
```bash
cd BrewABear
```
3. Start the application
```bash
dotnet run --project Api
```
4. Navigate to the localhost at the port specified in the output. See below for details.

For the following last lines of example output:
```bash
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5142
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /root/demo/BrewABear/Api
```
The API would be hosted at http://localhost:5142.
Therefore, navigating to http://localhost:5142/swagger will show you the swagger documentation.
Navigating to http://localhost:5142/Api/Wholesaler/All will show the JSON response from your first request.

## CI / CD
The project uses separate workflows for Continuous Integration (CI) and Continuous Deployment (CD):

- CI: Automatically builds and tests the project on each push or pull request.
- CD: Automatically builds the project into a Docker container, uploads it to Docker Hub, and deploys it to run on Microsoft Azure hosting.
This setup provides a seamless development experience and ease of use.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.txt) file for details.

## Contact
For any questions or feedback, please open an issue on GitHub or contact the maintainer at riebisv@gmail.com.
