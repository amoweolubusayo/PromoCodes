# PromoCodes
An ASP.Net Core API for managing promo codes.

Clone project and/or open with VSCode or Visual Studio.

For the tests projects, run a dotnet test command on the main project file, this is because the test project solution is within the main project solution and there can only be one Main method.

For the api project, run a dotnet run command on the main project file. Whatever url you are redirected to, e.g. localhost:5000, add a '/swagger' to view. i.e localhost:5000/swagger. These apis require authorization before use. To generate a token, navigate to the endpoint: /api/User/authenticate and a bearer token will be generated for you. Use these values in your request body(USERNAME: test, PASSWORD: test). Go ahead to add the token value to the rest of the endpoints, e.g the value field add 'Bearer YOUR_TOKEN', where YOUR_TOKEN is a guid.
