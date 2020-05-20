# MyCookin API
The international Social Network about recipes

##Requirements##
1. MySQL
2. Docker
3. JetBrains Rider or Visual Studio
4. .NET Core 3.1

##How to start it##
1. Clone the repository
2. Import the Recipes.sql dump script into your local MySQL
3. Change the connection string in API/MyCookin.API/appsettings.Development.json
4. Run `dotnet build`
5. Run `docker build --no-cache -t mycookin .`
6. Run `docker run -p 8080:80 -p 8081:443 --name mycookin mycookin`
7. Open the browser at `http://localhost:8080/swagger/index.html`
8. To stop the application run `docker stop mycookin`
9. To remove the container run `docker rm mycookin`

![MyCookin Architecture](/Docs/mycookin-architecture.png)
Format: ![Alt Text](url)
