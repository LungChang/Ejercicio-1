using Prueba.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Prueba
{
    class Mascota
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Raza { get; set; }
        public bool EstadoAdopcion { get; set; }
        public List<PersonaMascota> PersonasMascotas { get; set; }
    }
}
