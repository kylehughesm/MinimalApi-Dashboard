# Dashboard App (Weather + News)

A full-stack dashboard application built with ASP.NET Core, Blazor, and Docker, using a microservices + gateway architecture.

Features
Weather lookup by ZIP + country
Latest news by country
Parallel service calls (gateway aggregation)
Dockerized backend services

Gateway aggregates data from multiple services
Each microservice handles its own API + error logic
Gateway normalizes responses into a single contract

Tech Stack
Frontend: Blazor WebAssembly
Backend: ASP.NET Core (Minimal APIs)
Containerization: Docker + Docker Compose

# How to Run

1. Start backend services (Docker)

From the root project directory in the terminal:

    docker compose up --build

This starts:
Gateway API
Weather microservice
News microservice

2. Start the Blazor App

cd into Dashboard folder then run:

    dotnet run --launch-profile https

then in browser navigate to:

    http://localhost:5098/dashboard

*Currently, port 5098 is the only working frontend port.
Other ports shown in the terminal may not function correctly.*
