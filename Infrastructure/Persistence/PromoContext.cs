using Microsoft.EntityFrameworkCore;
using PromoCodes_main.Application.Models;

namespace PromoCodes_main.Infrastructure.Persistence {
    public class PromoContext : DbContext {
        public PromoContext (DbContextOptions<PromoContext> options) : base (options) {

        }

        public DbSet<TemppData> TemppData { get; set; }

      

    }
}