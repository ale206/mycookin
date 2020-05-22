/// <binding ProjectOpened='watch, default' />
var browserify = require("browserify");
var gulp = require("gulp");
var source = require("vinyl-source-stream");

gulp.task("browserify_angular",
    function() {
        console.log("Starting browserify_angular...");

        return browserify("./app/app.main.js").bundle()
            .on("error",
                function(err) {
                    console.log(err.message);
                    this.emit("end");
                })

            // Desired filename
            .pipe(source("angular_bundle.js"))

            // Output the file
            .pipe(gulp.dest("scripts/"));
    });

gulp.task("browserify_js",
    function() {
        console.log("Starting browserify_js...");

        return browserify("./assets/js/main.js").bundle()
            .on("error",
                function(err) {
                    console.log(err.message);
                    this.emit("end");
                })

            // Desired filename
            .pipe(source("bundle.js"))

            // Output the file
            .pipe(gulp.dest("scripts/"));
    });

gulp.task("default", ["browserify_js", "browserify_angular"]);

gulp.task("watch",
    function() {
        gulp.watch(
            ["app/**/*.js", "assets/**/*.js", "!gulpfile.js", "!scripts/angular_bundle.js", "!scripts/bundle.js"],
            ["default"]);
    });