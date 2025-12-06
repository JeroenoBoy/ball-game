const createError = require("http-errors");
const express = require("express");
const path = require("path");
const cookieParser = require("cookie-parser");
const logger = require("morgan");
const http = require("node:http");
const { createWsServer } = require("./ws/ws")

const indexRouter = require("./routes/index");
const gameRouter = require("./routes/game");

module.exports = function createServer() {
    const app = express();
    const httpServer = http.createServer(app)

    // view engine setup
    app.set("views", path.join(__dirname, "views"));
    app.set("view engine", "hbs");

    app.use(logger("dev"));
    app.use(express.json());
    app.use(express.urlencoded({ extended: false }));
    app.use(cookieParser());
    app.use(express.static(path.join(__dirname, "public")));

    app.use((req, res, next) => {
        if (req.cookies == null || req.cookies.identity == null) {
            const id = createUserId()
            res.cookie("identity", id);
            req.userId = id
        } else {
            req.userId = req.cookies.identity;
        }
        next()
    });

    app.use("/", indexRouter);
    app.use("/game", gameRouter);

    createWsServer(app, httpServer)

    // catch 404 and forward to error handler
    app.use((req, res, next) => {
        next(createError(404));
    });

    // error handler
    app.use((err, req, res, next) => {
        // set locals, only providing error in development
        res.locals.message = err.message;
        res.locals.error = req.app.get("env") === "development" ? err : {};

        // render the error page
        res.status(err.status || 500);
        res.render("error");
    });

    return {app, server: httpServer}
}

function createUserId() {
    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz_@!"
    const randomChar = () => chars[Math.floor(Math.random() * chars.length)];
    let id = ""
    for (let i = 0; i < 32; i++) {
        id += randomChar()
    }
    return id;
}
