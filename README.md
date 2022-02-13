# Product Catalog Manager Api
.NET 6 REST API for basic product development.

# Requirements
.NET 6 SDK

# Libraries
EntityFrameworkCore
AutoMapper
XUnit
Moq
AppMetrics

# Developer Notes
I added appmetrics to the project, it can easily run with Prometheus and Grafana, but since this project all about API, no external tool used.
I used In Memory Database for easy use and fast development. On production level, I have to change this with SQL or any other database.

# Metrics
I used AppMetrics library to monitor API, since I don't use any external tool like Prometheus and Grafana, I just use console to report it.  You can find the metrics at
https://localhost:5000/metrics
