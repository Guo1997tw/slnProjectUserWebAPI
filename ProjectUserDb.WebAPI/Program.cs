using CoreProfiler.Web;
using ProjectUser.Repository.Helpers;
using ProjectUser.Repository.Interface;
using ProjectUser.Repository.Repository;
using ProjectUser.Services.Interface;
using ProjectUser.Services.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//Add DI
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();
//builder.Services.AddScoped<IDatabaseHelper>(Option => new DatabaseHelper("UserDbContext"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserServices>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//Customized Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc
    (
        "v1",
        new()
        {
            Title = " User Open API",
            Version = new Version(1, 0).ToString(),
            Description = "This is User API Swagger Document."
        }
    );

    options.ExampleFilters();

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
})
    .AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//CoreProfiler
app.UseCoreProfiler(true);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();