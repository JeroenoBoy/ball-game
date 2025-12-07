/**
 * @param wsConnection {import("./ws").WsConnection}
 */
function getOptionsHandler(wsConnection) {
    const code = wsConnection.code
    wsConnection.connection.on("options", data => {
        if (!(data instanceof Array)) {
            console.log(`Connection ${code} gave invalid data`)
            return
        }

        for (let option of data) {
            if (option == null) {
                console.log(`Connection ${code} gave invalid data`)
                return;
            }
            if (typeof option.name !== "string") {
                console.log(`Connection ${code} gave invalid data`)
                return;
            }
            if (typeof option.color !== "string") {
                console.log(`Connection ${code} gave invalid data`)
                return;
            }
        }

        wsConnection.options = data;
    })
}

module.exports.getOptionsHandler = getOptionsHandler;