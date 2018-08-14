using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Modelos
{
    class PersonaMascota
    {
        public int PersonaId { get; set; }
        public int MascotaID { get; set; }
        public persona Persona { get; set; }
        public Mascota  Mascota { get; set; }
    }
}
