using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType("response", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "response", IsNullable = false)]
    public class ShoppingCarts
    {
        [XmlElement("account", Form = XmlSchemaForm.Unqualified)]
        public Account Account { get; set; }

        [XmlElement("shippingAddress", Form = XmlSchemaForm.Unqualified)]
        public Address ShippingAddress { get; set; }

        [XmlElement("shoppingCart", Form = XmlSchemaForm.Unqualified)]
        public ShoppingCart[] Carts { get; set; }
    }
}
