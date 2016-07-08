using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarketOptimizer.DataContracts
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class MetaProductIds
    {
        [XmlElement("idProduct", Form = XmlSchemaForm.Unqualified)]
        public int[] ProductId { get; set; }
    }
}
