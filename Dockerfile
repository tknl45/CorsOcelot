FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /code
COPY . .
# 覆蓋設定檔案
#RUN cp appsettings_azure.json appsettings.json

# 相依檔案下載
RUN dotnet restore 

# 佈署檔案
RUN dotnet publish -o /output 


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
COPY --from=build /output /app
WORKDIR /app
EXPOSE 80
# 執行服務
CMD ["dotnet", "ApiGateway.dll"]