# ✨Try it live : [Click here](https://student-management.runasp.net/swagger/index.html)

# Student Management System

A comprehensive API for managing students, classes, enrollments, and marks in an educational institution. This system allows administrators to track student information, class details, student enrollments, and academic performance.

## Features

### Student Management
- Create, read, update, and delete student records
- Search and filter students by name, age, and other attributes
- Pagination support for listing students

### Class Management
- Create, read, update, and delete class records
- Search and filter classes by name, teacher, and other attributes
- Pagination support for listing classes

### Enrollment Management
- Enroll students in classes
- View class enrollments
- Track enrollment dates

### Mark Recording
- Record exam and assignment marks for students in specific classes
- Calculate total marks automatically
- Generate student academic reports with average marks

### Reporting
- Generate comprehensive student reports including enrolled classes and marks
- Calculate class averages

## Technology Stack

- **Framework**: ASP.NET Core 9.0
- **API Architecture**: REST API using FastEndpoints
- **Documentation**: Swagger/OpenAPI
- **Validation**: FluentValidation
- **Data Storage**: In-memory concurrent collections (for demonstration purposes)

## Project Structure

```
StudentManagement.API/
├── Features/                 # Feature-based organization
│   ├── Classes/             # Class-related features
│   │   ├── DTOs/           # Data Transfer Objects
│   │   ├── Endpoints/      # API Endpoints
│   │   └── Validators/     # Request Validators
│   ├── Enrollments/        # Enrollment-related features
│   ├── Marks/              # Mark-related features
│   └── Students/           # Student-related features
├── Infrastructure/         # Infrastructure components
│   ├── ApiError.cs        # Error handling
│   └── DataStore.cs       # In-memory data storage
├── Models/                 # Domain models
│   ├── Class.cs
│   ├── Enrollment.cs
│   ├── Mark.cs
│   └── Student.cs
├── Services/               # Business logic services
│   ├── Implementations/    # Service implementations
│   └── Interfaces/         # Service interfaces
└── Program.cs              # Application entry point and configuration
```

## API Endpoints

### Students
- `GET /api/student/{id}` - Get a student by ID
- `GET /api/students` - Get all students (with filtering and pagination)
- `POST /api/student` - Create a new student
- `PUT /api/student/{id}` - Update a student
- `DELETE /api/student/{id}` - Delete a student
- `GET /api/student/{id}/report` - Get a comprehensive student report

### Classes
- `GET /api/class/{id}` - Get a class by ID
- `GET /api/classes` - Get all classes (with filtering and pagination)
- `POST /api/class` - Create a new class
- `PUT /api/class/{id}` - Update a class
- `DELETE /api/class/{id}` - Delete a class

### Enrollments
- `POST /api/enrollment` - Enroll a student in a class
- `DELETE /api/enrollment` - Remove a student from a class

### Marks
- `POST /api/mark` - Record a mark for a student in a class
- `GET /api/mark` - Get a mark for a student in a class

## Data Models

### Student
- `Id` - Unique identifier
- `FirstName` - Student's first name
- `LastName` - Student's last name
- `Age` - Student's age

### Class
- `Id` - Unique identifier
- `Name` - Class name
- `Teacher` - Teacher's name
- `Description` - Class description

### Enrollment
- `StudentId` - Student identifier
- `ClassId` - Class identifier
- `EnrolledAt` - Enrollment date and time

### Mark
- `StudentId` - Student identifier
- `ClassId` - Class identifier
- `ExamMark` - Exam mark (0-100)
- `AssignmentMark` - Assignment mark (0-100)
- `TotalMark` - Calculated total mark

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later

### Running the Application
1. Clone the repository
2. Navigate to the project directory
3. Run the application:
   ```
   dotnet run --project StudentManagement.API
   ```
4. Access the Swagger documentation at `https://localhost:5001/swagger`

## Validation

The application uses FluentValidation to validate requests:
- Student names must not be empty and cannot exceed 50 characters
- Student age must be between 1 and 119
- Class names must not be empty and cannot exceed 100 characters
- Teacher names must not be empty and cannot exceed 100 characters
- Class descriptions cannot exceed 500 characters
- Marks must be between 0 and 100

## Future Improvements

- Add authentication and authorization
- Implement persistent data storage (e.g., SQL Server, PostgreSQL)
- Add more comprehensive reporting features
- Implement caching for improved performance
- Add unit and integration tests
- Implement logging and monitoring
