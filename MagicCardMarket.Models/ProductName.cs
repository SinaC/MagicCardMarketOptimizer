using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ProductName
    {
        [XmlElement("idLanguage", Form = XmlSchemaForm.Unqualified)]
        public int LanguageId { get; set; }

        [XmlElement("languageName", Form = XmlSchemaForm.Unqualified)]
        public string LanguageName { get; set; }

        [XmlElement("productName", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
    //  <name>
    //    <idLanguage>1</idLanguage>
    //    <languageName>English</languageName>
    //    <productName>Island(Version 2)</productName>
    //  </name>
}
