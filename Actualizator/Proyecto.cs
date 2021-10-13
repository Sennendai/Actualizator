using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Actualizator
{
    [Serializable]
    public class Proyecto
    {
        [XmlElement("ProyectoName")]
        public string ProyectoName { get; set; }
        [XmlElement("PathOrigen")]
        public string PathOrigen { get; set; }
        [XmlElement("PathDestino")]
        public List<string> PathDestino { get; set; }
        [XmlElement("PathBackup")]
        public string PathBackup { get; set; }
        [XmlElement("FicherosExcluidos")]
        public BindingList<Filtro> FicherosExcluidos { get; set; }                

        public Proyecto()
        {

        }

        public Proyecto(string ProyectoName, string PathOrigen, List<string> PathDestino, string PathBackup, BindingList<Filtro> FicherosExcluidos)
        {
            this.ProyectoName = ProyectoName;
            this.PathOrigen = PathOrigen;
            this.PathDestino = PathDestino;
            this.PathBackup = PathBackup;
            this.FicherosExcluidos = FicherosExcluidos;
        }

    }
}
