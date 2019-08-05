using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzraAssessmentServer.Db;
using EzraAssessmentServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // For this asssessment, we'll be using an in-memory DB context that will get created every time the app starts.
            services.AddDbContext<EzraAssessmentDbContext>(options => options.UseInMemoryDatabase(databaseName: "EzraAssessmentDb"));

            // Dependency Injection:

            services.AddTransient<IEmployeeService, EmployeeService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }



            app.UseCors(MyAllowSpecificOrigins);

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
