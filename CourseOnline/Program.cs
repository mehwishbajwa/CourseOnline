using System;
using System.Text.Json.Serialization;
using CourseOnline.Domain.Entities;
using CourseOnline.Infrastructure.EFCore.Contexts;
using CourseOnline.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Infrastructure (DbContext etc.) --------------------
builder.Services.AddInfrastructure(builder.Configuration);

// -------------------- Fix JSON cycles (prevents 500 on GET with Includes) --------------------
builder.Services.ConfigureHttpJsonOptions(opt =>
{
    opt.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.SerializerOptions.WriteIndented = true;
});

// -------------------- CORS for React --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("Frontend");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Keep request bodies in Swagger UI (so don't retype every time)
    c.EnablePersistAuthorization();
    c.ConfigObject.AdditionalItems["persistRequestBody"] = true;
    c.ConfigObject.AdditionalItems["persistAuthorization"] = true;
});

// -------------------- Health --------------------
app.MapGet("/", () => "CourseOnline API is running");
app.MapGet("/health", () => Results.Ok("ok"));


// ==================== COURSES ====================

app.MapGet("/courses", async (AppDbContext db) =>
    Results.Ok(await db.Courses.AsNoTracking().ToListAsync()));

app.MapGet("/courses/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var course = await db.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    return course is null ? Results.NotFound() : Results.Ok(course);
});

app.MapPost("/courses", async (Course course, AppDbContext db) =>
{
    db.Courses.Add(course);
    await db.SaveChangesAsync();
    return Results.Created($"/courses/{course.Id}", course);
});

app.MapPut("/courses/{id:guid}", async (Guid id, Course input, AppDbContext db) =>
{
    var course = await db.Courses.FirstOrDefaultAsync(x => x.Id == id);
    if (course is null) return Results.NotFound();

    course.Title = input.Title;
    course.Description = input.Description;

    await db.SaveChangesAsync();
    return Results.Ok(course);
});

app.MapDelete("/courses/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var course = await db.Courses.FirstOrDefaultAsync(x => x.Id == id);
    if (course is null) return Results.NotFound();

    db.Courses.Remove(course);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// ==================== INSTRUCTORS ====================

app.MapGet("/instructors", async (AppDbContext db) =>
    Results.Ok(await db.Instructors.AsNoTracking().ToListAsync()));

app.MapGet("/instructors/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var instructor = await db.Instructors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    return instructor is null ? Results.NotFound() : Results.Ok(instructor);
});

app.MapPost("/instructors", async (Instructor instructor, AppDbContext db) =>
{
    db.Instructors.Add(instructor);
    await db.SaveChangesAsync();
    return Results.Created($"/instructors/{instructor.Id}", instructor);
});

app.MapPut("/instructors/{id:guid}", async (Guid id, Instructor input, AppDbContext db) =>
{
    var instructor = await db.Instructors.FirstOrDefaultAsync(x => x.Id == id);
    if (instructor is null) return Results.NotFound();

    instructor.FirstName = input.FirstName;
    instructor.LastName = input.LastName;
    instructor.Email = input.Email;
    instructor.PhoneNumber = input.PhoneNumber;

    await db.SaveChangesAsync();
    return Results.Ok(instructor);
});

app.MapDelete("/instructors/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var instructor = await db.Instructors.FirstOrDefaultAsync(x => x.Id == id);
    if (instructor is null) return Results.NotFound();

    db.Instructors.Remove(instructor);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// ==================== PARTICIPANTS ====================

app.MapGet("/participants", async (AppDbContext db) =>
    Results.Ok(await db.Participants.AsNoTracking().ToListAsync()));

app.MapGet("/participants/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var participant = await db.Participants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    return participant is null ? Results.NotFound() : Results.Ok(participant);
});

app.MapPost("/participants", async (Participant participant, AppDbContext db) =>
{
    db.Participants.Add(participant);
    await db.SaveChangesAsync();
    return Results.Created($"/participants/{participant.Id}", participant);
});

app.MapPut("/participants/{id:guid}", async (Guid id, Participant input, AppDbContext db) =>
{
    var participant = await db.Participants.FirstOrDefaultAsync(x => x.Id == id);
    if (participant is null) return Results.NotFound();

    participant.FirstName = input.FirstName;
    participant.LastName = input.LastName;
    participant.Email = input.Email;

    await db.SaveChangesAsync();
    return Results.Ok(participant);
});

