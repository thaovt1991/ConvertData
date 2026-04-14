using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{
    public class SQLProjectMember
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// ID dự án
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Loại thành viên trong dự án thuộc chính module QLCV hay phát sinh từ module khác (QLVB, TKS)
        /// </summary>
        /// 
        public int ModuleType { get; set; } = 1; //ProjectModuleType;  - 1 QLVB, 2 tk. 3 cv

        /// <summary>
        /// Id đối tượng phát sinh dự án từ Module khác có thành viên dự án với loại ProjectType là Module (QLVB, TKS)
        /// - Khi dự án phát sinh từ module QLVB sẽ chỉ lưu task vào 1 dự án mặc định nên phát sinh ModuleType và ModuleObjectId để phân loại lấy thành viên dự án
        /// </summary>
        public Guid? ModuleObjectId { get; set; }

        /// <summary>
        /// ID thành viên (liên kết tới người dùng)
        /// </summary>
        public Guid MemberId { get; set; }

        /// <summary>
        /// Vai trò của thành viên trong dự án (PM, Giám sát, Tech Leader, Member,...)
        /// 
        public int ProjectMemberType { get; set; } ///ProjectMemberType

        //| 0-Active / 1-Inactive / 2-Deleted |
        public int ActiveFlag { get; set; } = 0;

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int SortOrder { get; set; }

        // Navigation properties
       // public Project Project { get; set; }
    }

    public enum ProjectMemberType : byte
    {
        /// <summary>
        /// Quản lý dự án
        /// </summary>
        [Display(Name = "Quản lý dự án")]
        Manager = 1,
        /// <summary>
        /// Thành viên
        /// </summary>
        [Display(Name = "Thành viên")]
        Member = 2,
        /// <summary>
        /// Người giám sát
        /// </summary>
        [Display(Name = "Giám sát dự án")]
        Monitor = 3,
    }

    public enum ProjectModuleType : byte
    {
        QLCV = 1,
        QLVB = 2,
        TKS = 3
    }
}
