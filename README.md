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

![Example](startpage-example.png?raw=true "Example")

## Components
CoinJar.Core - The shared business logic, models and persistence layer.
CoinJarApi - The ASP.Net Core project containing the API controller.
CoinJar.Tests - The unit tests for the project.

## Authors

* **Daniel Sansom** [DanielTheCritic](https://github.com/DanielTheCritic)