using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataRepository.Repositories;
using Api.Infrastructure.Services;


namespace Api
{
    public class Startup
    {
        private bool runProd = false;
        private readonly ILogger<Startup> logger;
        public Startup(IConfiguration configuration, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            Configuration = configuration;
            runProd = env.IsProduction();
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IBrainWareRepository, BrainWareRepository>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                if (configuration == null)
                {
                    logger.LogError("Configuration service is null");
                    throw new InvalidOperationException("Configuration service is null.");
                }

                var connectionString = configuration.GetConnectionString("BrainWareDb");
                if (connectionString == null)
                {
                    logger.LogError("Connection string is null");
                    throw new InvalidOperationException("Connection string is null");
                }

                return new BrainWareRepository(connectionString);
            });

            services.AddScoped<IOrderService, OrderService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedHosts", builder =>
                {
                    var allowedHosts = Configuration.GetSection("AllowedHosts")?.Get<string[]>();
                    if (allowedHosts != null && allowedHosts.Length > 0)
                    {
                        builder.WithOrigins(allowedHosts)
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                    else
                    {
                        logger.LogError("Allowed hosts not found");
                    }
                });
            });
            // Add Swagger for API documentation (optional)
            if (!runProd)
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BrainWare Api", Version = "v1" }); // Replace with your API details
                });
                logger.LogInformation("Running in development mode.");
            }
            else
            {
                logger.LogInformation("Running in production mode.");
            }

            logger.LogInformation("Services configured successfully.");
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(); // Enable for production environments
            }

            // Use HTTPS redirection for production environments (optional)
           
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowedHosts");         

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add Swagger middleware for development environments (optional)
            if (!runProd)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"));
            }
        }
    }
}
