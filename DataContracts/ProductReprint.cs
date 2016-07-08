using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarketOptimizer.DataContracts
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ProductReprint
    {
        [XmlElement("idProduct", Form = XmlSchemaForm.Unqualified)]
        public int ProductId { get; set; }

        [XmlElement("expansion", Form = XmlSchemaForm.Unqualified)]
        public string Expansion { get; set; }

        [XmlElement("expIcon", Form = XmlSchemaForm.Unqualified)]
        public int ExpansionIcon { get; set; }
    }
}
