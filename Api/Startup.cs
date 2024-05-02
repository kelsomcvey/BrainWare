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
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            runProd = env.IsProduction();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();



           // var connectionString = Configuration.GetConnectionString("BrainWareDb");
            services.AddScoped<IBrainWareRepository, BrainWareRepository>(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("BrainWareDb");
                return new BrainWareRepository(connectionString);
            });

            //services.AddScoped<IBrainWareRepository, BrainWareRepository>(provider =>
            //{
            //    return new BrainWareRepository(connectionString);
            //});

            services.AddScoped<IOrderService, OrderService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Replace with your Angular origin
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            // Add Swagger for API documentation (optional)
            if (!runProd)
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" }); // Replace with your API details
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            // app.UseHttpsRedirection();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAngular");
          //  app.UseAuthorization();

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
