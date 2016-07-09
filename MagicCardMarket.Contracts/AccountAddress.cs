using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Contracts
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class AccountAddress
    {
        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }

        [XmlElement("extra", Form = XmlSchemaForm.Unqualified)]
        public string Extra { get; set; }

        [XmlElement("street", Form = XmlSchemaForm.Unqualified)]
        public string Street { get; set; }

        [XmlElement("zip", Form = XmlSchemaForm.Unqualified)]
        public string Zip { get; set; }

        [XmlElement("city", Form = XmlSchemaForm.Unqualified)]
        public string City { get; set; }

        [XmlElement("country", Form = XmlSchemaForm.Unqualified)]
        public string Country { get; set; }

    }
    //  <address>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </address>
}
