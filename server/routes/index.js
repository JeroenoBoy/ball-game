const express = require("express");
const router = express.Router();

/* GET home page. */
router.get("/", (req, res, next) => {
    res.render("index", {title: "Balls"});
});

router.post("/", (req, res, next) => {
    if (req.body.code == null || typeof req.body.code !== "string") {
        res.redirect("/");
    } else {
        res.redirect("/game/"+req.body.code.toUpperCase());
    }
})

module.exports = router;
