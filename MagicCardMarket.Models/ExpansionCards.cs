using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //http://www.mkmapi.eu/ws/documentation/API_1.1:Expansion_Singles
    [Serializable]
    [XmlType("response", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "response", IsNullable = false)]
    public class ExpansionCards
    {
        [XmlElement("expansion", Form = XmlSchemaForm.Unqualified)]
        public Expansion Expansion { get; set; }

        [XmlElement("card", Form = XmlSchemaForm.Unqualified)]
        public Product[] Cards { get; set; }
    }
}
