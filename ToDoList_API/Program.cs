using ToDoList_API.Extensions;
using ToDoList_API.Filters;
using ToDoList_API.Middlewares;
using ToDoList_BAL.Services;
using ToDoLIst_DAL.Contracts;
using ToDoLIst_DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentityCore();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskListGroupRepository, TaskListGroupRepository>();
builder.Services.AddScoped<ITaskListRepository, TaskListRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TaskListGroupService>();
builder.Services.AddScoped<TaskListRepository>();

builder.Services.AddScoped<ValidateRouteUserIdFilter>();
builder.Services.AddScoped(typeof(ValidateDtoIdFilter<>));

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.ConfigureControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
