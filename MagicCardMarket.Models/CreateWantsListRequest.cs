using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    //[XmlType("request", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "request", IsNullable = false)]
    public class CreateWantsListRequest
    {
        [XmlElement("wantslist", Form = XmlSchemaForm.Unqualified)]
        public CreateWantsList WantsList { get; set; }
    }
}
