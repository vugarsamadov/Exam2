using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business.Models
{
    public class AboutItemUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
