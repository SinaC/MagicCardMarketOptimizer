using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Contracts
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Article
    [Serializable]
    [XmlType("article", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "article", IsNullable = false)]
    public class Article
    {
        [XmlElement("idArticle", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("idProduct", Form = XmlSchemaForm.Unqualified)]
        public int ProductId { get; set; }

        [XmlElement("language", Form = XmlSchemaForm.Unqualified)]
        public ArticleLanguage Language { get; set; }

        [XmlElement("comments", Form = XmlSchemaForm.Unqualified)]
        public string Comments { get; set; }

        [XmlElement("price", Form = XmlSchemaForm.Unqualified)]
        public decimal Price { get; set; }

        [XmlElement("count", Form = XmlSchemaForm.Unqualified)]
        public int Count { get; set; }

        [XmlElement("inShoppingCart", Form = XmlSchemaForm.Unqualified)]
        public bool InShoppingCart { get; set; }

        [XmlElement("seller", Form = XmlSchemaForm.Unqualified)]
        public ArticleSeller Seller { get; set; }

        // TODO        lastEdited:                         // Date, the article was last updated

        [XmlElement("condition", Form = XmlSchemaForm.Unqualified)]
        public string Condition { get; set; }

        [XmlElement("isFoil", Form = XmlSchemaForm.Unqualified)]
        public bool IsFoil { get; set; }

        [XmlElement("isSigned", Form = XmlSchemaForm.Unqualified)]
        public bool IsSigned { get; set; }

        [XmlElement("isAltered", Form = XmlSchemaForm.Unqualified)]
        public bool IsAltered { get; set; }

        [XmlElement("isPlayset", Form = XmlSchemaForm.Unqualified)]
        public bool IsPlayset { get; set; }

        [XmlElement("isFirstEd", Form = XmlSchemaForm.Unqualified)]
        public bool IsFirstEdition { get; set; }
    }

}
