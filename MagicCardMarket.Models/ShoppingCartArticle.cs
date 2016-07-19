using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ShoppingCartArticle
    {
        [XmlElement("idArticle", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("amount", Form = XmlSchemaForm.Unqualified)]
        public int Amount { get; set; }
    }
}
