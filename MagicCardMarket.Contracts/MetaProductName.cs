using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Contracts
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class MetaProductName
    {
        [XmlElement("idLanguage", Form = XmlSchemaForm.Unqualified)]
        public int LanguageId { get; set; }

        [XmlElement("languageName", Form = XmlSchemaForm.Unqualified)]
        public string LanguageName { get; set; }

        [XmlElement("metaproductName", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
}
