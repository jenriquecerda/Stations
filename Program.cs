using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Acuity.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Stations {
    public class Program {
        public static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

        private static void CreateDbIfNotExists(IHost host) {
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var context = services.GetRequiredService<AcuityContext>();
                    FakeDbInitializer.Initialize(context);
                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
