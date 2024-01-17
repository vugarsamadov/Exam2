using Exam2.Business.Profiles;
using Exam2.Business.Services;
using Exam2.Business.Services.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services,IWebHostEnvironment env)
        {

            services.AddAutoMapper(options =>
            {
                options.AddProfile(new AboutItemProfile(env));
            });

            services.AddScoped<IAboutItemService, AboutItemService>();

            return services;
        }
    }
}
