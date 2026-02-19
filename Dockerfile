FROM mcr.microsoft.com/dotnet/nightly/sdk:10.0 as base

WORKDIR /src

COPY ./backend ./

RUN sed -i 's|localhost|db|g' appsettings.json

RUN dotnet publish -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=base /src/publish ./

EXPOSE 5059

ENV ASPNETCORE_URLS=http://+:5059

CMD ["dotnet", "BlogApp.dll"]