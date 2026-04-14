using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model
{
    public abstract class ParentEntity
    {
        protected ParentEntity()
        {
            //_id = ObjectId.GenerateNewId().ToString();
            _id = ObjectId.GenerateNewId();
        }

        //public string Id
        [BsonElement("_id")]
        public ObjectId Id
        {
            get { return _id; }
            set
            {
                //if (string.IsNullOrEmpty(value))
                if (value == null)
                    _id = ObjectId.GenerateNewId(); //.ToString();
                else
                    _id = value;
            }
        }

        //private string _id;
        private ObjectId _id;
    }
}
