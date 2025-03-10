using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using Ronalfy_Jimenez_P2_Ap1.Models;

namespace Ronalfy_Jimenez_P2_Ap1.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Ciudades> Ciudades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ciudades>().HasData(
                new List<Ciudades>()
            {
                new() { CiudadId = 1, Nombre = "San francisco", Monto=1500},
                new() { CiudadId = 2, Nombre = "Cotui", Monto=20000},
                new() { CiudadId = 3, Nombre = "La Vega", Monto=1800}

            }

            );
                
        }
    }
}
