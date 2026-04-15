using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{
    public class SQLTask
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// ID dự án chứa công việc này
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Công việc cha (nếu có)
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        ///  Mã công việc (ví dụ: "TASK-001", "TASK-002"...), có thể để null nếu không cần hoặc tự động sinh mã theo quy tắc riêng của dự án
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Tên công việc (ví dụ: "Thiết kế giao diện", "Lập trình API", "Viết tài liệu"...)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả (nếu có)
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Nhóm công việc
        /// </summary>
        public byte Category { get; set; }  //WorkTaskCategory

        /// <summary>
        /// Tình trạng công việc
        /// - Chưa thực hiện
        /// - Đang thực hiện
        /// - Báo cáo
        /// - Hoàn tất
        /// - Bị hủy
        /// - Từ chối duyệt
        /// </summary>
        public WorkTaskStatus Status { get; set; } //= WorkTaskStatus.New;

        /// <summary>
        /// ID Độ ưu tiên của công việc (ví dụ: Cao, Trung bình, Thấp,...)
        /// </summary>
        public PriorityType PriorityType { get; set; } //

        /// <summary>
        /// Đường dẫn phân cấp trong cây công việc
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Người giao việc
        /// </summary>
        public Guid AssignBy { get; set; }

        /// <summary>
        /// Có báo cáo hay không -> sẽ chỉ được cập nhật kéo qua trạng thái "Báo cáo" trước khi hoàn tất
        /// </summary>
        public bool IsReport { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public DateTimeOffset? StartDate { get; set; }

        /// <summary>
        /// Ngày dự kiến kết thúc
        /// </summary>
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        /// Ngày kết thúc thực tế
        /// </summary>
        public DateTimeOffset? EndDate { get; set; }

        /// <summary>
        /// Lặp task
        /// </summary>
        public bool IsRecurring { get; set; }

        /// <summary>
        /// Đơn vị lặp: 0-Không lặp, 1-Ngày, 2-Tuần, 3-Tháng, 4-Năm
        /// </summary>
        public byte RepeatType { get; set; } = 0;

        /// <summary>
        /// Tần suất lặp (Repeat Every N đơn vị)
        /// </summary>
        public int? RepeatInterval { get; set; }

        /// <summary>
        /// Số lần lặp tối đa (0 = vô hạn)
        /// </summary>
        public int? RepeatCount { get; set; }

        /// <summary>
        /// Ngày kết thúc lặp (nếu kết thúc theo ngày thay vì số lần)
        /// </summary>
        public DateTimeOffset? RepeatEndDate { get; set; }

        /// <summary>
        /// ID task gốc (nếu task này được sinh ra từ lặp lại)
        /// </summary>
        public Guid? RecurringSourceId { get; set; }

        /// <summary>
        /// Danh sách công việc cần làm
        /// </summary>
        public string Checklist { get; set; } //List<ChecklistItem>

        /// <summary>
        /// Cấp công việc: tự tính dựa vào Path
        /// </summary>
        public int? Level { get; set; }

        /// <summary>
        /// Phần trăm hoàn thành công việc
        /// </summary>
        public double? CompletePercent { get; set; }

        /// <summary>
        /// Phần trăm đánh giá của người duyệt / quản lý
        /// </summary>
        public double? ReviewPercent { get; set; }
        /// <summary>
        /// Trạng thái hoạt động (0-Active, 1-Inactive, 2-Deleted)
        /// </summary>
        public byte ActiveFlag { get; set; } = 0;

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        /// <summary>
        /// Người cập nhật cuối cùng
        /// </summary>
        public Guid? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public Guid? MigratedFromId { get; set; }

    }

   /// <summary>
   /// Model tham chieeus
   /// </summary>
    public enum WorkTaskStatus : byte
    {
        [Display(Name = "Chưa thực hiện")]
        New = 0,

        [Display(Name = "Đang thực hiện")]
        InProgress = 1,

        [Display(Name = "Báo cáo")]
        Reporting = 2,

        [Display(Name = "Hoàn tất")]
        Completed = 3,

        [Display(Name = "Bị hủy")]
        Cancelled = 4,

        [Display(Name = "Từ chối duyệt")]
        Rejected = 5
    }

    public enum WorkTaskCategory : byte
    {
        [Display(Name = "Nhóm công việc")]
        GroupTask = 1,

        [Display(Name = "Công việc cá nhân")]
        PersonalTask = 2,

        [Display(Name = "Công việc tính KPI")]
        KpiTask = 3
    }

    public class ChecklistItem
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }
        public int SortOrder { get; set; }
    }

}
