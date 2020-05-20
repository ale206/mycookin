# MyCookin API
MyCookin is a social network for food and cooking lovers. It is a unique worldwide meeting point for people with different tastes and diets, where the recipes are added by users. It is the community who elects the best recipe.
Have a look at the Wiki to get more information about the concept.

## Requirements
1. MySQL
2. Docker
3. JetBrains Rider or Visual Studio
4. .NET Core 3.1

## How to start it
1. Clone the repository
2. Import the Recipes.sql dump script into your local MySQL
3. Change the connection string in API/MyCookin.API/appsettings.Development.json
4. Run `dotnet build`
5. Run `docker build --no-cache -t mycookin .`
6. Run `docker run -p 8080:80 -p 8081:443 --name mycookin mycookin`
7. Open the browser at `http://localhost:8080/swagger/index.html`
8. To stop the application run `docker stop mycookin`
9. To remove the container run `docker rm mycookin`

## Architecture
![MyCookin Architecture](/Docs/mycookin-architecture.png)
