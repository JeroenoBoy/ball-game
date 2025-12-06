const express = require("express");
const {connections} = require("../ws/ws");
const router = express.Router();

router.get("/", (req, res) => {
    res.redirect("/")
});

router.get("/:id", (req, res) => {
    if (connections[req.params.id] == null) {
        return res.redirect("/");
    }

    if (connections.options == null || connections.options.size === 0) {
        return res.redirect("/");
    }

    res.render("game", {
        title: req.params.id + " | Balls",
        options: connections[req.params.id],
    })
})

router.post("/:id", (req, res) => {
    if (connections[req.params.id] == null) {
        return res.redirect("/");
    }

    if (connections.options == null || connections.options.size === 0) {
        return res.redirect("/");
    }

    const connection = connections[req.params.id];
    const option = req.body.options;
    let found = false
    for (let o in connection.options) {
        if (option === o) {
            found = true
            break
        }
    }

    if (!found) {
        res.redirect("/game/"+req.params.id);
    }

    connection.addOption(req.userId, req.body.options);
})

module.exports = router;
