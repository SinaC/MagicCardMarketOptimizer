using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class MessageThreadPartner
    {
        [XmlElement("idUser", Form = XmlSchemaForm.Unqualified)]
        public int UserId { get; set; }

        [XmlElement("username", Form = XmlSchemaForm.Unqualified)]
        public string UserName { get; set; }

        [XmlElement("country", Form = XmlSchemaForm.Unqualified)]
        public string Country { get; set; }

        [XmlElement("isCommercial", Form = XmlSchemaForm.Unqualified)]
        public bool IsCommercial { get; set; }

        // 0: no risk
        // 1: low risk
        // 2: high risk
        [XmlElement("riskGroup", Form = XmlSchemaForm.Unqualified)]
        public int RiskGroup { get; set; }

        // 0: not enough sells to rate
        // 1: outstanding seller
        // 2: very good seller
        // 3: good seller
        // 4: average seller
        // 5: bad seller
        [XmlElement("reputation", Form = XmlSchemaForm.Unqualified)]
        public int Reputation { get; set; }

        // 0: normal shipping speed
        // 1: ships very fast
        // 2: ships fast
        [XmlElement("shipsFast", Form = XmlSchemaForm.Unqualified)]
        public int ShipsFast { get; set; }

        [XmlElement("sellCount", Form = XmlSchemaForm.Unqualified)]
        public int SellCount { get; set; }

        [XmlElement("onVacation", Form = XmlSchemaForm.Unqualified)]
        public bool OnVacation { get; set; }

        // 1: English
        // 2: French
        // 3: German
        // 4: Spanish
        // 5: Italian
        [XmlElement("idDisplayLanguage", Form = XmlSchemaForm.Unqualified)]
        public int DisplayLanguageId { get; set; }

        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public AccountName AccountName { get; set; }
    }
}
