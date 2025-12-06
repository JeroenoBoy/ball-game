const express = require("express");
const {connections} = require("../ws/ws");
const router = express.Router();

router.get("/", (req, res) => {
    res.redirect("/")
});

router.get("/test/:id", (req, res) => {
    res.render("game", {
        title: "test | Balls",
        options: [
            { name: "Table Top Sim", color: "#ff0022" },
            { name: "Oh no Anyway", color: "#ff00ff" },
            { name: "1234", color: "#ffaa22" },
            { name: "pee", color: "#00ff22" },
        ],
    })
})

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

module.exports = router;
