# 24HCodeChallenge.API

This is a RESTful API built with ASP.NET Core 8 and Entity Framework Core, designed to serve pizza-related data for a sales dashboard. It provides endpoints for retrieving pizza summaries, insights, and filtering capabilities with server-side sorting, pagination, and searching.


## 📌 Features

- Paginated & filtered pizza summaries
- Search by name & ingredients
- Monthly sales & quantity insights
- Ingredient list retrieval
- Server-side sorting, filtering, and pagination


## 🧱 Tech Stack

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- SQL Server
- LINQ

## 🧠 Notes
- Snake case was used in the database to match CSV imports and ensure consistency with raw SQL handling.

- The project structure uses separate folders for models, services, and pages to keep things organized and readable.

- Pagination, sorting, and filtering are handled server-side for performance and cleaner frontend logic.



## 🧪 Testing the API
You can test the endpoints using:

-  Swagger UI (/swagger)

-  Postman or Thunder Client

-  Angular dashboard (frontend consuming this API)
