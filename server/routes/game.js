const express = require("express");
const QRCode = require("qrcode");
const {connections} = require("../ws/ws");
const router = express.Router();

router.get("/", (req, res) => {
    res.redirect("/")
});

router.get("/:id", (req, res) => {
    const connection = connections[req.params.id];
    if (connection == null) {
        return res.redirect("/");
    }

    if (connection.options == null || connection.options.length === 0) {
        return res.redirect("/");
    }

    res.render("game", {
        title: req.params.id + " | Balls",
        headerText: connection.title,
        options: connection.options,
    })
})

router.get("/:id/code", async (req, res) => {
    const connection = connections[req.params.id];
    if (connection == null) {
        return res.redirect("/");
    }

    res.status(200).send(await QRCode.toBuffer("https://balls.jeroenvdg.com/game/"+connection.code));
})

router.post("/:id", (req, res) => {
    const connection = connections[req.params.id];
    if (connection == null) {
        return res.redirect("/");
    }

    if (connection.options == null || connection.options.length === 0) {
        return res.redirect("/");
    }

    const option = req.body.option;
    let found = false
    for (let o of connection.options) {
        if (option === o.name) {
            found = true
            break
        }
    }

    if (!found) {
        return res.redirect("/game/"+req.params.id);
    }

    connection.playerJoin(req.userId, option);
    return res.render("gameConfirmed", {
        headerText: connection.title,
        option: option
    })
})

module.exports = router;
