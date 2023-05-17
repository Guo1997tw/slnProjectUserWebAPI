using CoreProfiler.Web;
using Evertrust.Core.Logging.Abstractions;
using Evertrust.Core.Logging.Exceptionless;
using Evertrust.ResponseWrapper.Extensions;
using Evertrust.ResponseWrapper.Middlewares;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using ProjectUser.Repository.Helpers;
using ProjectUser.Repository.Interface;
using ProjectUser.Repository.Repository;
using ProjectUser.Services.Interface;
using ProjectUser.Services.Mapping;
using ProjectUser.Services.Services;
using ProjectUser.WebAPI;
using ProjectUser.WebAPI.Infrastructure.Mapping;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//Add Dependency Injection
builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

//Add Evertrust ResponseWrapper
builder.Services.AddEvertrustResponseWrapper();

builder.Services.AddControllers(x =>
{
    x.AddEvertrustResponseWrapperFilters();
});

builder.Services.AddApiVersioning(x =>
{
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddSingleton<ILogHelperFactory, LogHelperFactory>();

//AutoMapper
builder.Services.AddAutoMapper( x =>
{
    x.AddProfile<ServiceProfile>();
    x.AddProfile<WebApplicationProfile>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Customized Swagger
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc
    (
        "v1",
        new()
        {
            Title = " User Open API",
            Version = new Version(1, 0).ToString(),
            Description = "This is User API Swagger Document."
        }
    );

    x.ExampleFilters();

    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}).AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwagger().UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint
        (
            url: "/swagger/v1/swagger.json",
            name: "slnProjectUserWebAPI"
        );
    });
}

//CoreProfiler
app.UseCoreProfiler(true);

app.UseHttpsRedirection();

app.UseRouting();

//Add Evertrust ResponseWrapper
app.UseEvertrustResponseWrapper(typeof(Startup).Assembly);

app.UseEvertrustExceptionHandling();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.MapControllers();

app.Run();