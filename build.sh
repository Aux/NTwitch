#!bin/sh

echo "Building branch: "$TRAVIS_BRANCH
sudo apt-get install dotnet-dev-1.0.1
dotnet restore

if [ "$TRAVIS_BRANCH" = "dev" ]
  dotnet build --configuration Release --version-suffix beta-$TRAVIS_BUILDNUMBER
else
  dotnet build --configuration Release
fi