app.MapDelete("/participants/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var participant = await db.Participants.FirstOrDefaultAsync(x => x.Id == id);
    if (participant is null) return Results.NotFound();

    db.Participants.Remove(participant);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// ==================== COURSE INSTANCES ====================

app.MapGet("/courseinstances", async (AppDbContext db) =>
{
    var instances = await db.CourseInstances
        .AsNoTracking()
        .Include(x => x.Course)
        .Include(x => x.Instructor)
        .ToListAsync();

    return Results.Ok(instances);
});

app.MapGet("/courseinstances/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var instance = await db.CourseInstances
        .AsNoTracking()
        .Include(x => x.Course)
        .Include(x => x.Instructor)
        .FirstOrDefaultAsync(x => x.Id == id);

    return instance is null ? Results.NotFound() : Results.Ok(instance);
});

app.MapPost("/courseinstances", async (CourseInstance instance, AppDbContext db) =>
{
    var courseExists = await db.Courses.AnyAsync(c => c.Id == instance.CourseId);
    if (!courseExists) return Results.BadRequest($"CourseId '{instance.CourseId}' does not exist.");

    var instructorExists = await db.Instructors.AnyAsync(i => i.Id == instance.InstructorId);
    if (!instructorExists) return Results.BadRequest($"InstructorId '{instance.InstructorId}' does not exist.");

    db.CourseInstances.Add(instance);
    await db.SaveChangesAsync();
    return Results.Created($"/courseinstances/{instance.Id}", instance);
});

app.MapPut("/courseinstances/{id:guid}", async (Guid id, CourseInstance input, AppDbContext db) =>
{
    var instance = await db.CourseInstances.FirstOrDefaultAsync(x => x.Id == id);
    if (instance is null) return Results.NotFound();

    instance.CourseId = input.CourseId;
    instance.InstructorId = input.InstructorId;
    instance.StartDate = input.StartDate;
    instance.EndDate = input.EndDate;

    await db.SaveChangesAsync();
    return Results.Ok(instance);
});

app.MapDelete("/courseinstances/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var instance = await db.CourseInstances.FirstOrDefaultAsync(x => x.Id == id);
    if (instance is null) return Results.NotFound();

    db.CourseInstances.Remove(instance);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// ==================== ENROLLMENTS ====================

app.MapGet("/enrollments", async (AppDbContext db) =>
{
    var enrollments = await db.Enrollments
        .AsNoTracking()
        .Include(e => e.Participant)
        .Include(e => e.CourseInstance)
            .ThenInclude(ci => ci.Course)
        .Include(e => e.CourseInstance)
            .ThenInclude(ci => ci.Instructor)
        .ToListAsync();

    return Results.Ok(enrollments);
});

app.MapGet("/enrollments/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var enrollment = await db.Enrollments
        .AsNoTracking()
        .Include(e => e.Participant)
        .Include(e => e.CourseInstance)
            .ThenInclude(ci => ci.Course)
        .Include(e => e.CourseInstance)
            .ThenInclude(ci => ci.Instructor)
        .FirstOrDefaultAsync(e => e.Id == id);

    return enrollment is null ? Results.NotFound() : Results.Ok(enrollment);
});

app.MapPost("/enrollments", async (Enrollment enrollment, AppDbContext db) =>
{
    var instanceExists = await db.CourseInstances.AnyAsync(ci => ci.Id == enrollment.CourseInstanceId);
    if (!instanceExists) return Results.BadRequest($"CourseInstanceId '{enrollment.CourseInstanceId}' does not exist.");

    var participantExists = await db.Participants.AnyAsync(p => p.Id == enrollment.ParticipantId);
    if (!participantExists) return Results.BadRequest($"ParticipantId '{enrollment.ParticipantId}' does not exist.");

    var alreadyEnrolled = await db.Enrollments.AnyAsync(e =>
        e.CourseInstanceId == enrollment.CourseInstanceId &&
        e.ParticipantId == enrollment.ParticipantId);

    if (alreadyEnrolled)
        return Results.Conflict("Participant is already enrolled in this course instance.");

    db.Enrollments.Add(enrollment);
    await db.SaveChangesAsync();
    return Results.Created($"/enrollments/{enrollment.Id}", enrollment);
});

app.MapDelete("/enrollments/{id:guid}", async (Guid id, AppDbContext db) =>
{
    var enrollment = await db.Enrollments.FirstOrDefaultAsync(e => e.Id == id);
    if (enrollment is null) return Results.NotFound();

    db.Enrollments.Remove(enrollment);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


// -------------------- RUN --------------------
app.Run();

public partial class Program { }