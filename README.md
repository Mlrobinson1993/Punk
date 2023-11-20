# Punk



# Introduction
The Punk Application is a .NET 7.0 Web API with a Nuxt 3 front-end application.

## System Requirements
To run the Punk Application, ensure you have the following installed:

.NET Core 7 SDK
Node.js (LTS version)
A suitable IDE (e.g., Visual Studio, VS Code)
Getting Started
Setting up the Backend (.NET 7.0 Web API)

## Clone the Repository:

git clone [repository-url](https://github.com/Mlrobinson1993/Punk.git)
cd Punk/Punk.API

## Back End

### Restore Dependencies:

Navigate to the Punk.API directory and run:

dotnet restore
dotnet build

### **Run the application with HTTPS configuration**


## Frontend

### Navigate to frontend directory

cd Punk/Punk.Frontend

### Install Node Modules:

npm install

### Check env 

Ensure that BASE_URL in .env is pointing to the correct back end url 

#### Ensure it is HTTPS

### Run the Frontend Application:
npm run dev
