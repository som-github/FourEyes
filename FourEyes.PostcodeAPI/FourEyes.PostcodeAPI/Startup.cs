using FourEyes.PostcodeAPI.Engine;
using FourEyes.PostcodeAPI.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FourEyes.PostcodeAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Engine = PostcodeAPIFactory.GetEngineInstance();
        }

        public IConfiguration Configuration { get; }

        IEngine Engine { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FourEyes.PostcodeAPI", Version = "v1" });
            });

            // Adding the Singleton instance of the API Engine to be accessed by the Controller/s
            services.AddSingleton(Engine);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FourEyes.PostcodeAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Verify if the Endpoint is accessible
            bool isEndpointValid = Engine.VerifyAPIAccessibility(Configuration[Constants.POSTCODE_IO_BASE_URL]);

            if(!isEndpointValid)
            {
                //TODO: Should we stop the Application?
            }
        }
    }
}
