using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    //[XmlType("request", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "request", IsNullable = false)]
    public class AddMetaProductOrProductRequest
    {
        [XmlElement("action", Form = XmlSchemaForm.Unqualified)]
        public string Action { get; set; }

        [XmlElement("product", Form = XmlSchemaForm.Unqualified)]
        public AddProduct[] Products { get; set; }

        [XmlElement("metaproduct", Form = XmlSchemaForm.Unqualified)]
        public AddMetaProduct[] MetaProducts { get; set; }
    }
}
