#!/bin/bash
set -e

echo "Publishing Icefrost.Authentication..."
dotnet publish Icefrost.Authentication/Icefrost.Authentication.csproj -c Release -o Icefrost.Authentication/publish

echo "Publishing Icefrost.GameService..."
dotnet publish Icefrost.GameService/Icefrost.GameService.csproj -c Release -o Icefrost.GameService/publish

echo "Publishing Icefrost.PlayerService..."
dotnet publish Icefrost.PlayerService/Icefrost.PlayerService.csproj -c Release -o Icefrost.PlayerService/publish

echo "Starting Docker Compose..."
docker compose up --build "$@"
