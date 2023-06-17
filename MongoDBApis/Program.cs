using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBApis.Models;
using MongoDBApis.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<StudentStoreDatabseSettings>(
    builder.Configuration.GetSection(nameof(StudentStoreDatabseSettings)));

builder.Services.AddSingleton<IStudentStoreDatabaseSettings>(sp => 
    sp.GetRequiredService<IOptions<StudentStoreDatabseSettings>>().Value );

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("StudentStoreDatabseSettings:ConnectionString")));

builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
