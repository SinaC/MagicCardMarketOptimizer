using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class Language
    {
        [XmlElement("idLanguage", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("languageName", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
}
