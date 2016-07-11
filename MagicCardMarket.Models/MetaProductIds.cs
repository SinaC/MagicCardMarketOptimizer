using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class MetaProductIds
    {
        [XmlElement("idProduct", Form = XmlSchemaForm.Unqualified)]
        public int[] ProductIds { get; set; }
    }
}
