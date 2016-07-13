using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType("message", AnonymousType = true)]
    public class Message
    {
        [XmlElement("idMessage", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("isSending", Form = XmlSchemaForm.Unqualified)]
        public bool IsSending { get; set; }

        [XmlElement("date", Form = XmlSchemaForm.Unqualified)]
        public string Date { get; set; } // TODO: DateTime

        [XmlElement("text", Form = XmlSchemaForm.Unqualified)]
        public string Text { get; set; }

        [XmlElement("unread", Form = XmlSchemaForm.Unqualified)]
        public bool IsUnread { get; set; }
    }
}
