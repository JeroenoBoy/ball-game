package com.jeroenvdg

import io.ktor.server.application.*
import io.ktor.server.html.*
import io.ktor.server.http.content.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import io.ktor.server.websocket.*
import io.ktor.websocket.*
import java.time.Duration
import kotlin.time.Duration.Companion.seconds
import kotlinx.html.*

fun Application.configureRouting() {
    routing {
        // Static plugin. Try to access `/static/index.html`
        staticResources("/static", "static")
    }
}
