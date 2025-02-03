var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure HttpClient for NumbersAPI
builder.Services.AddHttpClient("NumbersApi", client =>
{
    client.BaseAddress = new Uri("http://numbersapi.com/");
    client.Timeout = TimeSpan.FromSeconds(2);
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || true) // Enable Swagger in all environments
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware order matters!
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();