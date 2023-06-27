using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.API.Extensions;
using RestaurantManagement.API.Middlewares;
using RestaurantManagement.Application;
using RestaurantManagement.Persistence;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.
builder.Services.AddApplicationServicesAsync();
builder.Services.AddPersistenceServicesAsync(builder.Configuration);

builder.Services.AddControllers(conf =>
{
}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
.AddOData(conf =>
{
    conf.EnableQueryFeatures();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddResponseCaching();

builder.Services.AddLogging();

builder.Services.AddHealthChecks();

//builder.Services.AddScoped<LoginUser>();

//builder.Services.AddHostedService<DateTimeLogWriter>();

//builder.Services.AddHostedService<LogWriter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseMiddleware<RequestResponseMiddleware>();

app.UseHealthCheckExtension();

app.UseResponseCaching();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();