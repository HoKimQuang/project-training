FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ProjectTraining/*.csproj ./ProjectTraining/
COPY ProjectTraningxUnitTest/*.csproj ./ProjectTraningxUnitTest/
RUN dotnet restore

# copy everything else and build app
COPY ProjectTraining/. ./ProjectTraining/
COPY ProjectTraining/. ./ProjectTraningxUnitTest/
WORKDIR /app/ProjectTraining
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/ProjectTraining/out ./
ENTRYPOINT ["dotnet", "ProjectTraining.dll"]
