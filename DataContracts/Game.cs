using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarketOptimizer.DataContracts
{
    [Serializable]
    [XmlType("game", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "game", IsNullable = false)]
    public class Game
    {
        [XmlElement("idGame", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
    //<game>
    //  <idGame>1</idGame>
    //  <name>Magic the Gathering</name>
    //</game>
}
