using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType("order", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "order", IsNullable = false)]
    public class Order
    {
        [XmlElement("idOrder", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("isBuyer", Form = XmlSchemaForm.Unqualified)]
        public bool IsBuyer { get; set; }

        [XmlElement("seller", Form = XmlSchemaForm.Unqualified)]
        public ArticleSeller Seller { get; set; }

        [XmlElement("buyer", Form = XmlSchemaForm.Unqualified)]
        public ArticleSeller Buyer { get; set; }

        [XmlElement("state", Form = XmlSchemaForm.Unqualified)]
        public OrderState State { get; set; }

        [XmlElement("shippingMethod", Form = XmlSchemaForm.Unqualified)]
        public ShippingMethod ShippingMethod { get; set; }

        [XmlElement("isPresale", Form = XmlSchemaForm.Unqualified)]
        public bool IsPresale { get; set; } // indicates if this order is presale (can be only true, when state is "bought" or "paid"

        [XmlElement("shippingAddress", Form = XmlSchemaForm.Unqualified)]
        public Address ShippingAddress { get; set; } // (is empty when "isPresale" is "true")

        [XmlElement("note", Form = XmlSchemaForm.Unqualified)]
        public string Note { get; set; }

        [XmlElement("articleCount", Form = XmlSchemaForm.Unqualified)]
        public int ArticleCount { get; set; }

        [XmlElement("evaluation", Form = XmlSchemaForm.Unqualified)]
        public Evaluation Evaluation { get; set; }

        [XmlElement("article", Form = XmlSchemaForm.Unqualified)]
        public Article[] Articles { get; set; }

        [XmlElement("articleValue", Form = XmlSchemaForm.Unqualified)]
        public decimal ArticleValue { get; set; }

        [XmlElement("totalValue", Form = XmlSchemaForm.Unqualified)]
        public decimal TotalValue { get; set; }
    }
    //<order>
    //  <idOrder>35681911</idOrder>
    //  <isBuyer>true</isBuyer>
    //  <seller>
    //    <idUser>328612</idUser>
    //    <username>G-Kshop</username>
    //    <country>HR</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>0</riskGroup>
    //    <reputation>1</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>393</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Ivan</firstName>
    //      <lastName>Križnik</lastName>
    //    </name>
    //    <address>
    //      <name>Ivan Križnik</name>
    //      <extra></extra>
    //      <street>Dalmatinska 7</street>
    //      <zip>10000</zip>
    //      <city>Zagreb</city>
    //      <country>HR</country>
    //    </address>
    //  </seller>
    //  <buyer>
    //    <idUser>55207</idUser>
    //    <username>sinac</username>
    //    <country>BE</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>1</riskGroup>
    //    <reputation>0</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>0</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Joël</firstName>
    //      <lastName>Heymbeeck</lastName>
    //    </name>
    //    <address>
    //      <name>Joël Heymbeeck</name>
    //      <extra></extra>
    //      <street>Rue Du Menil, 35</street>
    //      <zip>1420</zip>
    //      <city>Braine-l'Alleud</city>
    //      <country>BE</country>
    //    </address>
    //  </buyer>
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-07-15T21:26:59+0200</dateBought>
    //    <datePaid>2016-07-15T21:26:59+0200</datePaid>
    //    <dateSent>2016-07-20T01:47:22+0200</dateSent>
    //  </state>
    //  <shippingMethod>
    //    <idShippingMethod>132657</idShippingMethod>
    //    <name>Priority Letter</name>
    //    <price>1.76</price>
    //    <isLetter>true</isLetter>
    //    <isInsured>false</isInsured>
    //  </shippingMethod>
    //  <isPresale>false</isPresale>
    //  <shippingAddress>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </shippingAddress>
    //  <note></note>
    //  <articleCount>11</articleCount>
    //  <article>
    //    <idArticle>256247682</idArticle>
    //    <idProduct>17722</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>0.2</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Gleancrawler</name>
    //      <image>./img/cards/Prerelease_Promos/gleancrawler.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Prerelease Promos</expansion>
    //      <expIcon>80</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>GD</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>260056835</idArticle>
    //    <idProduct>13417</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>0.1</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Grave-Shell Scarab</name>
    //      <image>./img/cards/Ravnica_City_of_Guilds/grave_shell_scarab.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Ravnica: City of Guilds</expansion>
    //      <expIcon>53</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>EX</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>260055765</idArticle>
    //    <idProduct>13813</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>0.2</price>
    //    <count>3</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Dread Return</name>
    //      <image>./img/cards/Time_Spiral/dread_return.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Time Spiral</expansion>
    //      <expIcon>54</expIcon>
    //      <rarity>Uncommon</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>256036902</idArticle>
    //    <idProduct>18961</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>0.12</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Reach of Branches</name>
    //      <image>./img/cards/Morningtide/reach_of_branches.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Morningtide</expansion>
    //      <expIcon>89</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>260056454</idArticle>
    //    <idProduct>22214</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>1.6</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Tectonic Edge</name>
    //      <image>./img/cards/Worldwake/tectonic_edge.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Worldwake</expansion>
    //      <expIcon>115</expIcon>
    //      <rarity>Uncommon</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>256037693</idArticle>
    //    <idProduct>257108</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>0.15</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Arbor Elf</name>
    //      <image>./img/cards/Magic_2013/arbor_elf.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Magic 2013</expansion>
    //      <expIcon>120</expIcon>
    //      <rarity>Common</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>256033688</idArticle>
    //    <idProduct>264784</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>0.14</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Serra Avatar</name>
    //      <image>./img/cards/Commander_2013/serra_avatar.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Commander 2013</expansion>
    //      <expIcon>226</expIcon>
    //      <rarity>Mythic</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>259355061</idArticle>
    //    <idProduct>283371</idProduct>
    //    <language>
    //      <idLanguage>4</idLanguage>
    //      <languageName>Spanish</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>9.99</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Nissa, vidente del Bosque Extenso / Nissa, animista sabia</name>
    //      <image>./img/cards/Magic_Origins/nissa_vastwood_seer_nissa_sage_animist.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Magic Orígenes</expansion>
    //      <expIcon>358</expIcon>
    //      <rarity>Mythic</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>259348577</idArticle>
    //    <idProduct>289101</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>fast shiping</comments>
    //    <price>2.5</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>The Gitrog Monster</name>
    //      <image>./img/cards/Shadows_over_Innistrad/the_gitrog_monster.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Shadows over Innistrad</expansion>
    //      <expIcon>379</expIcon>
    //      <rarity>Mythic</rarity>
    //    </product>
    //    <condition>EX</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <articleValue>15.4</articleValue>
    //  <totalValue>17.16</totalValue>
    //</order>
    //<order>
    //  <idOrder>35861833</idOrder>
    //  <isBuyer>true</isBuyer>
    //  <seller>
    //    <idUser>19716</idUser>
    //    <username>Fnyrri</username>
    //    <country>D</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>0</riskGroup>
    //    <reputation>1</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>589</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Kevin</firstName>
    //      <lastName>Schantz</lastName>
    //    </name>
    //    <address>
    //      <name>Kevin Schantz</name>
    //      <extra></extra>
    //      <street>Heimstattweg 5</street>
    //      <zip>01239</zip>
    //      <city>Dresden</city>
    //      <country>D</country>
    //    </address>
    //  </seller>
    //  <buyer>
    //    <idUser>55207</idUser>
    //    <username>sinac</username>
    //    <country>BE</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>1</riskGroup>
    //    <reputation>0</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>0</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Joël</firstName>
    //      <lastName>Heymbeeck</lastName>
    //    </name>
    //    <address>
    //      <name>Joël Heymbeeck</name>
    //      <extra></extra>
    //      <street>Rue Du Menil, 35</street>
    //      <zip>1420</zip>
    //      <city>Braine-l'Alleud</city>
    //      <country>BE</country>
    //    </address>
    //  </buyer>
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-07-18T20:52:41+0200</dateBought>
    //    <datePaid>2016-07-18T20:52:41+0200</datePaid>
    //    <dateSent>2016-07-19T09:22:44+0200</dateSent>
    //  </state>
    //  <shippingMethod>
    //    <idShippingMethod>52481</idShippingMethod>
    //    <name>Letter (Standardbrief)</name>
    //    <price>1.2</price>
    //    <isLetter>true</isLetter>
    //    <isInsured>false</isInsured>
    //  </shippingMethod>
    //  <isPresale>false</isPresale>
    //  <shippingAddress>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </shippingAddress>
    //  <note></note>
    //  <articleCount>4</articleCount>
    //  <article>
    //    <idArticle>257448961</idArticle>
    //    <idProduct>287190</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments></comments>
    //    <price>0.55</price>
    //    <count>4</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Ayli, Eternal Pilgrim</name>
    //      <image>./img/cards/Oath_of_the_Gatewatch/ayli_eternal_pilgrim.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Oath of the Gatewatch</expansion>
    //      <expIcon>374</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <articleValue>2.2</articleValue>
    //  <totalValue>3.4</totalValue>
    //</order>
    //<order>
    //  <idOrder>39832762</idOrder>
    //  <isBuyer>true</isBuyer>
    //  <seller>
    //    <idUser>1036357</idUser>
    //    <username>Pomatzki</username>
    //    <country>D</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>0</riskGroup>
    //    <reputation>1</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>242</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>3</idDisplayLanguage>
    //    <name>
    //      <firstName>Tom</firstName>
    //      <lastName>Förster</lastName>
    //    </name>
    //    <address>
    //      <name>Tom Förster</name>
    //      <extra></extra>
    //      <street>Einsteinstraße 8</street>
    //      <zip>39104</zip>
    //      <city>Magdeburg</city>
    //      <country>D</country>
    //    </address>
    //  </seller>
    //  <buyer>
    //    <idUser>55207</idUser>
    //    <username>sinac</username>
    //    <country>BE</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>1</riskGroup>
    //    <reputation>0</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>0</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Joël</firstName>
    //      <lastName>Heymbeeck</lastName>
    //    </name>
    //    <address>
    //      <name>Joël Heymbeeck</name>
    //      <extra></extra>
    //      <street>Rue Du Menil, 35</street>
    //      <zip>1420</zip>
    //      <city>Braine-l'Alleud</city>
    //      <country>BE</country>
    //    </address>
    //  </buyer>
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-07-18T20:05:13+0200</dateBought>
    //    <datePaid>2016-07-18T20:05:13+0200</datePaid>
    //    <dateSent>2016-07-19T07:04:18+0200</dateSent>
    //  </state>
    //  <shippingMethod>
    //    <idShippingMethod>52481</idShippingMethod>
    //    <name>Letter (Standardbrief)</name>
    //    <price>1.2</price>
    //    <isLetter>true</isLetter>
    //    <isInsured>false</isInsured>
    //  </shippingMethod>
    //  <isPresale>false</isPresale>
    //  <shippingAddress>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </shippingAddress>
    //  <note></note>
    //  <articleCount>1</articleCount>
    //  <article>
    //    <idArticle>260243145</idArticle>
    //    <idProduct>288644</idProduct>
    //    <language>
    //      <idLanguage>3</idLanguage>
    //      <languageName>German</languageName>
    //    </language>
    //    <comments>booster to sleeve</comments>
    //    <price>12.5</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Verkündung in Stein</name>
    //      <image>./img/cards/Shadows_over_Innistrad/declaration_in_stone.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Schatten über Innistrad</expansion>
    //      <expIcon>379</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>true</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <articleValue>12.5</articleValue>
    //  <totalValue>13.7</totalValue>
    //</order>
    //<order>
    //  <idOrder>39641136</idOrder>
    //  <isBuyer>true</isBuyer>
    //  <seller>
    //    <idUser>29861</idUser>
    //    <username>Donjoe</username>
    //    <country>D</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>0</riskGroup>
    //    <reputation>1</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>1619</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>3</idDisplayLanguage>
    //    <name>
    //      <firstName>Jonny</firstName>
    //      <lastName>Skara</lastName>
    //    </name>
    //    <address>
    //      <name>Jonny Skara</name>
    //      <extra></extra>
    //      <street>Marsweg 16</street>
    //      <zip>47443</zip>
    //      <city>Moers</city>
    //      <country>D</country>
    //    </address>
    //  </seller>
    //  <buyer>
    //    <idUser>55207</idUser>
    //    <username>sinac</username>
    //    <country>BE</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>1</riskGroup>
    //    <reputation>0</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>0</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Joël</firstName>
    //      <lastName>Heymbeeck</lastName>
    //    </name>
    //    <address>
    //      <name>Joël Heymbeeck</name>
    //      <extra></extra>
    //      <street>Rue Du Menil, 35</street>
    //      <zip>1420</zip>
    //      <city>Braine-l'Alleud</city>
    //      <country>BE</country>
    //    </address>
    //  </buyer>
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-07-18T20:52:41+0200</dateBought>
    //    <datePaid>2016-07-18T20:52:41+0200</datePaid>
    //    <dateSent>2016-07-18T23:29:18+0200</dateSent>
    //  </state>
    //  <shippingMethod>
    //    <idShippingMethod>52482</idShippingMethod>
    //    <name>Letter (Kompaktbrief)</name>
    //    <price>1.8</price>
    //    <isLetter>true</isLetter>
    //    <isInsured>false</isInsured>
    //  </shippingMethod>
    //  <isPresale>false</isPresale>
    //  <shippingAddress>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </shippingAddress>
    //  <note></note>
    //  <articleCount>7</articleCount>
    //  <article>
    //    <idArticle>260311120</idArticle>
    //    <idProduct>267013</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>boosterfresh, unplayed, fast shipping</comments>
    //    <price>1.29</price>
    //    <count>3</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Caves of Koilos</name>
    //      <image>./img/cards/Modern_Event_Deck_2014/caves_of_koilos.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Modern Event Deck 2014</expansion>
    //      <expIcon>250</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>228362512</idArticle>
    //    <idProduct>284164</idProduct>
    //    <language>
    //      <idLanguage>1</idLanguage>
    //      <languageName>English</languageName>
    //    </language>
    //    <comments>boosterfresh, unplayed, fast shipping</comments>
    //    <price>0.04</price>
    //    <count>4</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Read the Bones</name>
    //      <image>./img/cards/Duel_Decks_Zendikar_vs._Eldrazi/read_the_bones.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Duel Decks: Zendikar vs. Eldrazi</expansion>
    //      <expIcon>366</expIcon>
    //      <rarity>Common</rarity>
    //    </product>
    //    <condition>NM</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <articleValue>4.03</articleValue>
    //  <totalValue>5.83</totalValue>
    //</order>
    //<order>
    //  <idOrder>35983519</idOrder>
    //  <isBuyer>true</isBuyer>
    //  <seller>
    //    <idUser>1679796</idUser>
    //    <username>pgeffe76</username>
    //    <country>IT</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>0</riskGroup>
    //    <reputation>1</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>520</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>5</idDisplayLanguage>
    //    <name>
    //      <firstName>Pasquale</firstName>
    //      <lastName>Frecentese</lastName>
    //    </name>
    //    <address>
    //      <name>Pasquale Frecentese</name>
    //      <extra></extra>
    //      <street>Via Spartaco 7</street>
    //      <zip>81055</zip>
    //      <city>Santa Maria Capua Vetere</city>
    //      <country>IT</country>
    //    </address>
    //  </seller>
    //  <buyer>
    //    <idUser>55207</idUser>
    //    <username>sinac</username>
    //    <country>BE</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>1</riskGroup>
    //    <reputation>0</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>0</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Joël</firstName>
    //      <lastName>Heymbeeck</lastName>
    //    </name>
    //    <address>
    //      <name>Joël Heymbeeck</name>
    //      <extra></extra>
    //      <street>Rue Du Menil, 35</street>
    //      <zip>1420</zip>
    //      <city>Braine-l'Alleud</city>
    //      <country>BE</country>
    //    </address>
    //  </buyer>
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-07-05T11:31:08+0200</dateBought>
    //    <datePaid>2016-07-07T10:04:36+0200</datePaid>
    //    <dateSent>2016-07-08T11:37:30+0200</dateSent>
    //  </state>
    //  <shippingMethod>
    //    <idShippingMethod>47519</idShippingMethod>
    //    <name>Standard Letter (Postamail Int.)</name>
    //    <price>1.3</price>
    //    <isLetter>true</isLetter>
    //    <isInsured>false</isInsured>
    //  </shippingMethod>
    //  <isPresale>false</isPresale>
    //  <shippingAddress>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Menil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </shippingAddress>
    //  <note></note>
    //  <articleCount>4</articleCount>
    //  <article>
    //    <idArticle>258958393</idArticle>
    //    <idProduct>11112</idProduct>
    //    <language>
    //      <idLanguage>5</idLanguage>
    //      <languageName>Italian</languageName>
    //    </language>
    //    <comments></comments>
    //    <price>2.9</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Tutore Terreno</name>
    //      <image>./img/cards/Sixth_Edition/worldly_tutor.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Sesta Edizione</expansion>
    //      <expIcon>27</expIcon>
    //      <rarity>Uncommon</rarity>
    //    </product>
    //    <condition>MT</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>254425558</idArticle>
    //    <idProduct>22350</idProduct>
    //    <language>
    //      <idLanguage>5</idLanguage>
    //      <languageName>Italian</languageName>
    //    </language>
    //    <comments></comments>
    //    <price>10.4</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Kozilek, Macellaio della Verità</name>
    //      <image>./img/cards/Rise_of_the_Eldrazi/kozilek_butcher_of_truth.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Ascesa degli Eldrazi</expansion>
    //      <expIcon>117</expIcon>
    //      <rarity>Mythic</rarity>
    //    </product>
    //    <condition>MT</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>254425756</idArticle>
    //    <idProduct>22379</idProduct>
    //    <language>
    //      <idLanguage>5</idLanguage>
    //      <languageName>Italian</languageName>
    //    </language>
    //    <comments></comments>
    //    <price>8.8</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Ulamog, il Cerchio Infinito</name>
    //      <image>./img/cards/Rise_of_the_Eldrazi/ulamog_the_infinite_gyre.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Ascesa degli Eldrazi</expansion>
    //      <expIcon>117</expIcon>
    //      <rarity>Mythic</rarity>
    //    </product>
    //    <condition>MT</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <article>
    //    <idArticle>255386659</idArticle>
    //    <idProduct>245816</idProduct>
    //    <language>
    //      <idLanguage>5</idLanguage>
    //      <languageName>Italian</languageName>
    //    </language>
    //    <comments></comments>
    //    <price>1.9</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Metamorfosi Phyrexian</name>
    //      <image>./img/cards/New_Phyrexia/phyrexian_metamorph.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Nuova Phyrexia</expansion>
    //      <expIcon>183</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>MT</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <articleValue>24</articleValue>
    //  <totalValue>25.3</totalValue>
    //</order>
    //<order>
    //  <idOrder>38814372</idOrder>
    //  <isBuyer>true</isBuyer>
    //  <seller>
    //    <idUser>1845241</idUser>
    //    <username>Pawelito1990</username>
    //    <country>IT</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>0</riskGroup>
    //    <reputation>1</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>283</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>5</idDisplayLanguage>
    //    <name>
    //      <firstName>Pawel</firstName>
    //      <lastName>Jasinski</lastName>
    //    </name>
    //    <address>
    //      <name>Pawel Jasinski</name>
    //      <extra></extra>
    //      <street>Via Parasio 46/6</street>
    //      <zip>17019</zip>
    //      <city>Varazze ( SV)</city>
    //      <country>IT</country>
    //    </address>
    //  </seller>
    //  <buyer>
    //    <idUser>55207</idUser>
    //    <username>sinac</username>
    //    <country>BE</country>
    //    <isCommercial>0</isCommercial>
    //    <riskGroup>1</riskGroup>
    //    <reputation>0</reputation>
    //    <shipsFast>-1</shipsFast>
    //    <sellCount>0</sellCount>
    //    <onVacation>false</onVacation>
    //    <idDisplayLanguage>1</idDisplayLanguage>
    //    <name>
    //      <firstName>Joël</firstName>
    //      <lastName>Heymbeeck</lastName>
    //    </name>
    //    <address>
    //      <name>Joël Heymbeeck</name>
    //      <extra></extra>
    //      <street>Rue Du Menil, 35</street>
    //      <zip>1420</zip>
    //      <city>Braine-l'Alleud</city>
    //      <country>BE</country>
    //    </address>
    //  </buyer>
    //  <state>
    //    <state>sent</state>
    //    <dateBought>2016-06-22T19:38:11+0200</dateBought>
    //    <datePaid>2016-06-25T07:01:52+0200</datePaid>
    //    <dateSent>2016-06-25T09:40:25+0200</dateSent>
    //  </state>
    //  <shippingMethod>
    //    <idShippingMethod>47519</idShippingMethod>
    //    <name>Standard Letter (Postamail Int.)</name>
    //    <price>1.3</price>
    //    <isLetter>true</isLetter>
    //    <isInsured>false</isInsured>
    //  </shippingMethod>
    //  <isPresale>false</isPresale>
    //  <shippingAddress>
    //    <name>Joël Heymbeeck</name>
    //    <extra></extra>
    //    <street>Rue Du Ménil, 35</street>
    //    <zip>1420</zip>
    //    <city>Braine-l'Alleud</city>
    //    <country>BE</country>
    //  </shippingAddress>
    //  <note></note>
    //  <articleCount>1</articleCount>
    //  <article>
    //    <idArticle>256991471</idArticle>
    //    <idProduct>485</idProduct>
    //    <language>
    //      <idLanguage>5</idLanguage>
    //      <languageName>Italian</languageName>
    //    </language>
    //    <comments>GD +</comments>
    //    <price>24.99</price>
    //    <count>1</count>
    //    <inShoppingCart>false</inShoppingCart>
    //    <product>
    //      <name>Crogiolo di Mondi</name>
    //      <image>./img/cards/Fifth_Dawn/crucible_of_worlds.jpg</image>
    //      <idGame>1</idGame>
    //      <expansion>Quinta Alba</expansion>
    //      <expIcon>45</expIcon>
    //      <rarity>Rare</rarity>
    //    </product>
    //    <condition>GD</condition>
    //    <isFoil>false</isFoil>
    //    <isSigned>false</isSigned>
    //    <isPlayset>false</isPlayset>
    //    <isAltered>false</isAltered>
    //  </article>
    //  <articleValue>24.99</articleValue>
    //  <totalValue>26.29</totalValue>
    //</order>
}
