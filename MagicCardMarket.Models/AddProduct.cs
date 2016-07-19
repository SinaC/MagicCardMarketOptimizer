using System;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class AddProduct
    {
        [XmlElement("idProduct", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("count", Form = XmlSchemaForm.Unqualified)]
        public int Count { get; set; }

        [XmlElement("idLanguage", Form = XmlSchemaForm.Unqualified)]
        public int LanguageId { get; set; }

        [XmlElement("minCondition", Form = XmlSchemaForm.Unqualified)] // MANDATORY
        public string MinCondition { get; set; }

        [XmlElement("wishPrice", Form = XmlSchemaForm.Unqualified)]
        public decimal WishPrice { get; set; }

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

        public AddProduct()
        {
            MinCondition = "PO";
        }
    }
}
