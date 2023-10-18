using EasyGroceries.Product.Application.Contracts.Infrastructure;
using EasyGroceries.Product.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IProductInfoRepository, ProductInfoRepository>();
            return services;
        }
    }
}
