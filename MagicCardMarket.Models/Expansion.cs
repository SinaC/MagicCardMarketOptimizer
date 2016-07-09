using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Expansion
    [Serializable]
    [XmlType("expansion", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "expansion", IsNullable = false)]
    //[Serializable]
    //[XmlType(AnonymousType = true)]
    public class Expansion
    {
        [XmlElement("idExpansion", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        [XmlElement("icon", Form = XmlSchemaForm.Unqualified)]
        public int Icon { get; set; }
    }
}
