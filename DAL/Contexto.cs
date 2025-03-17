using Microsoft.EntityFrameworkCore;
using Ronalfy_Jimenez_P2_Ap1.DAL;
using Ronalfy_Jimenez_P2_Ap1.Models;

namespace Ronalfy_Jimenez_P2_Ap1.DAL
{
    public class Contexto : DbContext
    {
        internal object ciudades;

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Ciudades> Ciudades { get; set; }
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<CursosDetalle> CursosDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ciudades>().HasData(
                new List<Ciudades>()
            {
                new() { CiudadId = 1, Nombre = "San francisco", MontoBase=1500},
                new() { CiudadId = 2, Nombre = "Cotui", MontoBase=20000},
                new() { CiudadId = 3, Nombre = "La Vega", MontoBase=1800}

            }

            );


      //      modelBuilder.Entity<Cursos>()
      //.HasMany(c => c.CursosDetalle)
      //.WithOne(d => d.Cursos)
      //.HasForeignKey(d => d.CursosId)
      //.OnDelete(DeleteBehavior.Cascade);


        //    modelBuilder.Entity<CursosDetalle>()
        //.HasOne(d => d.Ciudades)
        //.WithMany()
        //.HasForeignKey(d => d.CiudadId)
        //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
