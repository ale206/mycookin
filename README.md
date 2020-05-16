# MyCookin API
The international Social Network about recipes

1. Clone the repository
2. Run `dotnet build`
3. Run `docker build --no-cache -t mycookin .`
4. Run `docker run -p 8080:80 -p 8081:443 --name mycookin mycookin`
5. Open the browser at `http://localhost:8080/swagger/index.html`
6. To stop the application run `docker stop mycookin`
7. To remove the container run `docker rm mycookin`
