const { Server } = require("socket.io")
const {getOptionsHandler} = require("./getOptionsHandler");
const {getTitleHandler} = require("./getTitleHandler");

class WsConnection {
    /** @type {import("socket.io").Socker} */
    connection;
    /** @type {String} */
    code
    /** @type {String} */
    title
    /** @type {{name: String, color: String}[]} */
    options = []

    /**
     * @param {import("socket.io").Socket} connection
     * @param {String} code
     */
    constructor(connection, code) {
        this.connection = connection;
        this.code = code;
    }

    /**
     * @param {String} user
     * @param {String} option
     */
    playerJoin(user, option) {
        this.connection.emit("playerJoin", {user, option});
    }
}

module.exports.WsConnection = WsConnection;

/**
 * @type {{[key: String]: WsConnection}}
 */
module.exports.connections = {}

/**
 * @param {import("express").Express} app
 * @param {import("node:http").Server} httpServer
 */
module.exports.createWsServer = (app, httpServer) => {

    const io = new Server(httpServer);
    io.on("connection", (socket) => {
        console.log(`Client connecting with id ${socket.id}`)

        let code = ""
        do {
            code = createCode()
        } while (module.exports.connections[code]);

        const wsConnection = new WsConnection(socket, code)
        module.exports.connections[code] = wsConnection;

        socket.emit("code", code);

        getOptionsHandler(wsConnection)
        getTitleHandler(wsConnection)

        socket.on("disconnect", () => {
            delete module.exports.connections[code];
        })
    })
};

function createCode() {
    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    const randomChar = () => chars[Math.floor(Math.random() * chars.length)];
    return `${randomChar()}${randomChar()}${randomChar()}-${randomChar()}${randomChar()}${randomChar()}`;
}