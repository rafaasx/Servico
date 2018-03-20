using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using WebServicos.Domain;
using static WebServicos.Domain.Enum;

[assembly: OwinStartupAttribute(typeof(WebServicos.Startup))]
namespace WebServicos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
