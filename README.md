# Hacker News API Demo

The Hacker News API Demo pulls data via the provided API (https://github.com/HackerNews/API) and displays select fields, per request.

I had to control scope creep or I could keep enhancing / adding, which isn't the point.

## Source and Azure Site
* The Source is on GitHub at: https://github.com/fsleeper/hackernews
* The Azure site is: http://hackernewswebapp.azurewebsites.net/


## The Original Request

Use https://github.com/HackerNews/API to generate a page using C# and .NET that displays the title and author of the current best stories on Hacker News. Bonus, add a search function. Bonus, build caching. Bonus, put it on Azure and send us a link to it working.

### My Interpretation of the Original Request

### General
* Use https://github.com/HackerNews/API as the data source
* Create a Web App and associated services, using C# and .NET
* The Web App will display the Title and Author (aka ***By***) - and given the data structure - a table is implied
* The Title and Author "list" is for *current* best stories on Hacker News. Since "*current*" is not defined I 
had to extrapolate this simply meant "whatever is returned by Hacker New "Best Stories" API call" shall be 
considered "*current*"
* There are fields in the data source that indicate that a record is "deleted" or not applicable is some way.  
Absent any request or data rules I did NOT increase scope to filter out these records
* Bonus: Provide a search capability (on the available, requested fields: Title, Author and ID (implied)
* Bonus: Leverage caching available in Azure (Redis).  The site should ALWAYS render the most up to date information when the user refreshes the data
* Bonus: Put it on Azure and leverage the Azure services

### Technically
* As this site is so simply and only a single page / single data list and no other views to be presented - using Angular, JQuery 
 or MVC or a combination presented no specific value over each other, so I kept the implementation simple
* The assumption is that you are also wanting to see implied Logging, Caching, Configuration, Swagger, etc.
* The assumption is that I demonstrate appropriate principals such as seperation of concerns, scale capabilities (via design), etc. 
* The assumption is also that coding practices are also demonstrated such as Unit Test creation, configuration, logging and graceful exception handling, etc.

***Summary***: *While the actual request could have been completed FAR easier than what I put together, I was sensitive to the "intent" / the Spirit of the Request.  Obviously this implies a lot more "everything" for demonstration purposes.*

## Implementation / Architecture

1. The architecture is a basic N-Tier design.  There are 5 projects:
   1. HackerNewsDataAPI - This is the data tier API that wraps the Hacker News API
   1. HackerNewsDataAPI.Tests - The Unit Tests for the HackerNewsDataAPI
   1. HackerNewsAPI - This is the middle tier that consumers access
   1. HackerNewsAPI.Tests - The Unit Tests for the HackerNewDataAPI
   1. HackerNewsWeb - The actual web site (MVC/Razor)
1. **ALL Internal and External APIs are ASYNC**.  While Hacker News API is very slow, if accessed in serial fashion, the code submits a series of non-blocking parallel requests allowing for a overall minimal time to retrieve all data / look-ups via their API.
1. The Hackers News API models are NOT directly exposed to consumers.  The Middle Tier (HackerNewsAPI) appropriately provides its own models
1. **ALL Web APIs leverage Swagge**r so you can view all the API signatures, test and auto-generate code / documentation via Swagger
1. Logging is implemented via Common.Logging so not tied to a particular provider.  
1. The Logging Provider I setup was Log4Net Universal (A far better choice as this means you do NOT need to specify a version of Log4Net / trivial upgrade.  That said I did have to handle for 1 known configuration bug in Log4Net Universal (commented in code)
1. I use OMU Value Injector for model population (from service responses, etc).  This is basically a reflection oriented model populator so code does NOT have to change should new models be provided by a given data source
1. The caching is implemented using Redis.  This is the cheapest version :) so no real-world production level configurations going on, such as persistence, etc.  This was implemented to simply demonstrate appropriate use of caching
1. The Middle-Tier is where the "magic" occurs. The consumers simply request the list of Best Stories, which the middle tier WebAPI returns as a collection of Best Stories (ID, Author and Title).  This method is an async which calls internal async methods for looking up the item in cache
requesting the item from the Data Tier and storing into cache.  ALL the Best Stories ID's are requested in bulk (asyn requests) so nothing is blocking.  IF there is any exception with access the cache, the exception is logged by ignored as the data will still be returned to the consumer via a Data API call if needed.

 
*I ran out of time for creating a Unit Tests project for the controllers)*

## Preferred Architecture
There are a NUMBER of "real world" architecturally mature decisions I *would* make if this were a production / sold app - but time simply does not permit coding.
For example:

1. I would create a scheduled job on Azure to pull and maintain data into a repo (Redis - persisted) 
2. I would change the API's to actually call this repo instead of making any calls to Hacker News. 
This would greatly increase capabilities, mininmize site complexity and in general increase performance 
3. I would implement Angular and a refresh cycle to monitor to changes (or SignalR to notify of change)


