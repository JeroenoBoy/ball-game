/**
 * @param wsConnection {import("./ws").WsConnection}
 */
function getTitleHandler(wsConnection) {
    const code = wsConnection.code
    wsConnection.connection.on("title", data => {
        if (typeof data !== "string") {
            return;
        }
        wsConnection.title = data;
    })
}

module.exports.getTitleHandler = getTitleHandler;