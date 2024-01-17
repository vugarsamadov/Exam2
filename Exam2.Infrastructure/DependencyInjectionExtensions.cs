using Exam2.Infrastructure.Repositories;
using Exam2.Infrastructure.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Infrastructure
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            return services;
        }


    }
}
