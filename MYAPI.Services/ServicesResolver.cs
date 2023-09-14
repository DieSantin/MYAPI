using Microsoft.Extensions.DependencyInjection;

namespace MYAPI.Services
{
    public static class ServicesResolver
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
