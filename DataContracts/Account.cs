using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarketOptimizer.DataContracts
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:User
    [Serializable]
    [XmlType("account", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "account", IsNullable = false)]
    public class Account
    {
        [XmlElement("idUser", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("username", Form = XmlSchemaForm.Unqualified)]
        public string UserName { get; set; }

        [XmlElement("country", Form = XmlSchemaForm.Unqualified)]
        public string Country { get; set; }

        // 0: private user
        // 1: commercial user
        // 2: powerseller
        [XmlElement("isCommercial", Form = XmlSchemaForm.Unqualified)]
        public int IsCommercial { get; set; }

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

        [XmlElement("address", Form = XmlSchemaForm.Unqualified)]
        public AccountAddress Address { get; set; }

        [XmlElement("accountBalance", Form = XmlSchemaForm.Unqualified)]
        public decimal AccountBalance { get; set; }

        [XmlElement("bankRecharge", Form = XmlSchemaForm.Unqualified)]
        public decimal BankRecharge { get; set; }

        [XmlElement("paypalRecharge", Form = XmlSchemaForm.Unqualified)]
        public decimal PayPalRecharge { get; set; }

        [XmlElement("unreadMessages", Form = XmlSchemaForm.Unqualified)]
        public decimal UnreadMessages { get; set; }
    }
    //  <account>
    //  <idUser>55207</idUser>
    //  <username>sinac</username>
    //  <country>BE</country>
    //  <isCommercial>0</isCommercial>
    //  <riskGroup>1</riskGroup>
    //  <reputation>0</reputation>
    //  <shipsFast>-1</shipsFast>
    //  <sellCount>0</sellCount>
    //  <onVacation>false</onVacation>
    //  <idDisplayLanguage>1</idDisplayLanguage>
    //  <name>
    //    <firstName>Joël</firstName>
    //    <lastName>Heymbeeck</lastName>
    //  </name>
    //  <address>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </address>
    //  <accountBalance>1.34</accountBalance>
    //  <bankRecharge>138.11</bankRecharge>
    //  <paypalRecharge>145.37</paypalRecharge>
    //  <articlesInShoppingCart>0</articlesInShoppingCart>
    //  <unreadMessages>0</unreadMessages>
    //</account>
}
