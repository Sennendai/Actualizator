using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Actualizator
{
    [Serializable]
    public class Proyecto
    {
        [XmlElement("Identifier")]
        public Guid Identifier { get; set; }
        [XmlElement("ProyectoName")]
        public string ProyectoName { get ; set; }        
        [XmlElement("PathOrigen")]
        public string PathOrigen { get; set; }
        [XmlElement("PathDestino")]
        public List<string> PathDestino { get; set; }
        [XmlElement("PathBackup")]
        public string PathBackup { get; set; }
        [XmlElement("LastPathBackup")]
        public string LastPathBackup { get; set; }
        [XmlElement("FicherosExcluidos")]
        public BindingList<Filtro> FicherosExcluidos { get; set; }
        [XmlElement("HacerBackup")]
        public bool HacerBackup { get; set; }
        [XmlElement("Filtrar")]
        public bool Filtrar { get; set; }

        public Proyecto()
        {
            
        }

        public Proyecto(Guid Identifier)
        {
            this.Identifier = Identifier;
        }

        public Proyecto(Guid Identifier, string ProyectoName, string PathOrigen, List<string> PathDestino, string PathBackup, string LastPathBackup,
            BindingList<Filtro> FicherosExcluidos, bool HacerBackup, bool Filtrar)
        {
            this.Identifier = Identifier;
            this.ProyectoName = ProyectoName;
            this.PathOrigen = PathOrigen;
            this.PathDestino = PathDestino;
            this.PathBackup = PathBackup;
            this.LastPathBackup = LastPathBackup;
            this.FicherosExcluidos = FicherosExcluidos;
            this.HacerBackup = HacerBackup;
            this.Filtrar = Filtrar;
        }

    }
}
