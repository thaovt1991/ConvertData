using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{
    public class SQLHistory 
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Loại đối tượng phát sinh
        /// 1 - Project
        /// 2 - Task
        /// 3 - TaskAssign
        /// </summary>
        public WorkObjectType ObjectType { get; set; }

        /// <summary>
        /// Id của đối tượng liên quan
        /// Ví dụ:
        /// - Nếu ObjectType = Task → ObjectId = TaskId
        /// - Nếu ObjectType = Project → ObjectId = ProjectId
        /// </summary>
        public Guid ObjectId { get; set; }

        /// <summary>
        /// Loại hành động nghiệp vụ
        /// Ví dụ:
        /// - TaskCreated
        /// - TaskStatusChanged
        /// - TaskProgressChanged
        /// - TaskReassigned
        /// - ProjectProgressChanged
        /// </summary>
        public HistoryActionType ActionType { get; set; }

        /// <summary>
        /// Giá trị trước khi thay đổi
        /// Ví dụ:
        /// - Status cũ
        /// - % tiến độ cũ
        /// - User cũ
        /// </summary>
        public string? OldValue { get; set; }

        /// <summary>
        /// Giá trị sau khi thay đổi
        /// Ví dụ:
        /// - Status mới
        /// - % tiến độ mới
        /// - User mới
        /// </summary>
        public string? NewValue { get; set; }

        /// <summary>
        /// User liên quan đến hành động (nếu có)
        /// Ví dụ:
        /// - Chuyển người xử lý
        /// - Phân công công việc
        /// </summary>
        public Guid? RelatedUserId { get; set; }

        /// <summary>
        /// Nội dung tin nhắn cập nhật xử lý task của người dùng
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// Dữ liệu bổ sung dạng JSON
        /// Dùng khi cần thêm thông tin để render message
        ///
        /// Ví dụ:
        /// {
        ///   "taskName": "BE-Test",
        ///   "projectName": "CRM System"
        /// }
        /// </summary>
        public string? Metadata { get; set; }

        /// <summary>
        /// Người thực hiện hành động
        /// </summary>
        public Guid CreatedBy { get; set; }
    }
    public enum HistoryActionType
    {
        #region Project Action
        /// <summary>
        /// Tạo mới dự án
        /// </summary>
        ProjectCreated = 1,

        /// <summary>
        /// Cập nhật thông tin dự án
        /// </summary>
        ProjectUpdated,

        /// <summary>
        /// Thay đổi tiến độ dự án
        /// </summary>
        ProjectProgressChanged,

        /// <summary>
        /// Thay đổi trạng thái dự án
        /// </summary>
        ProjectStatusChanged,

        /// <summary>
        /// Lưu trữ dự án
        /// </summary>
        ProjectArchived,

        /// <summary>
        /// Khôi phục dự án
        /// </summary>
        ProjectRestored,

        #endregion

        #region Task lifecycle

        /// <summary>
        /// Tạo mới công việc
        /// </summary>
        TaskCreated,

        /// <summary>
        /// Cập nhật thông tin công việc
        /// </summary>
        TaskUpdated,

        /// <summary>
        /// Xóa công việc
        /// </summary>
        TaskDeleted,

        /// <summary>
        /// Khôi phục công việc đã xóa
        /// </summary>
        TaskRestored,

        #endregion

        #region Task status / progress

        /// <summary>
        /// Thay đổi trạng thái công việc
        /// </summary>
        TaskStatusChanged,

        /// <summary>
        /// Cập nhật tiến độ công việc
        /// </summary>
        TaskProgressChanged,

        /// <summary>
        /// Hoàn thành công việc
        /// </summary>
        TaskCompleted,

        /// <summary>
        /// Từ chối công việc
        /// </summary>
        TaskRejected,

        /// <summary>
        /// Bị hủy công việc
        /// </summary>
        TaskCancelled,

        /// <summary>
        /// Đã duyệt công việc
        /// </summary>
        TaskApproved,

        /// <summary>
        /// Báo cáo công việc
        /// </summary>
        TaskReported,

        /// <summary>
        /// Mở lại công việc đã hoàn thành
        /// </summary>
        TaskReopened,

        /// <summary>
        /// Kết thúc công việc (người giao việc hoặc người xử lý đóng sớm)
        /// </summary>
        TaskFinished,

        /// <summary>
        /// Yêu cầu gia hạn công việc
        /// </summary>
        TaskExtensionRequest,

        /// <summary>
        /// Đồng ý gia hạn công việc
        /// </summary>
        TaskExtensionApproved,

        /// <summary>
        /// Từ chối gia hạn công việc
        /// </summary>
        TaskExtensionRejected,
        #endregion

        #region Task structure (Tree)

        /// <summary>
        /// Di chuyển công việc sang vị trí khác trong cây
        /// </summary>
        TaskMoved,

        /// <summary>
        /// Thay đổi công việc cha
        /// </summary>
        TaskParentChanged,

        /// <summary>
        /// Thay đổi thứ tự công việc
        /// </summary>
        TaskOrderChanged,

        #endregion

        #region Assignment

        /// <summary>
        /// Phân công công việc
        /// </summary>
        TaskAssigned,

        /// <summary>
        /// Thay đổi người được phân công
        /// </summary>
        TaskReassigned,

        /// <summary>
        /// Hủy phân công công việc
        /// </summary>
        TaskUnassigned,

        #endregion

        #region Deadline / planning

        /// <summary>
        /// Thay đổi ngày bắt đầu
        /// </summary>
        TaskStartDateChanged,

        /// <summary>
        /// Thay đổi deadline
        /// </summary>
        TaskDueDateChanged,

        /// <summary>
        /// Thay đổi ngày hoàn thành thực tế
        /// </summary>
        TaskEndDateChanged,

        #endregion

        #region Priority / metadata

        /// <summary>
        /// Thay đổi độ ưu tiên
        /// </summary>
        TaskPriorityChanged,

        /// <summary>
        /// Thêm tag vào công việc
        /// </summary>
        TaskTagAdded,

        /// <summary>
        /// Xóa tag khỏi công việc
        /// </summary>
        TaskTagRemoved,

        #endregion

        #region Checklist

        /// <summary>
        /// Thêm checklist item
        /// </summary>
        ChecklistItemAdded,

        /// <summary>
        /// Cập nhật checklist item
        /// </summary>
        ChecklistItemUpdated,

        /// <summary>
        /// Hoàn thành checklist item
        /// </summary>
        ChecklistItemCompleted,

        /// <summary>
        /// Xóa checklist item
        /// </summary>
        ChecklistItemDeleted,

        #endregion

        #region Comment

        /// <summary>
        /// Thêm bình luận
        /// </summary>
        CommentAdded,

        /// <summary>
        /// Cập nhật bình luận
        /// </summary>
        CommentUpdated,

        /// <summary>
        /// Xóa bình luận
        /// </summary>
        CommentDeleted

        #endregion
    }


    public enum WorkObjectType
    {
        Project = 1,
        Task = 2,
        TaskAssign = 3
    }
}
