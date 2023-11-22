# Punk

### App url: [app url](https://transcendent-entremet-4b0e75.netlify.app/)

## System Requirements
To run the Punk Application, ensure you have the following installed:

- .NET Core 7 SDK
- Node.js (LTS version)
- A suitable IDE (e.g., Visual Studio, VS Code)

## Clone the Repository:

- git clone [repository-url](https://github.com/Mlrobinson1993/Punk.git)
- cd Punk/Punk.API

## If running locally
-Head to Punk.Frontend/nuxt.config.js
-- Comment out the line labelled (// Comment me out to test live)
-- Uncomment the line labelled (// Uncomment me to test locally)

## Back End

### Restore Dependencies:

#### Navigate to the Punk.API directory and run:

- dotnet restore,
- dotnet build

## Frontend

### Navigate to frontend directory

- cd Punk/Punk.Frontend

### Run the Frontend Application:

- npm install
- npm run dev

# About the app

The Punk Application is a .NET 7.0 Web API with a Nuxt 3 front-end, deployed as a static web app on Netlify (front end), an App Service on Azure (back end) and an Azure SQL database.

#### Note: You may experience some slowness / erroring on DB transactions when you first start up as the sql server warms up.

## Technologies used

#### Backend
- .NET 7.0 - Performant, cross-platform compatible, huge ecosystem.
- Entity Framework Core (ORM) - Industry gold standard, performant, allows for easy database migrations (in this context is very handy).
- NUnit (Testing)

#### Frontend
- Nuxt 3 - Convention over configuration. Allows you to get off of the ground quickly and easily, abstracting away redundant tasks (i.e. routing).
- Pinia (State management) - Allows for modular stores and clean code. Is the gold standard for Nuxt 3 apps.
- SCSS (styling) - Allows for cleaner code with nesting and provides helpers that vanilla CSS does not.


#### Database
- Azure SQL - Quick, easy, out-of-the-box, limited configuration database deployments.

## Design Patterns 

#### Back End
- CQRS (Command Query Responsibility Segregation)
- Mediatr

##### Why?
- Clear separation of commands and queries, resulting in a scalable and maintainable codebase.

#### Front End
- Composables pattern

##### Why? 
- Clear separation and modularisation of code.

