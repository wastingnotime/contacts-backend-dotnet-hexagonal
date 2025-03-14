FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-stage

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef

WORKDIR /app

COPY ./*.sln ./
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}; done
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out
RUN dotnet test
RUN dotnet ef database update --project WastingNoTime.Contacts.Adapter.SQLite

FROM mcr.microsoft.com/dotnet/aspnet:9.0 as deploy-stage
WORKDIR /app
EXPOSE 8010:8080

COPY --from=build-stage /app/out .

VOLUME data
COPY --from=build-stage /app/contacts.db /data/

ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "WastingNoTime.Contacts.Adapter.Api.dll"]