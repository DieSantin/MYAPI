using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MYAPI.AutoMapper;

namespace MYAPI.Data
{
    public static class DataResolver
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            services.AddAutoMapper(typeof(DataMapper));

            services.AddDbContext<DataContext>(opt 
                => opt.UseMySql(
                    connectionString, 
                    ServerVersion.AutoDetect(connectionString)));

            services.AddScoped<DataContext>();
            services.AddScoped<IDataStore, DataStore>();
        }
    }
}
