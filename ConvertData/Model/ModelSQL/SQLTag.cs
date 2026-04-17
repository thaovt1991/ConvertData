using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{
    public class SQLTag : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; } //viet thuong
        public string? HexColor { get; set; }
        public TagType Type { get; set; }
        public string? Icon { get; set; }
       
    }
    public enum TagType : byte
    {
        Project = 0,
        Task = 1
    }
}
