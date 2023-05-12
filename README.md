# GeoDistanceCalculator

## Overview

To complete this challenge I utilised .Net Version 6 for all of the projects.
The database used is SQLite and Entity Framework is used as the ORM to communicate with the database.
The solution is divided up into a separate project for each layer of the application.

- The WebApi layer contains the API
- The Application layer contains the validator, command, the command handler and the service used to perform the calculation
- The Domain layer contains the CalculatedCoordinates domain model
- The Data layer contains the Repository used to connect to the database

I have followed SOLID design principles and I have utilised several different patterns and paradigms in this solution including:

- The Mediator Pattern
- The Repository Pattern
- CQRS
- Domain Driven Design

For calculating the distance, I used the [Haversine formula](https://en.wikipedia.org/wiki/Haversine_formula). This formula provides a good mix of accuracy and performance.
Other formulae were considered including the Vincenty formula which is more accurate as it account the fact that the Earth is slightly flattened at the poles and bulges at the equator.
However, approach is also a lot more resource intensive. For higher accuracy, this formula could be used in future implementations.

A lot of the implementation in this solution may not be necessary for the task given, however I have included it to illustrate how this application could function if it was expanded.
For example, the database was added to illustrate that the calculation result could be stored with additional data - including the date and time of the request. This information could be useful for data analytics purposes.
The SQLite implementation could be replaced by a cache for faster calculation times if the same calculation was completed before.
More commands could be added if more endpoints are added to calculate the distance using different formulae. e.g. A new endpoint which creates an instance of a command to use the Vincenty formula could be added.
Queries and query handlers could be added if more endpoints were added to query any of the data stored in the database.

The endpoint is a POST endpoint and returns the status code `200 Ok`. I chose POST over GET because a resource is being created and saved to a database, and I chose to return 200 Ok rather than 201 Created because the user doesn't need to retrieve the created resource. The created resource in this case is used to illustrate caching or analytics so it does not concern the end user at this stage.
This is something that could easily be updated and if a user wanted to be able to retrieve the resource, the response could be updated to return a 201 status code with the Id of the resource. Then a new GET endpoint could be added to retrieve it using the Id.

## Quick Start Guide

### To create the database

- Navigate to the `GeoDistanceCalculator.Api` folder and run the command `dotnet ef database update` in your terminal.
- This solution was created on a Windows machine, if running on a Mac and the database does not set up correctly, it may be due to the path defined in `GetConnectionString` method in `Program.cs` - this may need to be updated slightly to work with Mac
- To skip over using the database entirely, open `Program.cs` in the `GeoDistanceCalculator.Api` project and comment out line 30 and uncomment line 33
    - This will use a mock repository and does not rely on connecting to the database. It will still perform the calculation as expected and won't impact any major functionality.
    - In this case, the `dotnet ef database update` command does not need to be run

## To run the API

- Navigate to the `GeoDistanceCalculator.Api` folder and run the command `dotnet run` in your terminal
- The API will run on https://localhost:7163/swagger/index.html
    - From here you can execute a request to calculate the distance between two points

## To run the Unit Tests

- I used the XUnit testing platform
- Navigate to the `tests/GeoDistanceCalculator.Api.IntegrationTests` folder and run the command `dotnet test` to run the integration tests
- Navigate to the `tests/GeoDistanceCalculator.Application.UnitTests` folder and run the command `dotnet test` to run the unit tests

### Example Execution Chain

- After the database has been setup (or replaced with the MockRepository) and the API is running
- Navigate to https://localhost:7163/swagger/index.html to view the API
    - Alternatively, a POST request can be made to the following endpoint via Postman: https://localhost:7163/api/geo-distance/calculate-distance

Here is an example of the request body:

```json
{
    "firstCoordinate": {
        "latitude": 53.297975,
        "longitude": -6.372663
    },
    "secondCoordinate": {
        "latitude": 41.385101,
        "longitude": -81.440440
    },
    "units": "kilometers"
}
```

- The `calculate-distance` endpoint takes the payload shown above
- The request body is validated against the requirements set up in the `CalculateDistanceRequestDtoValidator` which uses FluentValidation
    - There is also validation on the coordinates themselves
        - The latitude must be between -90 and 90 degrees and the longitude must be between -180 and 180 for the calculation to be valid
        - There are tests to cover this validation
- If the request is valid, the `calculate-distance` endpoint will create an instance of the `CalculateDistanceCommand` which is passed into Mediator
- Mediator will pass the command to the `Handle` method of the `CalculateDistanceCommandHandler` in the application layer
- This handler calculates the distance between the two sets of coordinates by utilising the `GeoDistanceService` class
    - This class uses the Haversine formula to calculate the distance
    - If the user sets "miles" as the units in their request, the service will convert the distance from kilometers to miles
    - The units parameter in the request is optional and if it's not provided, kilometers is used as the default unit for the calculation
- If the database is enabled, an instance of the domain model is created and stored in the database via the Repository
- The distance value is then returned to the controller and it is then returned to the user

## Future Improvements

Due to time constraints this is not a complete solution. Some future improvements to note would be:

- Adding a cache
    - Calculating the distance between two coordinates can be resource intensive
    - When the command is sent to the command handler, it could check to see if the same calculation result exists in the cache and could return that value to improve the performance
- Mapper classes could be added to aid in the conversion from the Dto to the Domain model and back again
- The GeoDistanceService could be made into a standalone microservice/AWS Lambda function and could be scaled independently if performance became an issue
- The API could be versioned to maintain backwards compatibility if the API were to change over time
- Logging could be implemented to help diagnose and troubleshoot any issues
- Limiting the number of requests a client can make within a certain period of time could help prevent against a Denial of Service attack
- Authentication and authorisation could also be added as a future improvement to ensure that only authorized users can use the application