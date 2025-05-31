using ContactSystem.Application.Dtos;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Services;
using ContactSystem.Infrastructure.Persistence.Repositories;

namespace ContactSystem.Server.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependecies(this IServiceCollection services)
        {
            //services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            //services.AddScoped<ITokenService, TokenService>();
            //services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
            //services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
            //services.AddScoped<IValidator<ContactCreateDto>, ContactCreateDtoValidator>();
            //services.AddScoped<IValidator<ContactDto>, ContactDtoValidator>();
        }
    }
}
