using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model
{
    public class ConnectionModel
    {
         //Mongo
        public string ConnectionStringMG { get; set; }
        public string DatabaseNameMG { get; set; }
        //SQL
        public string ConnectionStringSQL { get; set; }
        public string DatabaseNameSQL { get; set; }

        //PG
        public string ConnectionStringPG { get; set; }

    }

    public class ParameterModel
    {
        public string StartCreatedDate { get; set; }
        public string EndCreatedDate { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class ParameterModelTask : ParameterModel
    {
        public bool IsUpdateFull { get; set; } = true;
        public bool IsUpdateTask { get; set; } = true;
        public bool IsUpdateTaskRes { get; set; } = true;
    }

    public class ParameterModelTag : ParameterModel
    {
       public List<string> EntityNames { get; set; }
    }
}
