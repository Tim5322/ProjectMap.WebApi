//todo: Claims bij builder.Services.AddAuthorization

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using ProjectMap.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<RouteOptions>(o => o.LowercaseUrls = true);

var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

//builder.Services.AddTransient<Object2DRepository, Object2DRepository>(o => new Object2DRepository(sqlConnectionString));
//builder.Services.AddTransient<Environment2DRepository, Environment2DRepository>(o => new Environment2DRepository(sqlConnectionString));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("profielKeuzeId", policy =>
    {
        policy.RequireClaim("Profiel");
        policy.RequireClaim("Profielkeuze", "8"); //Ik weet niet wat ik in de positie van die 8 moet zetten.
    });
});

builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddDapperStores(options =>
    {
        options.ConnectionString = sqlConnectionString;
    });

// Adding the HTTP Context accessor to be injected. This is needed by the AspNetIdentityUserRepository
// to resolve the current user.
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();
builder.Services.AddTransient<IProfielKeuzeRepository, ProfielKeuzeRepository>(o => new ProfielKeuzeRepository(sqlConnectionString));
builder.Services.AddTransient<IDagboekRepository, DagboekRepository>(o => new DagboekRepository(sqlConnectionString));
builder.Services.AddTransient<IAgendaRepository, AgendaRepository>(o => new AgendaRepository(sqlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGet("/", () => $"The API is up. Connection string found: {(sqlConnectionStringFound ? "Yes" : "No")}");

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGroup("/account")
    .MapIdentityApi<IdentityUser>();

app.MapControllers().RequireAuthorization();

app.Run();



