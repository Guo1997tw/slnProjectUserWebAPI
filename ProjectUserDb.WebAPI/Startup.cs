using Evertrust.Core.Logging.Abstractions;
using Evertrust.Core.Logging.Exceptionless;
using Evertrust.ResponseWrapper.Extensions;
using Evertrust.ResponseWrapper.Middlewares;
using Exceptionless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ProjectUser.WebAPI
{
    public class Startup
    {
        public ExceptionlessClient? Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEvertrustResponseWrapper();

            services.AddControllers(options =>
            {
                options.AddEvertrustResponseWrapperFilters();
            });

            /*services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSingleton<ILogHelperFactory, LogHelperFactory>();*/
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionless(this.Configuration);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Evertrust.ResponseWrapper
            app.UseEvertrustResponseWrapper(typeof(Startup).Assembly);
            app.UseEvertrustExceptionHandling();

            app.UseSwagger().UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint
                (
                    url: "/swagger/v1/swagger.json",
                    name: "DemoApplication"
                );
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}