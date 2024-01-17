using AutoMapper;
using Exam2.Business.Models;
using Exam2.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business.Profiles
{
    public class AboutItemProfile : Profile
    {

        public AboutItemProfile(IWebHostEnvironment env)
        {
            CreateMap<AboutItemCreateVM, AboutItem>()
                .ForMember(c=>c.ImageUrl,opt=>opt.Ignore())
                .AfterMap(async (src,dest)=>
                {
                    if(src.Image != null)
                        dest.ImageUrl = await src.Image.SaveAndProvideNameAsync(env);
                });

            CreateMap<AboutItemUpdateVM, AboutItem>()
                .ForMember(c => c.ImageUrl, opt => opt.Ignore())
                .AfterMap(async (src, dest) =>
                {
                    if (src.Image != null)
                        dest.ImageUrl = await src.Image.SaveAndProvideNameAsync(env);
                });

            CreateMap<AboutItem, AboutItemVM>();

            CreateMap<AboutItemVM,AboutItemUpdateVM>();

        }

    }
}
