# Create a new .NET Core Web API project
dotnet new webapi -n EventApiDemo

# Navigate to the newly created project directory
cd .\EventApiDemo\

# Create the necessary directories and files
New-Item -ItemType Directory -Path .\Controllers\
New-Item -ItemType Directory -Path .\Services\
New-Item -ItemType File -Path .\Controllers\EventController.cs
New-Item -ItemType File -Path .\Services\EventService.cs

. .\Set-Program.ps1
. .\Set-EventController.ps1

# Initialize a new Git repository
git init

# Set your Git credentials
git config user.name "johnnyvreply"
git config user.email "johnnyvreply@gmail.com"

# Add all files to the Git repository
git add .

# Commit the files
git commit -m "Initial commit"

# Add your remote repository
# Replace 'your-remote-repository-url' with the URL of your remote repository
git remote add origin "https://github.com/JohnnyVReply/EventApiDemo"

# Push the changes to the remote repository
git branch -m master main
git push -u origin main

# Open the project in Visual Studio Code
code .

# Build and run the application
dotnet build
dotnet run

# Open the browser and navigate to localhost to test the API
start "http://localhost:5000"