using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Search.API.Middlewares;
using Search.Application.Commands;
using Search.Application.Handlers;
using Search.Application.Queries;
using Search.Domain.Dto;
using Search.Domain.Entities;
using Search.Domain.Interfaces;
using Search.Infrastructure;
using Search.Infrastructure.Repositories;
using Search.Infrastructure.Services;
using System;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var secretKey = configuration["JWT:Secret"];
var validIssuer = configuration["JWT:ValidIssuer"];
var expiryInMinutes = configuration["JWT:ExpiryInMinutes"];

builder.Services.AddDbContext<BookingDBContext>(opt => opt.UseInMemoryDatabase("BookingDB"));
foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}
builder.Services.AddScoped<IRequestHandler<CreateBookingCommandRequest>, CreateBookingCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetBookingListQuery, List<Booking>>, GetBookingListQueryHandler>();

builder.Services.AddScoped<IRequestHandler<CreateFlightCommandRequest, Flight>, CreateFlightCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetFlightListQuery, List<Flight>>, GetFlightListQueryHandler>();

builder.Services.AddScoped<IRequestHandler<CreateUserCommandRequest, User>, CreateUserCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetUserListQuery, List<User>>, GetUserListQueryHandler>();

builder.Services.AddScoped<IBookingRepository, InMemoryBookingRepository>();
builder.Services.AddScoped<IFlightRepository, InMemoryFlightRepository>();
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();

var jwtService = new JWTTokenService(secretKey, validIssuer, Convert.ToInt32(expiryInMinutes));
builder.Services.AddSingleton<IJWTTokenService>(x => { return jwtService; });


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Search API", Version = "v1" });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "JWT",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ValidateTokenMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


