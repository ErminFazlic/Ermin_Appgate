# How to Deploy the Backend Locally

This guide provides step-by-step instructions to deploy the backend project locally. The backend is a .NET 8 application, so ensure you have the required tools and dependencies installed.

---

## Prerequisites

Before starting, ensure you have the following installed:
- **.NET 8 SDK**: [Download here](https://dotnet.microsoft.com/).
- **SQL Server**: Install a local or cloud-based SQL Server instance.
- **Postman or cURL**: For testing API endpoints (optional).

---

## Steps to Deploy

### 1. Clone the Repository
Clone the repository to your local machine.

### 2. Publish the Application
1. **Restore Dependencies**:
   Run the following command to restore NuGet packages: 'dotnet restore'
2. **Build the Project**:
   Build the project to ensure there are no compilation errors: 'dotnet build'
4. **Publish the Project**:
   Publish the project: 'dotnet publish'
3. **Run the Application**:
   Start the application using: 'dotnet run' or starting the .exe from the publish folder



# How to Deploy the Frontend Locally

This guide provides step-by-step instructions to deploy the frontend project locally. The frontend is built using React and Vite.

---

## Prerequisites

Before starting, ensure you have the following installed:
- **Node.js**: [Download here](https://nodejs.org/). Ensure you have a version compatible with the project.
- **npm** or **yarn**: Comes with Node.js. Verify installation by running 'npm -v' or 'yarn -v' in your terminal.

---


## Steps to Deploy

### 1. Clone the Repository
Clone the repository to your local machine

### 2. Install Dependencies
Install the required dependencies for the frontend project: 'npm install'

### 3. Run the Development Server
Start the development server to preview the application locally: 'npm run dev'

### 4. Build the Project for Production
To create an optimized production build, run: 'npm run build'

### 5. Preview the Production Build
To preview the production build locally, use Vite's preview command: 'npm run preview'
