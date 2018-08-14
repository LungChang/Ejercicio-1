using Prueba.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prueba
{
    class persona
    {
        [Key]
        public int Id { get; set; }
        public int Cedula { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<PersonaMascota> PersonasMascotas { get; set; }
    }
}
