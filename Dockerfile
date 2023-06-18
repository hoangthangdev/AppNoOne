FROM node:18.15.0 AS buildvue
WORKDIR /app
COPY AppNoOne/package*.json ./
RUN npm install
COPY AppNoOne .
RUN npm run dev

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS buildnet
WORKDIR /src
COPY ["AppNoOne/AppNoOne.csproj", "AppNoOne/"]
COPY ["DTO/DTO.csproj", "DTO/"]
COPY ["Models/Models.csproj", "Model/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "AppNoOne/AppNoOne.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AppNoOne/AppNoOne.csproj" -c Release -o /app/build

FROM buildvue AS publishvue
RUN npm install -g serve
WORKDIR /app/AppNoOne
RUN npm run dev

FROM buildnet AS publishnet
RUN dotnet publish "AppNoOne/AppNoOne.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publishnet /app/publish .
COPY --from=publishvue /app/dist ./dist
EXPOSE 8081:80
ENTRYPOINT ["dotnet", "AppNoOne.dll"]
