using ClinicConsultation.Application.Interfaces;
using ClinicConsultation.Application.Services;
using ClinicConsultation.Infrastructure.Persistence;
using ClinicConsultation.Infrastructure.Repositories;
using ClinicConsultation.Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Specific repositories (for query-intensive services)
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();

// Application services
builder.Services.AddScoped<IConsultationService, ConsultationService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Global exception handling must be first in the pipeline
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

// UseAuthorization removed — no authentication is configured, the call was a no-op

app.MapControllers();

app.Run();
