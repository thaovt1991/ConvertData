using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelMongo
{
    public class BG_TrackLogs
    {
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }
        public string ObjectType { get; set; }

        public string ObjectID { get; set; }

        public string FunctionID { get; set; }
        public string ActionType { get; set; }

        public string Datas { get; set; }

        public int Attachments { get; set; }

        public string Image { get; set; }


        public string Comment { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string IPConnection { get; set; }

        public Guid? Reference { get; set; }

        public List<tmpObject> SendToObject { get; set; }
        public string Text { get; set; }

        public string TextValue { get; set; }
        public string ChildID { get; set; }
    }

    public class tmpObject
    {
        public string ObjectType { get; set; }
        public string ObjectID { get; set; }
        public string ObjectName { get; set; }

    }
}
