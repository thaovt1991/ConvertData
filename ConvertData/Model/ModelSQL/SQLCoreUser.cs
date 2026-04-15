using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{

    public class SQLCoreUserBase
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
    public class SQLCoreUser : SQLCoreUserBase
    {
        public string FullName { get; set; }
    }
}
