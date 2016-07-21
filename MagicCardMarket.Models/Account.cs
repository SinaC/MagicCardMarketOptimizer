using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:User
    [Serializable]
    [XmlType("account", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "account", IsNullable = false)]
    public class Account : User
    {
        [XmlElement("name", Form = XmlSchemaForm.Unqualified)]
        public AccountName AccountName { get; set; }

        [XmlElement("address", Form = XmlSchemaForm.Unqualified)]
        public Address Address { get; set; }

        [XmlElement("accountBalance", Form = XmlSchemaForm.Unqualified)]
        public decimal AccountBalance { get; set; }

        [XmlElement("bankRecharge", Form = XmlSchemaForm.Unqualified)]
        public decimal BankRecharge { get; set; }

        [XmlElement("paypalRecharge", Form = XmlSchemaForm.Unqualified)]
        public decimal PayPalRecharge { get; set; }

        [XmlElement("unreadMessages", Form = XmlSchemaForm.Unqualified)]
        public int UnreadMessages { get; set; }
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
