#!/bin/bash

echo "Attempting deploy: "$TRAVIS_BRANCH

if [ "$TRAVIS_BRANCH" = "master" ] || [ "$TRAVIS_BRANCH" = "dev" ]; then
  dotnet pack "./src/NTwitch/NTwitch.csproj" -c "Release" -o "../../pkgs" --no-build 
  dotnet pack "./src/NTwitch.Core/NTwitch.Core.csproj" -c "Release" -o "../../pkgs" --no-build 
  dotnet pack "./src/NTwitch.Rest/NTwitch.Rest.csproj" -c "Release" -o "../../pkgs" --no-build 
  dotnet nuget push "./pkgs/*.nupkg" "$NUGET_API_KEY" -verbosity detailed
else
  echo "Skipping deploy"
  echo "This branch is not master or dev: "$TRAVIS_BRANCH
fi
