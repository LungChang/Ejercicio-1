using Microsoft.EntityFrameworkCore;
using Prueba.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            bool Salir = false;
            while (!Salir)
            {
                Console.WriteLine("1-Agregar Persona\n2-Agregar Animal\n3-Ver todas las personas\n4-Ver todos los animales" +
                                  "\n5-Ver las mascotas de una persona\n6-Ver las personas de una mascota\n7-Modificar persona por cedula" +
                                  "\n8-Modificar Mascota por nombre\n9-Eliminar persona por cedula\n10-Eliminar mascota por nombre\n11-Salir");
                switch (Console.ReadLine())
                {
                    case "1":
                        
                        AgregarPersona(CrearPersona());
                        break;
                    case "2":
                      
                        AgregarMascota(CrearMascota());
                        break;
                    case "3":
                        VerTodasPersonas();
                        break;
                    case "4":
                        VerTodasMascotas();
                        break;
                    case "5":
                        VerLasMascotasDeUnaPersona();
                        break;
                    case "6":
                        VerLasPersonasDeUnaMascota();
                        break;
                    case "7":
                        ActualizarPersonaDesconectado();
                        break;
                    case "8":
                        ActualizarMascotaDesconectado();
                        break;
                    case "9":
                        EliminarPersonaConectado();
                        break;
                    case "10":
                        EliminarMascotaConectado();
                        break;
                    case "11":
                        Salir = true;
                        break;
                }
                Console.WriteLine("--------Listo--------");
            }
        }

        static persona CrearPersona()
        {
            Console.WriteLine("Nueva Cedula");
            int cedula = int.Parse(Console.ReadLine());
            Console.WriteLine("Nueva Nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Nueva  Fecha de nacimiento ejemplo 2000-01-01");//2009-02-26 18:37:58
            DateTime fecha = DateTime.Parse(Console.ReadLine() + " 18:37:58");
            persona per = new persona()
            {
                Cedula = cedula,
                Nombre = nombre,
                FechaNacimiento = fecha
            };
            return per;
        }

        static Mascota CrearMascota()
        {
            bool Adaptacion = false;
            Console.WriteLine("Nuevo Nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Nueva Raza");
            string Raza = Console.ReadLine();
            Console.WriteLine("Estado Adopcion ---> 1-adoptado 2-No adoptado");
            int estado = int.Parse(Console.ReadLine());
            if (estado == 1)
            {
                Adaptacion = true;
            }
            Mascota mas = new Mascota()
            {
                Nombre = nombre,
                Raza = Raza,
                EstadoAdopcion = Adaptacion
            };
            return mas;
        }

        //modelo conectado
        static void EliminarMascotaConectado()
        {
            Console.WriteLine("Nombre de la mascota que va a hacer eliminada");
            string nombre = Console.ReadLine();
            Mascota  mas;
            using (var context = new PersonaContext())
            {
                mas = context.Mascotas.Where(x => x.Nombre.Equals(nombre)).FirstOrDefault();
                context.Mascotas.Remove(mas);
                context.SaveChanges();
            }
        }

        //modelo conectado
        static void EliminarPersonaConectado()
        {
            Console.WriteLine("Cedula de la persona que va a hacer eliminada");
            int cedula = int.Parse(Console.ReadLine());
            persona per;
            using (var context = new PersonaContext())
            {
                per = context.Personas.Where(x => x.Cedula == cedula).FirstOrDefault();
                context.Personas.Remove(per);
                context.SaveChanges();
            }
        }

        static void VerLasPersonasDeUnaMascota()
        {
            Console.WriteLine("Nombre de la mascota");
            string nombre = Console.ReadLine();
            Mascota mas;
            List<PersonaMascota> List = null;
            using (var context = new PersonaContext())
            {
                mas = context.Mascotas.Where(x => x.Nombre.Equals(nombre)).FirstOrDefault();
            }

            using (var context = new PersonaContext())
            {
                List = context.Mascotas.Where(x => x.Nombre.Equals(nombre)).Include(x => x.PersonasMascotas)
                    .ThenInclude(y => y.Mascota).FirstOrDefault().PersonasMascotas.ToList();
            }

            foreach(var personas in List)
            {
                using (var context = new PersonaContext())
                {
                    persona per = context.Personas.Where(x => x.Id == personas.PersonaId).FirstOrDefault();
                    Console.WriteLine(per.Nombre);
                }
            }
        }

        static void VerLasMascotasDeUnaPersona()
        {
            Console.WriteLine("Cedula");
            int cedula = int.Parse(Console.ReadLine());
            persona per;
            List<PersonaMascota> List = null;
            using (var context = new PersonaContext())
            {
                per = context.Personas.Where(x => x.Cedula == cedula).FirstOrDefault();
            }

            using (var context = new PersonaContext())
            {
                List = context.Personas.Where(x => x.Id == per.Id).Include(x => x.PersonasMascotas)
                    .ThenInclude(y => y.Mascota).FirstOrDefault().PersonasMascotas.ToList();
            }
            foreach(var mascotas in List)
            {
                using (var context = new PersonaContext())
                {
                    Mascota mas = context.Mascotas.Where(x => x.Id == mascotas.MascotaID).FirstOrDefault();
                    Console.WriteLine(mas.Nombre);
                }
            }
        }

        //modelo desconectado
        static void ActualizarMascotaDesconectado()
        {
            Console.WriteLine("Nombre de la mascota que se va a actualizar");
            string nombre = Console.ReadLine();
            Mascota mas;
            using (var context = new PersonaContext())
            {
                mas = context.Mascotas.Where(x => x.Nombre == nombre).FirstOrDefault();
            }
            Mascota masActualizada = CrearMascota();
            mas.Nombre = masActualizada.Nombre;
            mas.Raza = masActualizada.Raza;
            mas.EstadoAdopcion = masActualizada.EstadoAdopcion;
            using (var context = new PersonaContext())
            {
                context.Entry(mas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }

        //modelo desconectado
        static void ActualizarPersonaDesconectado()
        {
            Console.WriteLine("Cedula la que se va a actualizar");
            int cedula = int.Parse(Console.ReadLine());
            persona per;
            using (var context = new PersonaContext())
            {
                per = context.Personas.Where(x => x.Cedula == cedula).FirstOrDefault();
            }
            persona PerActualizada = CrearPersona();
            per.Cedula = PerActualizada.Cedula;
            per.Nombre = PerActualizada.Nombre;
            per.FechaNacimiento = PerActualizada.FechaNacimiento;
            using (var context = new PersonaContext())
            {
                context.Entry(per).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }


        


        static void VerTodasMascotas()
        {
            Console.WriteLine("Nombre" + " | " + "Raza");
            List<Mascota> mas = null;
            using (var context = new PersonaContext())
            {
                mas = context.Mascotas.ToList();
            }
            foreach (var mascotas in mas)
            {
                Console.WriteLine(mascotas.Nombre + " | " + mascotas.Raza);
            }
        }
        static void VerTodasPersonas()
        {
            Console.WriteLine("Cedula" + " | " + "Nombre");
            List<persona> per = null;
            using (var context = new PersonaContext())
            {
                per = context.Personas.ToList();
            }
            foreach(var personas in per)
            {
                Console.WriteLine(personas.Cedula + " | " + personas.Nombre);
            }
        }

        static void AgregarPersona(persona Persona)
        {
            using (var context = new PersonaContext())
            {
                var pe = new persona();
                pe.Cedula = Persona.Cedula;
                pe.Nombre = Persona.Nombre;
                pe.FechaNacimiento = Persona.FechaNacimiento;
                context.Add(pe);
                context.SaveChanges();
            }
        }

        static void AgregarMascota(Mascota mascota)
        {
            using (var context = new PersonaContext())
            {
                var Mascota = new Mascota();
                Mascota.Nombre = mascota.Nombre;
                Mascota.Raza = mascota.Raza;
                Mascota.EstadoAdopcion = mascota.EstadoAdopcion;
                context.Add(Mascota);
                context.SaveChanges();
            }
        }



        static void AgregarRelacionPersonaAMascota()
        {
            using (var context = new PersonaContext())
            {
                var persona = context.Personas.FirstOrDefault();
                var mascotas = context.Mascotas.FirstOrDefault();
                var personaMascotas = new PersonaMascota();
                personaMascotas.PersonaId = mascotas.Id;
                personaMascotas.MascotaID = persona.Id;
                context.Add(personaMascotas);
                context.SaveChanges();
            }
        }

        

        

        

      

       


        ////modelo desconectado
        //static void EliminarPersonDesconectado()
        //{
        //    persona Lung;
        //    using (var context = new PersonaContext())
        //    {
        //        Lung = context.Personas.Where(x => x.Nombre == "Lung").FirstOrDefault();

        //    }

        //    using (var context = new PersonaContext())
        //    {
        //        context.Entry(Lung).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        //        context.SaveChanges();
        //    }
        //}

    }
}
