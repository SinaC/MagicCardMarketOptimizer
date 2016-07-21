using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace MagicCardMarket.Models
{
    //https://www.mkmapi.eu/ws/documentation/API_1.1:Entities:Evaluation
    [Serializable]
    [XmlType("evaluation", AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "evaluation", IsNullable = false)]
    public class Evaluation
    {
        //1 - very good 
        //2 - good 
        //3 - neutral 
        //4 - bad 
        //10 - n/a
        [XmlElement("evaluationGrade", Form = XmlSchemaForm.Unqualified)]
        public int EvaluationGrade { get; set; }

        [XmlElement("itemDescription", Form = XmlSchemaForm.Unqualified)]
        public string ItemDescription { get; set; }

        [XmlElement("packaging", Form = XmlSchemaForm.Unqualified)]
        public int PackageGrade { get; set; } // grades: see evaluationGrade

        [XmlElement("speed", Form = XmlSchemaForm.Unqualified)]
        public int Speed { get; set; } // grades: see evaluationGrade

        [XmlElement("comment", Form = XmlSchemaForm.Unqualified)]
        public string Comment { get; set; }

        //badCommunication 
        //incompleteShipment 
        //notFoil 
        //rudeSeller 
        //shipDamage 
        //unorderedShipment 
        //wrongEd 
        //wrongLang

        [XmlElement("complaint", Form = XmlSchemaForm.Unqualified)]
        public string[] Complaints { get; set; }
    }

    //evaluation: {
    //    evaluationGrade: 1
    //    itemDescription: 4
    //    packaging: 1
    //    speed: 1
    //    comment: "not foil, but alternate art and wrong edition"
    //    complaint: [
    //        "wrongEd",
    //        "notFoil"
    //    ]
    //}

}
