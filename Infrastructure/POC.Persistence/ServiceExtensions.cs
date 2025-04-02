using POC.Application.Helpers;
using POC.Application.Interfaces;
using POC.Helpers;
using POC.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;

namespace POC.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IFileManager, FileManager>();

            services.AddScoped<ISMSHelper, SMSHelper>();
            services.AddScoped<IConfigRefRepository, ConfigRefRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IAdminMasterRepository, AdminMasterRepository>();
            services.AddScoped<IManageSecurityRepository, ManageSecurityRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
        }
    }
}
