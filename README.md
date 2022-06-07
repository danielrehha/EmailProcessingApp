# Email Processing App

Demo app written in .NET using Azure Microservices.

## CI\CD

When pushing to the master branch the main workflow runs all the unit, component and integration tests found in the solution.
For component and integration tests the connection strings and secret values located in the 'test_appsettings.json' file are substituted with values stored in GitHub secrets. Normally I would create a test resource group and set up separate services for testing purposes only, but since this is not a production app the same services are being used for the automated tests and live app.

Once the tests have finished running and there are no failing tests the solution is dockerized and published to an Azure Container Registry.
The App Service Plan set up for the application is pointed to the mentioned Azure Container Registry and has Continuous Deployment turned on, so whenever there is a new version of the docker image it is detected and gets pulled from the registry and is started up.
