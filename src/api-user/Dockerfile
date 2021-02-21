# multi staged building

FROM mcr.microsoft.com/dotnet/sdk:5.0.103-focal AS builder

WORKDIR /src

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Release

RUN dotnet publish --configuration Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0.3-alpine3.13

WORKDIR /app

COPY --from=builder /app .

EXPOSE 5000

ENTRYPOINT [ "dotnet", "api-user.dll" ]
