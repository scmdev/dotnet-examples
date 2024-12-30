FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# DotnetExamples.WebApi start

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS webapi-build

WORKDIR /src

COPY ["../src/apps/DotnetExamples.WebApi/DotnetExamples.WebApi.csproj", "apps/DotnetExamples.WebApi/"]
RUN dotnet restore "apps/DotnetExamples.WebApi/DotnetExamples.WebApi.csproj"
COPY . .

WORKDIR "apps/DotnetExamples.WebApi"

RUN dotnet build "DotnetExamples.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM webapi-build AS webapi-publish

RUN dotnet publish "DotnetExamples.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS webapi-final

WORKDIR /app

COPY --from=webapi-publish /app/publish .

ENTRYPOINT ["dotnet", "DotnetExamples.WebApi.dll"]

# DotnetExamples.WebApi end


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

