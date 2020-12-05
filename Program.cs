using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
           {
               new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   config
                       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                       .AddJsonFile("appsettings.json", true, true)
                       .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                       .AddJsonFile("ocelot.json")
                       .AddEnvironmentVariables();
               })
               .ConfigureServices(s => {
                   s.AddOcelot();
               })
               .ConfigureLogging((hostingContext, logging) =>
               {
                   //add your logging
               })
               .UseIISIntegration()
               .Configure(app =>
               {
                    var configuration = new OcelotPipelineConfiguration
                    {
                        //如果是OPTION一律回CORS
                        PreErrorResponderMiddleware = async (context, next) =>
                        {
                            if(string.Equals(
                                context.Request.Method,
                                "options",
                                StringComparison.InvariantCultureIgnoreCase
                            )){
                                var method = (String)context.Request.Headers["Access-Control-Request-Method"];
                                var headers = (String)context.Request.Headers["Access-Control-Request-Headers"];

                                context.Response.Headers.Add(
                                    "Access-Control-Allow-Methods",
                                    method
                                );
                                context.Response.Headers.Add(
                                    "Access-Control-Allow-Origin",
                                    "*"
                                );
                                context.Response.Headers.Add(
                                    "Access-Control-Allow-Headers",
                                    headers
                                );
                                context.Response.StatusCode = 204;
                                
                            }
                            else
                            {
                               await next.Invoke();
                            }
                            
                        }
                    };

                    app.UseOcelot(configuration);
               })
               .Build()
               .Run();
           }
    }
}
