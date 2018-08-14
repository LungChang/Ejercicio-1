﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prueba.Modelos;

namespace Prueba.Migrations
{
    [DbContext(typeof(PersonaContext))]
    [Migration("20180813205853_PersonasMascotas")]
    partial class PersonasMascotas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Prueba.Mascota", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EstadoAdopcion");

                    b.Property<string>("Nombre");

                    b.Property<string>("Raza");

                    b.HasKey("Id");

                    b.ToTable("Mascotas");
                });

            modelBuilder.Entity("Prueba.Modelos.PersonaMascota", b =>
                {
                    b.Property<int>("PersonaId");

                    b.Property<int>("MascotaID");

                    b.HasKey("PersonaId", "MascotaID");

                    b.HasIndex("MascotaID");

                    b.ToTable("PersonasMascotas");
                });

            modelBuilder.Entity("Prueba.persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cedula");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("Prueba.Modelos.PersonaMascota", b =>
                {
                    b.HasOne("Prueba.Mascota", "Mascota")
                        .WithMany("PersonasMascotas")
                        .HasForeignKey("MascotaID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Prueba.persona", "Persona")
                        .WithMany("PersonasMascotas")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
