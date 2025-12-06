const { Server } = require("socket.io")
const {getOptionsHandler} = require("./getOptionsHandler");

class WsConnection {
    /** @type {import("socket.io").Socker} */
    connection;
    /** @type {String} */
    code
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
        let code = ""
        do {
            code = createCode()
        } while (module.exports.connections[code] == null)

        const wsConnection = new WsConnection(socket, code)
        module.exports.connections[code] = wsConnection;
        socket.emit("code", code);

        getOptionsHandler(wsConnection)

        socket.on("disconnect", () => {
            module.exports.connections[code] = null;
        })
    })
};

function createCode() {
    const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
    const randomChar = () => chars[Math.floor(Math.random() * chars.length)];
    return `${randomChar()}${randomChar()}${randomChar()}-${randomChar()}${randomChar()}${randomChar()}`;
}