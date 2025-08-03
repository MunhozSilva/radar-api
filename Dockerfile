FROM public.ecr.aws/lambda/dotnet:8 AS base
WORKDIR /var/task
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["radar-api.csproj", "."]
RUN dotnet restore "./radar-api.csproj"
COPY . .
RUN dotnet publish "./radar-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /var/task
COPY --from=build /app/publish .
CMD ["radar-api::radar_api.LambdaEntryPoint::FunctionHandlerAsync"]
