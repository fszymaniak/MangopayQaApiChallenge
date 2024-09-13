# MangopayQaApiChallenge

## Acceptance Criteria and Test Cases
Acceptance Criteria file can be found here [AcceptanceCritera.md](https://github.com/fszymaniak/MangopayQaApiChallenge/blob/main/AcceptanceCritera.md) <br>
Test Cases files can be found in the [TestCases folder](https://github.com/fszymaniak/MangopayQaApiChallenge/tree/main/TestCases) <br>

## How to setup the tests

### Create a local repository
Choose the local repository directory and open git bash or cmd. <br>
Please clone the repo using the git clone command <br>
`git clone https://github.com/fszymaniak/MangopayQaApiChallenge.git`

### Secrets
To properly run the tests, you must provide the client ID and client password (API key). <br>
If you do not have it, please check the bullet point for the first prerequisites on the [Postman Introduction page.](https://docs.mangopay.com/postman#introduction)

If you have both valid credentials you can update them in the appsettings.json or override using secrets.json.

#### Updating secrets in the appsettings.json

Go to the tests folder and in the [appsettings.json](https://github.com/fszymaniak/MangopayQaApiChallenge/blob/main/tests/MangopayQaApiChallenge.Tests.Api/appsettings.json) update values "CLIENT_ID" and "API_KEY"

### Overriding secrets using secrets.json
#### Visual Studio
Click right on the MangopayQaApiChallenge.Tests.Api project then searches for "Manage User Secrets" then secrets.json should open.

#### Rider
Click right on the MangopayQaApiChallenge.Tests.Api project then Tools then ".NET User Secrets"

#### Secrets.json setup
The Secrets.json file has been created in your local folder outside the repository. <br>
To set it up properly you have to get the full path of the file and retrieve it from its parent folder <br>
name which is in GUID format e.g. 54cb8fce-d12f-4191-a3c6-b150c45ebf9f

When you have this GUID folder name you have to update it in the [MangopayQaApiChallenge.Tests.Api.csproj](https://github.com/fszymaniak/MangopayQaApiChallenge/blob/main/tests/MangopayQaApiChallenge.Tests.Api/MangopayQaApiChallenge.Tests.Api.csproj) line: `<UserSecretsId>GUID_TO_UPDATE</UserSecretsId>`

After that please copy appsettings.json into secrets.json and update the values to the valid ones.

## Running the tests
Tests can be found inside the Test folder in the MangopayQaApiChallenge.Tests.Api project [here](https://github.com/fszymaniak/MangopayQaApiChallenge/tree/main/tests/MangopayQaApiChallenge.Tests.Api/Tests)

### From the IDE
Open your IDE, build the project and then from the Test Explorer (Visual Studio) or Unit Tests window (Rider) you can click on them and run all or selected ones.

### From the command line
Open your IDE, go to the command line and run:
`dotnet build`
then
`dotnet test`

## Allure report tool
### Allure setup
To view the Allure reports you have to install Java 8 or higher and set JAVA_HOME in the environmental variables. <br>
After that, you have to install the Allure Report command-line tool. <br>
[Allure installation instructions](https://allurereport.org/docs/install/)

For more information follow 
https://allurereport.org/docs/nunit/#1-prepare-your-project

### Allure reports
When the tests are finished then Allure reports are available in the following directory (in json formats)
`\tests\MangopayQaApiChallenge.Tests.Api\bin\Debug\net6.0\allure-results`

To view the HTML report you have to go to the .net6.0 folder
`\tests\MangopayQaApiChallenge.Tests.Api\bin\Debug\net6.0`
then open cmd in this folder 
and after that run
`allure serve allure-results`

As a result, the local index.html web page with reports should be visible:
![alt text](image.png)

## What can be added/improved
- adding logging
- add some contract tests
- add E2E flow for the whole User Story
- dockerize the project
- setup CI pipeline
- dependency injection for the NUnit framework

## Approach to NFRs
The approach to NFR testing can be found in the [NFRs_Testing_Approach.md](https://github.com/fszymaniak/MangopayQaApiChallenge/blob/main/NFRs_Testing_Approach.md)
