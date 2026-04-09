#!/bin/bash
set -e

echo "Publishing Ironfrost.Authentication..."
dotnet publish Ironfrost.Authentication/Ironfrost.Authentication.csproj -c Release -o Ironfrost.Authentication/publish

echo "Publishing Ironfrost.GameService..."
dotnet publish Ironfrost.GameService/Ironfrost.GameService.csproj -c Release -o Ironfrost.GameService/publish

echo "Publishing Ironfrost.PlayerService..."
dotnet publish Ironfrost.PlayerService/Ironfrost.PlayerService.csproj -c Release -o Ironfrost.PlayerService/publish

echo "Starting Docker Compose..."
docker compose up --build "$@"
