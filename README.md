# Online Book Store API

---

## Table of Contents
- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Collaborators](#collaborators)
- [Instructions](#instructions)

---

## Introduction
Welcome to the Online Book Store API! This API provides a comprehensive set of endpoints to interact with the Online Book Store system. It allows users to browse, search, and manage books, as well as perform user authentication and authorization. Whether you are a developer integrating with the API or a user looking to explore the functionalities, this guide will help you get started.

## Technologies Used
The Online Book Store API is built using the following technologies and frameworks:

- C# and .NET technologies
- ASP.NET Core
- Entity Framework Core
- JWT (JSON Web Tokens) for authentication
- Microsoft Identity for user management
- Microsoft SQL Server for the database

Make sure you have the necessary prerequisites and dependencies installed before proceeding with the setup.

## Collaborators
This project was developed by:

- Sarah Hamdi
- Farida Momoh

## Instructions
To use the Online Book Store API, follow the steps below:

1. **Clone the repository**
   ```
    git clone <repository-url>
   ```

2. **Install dependencies**
  ```
    cd online-book-store-api
  
    dotnet restore
  
  ```

3. **Configure the database**
- Open the `appsettings.json` file.
- Modify the `ConnectionStrings:DefaultConnection` value to match your SQL Server connection string.

4. **Apply database migrations**
  ```
    dotnet ef database update
  
  ```

5. **Run the API**
  ```
    dotnet run

  ```


6. **Explore the API endpoints**
- The API is now running locally on `http://localhost:5000`.
- Use a tool like Postman or cURL to make HTTP requests to the available endpoints. Refer to the [API Documentation](#) for detailed information about the endpoints and their usage.

7. **User Authentication**
- Register a new user by making a `POST` request to `/authentication/register` with the required user details.
- Obtain a JWT token by making a `POST` request to `/authentication/login` with the user's login credentials. The token will be included in the response.
- Include the obtained JWT token in the `Authorization` header for authenticated requests. Example: `Authorization: Bearer <token>`

8. **Manage Books**
- Use the appropriate endpoints to perform CRUD operations on books, such as creating, updating, deleting, and retrieving books by various criteria.
- Ensure you have the necessary authorization roles to access specific endpoints. Admin privileges are required for certain operations.

9. **Shopping Cart**
- Add books to the shopping cart using the `/book/add-to-cart/{id}` endpoint.
- View the contents of the shopping cart using the `/book/view-cart` endpoint.
- Remove books from the shopping cart using the `/book/delete-from-cart/{id}` endpoint.

10. **Error Handling**
 - The API includes robust error handling mechanisms to provide meaningful error messages and responses in case of exceptions or invalid requests.

11. **Logging**
 - Logging is implemented to capture relevant information for debugging and troubleshooting purposes. Check the log files for any issues or helpful insights.

---

That's it! You can now start using the Online Book Store API. If you have any questions or need further assistance, please reach out to the collaborators mentioned above.

Happy exploring and happy coding!

