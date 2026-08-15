using UserManagementAPI.Middleware;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers.
builder.Services.AddControllers();

// Register the user service.
builder.Services.AddSingleton<IUserService, UserService>();

// Add API explorer and Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

// Global exception handling middleware.
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Request logging middleware.
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
