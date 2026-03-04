## Assignment Information

This project was created as part of the **Databasteknik course assignment**.

The goal of the assignment was to:

- Design a normalized relational database
- Implement backend using ASP.NET Core Minimal API
- Use Entity Framework Core with Code First
- Follow Clean Architecture and Domain Driven Design
- Provide CRUD functionality
- Include a test project
- Publish the project on GitHub
## Author
**Mehwish Bajwa**

Following are the detail information about the Project.

# CourseOnline

CourseOnline is a backend system for managing courses, instructors, participants and course enrollments for an educational company.
The system is built using **ASP.NET Core Minimal API**, **Entity Framework Core**, and follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles.

## Project Structure
The solution is divided into multiple layers according to Clean Architecture:
CourseOnline
│
├── CourseOnline.PresentationWebApi # Minimal API (Presentation layer)
├── CourseOnline.Application # Application logic, DTOs, services
├── CourseOnline.Domain # Domain entities and business rules
├── CourseOnline.Infrastructure # Database, EF Core, repositories
├── CourseOnline.Tests # Unit tests
└── CourseOnline.sln # Solution file

## Technologies Used
- ASP.NET Core Minimal API
- C#
- Entity Framework Core
- SQLite
- Clean Architecture
- Domain Driven Design (DDD)
- xUnit (testing)
- Git & GitHub

## Database
The system uses **Entity Framework Core with Code First approach**.
The database stores:
- Courses
- Instructors
- Participants
- Course Instances
- Enrollments

Relationships are modeled according to relational database principles.

## API Endpoints
Examples of available endpoints:

### Courses
GET /courses
GET /courses/{id}
POST /courses
PUT /courses/{id}
DELETE /courses/{id}

### Instructors
GET /instructors
POST /instructors

### Participants
GET /participants
POST /participants


### Enrollments
POST /enrollments
GET /enrollments

## Running the Project

Clone the repository:
git clone https://github.com/mehwishbajwa/CourseOnline.git


Swagger UI will be available at:
http://localhost:5299/swagger

## Testing
The solution includes a **test project using xUnit**.



