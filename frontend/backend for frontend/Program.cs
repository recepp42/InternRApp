using backend_for_frontend.Configuration;
using backend_for_frontend.Middelware.BFF;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using ProxyKit;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policies =>
    {
        string[]? allowedOrigins = builder.Configuration.GetSection("Cors").GetValue<string>("AllowedOrigins")?.Split(';').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray() ?? Array.Empty<string>();

        _=policies
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
string[]? timeslotsApiScopes = builder.Configuration.GetValue<string>("Api:Scopes").Split(' ', StringSplitOptions.RemoveEmptyEntries);

//services.AddScoped<CustomCookieAuthenticationEvents>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddMicrosoftIdentityWebApp(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options);

    options.SaveTokens = true;

    options.Scope.Clear();

    // api scopes
    foreach (string? scope in timeslotsApiScopes)
    {
        options.Scope.Add(scope);
    }

    options.Scope.Add("openid");

    // requests a refresh token
    options.Scope.Add("offline_access");

    // Use the authorization code flow
    options.ResponseType = Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectResponseType.CodeIdToken;
    options.AuthenticationMethod = OpenIdConnectRedirectBehavior.FormPost;
    options.UsePkce = true;

}, options =>
{
    options.Cookie.Name = "__Host-bff";
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
    //options.EventsType = typeof(CustomCookieAuthenticationEvents);
})
.EnableTokenAcquisitionToCallDownstreamApi(timeslotsApiScopes)
.AddInMemoryTokenCaches(); // TODO : add redis


builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        AuthorizationPolicy? policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddMicrosoftIdentityUI();

builder.Services.AddProxy();

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration.GetConnectionString("Redis");
//    options.InstanceName = "t";
//});

if (!builder.Environment.IsDevelopment())
{
    // In production, the Angular files will be served from this directory
    builder.Services.AddSpaStaticFiles(configuration =>
    {
        configuration.RootPath = "ClientApp/dist";
        
    });
}

builder.Services.AddHealthChecks();

WebApplication? app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    //TODO
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseRewriter(new RewriteOptions().AddRedirect("/MicrosoftIdentity/Account/SignOut", "/"));

//app.UseBffAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
    endpoints.MapHealthChecks("/health");
});

app.UseProxy(builder.Configuration);

app.UseStaticFiles();

if (!builder.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (builder.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }

    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            // don't cache index.html
            Microsoft.AspNetCore.Http.Headers.ResponseHeaders? headers = ctx.Context.Response.GetTypedHeaders();
            headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
                MustRevalidate = true,
                MaxAge = TimeSpan.Zero
            };
        }
    };
});

app.Run();
