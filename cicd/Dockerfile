FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# DotnetExamplesApi start

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS webapi-build

WORKDIR /src

COPY ["../src/apps/DotnetExamplesApi/DotnetExamplesApi.csproj", "apps/DotnetExamplesApi/"]
RUN dotnet restore "apps/DotnetExamplesApi/DotnetExamplesApi.csproj"
COPY . .

WORKDIR "apps/DotnetExamplesApi"

RUN dotnet build "DotnetExamplesApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM webapi-build AS webapi-publish

RUN dotnet publish "DotnetExamplesApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS webapi-final

WORKDIR /app

COPY --from=webapi-publish /app/publish .

ENTRYPOINT ["dotnet", "DotnetExamplesApi.dll"]

# DotnetExamplesApi end


# DotnetExamples.BlazorApp start

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS blazorapp-build

WORKDIR /src

COPY ["../src/apps/DotnetExamples.BlazorApp/DotnetExamples.BlazorApp.csproj", "apps/DotnetExamples.BlazorApp/"]
RUN dotnet restore "apps/DotnetExamples.BlazorApp/DotnetExamples.BlazorApp.csproj"
COPY . .

WORKDIR "apps/DotnetExamples.BlazorApp"

RUN dotnet build "DotnetExamples.BlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM blazorapp-build AS blazorapp-publish

RUN dotnet publish "DotnetExamples.BlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=blazorapp-publish /app/publish .

ENTRYPOINT ["dotnet", "DotnetExamples.BlazorApp.dll"]

# DotnetExamples.BlazorApp end

