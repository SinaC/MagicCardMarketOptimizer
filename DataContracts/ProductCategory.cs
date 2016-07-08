using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarketOptimizer.DataContracts
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class ProductCategory
    {
        [XmlElement("idCategory", Form = XmlSchemaForm.Unqualified)]
        public int Id { get; set; }

        [XmlElement("categoryName", Form = XmlSchemaForm.Unqualified)]
        public string Name { get; set; }
    }
    //  <category>
    //    <idCategory>1</idCategory>
    //    <categoryName>Magic Single</categoryName>
    //  </category>
}
