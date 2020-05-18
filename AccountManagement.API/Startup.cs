using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.API.Data.Context;
using AccountManagement.API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AccountManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AccountManagementContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.Audience = Configuration["AAD:ResourceId"];
                opt.Authority = $"{Configuration["AAD:Instance"]}{Configuration["AAD:TenantId"]}";
            });
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<IApplicationOrganizationsService, ApplicationOrganizationsService>();
            services.AddScoped<IAccountLogService, AccountLogService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            UpdateDatabase(app);
            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AccountManagementContext>();
            context.Database.Migrate();
        }
    }
}
