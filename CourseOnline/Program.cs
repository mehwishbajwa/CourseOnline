using System.Text.Json.Serialization;
using CourseOnline.Application.DTOs;
using CourseOnline.Application.Extensions;
using CourseOnline.Application.Services;
using CourseOnline.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// -------------------- SERVICES --------------------

// Infrastructure (DbContext + repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Application services (use cases)
builder.Services.AddApplication();

// Prevent JSON circular reference errors
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.SerializerOptions.WriteIndented = true;
});

// CORS (for React frontend if used)
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

app.UseCors("Frontend");

app.UseSwagger();
app.UseSwaggerUI();

// -------------------- BASIC ROUTES --------------------

app.MapGet("/", () => "CourseOnline API is running");
app.MapGet("/health", () => Results.Ok("ok"));

// -------------------- COURSES --------------------

// Get all courses
app.MapGet("/courses", async (ICourseService service) =>
{
    var courses = await service.GetAllAsync();
    return Results.Ok(courses);
});

// Get course by id
app.MapGet("/courses/{id:guid}", async (Guid id, ICourseService service) =>
{
    var course = await service.GetByIdAsync(id);
    return course is null ? Results.NotFound() : Results.Ok(course);
});

// Create course
app.MapPost("/courses", async (CourseCreateDto dto, ICourseService service) =>
{
    var created = await service.CreateAsync(dto);
    return Results.Created($"/courses/{created.Id}", created);
});

// Update course
app.MapPut("/courses/{id:guid}", async (Guid id, CourseUpdateDto dto, ICourseService service) =>
{
    var updated = await service.UpdateAsync(id, dto);
    return updated is null ? Results.NotFound() : Results.Ok(updated);
});

// Delete course
app.MapDelete("/courses/{id:guid}", async (Guid id, ICourseService service) =>
{
    var deleted = await service.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();

public partial class Program { }