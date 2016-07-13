using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ShippingMethod
    {
        [XmlElement("idShippingMethod", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        [XmlElement("price", Form = XmlSchemaForm.Unqualified)]
        public decimal Price { get; set; }

        [XmlElement("isLetter", Form = XmlSchemaForm.Unqualified)]
        public bool IsLetter { get; set; }

        [XmlElement("isInsured", Form = XmlSchemaForm.Unqualified)]
        public bool IsInsured { get; set; }
    }
}
