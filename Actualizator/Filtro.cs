using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actualizator
{
    public class Filtro
    {
        public Filtrado cabecera;
        public string filtro;
        public string descripcion
        {
            get { return cabecera.ToString() + " - " + filtro; }
        }

        public override string ToString()
        {
            return descripcion;
        }

    }

    public enum Filtrado
    {
        TerminaPor = 0,
        Completo = 1        
    }
}
