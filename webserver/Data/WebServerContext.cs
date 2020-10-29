using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServerCon.Data
{
    public class WebServerContext : DbContext
    {
        public WebServerContext(DbContextOptions<WebServerContext> opt) : base(opt)
        {

        }
        public DbSet<Device> Devices { get; set; }
    }
}