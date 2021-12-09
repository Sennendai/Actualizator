using System.Collections.Generic;

namespace Actualizator
{
    public class Filtro
    {
        public Filtrado cabecera;
        public string NombreFiltro { get; set; }
        public string Descripcion { get; set; }
        public List<string> Filtros { get; set; }
    }

    public enum Filtrado
    {
        TerminaPor = 0,
        Completo = 1,
        Ruta = 2
    }
}
