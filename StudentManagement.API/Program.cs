using FastEndpoints;
using FastEndpoints.Swagger;
using StudentManagement.API.Infrastructure;
using StudentManagement.API.Services.Implementations;
using StudentManagement.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();
builder.Services.AddFastEndpoints()
    .SwaggerDocument();


// Register services
builder.Services.AddSingleton<DataStore>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IMarkService, MarkService>();
var app = builder.Build();


app.UseHttpsRedirection();
app.UseFastEndpoints()
    .UseSwaggerGen();





app.Run();


