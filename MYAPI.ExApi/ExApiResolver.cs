using Microsoft.Extensions.DependencyInjection;
using MYAPI.AutoMapper;

namespace MYAPI.ExApi
{
    public static class ExApiResolver
    {
        public static void Register(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ExApiMapper));

            services.AddHttpClient();
            services.AddScoped<ExApiContext>();
            services.AddScoped<IExApiWrapper, ExApiWrapper>();
        }
    }
}
