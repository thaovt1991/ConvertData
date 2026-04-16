using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{
    public class SQLTaskResource : BaseModel //taskAssign
    {
        public Guid TaskId { get; set; }

        /// <summary>
        /// Loại phân công
        /// - 1 : Xử lý chính (chịu trách nhiệm chính)
        /// - 2 : Phối hợp
        /// - 3 : Theo dõi
        /// </summary>
        public byte TaskAssignType { get; set; } //TaskAssignType

        /// <summary>
        /// Người nhận việc
        /// </summary>
        public Guid AssignTo { get; set; }

        /// <summary>
        /// Phần trăm hoàn thành công việc của người nhận việc này,
        /// chỉ dùng khi TaskAssignType = Xử lý chính để đánh giá tiến độ công việc, nếu có nhiều người nhận việc chính thì sẽ lấy mức phần trăm VÀ tính trung bình cộng làm phần trăm hoàn thành của cả công việc
        /// </summary>
        public double? CompletePercent { get; set; }

        /// <summary>
        /// Trạng thái riêng của người nhận việc này (Xử lý chính)
        /// nếu có 1 người xử lý chính thì cập nhật trạng thái này = trạng thái công việc (WorkTask), nếu có nhiều người xử lý chính thì cập nhật trạng thái này theo quy tắc sau để tổng hợp thành trạng thái công việc:
        /// # Quy tắc tổng hợp WorkTask.Status:
        /// Trạng thái các người Xử lý chính  WorkTask.Status
        /// Tất cả = New -> New
        /// Ít nhất 1 = InProgress, chưa ai Completed hết -> InProgress
        /// Ít nhất 1 Completed, nhưng chưa tất cả -> InProgress
        /// Ít nhất 1 Reporting, nhưng chưa tất cả -> InProgress
        /// Tất cả Completed -> Completed
        /// Tất cả Reporting -> Reporting
        /// Nếu Người theo dõi hoặc phối hợp có cập nhật thì trạng thái riêng của họ sẽ không ảnh hưởng đến trạng thái công việc
        /// </summary>
        public WorkTaskStatus? Status { get; set; }  //WorkTaskStatus

        /// <summary>
        /// ID Phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban của người nhận việc.
        /// Thông tin tên phòng có thể thay đổi theo thời điểm cập nhật, nên lưu lại tên phòng ban tại thời điểm phân công để hiển thị, không cần join bảng phòng ban để lấy tên khi hiển thị công việc
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Tên chức danh của người nhận việc.
        /// Tên chức danh có thể thay đổi theo thời điểm cập nhật, nên lưu lại tên chức danh tại thời điểm phân công để hiển thị, không cần join bảng chức danh để lấy tên khi hiển thị công việc
        /// </summary>
        public string? JobTitleName { get; set; }

        //public byte ActiveFlag { get; set; } = 0;

        //public Guid CreatedBy { get; set; }
        //public DateTimeOffset CreatedDate { get; set; }
        ///// <summary>
        ///// Người cập nhật cuối cùng
        ///// </summary>
        //public Guid? UpdatedBy { get; set; }
        //public DateTimeOffset? UpdatedDate { get; set; }
    }
}
