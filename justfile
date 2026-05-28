# https://just.systems

publish:
    dotnet publish -c Release -r linux-x64

run:
    dotnet run --project src/Shy

build:
    dotnet build

test:
    dotnet test