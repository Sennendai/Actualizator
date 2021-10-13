using System.Collections.Generic;
using System.Xml.Serialization;

namespace Actualizator
{
    [XmlRoot("Proyectos")]
    public class Proyectos
    {
        public Proyectos() { proyectoItems = new List<Proyecto>(); }

        [XmlElement("Proyecto")]
        public List<Proyecto> proyectoItems { get; set; }
    }
}
