using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Newtonsoft.Json.Serialization;
using ACDC2022.Data;
using ACDC2022.Models.Auth;
using ACDC2022.Services;
using ACDC2022.Repositories;
using ACDC2022.Hubs;

var builder = WebApplication.CreateBuilder(args);

#region Session Coockie

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = Session.ApiSession;
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

#endregion

#region Azure KeyVault

if (!builder.Environment.IsDevelopment())
{
    var keyVaultName = builder.Configuration.GetSection("Azure").GetSection("KeyVault").GetValue(typeof(string), "Name");
    builder.Configuration.AddAzureKeyVault(new Uri($"https://{keyVaultName}.vault.azure.net/"), new DefaultAzureCredential());
}

#endregion

#region Cors Settings

builder.Services.AddCors(options =>
{
    var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? new string[0];
    options.AddPolicy("CorsPolicy", policy => policy
        .WithOrigins(allowedOrigins)
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetPreflightMaxAge(TimeSpan.FromDays(7)));
});

#endregion

#region DB Context

var endPoint = builder.Configuration.GetSection("Azure").GetSection("CosmosDb").GetValue(typeof(string), "EndPoint").ToString();
var accountKey = builder.Configuration.GetSection("Azure").GetSection("CosmosDb").GetValue(typeof(string), "AccountKey").ToString();
var dataBaseName = builder.Configuration.GetSection("Azure").GetSection("CosmosDb").GetValue(typeof(string), "DatabaseName").ToString();

builder.Services.AddDbContext<ACDC2022DbContext>(options => options.UseCosmos(endPoint, accountKey, databaseName: dataBaseName));

#endregion

#region Controllers

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ContractResolver = new DefaultContractResolver());

#endregion

#region SignalR

builder.Services.AddSignalR().AddAzureSignalR();

#endregion

#region Custom Services And Repositories

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITelemetryService, TelemetryService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITelemetryRepository, TelemetryRepository>();

#endregion

#region Application Configuration

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseRouting();
app.UseFileServer();
app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<TelemetryHub>("/telemetry");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

#endregion