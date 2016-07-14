using System;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:MessageThread
    [Serializable]
    [XmlType("thread", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "thread", IsNullable = false)]
    public class MessageThread
    {
        [XmlElement("partner", Form = XmlSchemaForm.Unqualified)]
        public MessageThreadPartner Partner { get; set; }

        [XmlElement("message", Form = XmlSchemaForm.Unqualified)]
        public Message[] Messages { get; set; }

        [XmlElement("unreadMessages", Form = XmlSchemaForm.Unqualified)]
        public string UnreadMessagesCountRaw { get; set; }

        [XmlIgnore]
        public int UnreadMessagesCount
        {
            get { return Helpers.SafeConvertToInt(UnreadMessagesCountRaw); }
            set { UnreadMessagesCountRaw = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}
