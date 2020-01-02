using Actio.Common.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Extensions
{
    public static class Extensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app, IServiceProvider provider ) 
        {
            using (var scope = provider.CreateScope()) 
            {
                scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().InitializeAsync();
            }
        }
    }
}
