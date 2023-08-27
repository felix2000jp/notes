FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . . 
RUN dotnet restore
RUN dotnet publish -c release -o out


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "Api.dll" ]