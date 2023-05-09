using ProjectUser.Common.Interface;
using ProjectUser.Common.Helpers;
using ProjectUser.Repository.Interface;
using ProjectUser.Repository.Repository;
using ProjectUser.Services.Interface;
using ProjectUser.Services.Services;

var builder = WebApplication.CreateBuilder(args);

//Add DI
builder.Services.AddScoped<IUserDbCommon, UserDbCommon>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();

// Add services to the container.
builder.Services.AddControllers();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();