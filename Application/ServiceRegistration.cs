using Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // 1. Registra MediatR y todos los Handlers automáticamente
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // 2. Registra todos los Validadores de FluentValidation automáticamente
            services.AddValidatorsFromAssembly(assembly);

            // 3. Conecta el Pipeline Behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
