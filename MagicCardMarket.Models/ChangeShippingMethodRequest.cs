using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    //[XmlType("request", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "request", IsNullable = false)]
    public class ChangeShippingMethodRequest
    {
        [XmlElement("idShippingMethod", Form = XmlSchemaForm.Unqualified)]
        public int ShippingMethodId { get; set; }
    }
}
