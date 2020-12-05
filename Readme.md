# Ocelot with CORS
使用 Ocelot 當 api gateway ， 使用OcelotPipelineConfiguration 攔截 Option協定，給定 header `Access-Control-Allow-Methods`, `Access-Control-Allow-Origin`, `Access-Control-Allow-Headers` 處理後端介接API CORS問題

## 版本
Ocelot 16.0.1

## 使用方式
* 修改ocelot.json
> * DownstreamScheme 後端Scheme
> * DownstreamHostAndPorts  後端host與port
> * DownstreamPathTemplate 後端網址
> * UpstreamPathTemplate 接收端 網址
> * UpstreamHttpMethod 接收端Method

* 測試
> dotnet run

## 參考資料
* https://docs.microsoft.com/zh-tw/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot
* https://github.com/ThreeMammals/Ocelot#readme
* https://www.cnblogs.com/weihanli/p/config-cors-in-ocelot.html