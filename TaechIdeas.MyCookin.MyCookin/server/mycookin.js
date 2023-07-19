const PORT = 8080;
const ROOT = "/home/dave/source/mycookin/MyCookin.Website/";

var express = require("express");
var app = express();

// Homepage
app.get("/",
    function(request, response) {
        response.sendFile("index.html", { root: ROOT });
    });

app.get("/recipe",
    function(request, response) {
        response.sendFile("recipe.html", { root: ROOT });
    });

// Statics
app.use("/scripts", express.static(ROOT + "scripts"));
app.use("/app", express.static(ROOT + "app"));
app.use("/assets", express.static(ROOT + "assets"));

app.listen(PORT,
    function() {
        //Callback triggered when server is successfully listening. Hurray!
        console.log("Server listening on: http://localhost:%s", PORT);
    });