# Basic Authentication API

## Overview
This implementation only provides endpoints for user creation, token refresh, user login, and user logout. Token validation is not setup so created tokens are not in anyway validated. 
The implementation uses an in-memory database and can be ran directly after package restoration.

## Authentication

### Authenticate User and Generate JWT
Authenticates a user and generates a JSON Web Token (JWT) for authorization.

- **Endpoint:** `/api/v1/auth/login`
- **Method:** `POST`
- **Request Body:**
    - Content Type: `application/json`
    - Schema:
        ```json
        {
            "username": "string",
            "password": "string"
        }
        ```
- **Responses:**
    - `201 Created`: Authentication successful, JWT generated.
    - `400 Bad Request`: Invalid request body or authentication failed.

### Generate New Access and Refresh Tokens
Generates new access and refresh tokens for a user.

- **Endpoint:** `/api/v1/auth/refresh`
- **Method:** `POST`
- **Request Body:**
    - Content Type: `application/json`
    - Schema:
        ```json
        {
            "username": "string",
            "refreshToken": "string"
        }
        ```
- **Responses:**
    - `201 Created`: New access and refresh tokens generated.
    - `400 Bad Request`: Invalid request body or token refresh failed.

### Logout User
Logs out a user by invalidating the provided token.

- **Endpoint:** `/api/v1/auth/logout`
- **Method:** `POST`
- **Request Body:**
    - Content Type: `application/json`
    - Schema:
        ```json
        {
            "username": "string",
            "token": "string"
        }
        ```
- **Responses:**
    - `201 Created`: User successfully logged out.
    - `400 Bad Request`: Invalid request body or logout failed.

## User

### Create a New User
Creates a new user with the provided username and password.

- **Endpoint:** `/api/v1/user`
- **Method:** `POST`
- **Request Body:**
    - Content Type: `application/json`
    - Schema:
        ```json
        {
            "username": "string",
            "password": "string"
        }
        ```
- **Responses:**
    - `201 Created`: User created successfully.
    - `400 Bad Request`: Invalid request body or user creation failed.
