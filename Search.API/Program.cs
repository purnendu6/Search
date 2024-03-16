using MediatR;
using Microsoft.OpenApi.Models;
using Search.API.Middlewares;
using Search.Application.Commands;
using Search.Application.Handlers;
using Search.Application.Queries;
using Search.Domain.Entities;
using Search.Domain.Interfaces;
using Search.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}
builder.Services.AddScoped<IRequestHandler<CreateBookingCommand, int>, CreateBookingCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetBookingListQuery, List<Booking>>, GetBookingListQueryHandler>();
builder.Services.AddScoped<IBookingRepository, InMemoryBookingRepository>();
builder.Services.AddScoped<IFlightRepository, InMemoryFlightRepository>();
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();

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
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

//https://github.com/ajay7386/PatientSearch.WebAPI link to Ajay project

