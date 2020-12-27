using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(leave_management.Areas.Identity.IdentityHostingStartup))]

namespace leave_management.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}