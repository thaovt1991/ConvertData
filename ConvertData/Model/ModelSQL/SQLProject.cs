using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{

    public class SQLProjectBase
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Mã dự án
        /// </summary>
        public string Code { get; set; }
    }
    public class SQLProject : SQLProjectBase
    {
        //public Guid Id { get; set; }
        ///// <summary>
        ///// Mã dự án
        ///// </summary>
        //public string Code { get; set; }

        /// <summary>
        /// Tên dự án
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Đường dẫn hình ảnh đại diện
        /// </summary>
        public string? Avatar { get; set; }

        /// <summary>
        /// Trạng thái dự án
        /// 1-New, 2-Processing, 3-Completed, 4-Closed, 5-Archived, 6-OnHold, 7-Cancelled
        /// </summary>
        public byte Status { get; set; } //= ProjectStatus.New; //Cũ có 1 đến 4

        /// <summary>
        /// Loại dự án
        /// </summary>
        public ProjectType ProjectType { get; set; } = ProjectType.UserCreated;

        /// <summary>
        /// Loại mức ưu tiên của dự án (ví dụ: Cao, Trung bình, Thấp,...)
        /// </summary>
        public PriorityType PriorityType { get; set; }

        /// <summary>
        /// Ngày bắt đầu dự án
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc dự án (có thể để null nếu chưa xác định hoặc dự án mở)
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Tự động cập nhật tiến độ dự án dựa vào số công việc hoàn tất trên tổng số công việc hiện có.
        /// </summary>
        public bool IsProgressAutoCalculated { get; set; }

        /// <summary>
        /// Phần trăm tiến độ dự án, được tính toán dựa trên số công việc đã hoàn thành trên tổng số công việc của dự án (nếu IsProgressAutoCalculated = true) hoặc do người dùng cập nhật thủ công (nếu IsProgressAutoCalculated = false)
        /// </summary>
        public double ProgressPercentage { get; set; }

        /// <summary>
        /// Chế độ hiển thị mặc định (Tree, Kanban, Gantt,...)
        /// </summary>
        public ProjectViewMode DefaultViewMode { get; set; }

        /// <summary>
        /// Danh sách đối tượng có thể tham gia vào dự án (ví dụ: 1-Thành viên dự án, 2-Mọi người, 3-Công ty,...)
        /// </summary>
        public ProjectMemberScope ProjectAccessScope { get; set; } // AssignMode

        /// <summary>
        /// Được phép gia hạn công việc khi chưa bị quá hạn
        /// </summary>
        public bool AllowTaskDeadlineExtension { get; set; }

        /// <summary>
        /// Phạm vi thời gian xử lý công việc trong dự án⁠⁠‍‌‍⁣⁣ (Phạm vi dự án, Không kiểm soát, Phạm vi công việc cha)
        /// </summary>
        public DeadlineScope TaskDurationRangeMode { get; set; }

        /// <summary>
        /// Trạng thái hoạt động (0-Active, 1-Inactive, 2-Deleted)
        /// </summary>
        public int ActiveFlag { get; set; } = 0;

        // Đối tượng được phép xóa công việc trong dự án
        // Danh sách : Quản lý dự án; Người thực hiện; Người tạo
         public TaskDeletePermission TaskDeletePermissionMask { get; set; }

        /// <summary>
        /// Thiết lập hiển thị kanban
        /// Cho phép người dùng hiển thị theo thiết lập riêng, nếu không thiết lập sẽ hiển thị theo mặc định⁠⁠⁠‌‌⁣⁣
        /// </summary>
         public KanbanGroupMode DefaultKanbanGroupMode { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Người cập nhật cuối cùng
        /// </summary>
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        // Navigation properties
        //public List<ProjectMember> ProjectMembers { get; set; }
        //public List<WorkTask> Tasks { get; set; }
        //public List<ProjectTag> ProjectTags { get; set; }
    }

    
    public enum ProjectStatus : byte
    {
        [Display(Name = "Tạo mới")]
        New = 1,

        [Display(Name = "Đang thực hiện")]
        Processing = 2,

        [Display(Name = "Hoàn tất")]
        Completed = 3,

        [Display(Name = "Kết thúc")]
        Closed = 4,

        [Display(Name = "Lưu trữ")]
        Archived = 5,

        [Display(Name = "Tạm hoãn")]
        OnHold = 6,

        [Display(Name = "Hủy")]
        Cancelled = 7
    }

    public enum ProjectType : byte
    {
        /// <summary>
        /// Dự án do người dùng tạo trực tiếp trong module Quản lý công việc
        /// </summary>
        UserCreated = 1,

        /// <summary>
        /// Dự án được tạo từ module khác trong hệ thống (QLCV, TKS, ...)
        /// </summary>
        External = 2
    }

    public enum PriorityType : byte
    {
        [Display(Name = "Thấp")]
        Low = 1,

        [Display(Name = "Bình thường")]
        Normal = 2,

        [Display(Name = "Cao")]
        Hight = 3
    }

    public enum DeadlineScope
    {
        [Display(Name = "Không kiểm soát")]
        None = 0,

        [Display(Name = "Phạm vi dự án")]
        Project = 1,

        [Display(Name = "Phạm vi công việc cha")]
        ParentTask = 2
    }

    public enum KanbanGroupMode : byte
    {
        [Display(Name = "KBColumn")]
        KanbanColumn = 1,

        [Display(Name = "Status")]
        Status = 2,

        [Display(Name = "Priority")]
        Priority = 3
    }

    [Flags]
    public enum TaskDeletePermission
    {
        [Display(Name = "Bất kì ai")]
        None = 0,

        [Display(Name = "Quản lý dự án")]
        ProjectManager = 1,

        [Display(Name = "Người thực hiện")]
        Assignee = 2,

        [Display(Name = "Người tạo")]
        Creator = 4
    }

    public enum ProjectViewMode
    {
        [Display(Name = "Kanban")]
        Kanban = 1,

        [Display(Name = "Danh sách")]
        ListView = 7,

        [Display(Name = "Tree - Grid")]
        TreeGrid = 8
    }

    public enum ProjectMemberScope
    {
        [Display(Name = "Thành viên dự án")]
        ProjectMembers = 1,

        [Display(Name = "Công ty")]
        Company = 2,

        [Display(Name = "Mọi người")]
        Everyone = 3
    }
}
