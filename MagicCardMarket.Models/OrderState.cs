using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class OrderState
    {
        [XmlElement("state", Form = XmlSchemaForm.Unqualified)]
        public string State { get; set; }

        [XmlElement("dateBought", Form = XmlSchemaForm.Unqualified)]
        public string DateBought { get; set; } // optional

        [XmlElement("datePaid", Form = XmlSchemaForm.Unqualified)]
        public string DatePaid { get; set; } // optional

        [XmlElement("dateSent", Form = XmlSchemaForm.Unqualified)]
        public string DateSent { get; set; } // optional

        [XmlElement("dateReceived", Form = XmlSchemaForm.Unqualified)]
        public string DateReceived { get; set; } // optional
    }
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-07-15T21:26:59+0200</dateBought>
    //    <datePaid>2016-07-15T21:26:59+0200</datePaid>
    //    <dateSent>2016-07-20T01:47:22+0200</dateSent>
    //    <dateReceived>2016-07-20T01:47:22+0200</dateReceived>
    //  </state>
}
