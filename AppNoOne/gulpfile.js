const gulp = require('gulp');
const rename = require("gulp-rename");
const sass = require('gulp-sass')(require('sass'));
const cssmin = require("gulp-cssmin");
const uglify = require('gulp-uglify');
const { spawn } = require('child_process');
var fs = require('fs');
var path = require('path');
var filePath = path.join(__dirname, '.gulp/'); // or any other directory you want to create
if (!fs.existsSync(filePath)) {
    fs.mkdirSync(filePath);
}

var paths = {
    scss: "./Assets/scss/**/*.scss",
    minCss: "./wwwroot/css/min/",

    //js setting
    js: "./Assets/js/**/*.js",
    minJs: "./wwwroot/js/",


};

gulp.task('sass', function () {
    return gulp.src(paths.scss)
        .pipe(sass()) // Xử lý Sass thành CSS
        .pipe(cssmin()) // Nén CSS
        .pipe(rename({ suffix: ".min" })) // Đổi tên tệp
        .pipe(gulp.dest(paths.minCss)); // Lưu CSS đã nén vào thư mục đích
});

gulp.task('uglify', function () {
    return gulp.src(paths.js)
        .pipe(uglify()) // Nén JavaScript
        .pipe(rename({ suffix: ".min" })) // Đổi tên tệp
        .pipe(gulp.dest(paths.minJs)); // Lưu JavaScript đã nén vào thư mục đích
});

gulp.task('watch', function () {
    gulp.watch(paths.scss, gulp.series('sass')); // Theo dõi các tệp SCSS và chạy task 'sass' khi có thay đổi
    gulp.watch(paths.js, gulp.series('sass')); // Theo dõi các tệp JS và chạy task 'sass' khi có thay đổi
});

gulp.task('webpack', function (done) {
    const webpackProcess = spawn(' C:\\Users\\helo\\AppData\\Roaming\\npm\\npx.cmd', ['webpack', '--config=webpack.config.js'], { stdio: 'inherit' });

    webpackProcess.on('close', function (code) {
        if (code === 0) {
            done();
        } else {
            done('Webpack process exited with code ' + code);
        }
    });
});

gulp.task('default', gulp.series('sass', 'uglify', 'webpack', 'watch'));