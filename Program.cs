using ifficient_school.src.Infrastructure.Repositories;
using ifficient_school.src.Application.Interfaces;
using ifficient_school.src.Application.UseCases;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configuração explícita da porta
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Listen(IPAddress.Any, 80); 
});

// Registra o caminho do arquivo e o repositório de alunos
builder.Services.AddSingleton<IStudentRepository>(provider =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "Data", "students.csv");
    return new StudentRepository(filePath);
});

// Registra os casos de uso
builder.Services.AddTransient<GetApprovedAndFailedStudents>();
builder.Services.AddTransient<GetBestStudentBySubject>();
builder.Services.AddTransient<GetStudentByRegistration>();
builder.Services.AddTransient<GetSortStudentsByAverage>();
builder.Services.AddTransient<GetAllStudents>();


// Adiciona os controladores
builder.Services.AddControllers();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

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

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            message = "An unexpected error occurred. Please try again later.",
            error = ex.Message, // Remova essa linha em produção se não quiser expor detalhes
        });
    }
});

app.MapControllers();

app.Run();
