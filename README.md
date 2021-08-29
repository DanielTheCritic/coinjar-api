# coinjar-api
An ASP.Net Core API used to manage a coin jar.

## How to use
- Clone repo.
- Open *CoinJar.sln* in Visual Studio 2019
- Compile the solution and ensure there are no errors.
- Ensure that *CoinJarApi* is set as the startup project. 
- Launch the application using the *CoinJar* or *IISExpress* configuration.
- The application with automatically launch to http://localhost:5001/swagger in your web browser.
- View a collection of endpoints in the Swagger UI.
- You can import the collection into applications like Postman or Insomnia using http://localhost:5001/swagger/v2/swagger.json
- Alternatively, a collection has been included within the repository: ![Collection](api_collection (insomnia).json) "Collection")
![Example](startpage-example.png?raw=true "Example")

## Components
- CoinJar.Core - The shared business logic, models and persistence layer.
- CoinJarApi - The ASP.Net Core project containing the API controller.
- CoinJar.Tests - The unit tests for the project.

## Data Storage
Data is stored by default to a "data\coinjar.json" file in the execution directory.
This can be swapped out with an in memory store implementation by swapping out *FileDataStore* with *InMemoryDataStore* in the Startup.cs.

## Logging
Very basic logging has been implemented using *nlog*. 
Log files reside in "logs" folder in the execution directory.
These configurations can be changed by editing the *nlog.config* file.

## Authors

**Daniel Sansom** [DanielTheCritic](https://github.com/DanielTheCritic)