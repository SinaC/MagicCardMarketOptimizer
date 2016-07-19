using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    //[XmlType("request", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "request", IsNullable = false)]
    public class ShoppingCartRequest
    {
        [XmlElement("action", Form = XmlSchemaForm.Unqualified)]
        public string Action { get; set; }

        [XmlElement("article", Form=XmlSchemaForm.Unqualified)]
        public ShoppingCartArticle[] Articles { get; set; }
    }
}
