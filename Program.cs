using System.Net;
using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Application.UseCases;
using ifficient_school.src.Domain.Exceptions;
using ifficient_school.src.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 80);
});

builder.Services.AddSingleton<IStudentRepository>(provider =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "students.csv");

    if (!File.Exists(filePath))
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        File.Create(filePath).Dispose();
    }

    return new StudentRepository(filePath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowLocalhost3000",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

builder.Services.AddTransient<GetApprovedAndFailedStudents>();
builder.Services.AddTransient<GetBestStudentBySubject>();
builder.Services.AddTransient<GetStudentByRegistration>();
builder.Services.AddTransient<GetSortStudentsByAverage>();
builder.Services.AddTransient<GetAllStudents>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

app.UseCors("AllowLocalhost3000");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.Use(
    async (context, next) =>
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            bool isCustomException = ex is CustomException;

            context.Response.StatusCode = isCustomException
                ? StatusCodes.Status400BadRequest
                : StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = isCustomException
                        ? ex.Message
                        : "An unexpected error occurred. Please try again later.",
                    error = isCustomException ? "expected_error" : ex.Message,
                }
            );
        }
    }
);

app.MapControllers();

app.Run();
