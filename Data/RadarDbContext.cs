using Microsoft.EntityFrameworkCore;
using radar_api.Models;

namespace radar_api.Data
{
    public class RadarDbContext : DbContext
    {
        public RadarDbContext(DbContextOptions<RadarDbContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
    }
}
