using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using Web.Api.Core;
using Web.Api.Infrastructure;
using Web.Api.Presenters;
using Web.Api.Presenters.Chat;
using Web.Api.Presenters.File;
using Web.Api.Presenters.Offer;
using Web.Api.Presenters.QuoteRequest;

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
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/savvy-climber-252013",
                        ValidateAudience = true,
                        ValidAudience = "savvy-climber-252013",
                        ValidateLifetime = true
                    };
                });
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            InfrastructureConfigureServices.MapInfrastructureServices(services);
            CoreConfigureServices.MapCoreServices(services);

            services.AddSingleton<UserRegisterPresenter>();
            services.AddSingleton<FileUploadPresenter>();
            services.AddSingleton<UserLoginPresenter>();
            services.AddSingleton<FileFetchAllPresenter>();
            services.AddSingleton<FileFetchPresenter>();
            services.AddSingleton<FileDeletePresenter>();
            services.AddSingleton<HouseQuoteRequestPresenter>();
            services.AddSingleton<FinancialCapacityFindPresenter>();
            services.AddSingleton<FinancialCapacityRegisterPresenter>();
            services.AddSingleton<UserUpdatePresenter>();
            services.AddSingleton<OfferCreatePresenter>();
            services.AddSingleton<OfferDeletePresenter>();
            services.AddSingleton<OfferFetchAllPresenter>();
            services.AddSingleton<OfferFetchPresenter>();
            services.AddSingleton<OfferFetchAllByReqPresenter>();
            services.AddSingleton<OfferUpdatePresenter>();
            services.AddSingleton<ChatSendPresenter>();
            services.AddSingleton<ChatFetchPresenter>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hestia API", Version = "v1" });
            });
        }
        public void LoggerConfig()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // target where to log to : File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log_file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // mapping rules
            config.AddRule(LogLevel.Info, LogLevel.Error, logconsole);
            config.AddRule(LogLevel.Info, LogLevel.Error, logfile);

            // apply config
            NLog.LogManager.Configuration = config;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hestia API");
            });

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

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            //app.UseHttpsRedirection();

            // call the logger setup
            LoggerConfig();

            app.UseMvc();
        }
    }
}
