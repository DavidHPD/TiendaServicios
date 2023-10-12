using System;
using System.Collections.Generic;

namespace MicroService.Api.Autor.Models
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public List<GradoAcademico> listaGradoAcademico { get; set; }

        public string AutorLibroGuid { get; set; }
    }
}
