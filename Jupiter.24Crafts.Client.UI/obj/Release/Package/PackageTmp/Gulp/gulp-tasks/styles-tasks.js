// Loads the modules and plugins
var gulp = require("gulp"),
	concat = require("gulp-concat"),
	sourcemaps = require("gulp-sourcemaps"),
	cleanCSS = require("gulp-clean-css"),
	config = require("../configs/config.json"),
	base64 = require('gulp-base64');


// Minifies and bundles all css files into the output file.
function compileStyles(sourcePath, outputFile) {
	return gulp.src(sourcePath)
		.pipe(sourcemaps.init())
		.pipe(cleanCSS({ processImport: false }))
		.pipe(concat(outputFile))
		.pipe(base64())
		.pipe(sourcemaps.write("."))
		.pipe(gulp.dest(config.buildDest.css));
}

// Define the "style" tasks
gulp.task("client-main-master-styles",
	function () {
		compileStyles(config.styles.clientMainMaster, "client-main-master.min.css");
	});