var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS policy to allow requests from the S3 frontend URL
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendS3", policy =>
    {
        policy.WithOrigins("http://app-react-front-end.s3-website.eu-north-1.amazonaws.com") // Allow the S3 frontend URL
              .AllowAnyHeader()  // Allow any headers
              .AllowAnyMethod(); // Allow any HTTP method (GET, POST, etc.)
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS policy
app.UseCors("AllowFrontendS3"); // Apply the CORS policy globally

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
