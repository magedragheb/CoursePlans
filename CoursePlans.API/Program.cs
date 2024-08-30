using System.Reflection;
using CoursePlans.API.Data;
using CoursePlans.API.Endpoints;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<CoursePlansContext>(options =>
    options.UseSqlServer(builder.Configuration.GetSection("db").Value));
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(e => e.Run(async context =>
        await Results.Problem().ExecuteAsync(context)));

app.UseHttpsRedirection();
app.MapEndpoints();

app.Run();
