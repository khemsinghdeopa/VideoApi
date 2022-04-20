using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StreamingWebService.Controllers;
using StreamingWebService.DataSource;


namespace StreamingWebService
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _config.GetConnectionString("media");
            services.AddDbContextPool<StreamDbContext>(
                options => options.UseSqlServer(connectionString)
                );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Media", Version = "v1", });
            });

            services.AddScoped<ChannelRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<MediaCategoryRepository>();
            services.AddScoped<MediaRepository>();
            services.AddScoped<DefaultMediaRepository>();
            services.AddScoped<TaskGroupRepository>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<TaskGroupUserRelationRepository>();
            services.AddScoped<ScheduleRepository>();
            services.AddScoped<FpcRepository>();
            services.AddScoped<FpcProgramRepository>();
            services.AddScoped<FcpItemRepository>();           
            services.AddScoped<MediaRepository>();
            services.AddScoped<ScheduleItemRepository>();
            services.AddScoped<PlaylistItemRepository>();
            services.AddScoped<AsRunlogRepository>();
            services.AddScoped<ConfigurationRepository>();
            services.AddScoped<BookingRepository>();
            services.AddScoped<GroupRepository>();
            services.AddScoped<DefaultMediaRepository>();
            services.AddScoped<ClientRepository>();
            services.AddScoped < PlaylistInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Media");
            });
           
            
        }
    }
}
