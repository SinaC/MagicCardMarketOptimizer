using System;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:MessageThread
    [Serializable]
    [XmlType("thread", AnonymousType = true)]
    public class MessageThread
    {
        [XmlElement("partner", Form = XmlSchemaForm.Unqualified)]
        public MessageThreadPartner Partner { get; set; }

        [XmlElement("message", Form = XmlSchemaForm.Unqualified)]
        public Message[] Messages { get; set; }

        [XmlElement("unreadMessages", Form = XmlSchemaForm.Unqualified)]
        public string UnreadMessagesCountRaw  { get; set; }

        [XmlIgnore]
        public bool UnreadMessagesCount
    {
            get { return Helpers.SafeConvertToBool(UnreadMessagesCountRaw); }
            set { UnreadMessagesCountRaw = value.ToString(CultureInfo.InvariantCulture); }
        }
    }
}
