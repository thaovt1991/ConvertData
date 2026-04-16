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

    public class SQLCoreUserInfo
    {
        public Guid UserId { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public string JobTitleName { get; set; }
    }

    public class BaseModel
    {
        public Guid Id { get; set; }
        public byte ActiveFlag { get; set; } = 0;
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Người cập nhật cuối cùng
        /// </summary>
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
