using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:ShoppingCart
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ShoppingCart
    {
        [XmlElement("idReservation", Form = XmlSchemaForm.Unqualified)]
        public int ReservationId { get; set; }

        [XmlElement("seller", Form = XmlSchemaForm.Unqualified)]
        public ArticleSeller Seller { get; set; }

        [XmlElement("article", Form = XmlSchemaForm.Unqualified)]
        public Article[] Articles { get; set; }

        [XmlElement("articleValue", Form = XmlSchemaForm.Unqualified)]
        public decimal ArticleValue { get; set; }

        [XmlElement("articleCount", Form = XmlSchemaForm.Unqualified)]
        public int ArticleCount { get; set; }

        [XmlElement("shippingMethod", Form = XmlSchemaForm.Unqualified)]
        public ShippingMethod ShippingMethod { get; set; }

        [XmlElement("totalValue", Form = XmlSchemaForm.Unqualified)]
        public decimal TotalValue { get; set; }
    }
}
