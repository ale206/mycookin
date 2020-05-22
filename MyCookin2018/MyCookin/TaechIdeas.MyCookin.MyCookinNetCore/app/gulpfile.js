var browserify = require("browserify");
var gulp = require("gulp");
var source = require("vinyl-source-stream");

gulp.task("browserify_angular",
    function() {
        console.log("Starting browserify_angular...");

        return browserify("./app.main.js").bundle()
            // vinyl-source-stream makes the bundle compatible with gulp
            .pipe(source("angular_bundle.js")) // Desired filename
            // Output the file
            .pipe(gulp.dest("../scripts/"));
    });

gulp.task("browserify_js",
    function() {
        console.log("Starting browserify_js...");

        return browserify("../assets/js/main.js").bundle()
            // vinyl-source-stream makes the bundle compatible with gulp
            .pipe(source("bundle.js")) // Desired filename
            // Output the file
            .pipe(gulp.dest("../scripts/"));
    });

gulp.task("watch",
    function() {
        gulp.watch(["./**/*.js", "!./scripts/angular_bundle.js", "!./scripts/angular_bundle.js", "!./gulpfile.js"],
            ["browserify_angular", "browserify_js"]);

        //Other watchers.
    });