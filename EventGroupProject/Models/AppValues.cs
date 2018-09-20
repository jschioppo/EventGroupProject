using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class AppValues
    {
        public IConfiguration AppSettings { get; private set; }

        public AppValues()
        {
            AppSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public string ConnectionString
        {
            get
            {
                return AppSettings["ConnectionStrings:DefaultConnection"];
            }
        }
    }
}
