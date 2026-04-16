using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelMongo;
using ConvertData.Model.ModelPG;
using ConvertData.Model.ModelSQL;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using Newtonsoft.Json;
using NLog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Data.SqlClient.Internal.SqlClientEventSource;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ConvertData.WorkerConvert
{
    public class TaskConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;

        private static Guid DefaultUserId = new("9B621C58-A5F6-F011-8D1E-54BF6477FBB6"); //default User
        private static Guid DefaultProject = Guid.Empty; //default User
        private static string CodeDefault = "PROD-0002"; //ma mac dinh

        private IMongoCollection<TM_Tasks> _tmTasks_Collection;
        private IMongoCollection<TM_TaskResources> _tmTasksResource_Collection;
        private IMongoCollection<TM_TaskGoals> _tmTasksGoals_Collection;
        private IMongoCollection<TM_TaskExtends> _tmTasksExt_Collection;

        public TaskConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _logger = logger;
            _parentForm = parentForm;
            _connectModel = connectModel;
            var mongoClient = new MongoClient(connectModel.ConnectionStringMG);
            _tmTasks_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<TM_Tasks>(typeof(TM_Tasks).Name);
            _tmTasksResource_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<TM_TaskResources>(typeof(TM_TaskResources).Name);
            _tmTasksGoals_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<TM_TaskGoals>(typeof(TM_TaskGoals).Name);
            _tmTasksExt_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<TM_TaskExtends>(typeof(TM_TaskExtends).Name);
        }

        public async Task<bool> ConvertDataTasks(ParameterModelTask parameterModel)
        {
            try
            {
                var filterBuilder = Builders<TM_Tasks>.Filter;

                //              var filter = filterBuilder.And(
                //                              filterBuilder.Ne(x => x.ImportFrom, "convertToSQL"),
                //                              filterBuilder.Ne(x => x.IsTemplate, "1"),
                //                              filterBuilder.Ne(x => x.Category, "PBL")
                //                              );

                var filter = filterBuilder.Ne(x => x.ImportFrom, "convertToSQL")
                             & filterBuilder.Ne(x => x.IsTemplate, "1")
                             & filterBuilder.Ne(x => x.Category, "PBL");

                var startCreatedDate = parameterModel?.StartCreatedDate;
                var endCreatedDate = parameterModel?.EndCreatedDate;

                if (!string.IsNullOrEmpty(startCreatedDate))
                {
                    if (DateTime.TryParse(startCreatedDate, out DateTime start))
                    {
                        filter &= filterBuilder.Gte(x => x.CreatedOn, start);
                    }
                }

                if (!string.IsNullOrEmpty(endCreatedDate))
                {
                    if (DateTime.TryParse(endCreatedDate, out DateTime end))
                    {
                        var endOfDay = end.Date.AddDays(1).AddTicks(-1);
                        filter &= filterBuilder.Lte(x => x.CreatedOn, endOfDay);
                    }
                }


                var listTasks = await _tmTasks_Collection.Find(filter)
                                         .Skip((parameterModel.Page - 1) * parameterModel.PageSize)
                                         .Limit(parameterModel.PageSize).ToListAsync();

                _parentForm.countTask.Text = (listTasks?.Count ?? 0).ToString();
                _parentForm.convertSucTask.Text = "0";

                if (listTasks?.Count > 0)
                {
                    _parentForm.richTextBox1.AppendText($"Tìm thấy {listTasks.Count} công việc {(!string.IsNullOrEmpty(startCreatedDate) ? "từ " + startCreatedDate + " " : "")}{(!string.IsNullOrEmpty(endCreatedDate) ? "đến " + endCreatedDate : "")}\n");
                    var dicUser = await GetInfoUsers(null, true);//lấy all tolowcase
                    //danh sach du an
                    var dicProject = new Dictionary<string, Guid>();
                    var dicTaskGoals = new Dictionary<string, List<TM_TaskGoals>>();

                    var listTaskIDs = listTasks.Select(x => x.TaskID).Distinct().ToList() ?? new List<string>();
                    if (parameterModel.IsUpdateFull || parameterModel.IsUpdateTask)
                    {
                        var listProjectIDs = listTasks.Select(x => x.ProjectID).Distinct().ToList() ?? new List<string>();
                        dicProject = await GetProject(listProjectIDs);

                        dicTaskGoals = await GetListTaskGoalsToDictionary(listTaskIDs);
                    }
                    ////task assign
                    //var dicResource = await GetListTaskResourceToDictionary(listTaskIDs);
                    ////Gia hạn
                    //var dicExt = await GetListTaskExtendToDictionary(listTaskIDs);
                    
                    var dicResource = await GetTaskDataToDictionary<TM_TaskResources>(
                        _tmTasksResource_Collection,
                        listTaskIDs
                    );

                    // 2. Gọi lấy danh sách Extend
                    var dicExt = await GetTaskDataToDictionary<TM_TaskExtends>(
                        _tmTasksExt_Collection,
                        listTaskIDs
                    );

                    //lấy default 
                    DefaultProject = await new ProjectConvert(_parentForm, _logger, _connectModel).GetIdProjectByCode(CodeDefault) ?? Guid.Empty;

                    var i = 0;
                    var countSuccess = 0;
                    IProgress<int> progress = new Progress<int>(value =>
                    {
                        _parentForm.prgStatusTask.Value = value; // Cập nhật thanh tiến trình
                    });
                    IProgress<string> countSuccessFunc = new Progress<string>(value =>
                    {
                        _parentForm.convertSucTask.Text = value;
                    });

                    foreach (var item in listTasks)
                    {
                        //Chuyen dataa
                        var isSuccces = await AddTaskToSQLServer(item, dicUser, dicProject, dicTaskGoals, dicResource, dicExt, parameterModel);
                        //end chuyeern
                        //UPDATE NGAY
                        if (isSuccces)
                        {
                            var updateFilter = Builders<TM_Tasks>.Filter.Eq(x => x.Id, item.Id);
                            var updateDefinition = Builders<TM_Tasks>.Update.Set(x => x.ImportFrom, "convertToSQL");
                            await _tmTasks_Collection.UpdateOneAsync(updateFilter, updateDefinition);
                            countSuccess++;

                            countSuccessFunc.Report(countSuccess.ToString());
                        }

                        i++;
                        int percentComplete = (i * 100) / listTasks.Count;
                        progress.Report(percentComplete);

                    }
                }
                else _parentForm.richTextBox1.AppendText(($"Không có data nào được tìm thấy \n"));

                return true;
            }
            catch (Exception ex)
            {
                _parentForm.richTextBox1.AppendText(($"Lỗi truy vấn: {ex.Message}"));
                return false;
            }
        }

        #region task Convert
        public async Task<bool> AddTaskToSQLServer(TM_Tasks task, Dictionary<string, Guid> dicUsers, Dictionary<string, Guid> dicProject, Dictionary<string, List<TM_TaskGoals>> dicGoals,
            Dictionary<string, List<TM_TaskResources>> dicRes, Dictionary<string, List<TM_TaskExtends>> dicExt, ParameterModelTask parameterModel)
        {
            try
            {
                SQLTask taskSQL = await MapModelTask(task, dicUsers, dicProject, dicGoals);
                if (taskSQL == null) return false;

                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    string sql = @"
                                DELETE FROM qlcv.Tasks WHERE Code = @Code;   --//Xóa cũ 

                                INSERT INTO qlcv.Tasks(
                                   Id,ProjectId ,ParentId,RecurringSourceId,Code,Name,Description
                                  ,Status,Path,SortOrder,AssignBy,RepeatInterval,StartDate,DueDate,EndDate
                                  ,IsReport,RepeatType,Level
                                  ,CompletePercent,ActiveFlag ,CreatedBy,UpdatedBy
                                  ,CreatedDate,UpdatedDate,Checklist,RepeatCount,RepeatEndDate
                                  ,Category,IsRecurring,ReviewPercent,PriorityType,MigratedFromId
                                ) 
                                VALUES (
                                    @Id,@ProjectId,@ParentId,@RecurringSourceId,@Code,@Name,@Description
                                    ,@Status,@Path,@SortOrder,@AssignBy,@RepeatInterval,@StartDate,@DueDate,@EndDate
                                    ,@IsReport,@RepeatType,@Level
                                    ,@CompletePercent,@ActiveFlag ,@CreatedBy,@UpdatedBy
                                    ,@CreatedDate,@UpdatedDate,@Checklist,@RepeatCount,@RepeatEndDate
                                    ,@Category,@IsRecurring,@ReviewPercent,@PriorityType,@MigratedFromId
                                    )";

                    try
                    {
                        int rowsAffected = await db.ExecuteAsync(sql, taskSQL);
                        var result = rowsAffected > 0;
                        if (result)
                        {
                            var listIDs = new List<Guid>() { taskSQL.Id};
                            //chuyen source
                            if (dicRes.TryGetValue(task.TaskID, out var listRes))
                            {
                                //xóa cũ
                                await DeleteTaskAssignsByIds(listIDs);
                                //tạo mới
                                result = await UpdateTaskAssign(listRes, dicUsers, taskSQL);
                               
                            }
                            //Chuyen gia han
                            if (dicExt.TryGetValue(task.TaskID, out var listExt))
                            {
                                //xóa cũ
                                await DeleteTaskExtendByTaskIds(listIDs);
                                //tạo mới
                                result = await UpdateWorkTaskExtensionRequest(listExt, dicUsers, taskSQL.Id);
                            }
                        }
                        return result;
                    }
                    catch (Exception ex)
                    {
                        _parentForm.richTextBox1.AppendText($"❌ Lỗi SQL Insert: {ex.Message}\n");
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                _parentForm.richTextBox1.AppendText($"❌ Lỗi SQL Insert: {ex.Message}\n");
                return false;
            }

        }

        private async Task<SQLTask> MapModelTask(TM_Tasks task, Dictionary<string, Guid> dicUsers, Dictionary<string, Guid> dicProject, Dictionary<string, List<TM_TaskGoals>> dicGoals)
        {
            var prID = DefaultProject; ///Do no bat buộc nên ko có là defaule
            if (!string.IsNullOrEmpty(task.ProjectID))
            {
                var projectID = task.ProjectID ?? "";
                if (!dicProject.TryGetValue(projectID, out prID))
                {
                    _parentForm.richTextBox1.AppendText($"❌ Lỗi Logic:Dư án mã '{task.ProjectID}' chưa có trong databasse! \n");
                    return null;
                }
            }

            if(prID == Guid.Empty)
            {
                _parentForm.richTextBox1.AppendText($"❌ Lỗi Logic:Dư án mã '{task.ProjectID}' chưa có trong databasse! \n");
                return null;
            }
         

            var taskSQL = new SQLTask()
            {
                Id = task.RecID, //giu recID
                ProjectId = prID,
                ParentId = Guid.TryParse(task.ParentID, out var pr) ? (Guid?)pr : null,
                Name = task.TaskName,
                Code = task.TaskID,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                DueDate = task.DueDate,
                CompletePercent = (double)task.Percentage,
                SortOrder = task.IndexNo > 0 ? (int)task.IndexNo : 0,
                Level = task.Level,
                CreatedBy = DefaultUserId,
                Checklist = "[]",
                IsReport = !string.IsNullOrEmpty(task.Approvers) && task.ApproveControl == "1",
                IsRecurring = false,
                RepeatType = 0,
                PriorityType = task.Priority == "1" ? PriorityType.Low : (task.Priority == "3" ? PriorityType.Hight : PriorityType.Normal),
                CreatedDate = task.CreatedOn,
            };

            //TaskGoals
            if (dicGoals.TryGetValue(task.TaskID, out var listTaskGoals))
            {
                List<ChecklistItem> mongoChecklist = listTaskGoals.Select((x, index) =>
                new ChecklistItem()
                {
                    Name = x.Memo,
                    IsChecked = x.Status == "90",
                    SortOrder = index,
                }).ToList();
                taskSQL.Checklist = JsonConvert.SerializeObject(mongoChecklist);
            }


            
            //Status
            switch (task.Status)
            {
                case "00":
                case "10":
                    taskSQL.Status = WorkTaskStatus.New;
                    break;
                case "20":
                    taskSQL.Status = WorkTaskStatus.InProgress;
                    break;
                case "30":
                    taskSQL.Status = WorkTaskStatus.Reporting;
                    break;
                case "50": //Hoan ko có thì hủy luôn
                           //taskSQL.Status = WorkTaskStatus.InProgress;
                           //break;
                case "80":
                    taskSQL.Status = WorkTaskStatus.Cancelled;
                    break;
                case "90":
                    taskSQL.Status = WorkTaskStatus.Completed;
                    break;
                case "95":
                    taskSQL.Status = WorkTaskStatus.Rejected;
                    break;
            }


            var createdBy = task.CreatedBy?.ToLower() ?? "";
            if (dicUsers.TryGetValue(createdBy, out var createdByID) ||
                dicUsers.TryGetValue($@"pvoil\{createdBy}", out createdByID))
            {
                taskSQL.CreatedBy = createdByID;
            }

            if (task.ModifiedOn != null && task.ModifiedOn > DateTime.MinValue)
            {
                taskSQL.UpdatedDate = task.ModifiedOn;
                taskSQL.UpdatedBy = DefaultUserId;
            }

            if (!string.IsNullOrEmpty(task.ModifiedBy))
            {
                var modifiedBy = task.ModifiedBy?.ToLower() ?? "";
                if (dicUsers.TryGetValue(modifiedBy, out var modifineBy) ||
                    dicUsers.TryGetValue($@"pvoil\{modifiedBy}", out modifineBy))
                {
                    taskSQL.UpdatedBy = modifineBy;
                }
            }

            if (string.IsNullOrEmpty(task.AssignBy) || task.AssignBy == task.CreatedBy)
            {
                taskSQL.AssignBy = taskSQL.CreatedBy;
            }
            else
            {
                var assignBy = task.AssignBy?.ToLower() ?? "";
                if (dicUsers.TryGetValue(assignBy, out var assignByID) ||
                    dicUsers.TryGetValue($@"pvoil\{assignBy}", out assignByID))
                {
                    taskSQL.AssignBy = assignByID;
                }
            }


            //Kho nhat la parentID
            if (string.IsNullOrEmpty(task.ParentID))
            {
                taskSQL.Path = task.RecID.ToString();
            }
            else if (!string.IsNullOrEmpty(task.ParentList))
            {
                var parentList = task.ParentList.Replace(";", "\\");
                taskSQL.Path = $@"{parentList}\{task.RecID.ToString()}";
            }
            else
            {
                //không có thì đệ quy và tính
                taskSQL.Path = await GetParentList(task);
            }
            //category
            switch (task.Category)
            {
                case "G":
                    taskSQL.Category = 1;
                    break;
                case "4":
                    taskSQL.Category = 2;
                    break;
                case "5":
                    taskSQL.Category = 3;
                    break;
            }

            return taskSQL;
        }
        private async Task<string> GetParentList(TM_Tasks task)
        {
            if (string.IsNullOrEmpty(task.ParentID))
            {
                return task.RecID.ToString();
            }
            var taskParent = await GetTaskParent(task.ParentID);
            if (taskParent != null)
            {
                string upstreamPath = await GetParentList(taskParent);
                return $@"{upstreamPath}\{task.RecID}";
            }

            return $@"{task.ParentID}\{task.RecID}";
        }
        private async Task<string> GetParentList2(TM_Tasks task, string parentList = "")
        {
            if (string.IsNullOrEmpty(parentList))
            {
                parentList = task.RecID.ToString();
            }
            if (!string.IsNullOrEmpty(task.ParentID))
            {
                parentList = $@"{task.ParentID}\{parentList}";
            }
            var taskParent = await GetTaskParent(task.ParentID);
            if (taskParent != null)
            {
                return await GetParentList2(taskParent, parentList);
            }
            return parentList;
        }
        private async Task<TM_Tasks> GetTaskParent(string parentID)
        {
            if (!Guid.TryParse(parentID, out Guid parentGuid))
            {
                return null;
            }

            var task = await _tmTasks_Collection
                .Find(x => x.RecID == parentGuid)
                .Project(x => new TM_Tasks()
                {
                    RecID = x.RecID,
                    ParentID = x.ParentID
                })
                .FirstOrDefaultAsync();

            return task;
        }
        #endregion

        #region TaskResoure => taskAssign
        private async Task<bool> UpdateTaskAssign(List<TM_TaskResources> taskResources, Dictionary<string, Guid> dicUsers, SQLTask sqlTask)
        {
            var listAssignTask = new List<SQLTaskResource>();
            var listResouce =taskResources.Select(x=>x.ResourceID).Distinct().ToList();
            var dicInfoRes = await GetListDataInfoPG(listResouce);
            foreach (var resource in taskResources)
            {
                SQLTaskResource asignTask = MapTaskResource(resource, dicUsers, sqlTask, dicInfoRes);
                if (asignTask != null)
                {
                    listAssignTask.Add(asignTask);
                }
            }

            if (listAssignTask.Any())
            {
                //Lấy thông tin thêm
                var listUserId = listAssignTask.Select(x => x.AssignTo).Distinct().ToList();
                var dicInfoDep = await GetInfoUserDepartment(listUserId);
                if (dicInfoDep?.Count > 0)
                {
                    foreach (var asTask in listAssignTask)
                    {
                        if (dicInfoDep.TryGetValue(asTask.AssignTo, out var ud))
                        {
                            asTask.DepartmentId = ud.DepartmentId;
                            asTask.DepartmentName = ud.DepartmentName;
                        }else
                        {
                            asTask.DepartmentId = Guid.Empty;
                        }

                    }
                }

                string insertSql = @"
                                      INSERT INTO qlcv.TaskAssigns (
                                        TaskId,
                                        TaskAssignType,
                                        AssignTo,
                                        CompletePercent,
                                        [Status],
                                        DepartmentId,
                                        DepartmentName,
                                        JobTitleName,
                                        ActiveFlag,
                                        CreatedBy,
                                        UpdatedBy,
                                        CreatedDate,
                                        UpdatedDate
                                    ) 
                                    VALUES (
                                        @TaskId,
                                        @TaskAssignType,
                                        @AssignTo,
                                        @CompletePercent,
                                        @Status,
                                        @DepartmentId,
                                        @DepartmentName,
                                        @JobTitleName,
                                        @ActiveFlag,
                                        @CreatedBy,
                                        @UpdatedBy,
                                        @CreatedDate,
                                        @UpdatedDate
                                    )";

                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    try
                    {
                        await db.ExecuteAsync(insertSql, listAssignTask);
                    }
                    catch (Exception ex)
                    {
                        _parentForm.richTextBox1.AppendText($"❌ Lỗi SQL Insert TASK_ASSIGN: {ex.Message}\n");
                        return false;
                    }
                }
            }

            return true;
        }
        private SQLTaskResource MapTaskResource(TM_TaskResources res, Dictionary<string, Guid> dicUsers, SQLTask sqltask,Dictionary<string,AD_UserDto> dicADUser)
        {
            var resourceID = res?.ResourceID?.ToLower();
            if (string.IsNullOrEmpty(resourceID)) return null;
            if (!dicUsers.TryGetValue(resourceID, out var assignTo) &&
                !dicUsers.TryGetValue($@"pvoil\{resourceID}", out assignTo))
            {
                _parentForm.richTextBox1.AppendText($"⚠️ Bỏ qua: Không tìm thấy User {res?.ResourceID}\n");
                return null;
            }

            var assign = new SQLTaskResource()
            {
                TaskId = sqltask.Id,
                CompletePercent = sqltask.CompletePercent,
                Status = sqltask.Status,
                ActiveFlag = 0,
                AssignTo = assignTo,
                CreatedDate = res.CreatedOn,
                CreatedBy = sqltask.CreatedBy,
                DepartmentId = Guid.Empty
            };
            //Lấy thông tin thêm
            if (dicADUser.TryGetValue(res.ResourceID,out var info))
            {
                assign.JobTitleName = info.PositionName;
                assign.DepartmentName = info.DepartmentName ?? info.OrgUnitName;
            }

            return assign;
        }
        //Xoa
        public async Task<int> DeleteTaskAssignsByIds(List<Guid> listTaskId)
        {
            using (var conn = new SqlConnection(_connectModel.ConnectionStringSQL))
            {
                await conn.OpenAsync();

                string sql = "DELETE FROM qlcv.TaskAssigns WHERE TaskId IN @listTaskId";
                int rowsAffected = await conn.ExecuteAsync(sql, new { listTaskId });
                return rowsAffected; 
            }
        }
        #endregion

        #region TaskExtend => WorkTaskExtensionRequest
        private async Task<bool> UpdateWorkTaskExtensionRequest(List<TM_TaskExtends> listTasksExt, Dictionary<string, Guid> dicUsers, Guid taskID)
        {
            var listTaskExte = new List<SQLTaskExtend>();
            foreach (var et in listTasksExt)
            {
                var extend = await MapModelExt(et,dicUsers,taskID);
                if (extend != null)
                {
                    listTaskExte.Add(extend);
                }
            }
            if (listTaskExte.Any())
            {
                string sql = @"
                            INSERT INTO qlcv.TaskExtensionRequests (
                                Id,
                                TaskId,
                                TaskAssignId,
                                RequestedBy,
                                OldDueDate,
                                RequestedDueDate,
                                Reason,
                                Status,
                                ReviewedBy,
                                ReviewedDate,
                                ReviewComment,
                                ActiveFlag,
                                CreatedBy,
                                UpdatedBy,
                                CreatedDate,
                                UpdatedDate
                            ) 
                            VALUES (
                                @Id
                                @TaskId,
                                @TaskAssignId,
                                @RequestedBy,
                                @OldDueDate,
                                @RequestedDueDate,
                                @Reason,
                                @Status,
                                @ReviewedBy,
                                @ReviewedDate,
                                @ReviewComment,
                                @ActiveFlag,
                                @CreatedBy,
                                @UpdatedBy,
                                @CreatedDate,
                                @UpdatedDate
                            )";

                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {                 
                    try
                    {
                        await db.ExecuteAsync(sql, listTaskExte);
                    }
                    catch (Exception ex)
                    {
                        _parentForm.richTextBox1.AppendText($"❌ Lỗi SQL Insert TASK_ASSIGN: {ex.Message}\n");
                        return false;
                    }
                }
            }

            return true;
        }

        private async Task<SQLTaskExtend> MapModelExt(TM_TaskExtends et, Dictionary<string, Guid> dicUsers, Guid taskID)
        {
            //Kiem tra nguoi
            var requestID = et?.CreatedBy?.ToLower();
            if (string.IsNullOrEmpty(requestID)) return null;
            if (!dicUsers.TryGetValue(requestID, out var requestFrom) &&
                !dicUsers.TryGetValue($@"pvoil\{requestID}", out requestFrom))
            {
                _parentForm.richTextBox1.AppendText($"⚠️ Bỏ qua: Không tìm thấy User {et?.CreatedBy}\n");
                return null;
            }


            //Kiem tra duyet gia han
            var approverID = et?.ExtendApprover?.ToLower();
            if (string.IsNullOrEmpty(approverID)) return null;
            if (!dicUsers.TryGetValue(approverID, out var aproverID) &&
                !dicUsers.TryGetValue($@"pvoil\{approverID}", out aproverID))
            {
                _parentForm.richTextBox1.AppendText($"⚠️ Bỏ qua: Không tìm thấy User {et?.ExtendApprover}\n");
                return null;
            }

            var sqlEt = new SQLTaskExtend()
            {
                Id = Guid.NewGuid(),
                TaskId =taskID,
                TaskAssignId= await GetIDTaskAssignByID(taskID), //tim trong bang sql d chenf vafo
                CreatedDate = et.CreatedOn,
                Status = (byte)(et.Status == "3" ? 1 : (et.Status == "5" ? 2 : 3)),
                Reason = et.Reason,
                ReviewComment =et.ExtendComment,
                OldDueDate =et.DueDate,
                RequestedDueDate =et.ExtendDate,
                RequestedBy =requestFrom,
                ReviewedBy = aproverID,
                ReviewedDate= et.ModifiedBy == et.ExtendApprover ? et.ModifiedOn:null,
                ActiveFlag = 0
            };


            return sqlEt;
        }
        private async Task<Guid?> GetIDTaskAssignByID(Guid taskID)
        {
            try
            {
                using (var conn = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    await conn.OpenAsync();
                    string sql = @"SELECT Id FROM qlcv.TaskAssigns WHERE  TaskAssignType = @TaskAssignType AND TaskId = @TaskId";

                    var result = await conn.ExecuteScalarAsync<Guid?>(sql, new { TaskAssignType = 1, TaskId = taskID });
                    return result;
                }
            }
            catch (Exception ex) {
                _logger.Error(ex);
                return null;
            }
            
        }
        //Xoa
        public async Task<int> DeleteTaskExtendByTaskIds(List<Guid> listTaskId)
        {
            using (var conn = new SqlConnection(_connectModel.ConnectionStringSQL))
            {
                await conn.OpenAsync();

                string sql = "DELETE FROM qlcv.TaskExtensionRequests WHERE TaskId IN @listTaskId";
                int rowsAffected = await conn.ExecuteAsync(sql, new { listTaskId });
                return rowsAffected;
            }
        }
        #endregion

        //-----------------------------USER-------------------------------------
        #region Helper User
        public async Task<Dictionary<string, Guid>> GetInfoUsers(List<string> listUserIDs = null, bool isLowerCase = false)
        {

            try
            {
                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    IEnumerable<SQLCoreUserBase> usersAll = new List<SQLCoreUserBase>();

                    if (listUserIDs?.Count > 0)
                    {
                        //lấy theo danh sách
                        string sql = @"
                        SELECT Id, UserName 
                        FROM [core].[Users] 
                        WHERE LOWER(UserName) IN @UserNames";
                        usersAll = await db.QueryAsync<SQLCoreUserBase>(sql, new { UserNames = listUserIDs });
                    }
                    else
                    {
                        //lấy all
                        string sql = @"SELECT Id, UserName FROM core.Users WHERE UserName IS NOT NULL";
                        usersAll = await db.QueryAsync<SQLCoreUserBase>(sql, commandTimeout: 30);
                    }

                    if (isLowerCase)
                    {
                        return usersAll
                             .DistinctBy(x => x.UserName)
                             .ToDictionary(
                               x => x.UserName.ToString().ToLower(),
                               x => (Guid)x.Id
                           );
                    }
                    else
                    {
                        return usersAll
                             .DistinctBy(x => x.UserName)
                             .ToDictionary(
                               x => x.UserName.ToString(),
                               x => (Guid)x.Id
                           );
                    }

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"❌ Lỗi get user: {ex.Message}\n");
                return new Dictionary<string, Guid>();
            }

        }

        public async Task<Dictionary<Guid, SQLCoreUserInfo>> GetInfoUserDepartment(List<Guid> idUsers)
        {
            var dic = new Dictionary<Guid, SQLCoreUserInfo>();
            try
            {
                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    string sql = @"
                    SELECT u.UserId, u.DepartmentId, d.DepartmentName 
                    FROM core.UserDepartment u
                    JOIN core.Department d ON ud.DepartmentID = d.DepartmentID
                    WHERE u.UserId IN @idUsers
                   ";

                    var usersInfo = db.Query<SQLCoreUserInfo>(sql, new { idUsers }).ToList();

                    dic = usersInfo.ToDictionary(x => x.UserId, x => x);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return dic;
            }

            return dic;
        }

        //Kết nối lấy thông tin PG
        public async Task<Dictionary<string, AD_UserDto>> GetListDataInfoPG(List<string> listUserIDs)
        {
            var dic = new Dictionary<string, AD_UserDto>();

            try
            {
                using (var db = new NpgsqlConnection(_connectModel.ConnectionStringPG))
                {
                    await db.OpenAsync();

                      string sql = @"
                                    SELECT 
                                    ""UserID"", 
                                    ""UserName"", 
                                    ""DepartmentName"", 
                                    ""OrgUnitName""
                                    FROM public.""AD_Users""
                                    WHERE ""UserID"" = ANY(@listUserIDs)";

                    // Dapper xử lý truyền mảng listUserIDs vào toán tử ANY của PG
                    var usersInfo = await db.QueryAsync<AD_UserDto>(sql, new { listUserIDs });

                    dic = usersInfo.ToDictionary(x => x.UserID, x => x);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return dic;
            }

            return dic;
        }
        #endregion


        //-----------------------------NGHIEP VU-------------------------------------
        #region Nghiệp vụ
        public async Task<Dictionary<string, Guid>> GetProject(List<string> listProjectIDs = null)
        {

            try
            {
                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    IEnumerable<SQLProjectBase> usersAll = new List<SQLProjectBase>();

                    if (listProjectIDs?.Count > 0)
                    {
                        //lấy theo danh sách
                        string sql = @"
                        SELECT Id, Code 
                        FROM [qlcv].[Projects] 
                        WHERE Code IN @Code";
                        usersAll = await db.QueryAsync<SQLProjectBase>(sql, new { Code = listProjectIDs });
                    }
                    else
                    {
                        //lấy all
                        string sql = @"
                        SELECT Id, Code 
                        FROM [qlcv].[Projects]  WHERE Code IS NOT NULL";
                        usersAll = await db.QueryAsync<SQLProjectBase>(sql, commandTimeout: 30);
                    }


                    return usersAll
                             .DistinctBy(x => x.Code)
                             .ToDictionary(
                               x => x.Code,
                               x => (Guid)x.Id
                           );
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"❌ Lỗi get user: {ex.Message}\n");
                return new Dictionary<string, Guid>();
            }

        }

        public async Task<Dictionary<string, List<TM_TaskGoals>>> GetListTaskGoalsToDictionary(List<string> listTasks = null)
        {
            try
            {

                var filterBuilder = Builders<TM_TaskGoals>.Filter;
                var filter = FilterDefinition<TM_TaskGoals>.Empty; // Mặc định lấy tất cả

                // Nếu có danh sách ID thì lọc theo ID, ngược lại thì để trống (lấy hết)
                if (listTasks?.Count > 0)
                {
                    filter = filterBuilder.In(x => x.TaskID, listTasks);
                }

                var data = _tmTasksGoals_Collection
                    .Find(filter)
                    .Project(x => new TM_TaskGoals
                    {
                        TaskID = x.TaskID,
                        Memo = x.Memo,
                        Status = x.Status,
                        //CreatedOn = x.CreatedOn,
                    })
                    .ToList();
                return data?.Count > 0 ? data.GroupBy(x => x.TaskID).ToDictionary(x => x.Key, g => g.ToList()) : new Dictionary<string, List<TM_TaskGoals>>();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Dictionary<string, List<TM_TaskGoals>>();
            }
        }

        public async Task<Dictionary<string, List<TM_TaskResources>>> GetListTaskResourceToDictionary(List<string> listTasks = null)
        {
            try
            {

                var filterBuilder = Builders<TM_TaskResources>.Filter;
                var filter = FilterDefinition<TM_TaskResources>.Empty; // Mặc định lấy tất cả

                // Nếu có danh sách ID thì lọc theo ID, ngược lại thì để trống (lấy hết)
                if (listTasks?.Count > 0)
                {
                    filter = filterBuilder.In(x => x.TaskID, listTasks);
                }

                var data = _tmTasksResource_Collection
                    .Find(filter)
                    .ToList();
                return data?.Count > 0 ? data.GroupBy(x => x.TaskID).ToDictionary(x => x.Key, g => g.ToList()) : new Dictionary<string, List<TM_TaskResources>>();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Dictionary<string, List<TM_TaskResources>>();
            }
        }

        public async Task<Dictionary<string, List<TM_TaskExtends>>> GetListTaskExtendToDictionary(List<string> listTasks = null)
        {
            try
            {

                var filterBuilder = Builders<TM_TaskExtends>.Filter;
                var filter = FilterDefinition<TM_TaskExtends>.Empty; 

                if (listTasks?.Count > 0)
                {
                    filter = filterBuilder.In(x => x.TaskID, listTasks);
                }

                var data = _tmTasksExt_Collection
                    .Find(filter)
                    .ToList();
                return data?.Count > 0 ? data.GroupBy(x => x.TaskID).ToDictionary(x => x.Key, g => g.ToList()) : new Dictionary<string, List<TM_TaskExtends>>();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Dictionary<string, List<TM_TaskExtends>>();
            }
        }

        //Chung 
        public async Task<Dictionary<string, List<T>>> GetTaskDataToDictionary<T>(
                                IMongoCollection<T> collection,
                                List<string> listTaskIDs = null) where T : class
        {
            try
            {
                var filterBuilder = Builders<T>.Filter;
                var filter = FilterDefinition<T>.Empty;
                if (listTaskIDs?.Count > 0)
                {
                    filter = filterBuilder.In("TaskID", listTaskIDs);
                }

                var data = await collection.Find(filter).ToListAsync();

                if (data == null || data.Count == 0)
                    return new Dictionary<string, List<T>>();

                return data.GroupBy(x => x.GetType().GetProperty("TaskID")?.GetValue(x, null)?.ToString())
                           .Where(g => g.Key != null)
                           .ToDictionary(g => g.Key, g => g.ToList());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Dictionary<string, List<T>>();
            }
        }
        #endregion
    }
}
