using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Prueba.Modelos
{
    class PersonaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Prueba;Integrated Security=True;")
            .EnableSensitiveDataLogging(true);
        }

        //indiciamos que las llaves del modelo personaMascotas son llaves compuestas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaMascota>().HasKey(x => new { x.PersonaId, x.MascotaID });
        }

        public DbSet<persona> Personas { get; set; }
        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<PersonaMascota> PersonasMascotas { get; set; }
    }
}
