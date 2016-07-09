using System;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Want
    [Serializable]
    [XmlType("want", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "want", IsNullable = false)]
    public class Want
    {
        [XmlElement("idWant", Form = XmlSchemaForm.Unqualified)]
        public string Id { get; set; }

        [XmlElement("count", Form = XmlSchemaForm.Unqualified)]
        public int Count { get; set; }

        [XmlElement("wishPrice", Form = XmlSchemaForm.Unqualified)]
        public decimal WishPrice { get; set; }

        [XmlElement("type", Form = XmlSchemaForm.Unqualified)]
        public string Type { get; set; }

        [XmlElement("idMetaproduct", Form = XmlSchemaForm.Unqualified)] // type is "metaproduct"
        public int MetaProductIc { get; set; }

        [XmlElement("idProduct", Form = XmlSchemaForm.Unqualified)] // if type is "product"
        public int ProductId { get; set; }

        [XmlElement("minCondition", Form = XmlSchemaForm.Unqualified)]
        public string MinCondition { get; set; }

        [XmlElement("language", Form = XmlSchemaForm.Unqualified)]
        public Language[] Languages { get; set; }

        [XmlElement("isFoil", Form = XmlSchemaForm.Unqualified)]
        public string IsFoilRaw { get; set; }

        [XmlIgnore]
        public bool IsFoil
        {
            get { return Helpers.SafeConvertToBool(IsFoilRaw); }
            set { IsFoilRaw = value.ToString(CultureInfo.InvariantCulture); }
        }

        [XmlElement("isSigned", Form = XmlSchemaForm.Unqualified)]
        public string IsSignedRaw { get; set; }

        [XmlIgnore]
        public bool IsSigned
        {
            get { return Helpers.SafeConvertToBool(IsSignedRaw); }
            set { IsSignedRaw = value.ToString(CultureInfo.InvariantCulture); }
        }

        [XmlElement("isPlayset", Form = XmlSchemaForm.Unqualified)]
        public string IsPlaysetRaw { get; set; }

        [XmlIgnore]
        public bool IsPlayset
        {
            get { return Helpers.SafeConvertToBool(IsPlaysetRaw); }
            set { IsPlaysetRaw = value.ToString(CultureInfo.InvariantCulture); }
        }

        [XmlElement("isAltered", Form = XmlSchemaForm.Unqualified)]
        public string IsAlteredRaw { get; set; }

        [XmlIgnore]
        public bool IsAltered
        {
            get { return Helpers.SafeConvertToBool(IsAlteredRaw); }
            set { IsAlteredRaw = value.ToString(CultureInfo.InvariantCulture); }
        }

        [XmlElement("isFirstEd", Form = XmlSchemaForm.Unqualified)]
        public string IsFirstEdRaw { get; set; }

        [XmlIgnore]
        public bool IsFirstEd
        {
            get { return Helpers.SafeConvertToBool(IsFirstEdRaw); }
            set { IsFirstEdRaw = value.ToString(CultureInfo.InvariantCulture); }
        }
    }

//<want>
//  <idWant>577e150bdcbc94783b8b4567</idWant>
//  <count>1</count>
//  <wishPrice>0</wishPrice>
//  <idMetaproduct>221363</idMetaproduct>
//  <type>metaproduct</type>
//  <minCondition>PO</minCondition>
//  <isFoil></isFoil>
//  <isSigned></isSigned>
//  <isPlayset></isPlayset>
//  <isAltered></isAltered>
//</want>
}
