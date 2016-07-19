using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class CreateWantsList
    {
        [XmlElement("idGame", Form = XmlSchemaForm.Unqualified)]
        public int GameId { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
}
