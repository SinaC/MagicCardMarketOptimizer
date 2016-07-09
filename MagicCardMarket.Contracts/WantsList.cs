using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Contracts
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Wantslist
    [Serializable]
    [XmlType("wantslist", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "wantslist", IsNullable = false)]
    public class WantsList
    {
        [XmlElement("idWantsList", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("game", Form = XmlSchemaForm.Unqualified)]
        public Game Game { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        [XmlElement("itemCount", Form = XmlSchemaForm.Unqualified)]
        public int ItemCount { get; set; }

    }
}
