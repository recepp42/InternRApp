using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using ProxyKit;
using System.Net;
using System.Net.Http.Headers;

namespace backend_for_frontend.Configuration
{
    public static class ProxyConfiguration
    {
        public static IApplicationBuilder UseProxy(this IApplicationBuilder app, IConfiguration configuration)
        {
            string? timeslotsApiBaseUrl = configuration.GetValue<string>("Api:BaseUrl");
            string[]? timeslotsApiScopes = configuration.GetValue<string>("Api:Scopes").Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return app.Map("/api", true, api =>
            {
                api.RunProxy(async context =>
                {
                    try
                    {
                        var path = context.Request.Headers["path"].ToString();
                        if (path[path.Length - 1] == '&')
                            path = path.Substring(0, path.Length - 1);
                        if (path.StartsWith("/"))
                            path=path.Substring(1);
                        context.Request.Path = "";
                        var queryStrings=path.Split("?");
                        ForwardContext? forwardContext = null;
                        if(queryStrings.Length<=1)
                            forwardContext = context.ForwardTo($"{timeslotsApiBaseUrl}/{path}");
                        else
                        {

                            //https://localhost:7171/api/Unit/?PageIndex=1&PageSize=10
                            forwardContext = context.ForwardTo($"{timeslotsApiBaseUrl}");
                            var uri = $"{timeslotsApiBaseUrl}/{path}";
                            forwardContext.UpstreamRequest.RequestUri = new Uri(uri);
                            //forwardContext.UpstreamRequest.RequestUri = new Uri("https://localhost:7171/api/Unit/?PageIndex=1&PageSize=10");

                        }
                        
                        // ITokenAcquisition? tokenAcquisition = context.RequestServices.GetRequiredService<ITokenAcquisition>();
                        //string? token = await tokenAcquisition.GetAccessTokenForUserAsync(timeslotsApiScopes, authenticationScheme: OpenIdConnectDefaults.AuthenticationScheme);

                        // forwardContext.UpstreamRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


                        if (!string.IsNullOrEmpty(context.Request.ContentType) && forwardContext.UpstreamRequest.Content == null)
                        {
                            forwardContext.UpstreamRequest.Content = new ByteArrayContent(new byte[0]);
                            forwardContext.UpstreamRequest.Content.Headers.ContentType = new MediaTypeHeaderValue(context.Request.ContentType);
                        }
                        var method= context.Request.Headers["method"].ToString() switch
                        {
                            "POST" => HttpMethod.Post,
                            "PATCH" => HttpMethod.Patch,
                            "DELETE" => HttpMethod.Delete,
                            "GET"=>HttpMethod.Get,
                            "PUT"=>HttpMethod.Put,
                            _ => throw new NotImplementedException()
                        };
                        forwardContext.UpstreamRequest.Method=method;
                        return await forwardContext.Send();
                    }
                    catch (MsalUiRequiredException)
                    {
                        HttpResponseMessage response = new(HttpStatusCode.Unauthorized)
                        {
                            Content = new StringContent("An error has occured")
                        };

                        return response;
                    }
                });
            });
        }
    }
}
