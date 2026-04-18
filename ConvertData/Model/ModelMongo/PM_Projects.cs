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
    public class PM_Projects :ParentEntity
    {
        //public string Id { get; set; }
        public Guid RecID { get; set; }
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectName2 { get; set; }
        public DateTime? ProjectDate { get; set; }
        public string ProjectType { get; set; }
        public string ProjectCategory { get; set; }
        public string ProjectGroupID { get; set; }
        public string ProjectPoolID { get; set; }
        public string ProjectTemplate { get; set; }
        public string ParentID { get; set; }
        public string CurrencyID { get; set; }
        public decimal ExchangeRate { get; set; }
        public bool Multi { get; set; }
        public decimal ProjectValue { get; set; }
        public decimal ProjectValue2 { get; set; }
        public decimal ContractValue { get; set; }
        public decimal Construction { get; set; }
        public decimal Construction2 { get; set; }
        public decimal Goods { get; set; }
        public decimal Goods2 { get; set; }
        public decimal Withdraw { get; set; }
        public decimal Remaining { get; set; }
        public decimal FundValue { get; set; }
        public string FundID { get; set; }
        public string FundStatus { get; set; }
        public string AreaID { get; set; }
        public string AttributeGroupID { get; set; }
        public string CustomerID { get; set; }
        public string ObjectType { get; set; }
        public string ObjectID { get; set; }
        public string ContractNo { get; set; }
        public string SalesUnitID { get; set; }
        public string SalespersonID { get; set; }
        public string SupervisorID { get; set; }
        public string PmtTermID { get; set; }
        public string PmtMethodID { get; set; }
        public string DelModeID { get; set; }
        public string DelTermID { get; set; }
        public string RefType { get; set; }
        public string RefNo { get; set; }
        public DateTime? RefDate { get; set; }
        public string Investor { get; set; }
        public string Beneficiary { get; set; }
        public string Tenderer { get; set; }
        public string Consultant { get; set; }
        public string Designer { get; set; }
        public string PMUnit { get; set; }
        public string ProjectManeger { get; set; }
        public string ProjectManager { get; set; }

        public string ProjectManagerName { get; set; }
        public string Controllor { get; set; }
        public string CalendarType { get; set; }
        public string CalendarID { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string StatusItems { get; set; }
        public DateTime? StatusDate { get; set; }

        public string StatusNote { get; set; }
        public List<PM_Permissions> Permissions { get; set; }
        public string Attachment { get; set; }
        public string ApprovalRule { get; set; }
        public string ApprovalStatus { get; set; }
        public string ApprovalControl { get; set; }
        public string OutlineFormat { get; set; }
        public string Separator { get; set; }
        public bool Scheduled { get; set; }
        public DateTime? ScheduledOn { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleFinishDate { get; set; }
        public decimal CompletedPct { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualFinishDate { get; set; }
        public decimal ActualCost { get; set; }
        public decimal BudgetCost { get; set; }
        public decimal EarnedValue { get; set; }
        public decimal PlannedValue { get; set; }
        public string LastActivityID { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? PlaceInService { get; set; }
        public decimal TotalDiscPct { get; set; }
        public string CommissionBase { get; set; }
        public decimal CommissionPct { get; set; }
        public string BonusBase { get; set; }
        public decimal BonusPct { get; set; }
        public string DIM1 { get; set; }
        public string DIM2 { get; set; }
        public string DIM3 { get; set; }
        public string WarehouseControl { get; set; }
        public string Warehouse { get; set; }
        public string Note { get; set; }
        public string Owner { get; set; }
        public string BUID { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string ApprovedBy { get; set; }

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

        public string DepartmentID { get; set; }
        public string CompanyID { get; set; }

        public string Priority { get; set; }

        public string Tags { get; set; }
        public bool? Stop { get; set; }
        public List<PM_Projects_Settings> Settings { get; set; }
        public decimal? TotalTask { get; set; }
        public decimal? CompletedTask { get; set; }
        public decimal? OverDueTask { get; set; }
        public string IsTemplate { get; set; }
        public string KBColumnView { get; set; }
        public string KBColumnSetting { get; set; }
        public string DeptIDs { get; set; }
        public string CompanyName { get; set; }
        public string DW { get; set; }// - bien de biet có data Fact
        public string PortalStatus { get; set; }  //Không biết dùng làm gì thì dùng để biết là chuyển đổi
        public int? ConvertStatus { get; set; } //1 file 2 his 3 tag
    }

    public class PM_Permissions {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }
        public string ObjectType { get; set; }
        public string ObjectID { get; set; }
        public string ObjectName { get; set; }
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
        public string ApprovalRule { get; set; }
        public string ApproverType { get; set; }
        public string Approvers { get; set; }
        public string ApprovedBy { get; set; }
        public string ApproveStatus { get; set; }
        public DateTime? ApproveOn { get; set; }
        public string AutoCreate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
    }

    public class PM_Projects_Settings 
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid RecID { get; set; }

        public string LineType { get; set; }

        public string FieldName { get; set; }

        public string FieldValue { get; set; }

        public string DataType { get; set; }

        public string DataFormat { get; set; }

        public string ControlType { get; set; }

        public string ReferedType { get; set; }

        public string ReferdValue { get; set; }

        public string Tilte { get; set; }

        public string Description { get; set; }

        public bool? IsVisible { get; set; }

        public int? Sorting { get; set; }

        public string Icon { get; set; }

        public bool Stop { get; set; }

        public Guid? RefLineID { get; set; }
        public string DisplayMode { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
