﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AchievevementWebAPIExample.Startup))]

namespace AchievevementWebAPIExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // trigger new deploy
            ConfigureAuth(app);
        }
    }
}
