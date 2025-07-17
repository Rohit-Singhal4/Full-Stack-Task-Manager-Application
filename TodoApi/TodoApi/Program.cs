using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TodoApi.API.Endpoints;
using TodoApi.API.Validators;
using TodoApi.API.Validators.Group;
using TodoApi.API.Validators.TodoItem;
using TodoApi.Domain.Services;
using TodoApi.Domain.Services.Interfaces;
using TodoApi.DTO;
using TodoApi.DTO.Group;
using TodoApi.DTO.TodoItem;
using TodoApi.Infrastructure.Context;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
// Adds database context to DI container and specifies that the db contact will use in-memory database
//builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
//builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("GroupList"));


var connectionString = builder.Configuration.GetValue<string>("TODO_DB_CONNECTION");
Console.WriteLine("Connection string: " + connectionString);

builder.Services.AddDbContext<TodoContext>(options =>
{ 
    options.UseNpgsql(builder.Configuration.GetValue<string>("TODO_DB_CONNECTION"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITodoItemService, TodoItemService>()
    .AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IValidator<DeleteRequest>, DeleteRequestValidator>()
    .AddScoped<IValidator<CreateTodoItemRequest>, CreateTodoItemRequestValidator>()
    .AddScoped<IValidator<UpdateTodoItemRequest>, UpdateTodoItemRequestValidator>()
    .AddScoped<IValidator<PatchTodoItemRequest>, PatchTodoItemRequestValidator>()
    .AddScoped<IValidator<CreateGroupRequest>, CreateGroupRequestValidator>()
    .AddScoped<IValidator<UpdateGroupRequest>, UpdateGroupRequestValidator>();
    
// Can use number or string for enum (String will be mapped to int)
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build(); 

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//app.UseHttpsRedirection(); // not needed for containerized APIs as they sit behind Kubernetes/Docker which give them the layer of secuirty
//app.MapControllers().WithOpenApi(); // maps instances of api controllers
app.MapTodoItemEndpoints(); // maps instance of api endpoints
app.MapGroupEndpoints();

app.UseAuthorization();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
await dbContext.Database.MigrateAsync();

app.Run();