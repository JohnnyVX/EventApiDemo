#!/bin/bash

# List of directories and files to delete
items_to_delete=(
    "Controllers"
    "Services"
    "EventApiDemo.csproj"
    "EventApiDemo.sln"
    "EventApiDemo.http"
    "Program.cs"
    "Properties"
    "obj"
    "SetupScripts/New-ApiDemo.ps1"
    "SetupScripts/Set-Program.ps1"
    "SetupScripts/Set-EventService.ps1"
    "SetupScripts/Set-EventController.ps1"
    "appsettings.Development.json"
    "appsettings.json"
    "scripts"
)

# Loop through each item and delete it
for item in "${items_to_delete[@]}"; do
    if [ -e "$item" ]; then
        echo "Deleting $item..."
        rm -rf "$item"
    else
        echo "$item does not exist."
    fi
done

echo "Cleanup completed."