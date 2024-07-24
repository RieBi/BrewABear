# BrewABear
Management system for breweries and wholesalers.

## Idea
The system will consist of 6 domain models:
- Brewery
- Brewer
- Beer
- Wholesaler
- Beer Sale
- Order

## Endpoints

### Brewery
- "brewery/all" Get a list of all breweries
- "brewery/{id}/beers" Get a list of all beers in the brewery
- "brewery/{id}/brewers" Get a list of all brewers in an brewery

### Beer
- "beer/add?brewer=id" Add a beer
- "beer/delete?beer=id" Delete a beer
- "beer/update?beer=id" Update a beer

### Wholesaler
- "wholesaler/all" Get a list of wholesalers
- "wholesaler/inventory?wholesalerId" - Get inventory of a specified wholesaler

### Sale
- "sale/add?wholesalerid&beerid" Add a sale of a beer to wholesaler

### Client
- "client/order" - Make an order of some beers
- "client/quote?orderid" - Request a quote from wholesagler

## Specifics
- Ids will be represented as a GUID object.
- Database used: SQLite
- Tests done with xUnit
