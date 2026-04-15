using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelSQL
{
    public class SQLTaskExtend
    {
        public Guid TaskId { get; set; }

        /// <summary>
        /// Phân công xử lý chính tạo yêu cầu gia hạn.
        /// </summary>
        public Guid? TaskAssignId { get; set; }

        public Guid RequestedBy { get; set; }
        public DateTimeOffset? OldDueDate { get; set; }
        public DateTimeOffset RequestedDueDate { get; set; }
        public string? Reason { get; set; }
        public WorkTaskExtensionRequestStatus Status { get; set; } = WorkTaskExtensionRequestStatus.Pending;
        public Guid? ReviewedBy { get; set; }
        public DateTimeOffset? ReviewedDate { get; set; }
        public string? ReviewComment { get; set; }
        public byte ActiveFlag { get; set; } = 0;
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
    public enum WorkTaskExtensionRequestStatus : byte
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
    }

}
