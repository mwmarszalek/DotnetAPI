// dotnet watch run to run the application and listening for changes
// PACKAGES:
// dotnet add package dappper
// dotnet add package automapper
// dotnet add package Microsoft.Data.SqlClient

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) => 
    {
        options.AddPolicy("DevCors", (corsBuilder) => 
            //Specify which frameworks (React, Vue, Angular in this case) can allow what for CORS
            {
                corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000","http://localhost:8000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        options.AddPolicy("ProdCors", (corsBuilder) => 
            {   
                // later on add domain name here that you are actually going to use:
                corsBuilder.WithOrigins("https://myProductionSite.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else 
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
