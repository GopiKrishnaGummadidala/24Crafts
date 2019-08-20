// Loads the modules and plugins
var gulp = require("gulp"),
	concat = require("gulp-concat"),
	uglify = require("gulp-uglify"),
	sourcemaps = require("gulp-sourcemaps"),
	config = require("../configs/config.json");

// Minifies and bundles all JavaScript files into the output file with sourcemaps.
function compileScripts(sourcePath, outputFile) {
	return gulp.src(sourcePath)
		.pipe(sourcemaps.init())
		.pipe(uglify())
		.pipe(concat(outputFile))
		.pipe(sourcemaps.write("."))
		.pipe(gulp.dest(config.buildDest.js));
}


// Define the "script" tasks
gulp.task("client-main-master-scripts",
	function() {
	    compileScripts(config.scripts.clientMainMaster, "client-main-master.min.js");
	});

//gulp.task("admin-sms-scripts",
//	function() {
//	    compileScripts(config.scripts.orderStatus, "admin-sms.min.js");
//	});

