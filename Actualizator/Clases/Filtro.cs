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
        public string filtro { get; set; }
        public string descripcion { get; set; }

        //public string descripcion
        //{
        //    get
        //    {
        //        return cabecera.ToString() + " - " + filtro;
        //    }
        //    set { }
        //}

        //public override string ToString()
        //{
        //    return descripcion;
        //}
    }

    public enum Filtrado
    {
        TerminaPor = 0,
        Completo = 1
    }
}
