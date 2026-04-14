using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelMongo;
using ConvertData.Model.ModelSQL;
using Dapper;
using Microsoft.Data.SqlClient;
//SQL
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using NLog;
using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ConvertData.WorkerConvert
{
    public class ProjectConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;
        private IMongoCollection<PM_Projects> _pmProject_Collection;

        //Contructor
        public ProjectConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _logger = logger;
            _parentForm = parentForm;
            _connectModel = connectModel;
            var mongoClient = new MongoClient(connectModel.ConnectionStringMG);
            _pmProject_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<PM_Projects>(typeof(PM_Projects).Name);
        }

        public async Task<bool> ConvertDataProject(ParameterModel parameterModel)
        {
            try
            {

                var filterBuilder = Builders<PM_Projects>.Filter;
                //var filter = filterBuilder.Empty;
                var filter = !filterBuilder.Eq(x => x.PortalStatus, "2"); //đã chuyển đổi
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

                var listProjects = await _pmProject_Collection.Find(filter)
                                            .Skip((parameterModel.Page -1) * parameterModel.PageSize)
                                          .Limit(parameterModel.PageSize).ToListAsync();

                if (listProjects.Count > 0)
                {
                    _parentForm.richTextBox1.AppendText($"Tìm thấy {listProjects.Count} dự án {(!string.IsNullOrEmpty(startCreatedDate) ? "từ " + startCreatedDate + " " : "")}{(!string.IsNullOrEmpty(endCreatedDate) ? "đến " + endCreatedDate : "")}\n");
                    // Thực hiện tiếp logic convert dữ liệu ở đây...
                    //lấy danh sách AD User tương ứng
                    var allUniqueUserNames = listProjects
                                            .SelectMany(p => p.Permissions.Select(m => m.ObjectID?.ToString())
                                            .Concat(listProjects.Select(p => p.CreatedBy)))                      
                                            .Where(id => !string.IsNullOrEmpty(id))                           
                                            .Distinct(StringComparer.OrdinalIgnoreCase)  // Lấy duy nhất, bất chấp hoa thường
                                            .ToList();
                    var dicUser = await GetInfoUsers(allUniqueUserNames);

                    //Chạy
                    var i = 0;
                    IProgress<int> progress = new Progress<int>(value =>
                    {
                        _parentForm.prgStatusProject.Value = value; // Cập nhật thanh tiến trình
                    });
                    // var bulkOps = new List<WriteModel<PM_Projects>>();
                    foreach (var item in listProjects)
                    {
                        //Chuyen dataa
                        var isSuccces = await AddProjectToSQLServer(item, dicUser);
                        //end chuyeern
                        //UPDATE NGAY
                        if (isSuccces)
                        {
                            var updateFilter = Builders<PM_Projects>.Filter.Eq(x => x.Id, item.Id);
                            var updateDefinition = Builders<PM_Projects>.Update.Set(x => x.PortalStatus, "2");
                            await _pmProject_Collection.UpdateOneAsync(updateFilter, updateDefinition);

                            // Thay vì update ngay, ta cho vào danh sách chờ
                            //var upsertModel = new UpdateOneModel<PM_Projects>(
                            //    Builders<PM_Projects>.Filter.Eq(x => x.Id, item.Id),
                            //    Builders<PM_Projects>.Update.Set(x => x.PortalStatus, "2")
                            //);
                            //bulkOps.Add(upsertModel);
                        }

                        i++;
                        int percentComplete = (i * 100) / listProjects.Count;
                        progress.Report(percentComplete);

                    }
                    //if (bulkOps.Count > 0)
                    //{
                    //    await _pmProject_Collection.BulkWriteAsync(bulkOps);
                    //}
                }
                else
                {
                    _parentForm.richTextBox1.AppendText("Không tìm thấy dữ liệu trong khoảng thời gian này.");
                }

                return true;
            }
            catch (Exception ex)
            {
                _parentForm.richTextBox1.AppendText(($"Lỗi truy vấn: {ex.Message}"));
                return false;
            }

        }
        //Update sang SQL sever
        private SQLProject MapToSQL(PM_Projects mongoItem, Dictionary<string,Guid> dic)
        {
            var project = new SQLProject
            {
                Id = Guid.NewGuid(),
                Code = mongoItem.ProjectID ,
                Name = mongoItem.ProjectName ?? "No Name",
                // Description = mongoItem.Description,
                StartDate = mongoItem.StartDate != null ? mongoItem.StartDate.Value : mongoItem.CreatedOn,
                EndDate = mongoItem.FinishDate != null ? mongoItem.FinishDate.Value : null,
                Status = ProjectStatus.New, // Mặc định là mới
                ProjectType = ProjectType.UserCreated,
                PriorityType = mongoItem.Priority == "1" ? PriorityType.Low : (mongoItem.Priority == "3"? PriorityType.Hight : PriorityType.Normal ),
                SortOrder = 1,
                IsProgressAutoCalculated = true,
                ProgressPercentage = (double)mongoItem.CompletedPct,
                DefaultViewMode = ProjectViewMode.Kanban,
                CreatedBy = Guid.Parse("A1B2C3D4-E5F6-7890-ABCD-1234567890AB"), // ID cố định hoặc lấy từ cấu hình
                TaskDeletePermissionMask = TaskDeletePermission.ProjectManager | TaskDeletePermission.Creator,
                DefaultKanbanGroupMode = KanbanGroupMode.Status,
                Note = "DataConvert", //data chuyển đổi
                ActiveFlag = 0,
                ProjectAccessScope = ProjectMemberScope.ProjectMembers,
                CreatedDate = mongoItem.CreatedOn,
                UpdatedDate = mongoItem.ModifiedOn,
            };
            //doc setting
            var memberType = mongoItem.Settings.FirstOrDefault(x => x.FieldName == "MemberType")?.FieldValue;
            if(!string.IsNullOrEmpty(memberType)) {
                switch (memberType)
                {
                    //4 5 cho trung 1
                    case "1":
                    case "4":
                    case "5": project.ProjectAccessScope = ProjectMemberScope.ProjectMembers;
                        break;
                    case "2":
                        project.ProjectAccessScope = ProjectMemberScope.Company;
                        break;
                    case "3":
                        project.ProjectAccessScope = ProjectMemberScope.Everyone;
                        break;
                    //case "4":
                    //    project.ProjectAccessScope = ProjectMemberScope.ProjectMembers;
                    //    break;
                    //case "5":
                    //    project.ProjectAccessScope = ProjectMemberScope.ProjectMembers;
                    //    break;
                }
            }

            if (dic.ContainsKey(mongoItem.CreatedBy.ToLower()) && dic.TryGetValue(mongoItem.CreatedBy.ToLower(),out var createdBy))
            {
                project.CreatedBy = createdBy;
            }

            return project;
        }
         
      

        public async Task<bool> AddProjectToSQLServer(PM_Projects item, Dictionary<string,Guid> dicUsers)
        {
            using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
            {
                SQLProject project = MapToSQL(item, dicUsers);
                //Conver

                string sql = @"
                                DELETE FROM qlcv.Projects WHERE Code = @Code;   --//Xóa cũ 

                                INSERT INTO qlcv.Projects (
                                    Id,Code, Name, Description, Note, Avatar, 
                                    Status, ProjectType, PriorityType, 
                                    StartDate, EndDate, SortOrder, 
                                    IsProgressAutoCalculated, ProgressPercentage, 
                                    DefaultViewMode,ProjectAccessScope, AllowTaskDeadlineExtension, 
                                    TaskDurationRangeMode,ActiveFlag, TaskDeletePermissionMask, 
                                    DefaultKanbanGroupMode, CreatedBy,CreatedDate,UpdatedDate
                                ) 
                                VALUES (
                                    @Id,@Code, @Name, @Description, @Note, @Avatar, 
                                    @Status, @ProjectType, @PriorityType, 
                                    @StartDate, @EndDate, @SortOrder, 
                                    @IsProgressAutoCalculated, @ProgressPercentage, 
                                    @DefaultViewMode,@ProjectAccessScope, @AllowTaskDeadlineExtension, 
                                    @TaskDurationRangeMode,@ActiveFlag, @TaskDeletePermissionMask, 
                                    @DefaultKanbanGroupMode, @CreatedBy,@CreatedDate,@UpdatedDate
                                )";

                try
                {
                    int rowsAffected = await db.ExecuteAsync(sql, project);
                    var result = rowsAffected > 0;
                    if (result)
                    {
                        result = await UpdateMembersProject(item, project.Id,dicUsers);
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

        private async Task<bool> UpdateMembersProject(PM_Projects mogoItem, Guid projectId, Dictionary<string, Guid> dic)
        {
            var sqlMembers = new List<SQLProjectMember>();
            var i = 0;
            foreach (var member in mogoItem?.Permissions)
            {

                var projectMb = new SQLProjectMember()
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    ProjectMemberType = member.RoleType == "PM" ? 1 : (member.RoleType == "S" ? 3 : 2),
                    SortOrder = i,
                    CreatedBy = Guid.Parse("A1B2C3D4-E5F6-7890-ABCD-1234567890AB"), // ID cố định hoặc lấy từ cấu hình
                    MemberId = Guid.NewGuid(), //tạo ảo sau này máp với User
                    ActiveFlag = 0,
                    CreatedDate = member.CreatedOn > DateTime.MinValue ? member.CreatedOn : mogoItem.CreatedOn,
                };
                if (dic.ContainsKey(member.ObjectID.ToLower()) && dic.TryGetValue(member.ObjectID.ToLower(), out var memberID))
                {
                    projectMb.MemberId = memberID;
                }
                var createdBy = mogoItem.CreatedBy.ToLower();
                if (dic.ContainsKey(createdBy) && dic.TryGetValue(createdBy, out var createdByID))
                {
                    projectMb.CreatedBy = createdByID;
                }

                i++;
               sqlMembers.Add(projectMb);
            }
            if (sqlMembers.Any())
            {
                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    string insertSql = @"
                                        INSERT INTO qlcv.ProjectMembers (
                                        ProjectId, ModuleType, ModuleObjectId,MemberId,ProjectMemberType,
                                        ActiveFlag, CreatedBy,SortOrder,CreatedDate
                                        ) 
                                        VALUES(
                                        @ProjectId,@ModuleType, @ModuleObjectId,@MemberId,@ProjectMemberType,
                                        @ActiveFlag, @CreatedBy,@SortOrder,@CreatedDate
                                        )";

                    try
                    {
                        await db.ExecuteAsync(insertSql, sqlMembers);
                    }
                    catch(Exception ex)
                    {
                        _parentForm.richTextBox1.AppendText($"❌ Lỗi SQL Insert Member: {ex.Message}\n");
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<Dictionary<string,Guid>> GetInfoUsers(List<string> listUserIDs)
        {
            if (listUserIDs == null || listUserIDs?.Count == 0) return new Dictionary<string, Guid>();
            try
            {
                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    string sql = @"
                        SELECT Id, UserName 
                        FROM [core].[Users] 
                        WHERE LOWER(UserName) IN @UserNames";

                    var usersAll = await db.QueryAsync<SQLCoreUserBase>(sql, new { UserNames = listUserIDs });
                    var userDict = usersAll.ToDictionary(
                                x => x.UserName.ToString().ToLower(),
                                x => (Guid)x.Id
                            );

                    return userDict;
                }
               
            }
            catch (Exception ex) { 
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"❌ Lỗi get user: {ex.Message}\n");
                return new Dictionary<string, Guid>();
            }

        }
    }
}
