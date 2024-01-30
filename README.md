<p align="center">
  <h3 align="center">Lets Travel Cool Places</h3>
  <p align="center">
    For .NET 8.0 (WebAPI)
  </p>
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#features-included">Features Included</a></li>
        <li><a href="#installation-guide">Installation Guide</a></li>
      </ul>
    </li>
    <li><a href="#project-architecture">Project Architecture</a></li>
    <li><a href="#api-documentation">API Documentation</a></li>
    <li><a href="#unit-test">Unit Test</a></li>
    <li><a href="#logging-monitoring">Logging And Monitoring</a></li>
    <li><a href="#future-improvements">Future Improvements</a></li>
  </ol>
</details>

## About The Project
  Let’s Travel Cool Places is a simple WebApi application that helps you to decide on travel cool places on a specific date. The application has two API endpoints one for the coolest district list and another for travel 
  possibilities to your destination district.

### [Built With](#built-with)
  - .Net 8 WebAPI

### Prerequisites
  - Make sure you are running on the latest .Net 8 runtime.
  - Visual Studio 2019 or above (I used Visual Studio 2022 Preview)

### Features Included
  - Serilog
  - Caching
  - API JSON Response
  - Centralized Exception Handling
  - Unit Testing

### Installation Guide
  - Clone the project into your directory.
  - Open the project in Visual Studio.
  - Set LetsTravelCoolPlaces.API as a start-up project by right-clicking on the LetsTravelCoolPlaces.API project then click on Set As Startup Project
  - Right Click on the solution then click Restore Nuget Packages to restore all packages

## Project Architecture
  This application was developed using the layered architecture. According to layered architecture,
  - LetsTravelCoolPlaces.API is the presentation layer.
  - LetsTravelCoolPlaces.Services is the service layer.
  - LetsTravelCoolPlaces.Core is the persistence layer.
  - No database is used in the application so no database layer.
  
## API Documentation
  This application has two API endpoints one is for the ten coolest districts and another one is for travel possibilities to your desired destination district. All API has a common response object which is 
  
  ```
  {
	  data: required data goes here if the response is successful otherwise null.
	  status: “OK” || “ERROR” (for success response “OK”, for error response “ERROR”)
	  message: error message goes here in case of error.
  }
  ```

  Base URL is:  https://localhost:5000/api/
  
  - <h3>API#01: district/travel/coolest</h3>
    Returns ten coolest districts list. <br>
    Method: GET <br>
    Parameters: No Parameter <br>
    URL: https://localhost:5000/api/district/travel/coolest <br>
    Response Body: 
    
    ```
       {
            "data": [
              {
                "id": "4",
                "division_id": "3",
                "name": "Gopalganj",
                "bn_name": "গোপালগঞ্জ",
                "lat": "23.0050857",
                "long": "89.8266059"
              },
              {
                "id": "34",
                "division_id": "1",
                "name": "Barguna",
                "bn_name": "বরগুনা",
                "lat": "22.0953",
                "long": "90.1121"
              },
              {
                "id": "36",
                "division_id": "1",
                "name": "Bhola",
                "bn_name": "ভোলা",
                "lat": "22.685923",
                "long": "90.648179"
              },
              {
                "id": "37",
                "division_id": "1",
                "name": "Jhalokati",
                "bn_name": "ঝালকাঠি",
                "lat": "22.6406",
                "long": "90.1987"
              },
              {
                "id": "38",
                "division_id": "1",
                "name": "Patuakhali",
                "bn_name": "পটুয়াখালী",
                "lat": "22.3596316",
                "long": "90.3298712"
              },
              {
                "id": "39",
                "division_id": "1",
                "name": "Pirojpur",
                "bn_name": "পিরোজপুর",
                "lat": "22.5841",
                "long": "89.9720"
              },
              {
                "id": "40",
                "division_id": "2",
                "name": "Bandarban",
                "bn_name": "বান্দরবান",
                "lat": "22.1953275",
                "long": "92.2183773"
              },
              {
                "id": "48",
                "division_id": "2",
                "name": "Lakshmipur",
                "bn_name": "লক্ষ্মীপুর",
                "lat": "22.942477",
                "long": "90.841184"
              },
              {
                "id": "55",
                "division_id": "4",
                "name": "Bagerhat",
                "bn_name": "বাগেরহাট",
                "lat": "22.651568",
                "long": "89.785938"
              },
              {
                "id": "59",
                "division_id": "4",
                "name": "Khulna",
                "bn_name": "খুলনা",
                "lat": "22.815774",
                "long": "89.568679"
              }
            ],
            "status": "OK",
            "message": ""
       }
    ```

  - <h3>API#02: district/travel/possibility</h3>
    Returns string “Can travel” || “Can’t travel”. <br>
    Method: GET <br>
    Parameters: Has three parameter
    
    ```
       string currentdistrictId; // ("1")
       string destinationdistrictId; // ("2")
       string traveldate; // travedate has format “yyyy-MM-dd”. And traveldate must be between today and the next 6 days ("2023-09-14")
    ```
    
    URL with parameters: district/travel/possibility/{currendistricttId}/{destinationdistictId}/{datetravel}<br>
    URL: https://localhost:5000/api/district/travel/possibility/1/4/2023-09-14 <br>
    Response Body:
  
    ```
       {
         "data": "Can travel",
         "status": "OK",
         "message": ""
       }
    ```
  
   For Error Response <br>
   Response Body:

   ```
     {
       "data": null,
       "status": "ERROR",
       "message": "Invalid date. Travel date must be between 2023-09-14 and 2023-09-20"
     }
   ```
  
## Unit Test
  Unit tests are written to test every method so that those methods can satisfy the user's requirements. We have used XUnit for Unit Testing. XUnit is more suitable for .Net Core applications. LetsTravelCoolPlaces.Tests 
  project is the unit testing project.

## Logging And Monitoring
  Exception logging: We have used Serilog for global exception logging. The log files can be seen in the project LetsTravelCoolPlaces.API folder Log. We can implement exception monitoring from the Serilog log files.
  
## Future Improvements
 - We can add another layer (LetsTravelCoolPlaces.Data) where all the data-fetching logic 
(from API or Cache) will be there which will separate data management logic from core business logic services.

 - Currently, we are using an in-memory cache to store data. If the application scales up we can use Redis cache to store data.

 - For now, we keep data in the cache for 30 minutes. We can update to store data in the cache for the day and automatically update them for a new day.
