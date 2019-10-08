using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Web.Api.Core;
using Web.Api.Infrastructure;
using Web.Api.Presenters;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // OATH
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/savvy-climber-252013";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer =true,
                        ValidIssuer = "https://securetoken.google.com/savvy-climber-252013",
                        ValidateAudience = true,
                        ValidAudience = "savvy-climber-252013",
                        ValidateLifetime = true
                    };
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            InfrastructureConfigureServices.MapInfrastructureServices(services);
            CoreConfigureServices.MapCoreServices(services);
            services.AddSingleton<RegisterUserPresenter>();
            services.AddSingleton<FileUploadPresenter>();
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
                app.UseAuthentication();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            
            app.UseMvc();
            
        }
    }
}
