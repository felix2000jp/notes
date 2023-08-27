FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /server-dotnet
COPY . . 
RUN dotnet restore
RUN dotnet publish -c release -o out


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /server-dotnet
COPY --from=build /server-dotnet/out .

ENTRYPOINT [ "dotnet", "Api.dll" ]