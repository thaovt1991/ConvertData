using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.Model.ModelMongo
{
    //[BsonCollection("TM_Tasks")]
    public class TM_Tasks : ParentEntity
    {
        public Guid RecID { get; set; } //Guid

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

        public List<TM_Permissions> Permissions { get; set; }

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
        //// import
        ////[NotMapped]
        //public string Combination { get; set; } //nguoi phoi hop - nhan tu temp import
        ////[NotMapped]
        //public string Monitor { get; set; } //nguoi theo doi -han tu temp import

        public string ParentNo { get; set; } //Phuc vu import-import xong up no bang ""

        public string UseCode { get; set; }
        public string IsTemplate { get; set; } // "0" mac dinh, "1" mau, "2" clone 
        public string IsRepeatTmp { get; set; }
        public string RepeatTmp { get; set; }
        public string RepeatConfig { get; set; }
        public string RepeatEnd { get; set; }
        public string RefFunctionID { get; set; }
        // ve chart cho nhanh
        //[NotMapped]
        public string PerStatus { get; set; } //tr?ng thái công vi?c-h? tr? v? chart cho nhanh

        // 2 field này dành cho việc mapping data import từ SurePortal sang Codx - gio sài để máp pvoid
        public string ImportFrom { get; set; } // "sqlsvr", // 2 field này cần thống nhất với Thương trước khi sử dụng
        public string MapID { get; set; } // 2 field này cần thống nhất với Thương trước khi sử dụng
        public int? ImportStatus { get; set; } // Trạng thái import dữ liệu: bit-1: AttachFile; bit-2: History

        public List<TM_History> History { get; set; }
        /// <summary>
        /// Column: TM_Sprints.IterationID
        /// </summary>
        [ForeignKey("IterationID")]
        public TM_Sprints TM_Sprints { get; set; } //biến rác phải có để chuyển chứ nó ko có tích sự gì
        //
        public string CreatedByName { get; set; }
        public string EmployeeName { get; set; }
        public string PositionName { get; set; }
        public string OrgUnitName { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionName { get; set; }
        public string CompanyName { get; set; }
        public string FullTextSearch { get; set; }
        public string IndexMeta { get; set; }
        public string IndexContain { get; set; }
        public string DW { get; set; } //- bien de biet có data Fact
        public string DocID { get; set; } //ds file ;
    }

    public class TM_Permissions
    {

        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }
        public string ObjectType { get; set; }
        public string ObjectID { get; set; }
        public string ObjectName { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool Full { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Assign { get; set; }
        public bool Delete { get; set; }
        public bool Share { get; set; }

        public bool Upload { get; set; }
        public bool Download { get; set; }
        public bool AllowPermit { get; set; }
        public string AllowUpdateStatus { get; set; }
        public bool Publish { get; set; }
        public string RoleType { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    //Resource
    public class TM_TaskResources
    {
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }
        public string TaskID { get; set; }
        public string ResourceID { get; set; }
        public string RoleType { get; set; }
        public string Memo { get; set; }
        public string RefID { get; set; }

        public List<Permission> Permissions { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string BUID { get; set; }
        public string EmployeeID { get; set; }
        public string PositionID { get; set; }
        public string OrgUnitID { get; set; }
        public string DivisionID { get; set; }
        public string DepartmentID { get; set; }
        public string CompanyID { get; set; }
    }

    public class Permission
    {
        public bool Create { get; set; }

        public bool Read { get; set; }

        public bool Update { get; set; }

        public bool Assign { get; set; }

        public bool Delete { get; set; }

        public bool Share { get; set; }

        public bool Upload { get; set; }

        public bool Download { get; set; }

        public bool Publish { get; set; }

        public bool AllowPermit { get; set; }

        public bool? SentMessageFirebase { get; set; }

        public string AllowUpdateStatus { get; set; }

        public bool IsActive { get; set; }

        public bool IsSharing { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
    }

    public class TM_TaskExtends
    {
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }
        public string TaskID { get; set; }
        public string ExtendApprover { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

        public string ExtendComment { get; set; }

        public List<Permission> Permissions { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExtendDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class TM_TaskGoals
    {
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }

        public string TaskID { get; set; }
        public string Category { get; set; }

        public string Memo { get; set; }
        public string Status { get; set; }
        public DateTime? ActualEndDate { get; set; }

        public string Note { get; set; }

        public List<Permission> Permissions { get; set; }
        public int Sorting { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Owner { get; set; }
        public string BUID { get; set; }

        public string EmployeeID { get; set; }
        public string PositionID { get; set; }
        public string OrgUnitID { get; set; }
        public string DivisionID { get; set; }
    }

    public class TM_TaskGroups
    {
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TaskGroupID { get; set; }
        public string TaskGroupName { get; set; }
        public string TaskGroupName2 { get; set; }
        public string TaskNoteControl { get; set; }
        public string TaskNoteStatus { get; set; }
        public string TaskType { get; set; }
        public decimal StdDays { get; set; }
        public decimal StdHours { get; set; }
        public string ParentID { get; set; }
        public string Category { get; set; }
        public string Note { get; set; }
        public string CascadeUpdate { get; set; }
        public string CheckListControl { get; set; }

        public string CheckList { get; set; }
        public string AttachmentControl { get; set; }
        public string ProjectControl { get; set; }
        public string ApproveControl { get; set; }
        public string ApproveBy { get; set; }
        public string Approvers { get; set; }
        public string MaxHoursControl { get; set; }
        public decimal MaxHours { get; set; }
        public string LocationControl { get; set; }
        public string PlanControl { get; set; }
        public string UpdateControl { get; set; }
        public string AutoCompleted { get; set; }
        public string CompletedControl { get; set; }
        public string DueDateControl { get; set; }
        public string ExtendControl { get; set; }
        public string ExtendBy { get; set; }
        public string ConfirmControl { get; set; }
        public string VerifyControl { get; set; }
        public string VerifyByType { get; set; }
        public string VerifyBy { get; set; }
        public string EditControl { get; set; }
        public string OnStart { get; set; }
        public string OnDelay { get; set; }
        public string OnCancel { get; set; }
        public string OnFinish { get; set; }
        public string Sorting { get; set; }

        public List<Permission> Permissions { get; set; }

        public int? Attachments { get; set; }

        public int? Comments { get; set; }
        public bool Stop { get; set; }
        public string Owner { get; set; }
        public string BUID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string EmployeeID { get; set; }
        public string PositionID { get; set; }
        public string OrgUnitID { get; set; }
        public string DivisionID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Manager { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string ProjectID { get; set; }
        public string Members { get; set; }
        public decimal? Percentage { get; set; }
    }
    public class TM_Sprints
    {
        [Key]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string IterationID { get; set; }
        public string IterationType { get; set; }
        public string IterationName { get; set; }
        public string Interval { get; set; }

        public short? Year { get; set; }

        public short? Month { get; set; }

        public short? Week { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
        public string AreaID { get; set; }
        public string ProjectID { get; set; }
        public string Resources { get; set; }
        public string ViewMode { get; set; }
        public string ViewTemplate { get; set; }
        public bool IsShared { get; set; }
        public string Owner { get; set; }
        public string BUID { get; set; }
        public string Note { get; set; }

        public List<Permission> Permissions { get; set; }
        public bool Closed { get; set; }
        public DateTime? ClosedOn { get; set; }
        public string ClosedBy { get; set; }

        public int? Attachments { get; set; }

        public int? Comments { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string EmployeeID { get; set; }
        public string PositionID { get; set; }
        public string OrgUnitID { get; set; }
        public string DivisionID { get; set; }
    }

    public class TM_History
    {
        //RoleType ; UserID; UserName; PositionName; OrgUnitName ; Comment; Percentage ; LastUpdate
        public string RoleType { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PositionName { get; set; }
        public string OrgUnitName { get; set; }
        public string Comment { get; set; }
        public decimal Percentage { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
