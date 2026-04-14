using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelMongo
{
    //[BsonCollection("TM_Tasks")]
    public class TM_Tasks : ParentEntity
    {
        public string RecID { get; set; } //Guid

        public string TaskID { get; set; }

        public string TaskName { get; set; }

        public string Tags { get; set; }

        public string TaskType { get; set; }

        public string Category { get; set; }

        public string TaskGroupID { get; set; }

        public string ParentID { get; set; } // Guid?
        public string ParentList { get; set; }

        public string ProjectID { get; set; }

        public string ActivityID { get; set; }

        public string IterationID { get; set; }

        public bool Recurrence { get; set; }

        public string Interval { get; set; }

        public string Weekday { get; set; }

        public string Memo { get; set; }

        public string Memo2 { get; set; }

        public string Location { get; set; }

        public string ObjectID { get; set; }

        public string ObjectType { get; set; }

        public string Status { get; set; }

        public string StatusCode { get; set; }

        public string ReasonCode { get; set; }

        public string Priority { get; set; }

        public string Rank { get; set; }

        public decimal Points { get; set; }

        public decimal Estimated { get; set; }

        public decimal EstimatedCost { get; set; }

        public decimal? Remaining { get; set; }

        public string AssignTo { get; set; }

        public DateTime? AssignedOn { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? StartedOn { get; set; }

        public decimal Percentage { get; set; }

        public decimal ApprovePercentage { get; set; }

        public decimal Completed { get; set; }

        public DateTime? CompletedOn { get; set; }

        public string CompletedTime { get; set; }

        public decimal CompletedQty { get; set; }

        public decimal Duration { get; set; }

        public string LateCode { get; set; }

        public string SessionID { get; set; }
        public string SessionType { get; set; }

        public string RefID { get; set; }

        public string RefType { get; set; }

        public string RefNo { get; set; }

        public string RefNote { get; set; }

        public string AssignBy { get; set; }

        public string Note { get; set; }

        //public List<TM_Permissions> Permissions { get; set; }

        public string Owner { get; set; }

        public string OwnerName { get; set; }

        public string BUID { get; set; }

        public string Attachment { get; set; }

        public string ApproveControl { get; set; }

        public string ApproveStatus { get; set; }

        public string ApproveComments { get; set; }

        public string Approvers { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public string ApprovedBy { get; set; }

        public bool IsAssign { get; set; }

        public bool IsOverdue { get; set; }

        public bool PrivateTask { get; set; }

        public bool Remainder { get; set; }

        public short RemainderDays { get; set; }

        public short ReOpens { get; set; }

        public short SplitedNo { get; set; }

        public int? Attachments { get; set; }

        public int? Comments { get; set; }

        public string Avatar { get; set; }

        public string ConfirmControl { get; set; }

        public string ConfirmStatus { get; set; }

        public DateTime? ConfirmDate { get; set; }

        public string ConfirmComment { get; set; }

        public string VerifyControl { get; set; }

        public string VerifyStatus { get; set; }

        public DateTime? VerifyDate { get; set; }

        public string VerifyByType { get; set; }

        public string VerifyBy { get; set; }

        public string VerifyComment { get; set; }

        public int Extends { get; set; }

        public string ExtendStatus { get; set; }

        public string AutoCompleted { get; set; }

        public bool Closed { get; set; }

        public string KBColumn { get; set; }

        public double? IndexNo { get; set; }

        public int? Level { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public string EmployeeID { get; set; }

        public string PositionID { get; set; }

        public string OrgUnitID { get; set; }

        public string DivisionID { get; set; }

        public string DepartmentID { get; set; }

        public string CompanyID { get; set; }
      
        public string ExtendApprover { get; set; }
        public string IsGenerated { get; set; }
        // import
        //[NotMapped]
        public string Combination { get; set; } //nguoi phoi hop - nhan tu temp import
        //[NotMapped]
        public string Monitor { get; set; } //nguoi theo doi -han tu temp import

        public string ParentNo { get; set; } //Phuc vu import-import xong up no bang ""

        public string UseCode { get; set; }
        public string IsTemplate { get; set; } // "0" mac dinh, "1" mau, "2" clone 
        public string IsRepeatTmp { get; set; }
        public string RepeatTmp { get; set; }
        public string RepeatConfig { get; set; }
        public string RepeatEnd { get; set; }

        // ve chart cho nhanh
        //[NotMapped]
        public string PerStatus { get; set; } //tr?ng thái công vi?c-h? tr? v? chart cho nhanh

        // 2 field này dành cho việc mapping data import từ SurePortal sang Codx
        public string ImportFrom { get; set; } // "sqlsvr", // 2 field này cần thống nhất với Thương trước khi sử dụng
        public string MapID { get; set; } // 2 field này cần thống nhất với Thương trước khi sử dụng
        public int? ImportStatus { get; set; } // Trạng thái import dữ liệu: bit-1: AttachFile; bit-2: History
    }
}
