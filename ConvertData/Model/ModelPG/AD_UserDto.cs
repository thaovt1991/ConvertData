using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelPG
{
    public class AD_UserDto
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string DepartmentName { get; set; }
        public string OrgUnitName { get; set; }
        public string PositionName { get; set; }
    }
}
