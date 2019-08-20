var gulp = require("gulp"),
	config = require("./Gulp/configs/config.json"),
	requireDir = require("require-dir");

requireDir("./Gulp/gulp-tasks");

gulp.task("watch",
	function() {
		gulp.watch(config.scripts.mainMaster, ["main-master-scripts"]);
	});
gulp.task("compile",
[
	"main-master-scripts",
	"main-master-styles"
]);

// The default task 
gulp.task("default", ["compile"]);