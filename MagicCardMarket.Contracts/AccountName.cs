using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Contracts
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class AccountName
    {
        [XmlElement("firstName", Form = XmlSchemaForm.Unqualified)]
        public string FirstName { get; set; }

        [XmlElement("lastName", Form = XmlSchemaForm.Unqualified)]
        public string LastName { get; set; }
    }
    //  <name>
    //    <firstName>Joël</firstName>
    //    <lastName>Heymbeeck</lastName>
    //  </name>
}
