#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TodoApp.WebAPI/TodoApp.WebAPI.csproj", "TodoApp.WebAPI/"]
COPY ["TodoApp.BLL/TodoApp.BLL.csproj", "TodoApp.BLL/"]
COPY ["TodoApp.DAL/TodoApp.DAL.csproj", "TodoApp.DAL/"]
COPY ["TodoApp.Entities/TodoApp.Entities.csproj", "TodoApp.Entities/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ./*.sln .
RUN dotnet restore --disable-parallel
COPY . .
WORKDIR "/src/TodoApp.WebAPI"
RUN dotnet build "TodoApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApp.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApp.WebAPI.dll"]
