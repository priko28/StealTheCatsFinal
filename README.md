# Web API project

## Overview

StealTheCats is a Web API application designed to communicate with a database using Docker. This project allows users to fetch, store, and manage cat-related data efficiently through a RESTful API.
It is built with scalability using containers, making it easy to deploy and manage in different environments. By leveraging Docker, the application ensures database connectivity and improved maintainability.
- **TheCatAPI**


This project provides an efficient way to manage data through RESTful API endpoints.

---

## Base URL

https://localhost:44325


---

## Authentication

All requests to the API must be authenticated using an API key. You can obtain the necessary API key by registering on the following developer portals:

- [TheCatAPI](https://thecatapi.com/)

---

#### **API Endpoints**

| Method | Endpoint | Description                          |
|--------|----------|--------------------------------------|
| POST | /api/cats/fetch       | Fetch and save cat data from an external source |
| GET | /api/cats/{id}       | Sort results by a specific attribute, such as `source` or `date`. |
| GET | /api/cats       | Get a paginated list of cats |
| GET | /api/cats/by-tag       | Get cats filtered by a specific tag |

#### **Sample Request**

```http
POST https://localhost:44325/api/images/search?limit=25&has_breeds=1
```

#### **Installation**

Open the docker-compose.yml file. Copy the file content locally. At the application service section "stealthecatsfinal" replace the image with the docker hub image "priko/stealthecatsfinal". 
Remove the build section as the docker image is already compiled.
Save the file locally as docker-compose.yml.
Open command prompt, move to the location of the file and run "docker-compose up -d"
