using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelPG
{
    public class BS_Tags
    {
        [Key]
        public Guid RecID { get; set; }
        public string EntityName { get; set; }
        public string Value { get; set; }

        public string Icon { get; set; }
        public string Style { get; set; }
        public string TagType { get; set; }

        public string Color { get; set; }
        public string ObjectID { get; set; }
        public string UserID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
