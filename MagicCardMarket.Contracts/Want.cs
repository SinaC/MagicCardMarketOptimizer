using System;
using System.Xml.Serialization;

namespace MagicCardMarket.Contracts
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Want
    // TODO
    [Serializable]
    [XmlType("want", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "want", IsNullable = false)]
    public class Want
    {
        //idWant:                         // Want ID
        //count:                          // Quantity
        //wishPrice:                      // Maximum price
        //type:                           // either "product" or "metaproduct"
        //idMetaproduct:                  // Metaproduct ID, if type is "metaproduct" OR
        //idProduct:                      // Product ID, if type is "product"
        //minCondition:                   // Minimum Condition
        //language:                       // Array of language entities
        //isFoil:
        //isSigned:
        //isPlayset:
        //isAltered:
        //isFirstEd:
    }
}
