using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ProductPriceGuide
    {
        [XmlElement("SELL", Form = XmlSchemaForm.Unqualified)]
        public decimal Sell { get; set; }

        [XmlElement("LOW", Form = XmlSchemaForm.Unqualified)]
        public decimal Low { get; set; }

        [XmlElement("LOWEX", Form = XmlSchemaForm.Unqualified)]
        public decimal LowEx { get; set; }

        [XmlElement("LOWFOIL", Form = XmlSchemaForm.Unqualified)]
        public decimal LowFoil { get; set; }

        [XmlElement("AVG", Form = XmlSchemaForm.Unqualified)]
        public decimal Average { get; set; }

        [XmlElement("TREND", Form = XmlSchemaForm.Unqualified)]
        public double Trend { get; set; }
    }
    //  <priceGuide>
    //    <SELL>0</SELL>
    //    <LOW>5</LOW>
    //    <LOWEX>7.5</LOWEX>
    //    <LOWFOIL>0</LOWFOIL>
    //    <AVG>20.37</AVG>
    //    <TREND>11.84</TREND>
    //  </priceGuide>
}
