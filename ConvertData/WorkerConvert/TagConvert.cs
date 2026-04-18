using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelMongo;
using ConvertData.Model.ModelPG;
using ConvertData.Model.ModelSQL;
using ConvertData.WorkerConvert.Helper;
using Dapper;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using NLog;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Data.SqlClient.Internal.SqlClientEventSource;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConvertData.WorkerConvert
{
    public class TagConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;
        private BaseConvert _baseConvert;
        private IMongoCollection<TM_Tasks> _tmTasks_Collection;
        private IMongoCollection<PM_Projects> _pmProject_Collection;

        private static Guid DefaultUserId = new("9B621C58-A5F6-F011-8D1E-54BF6477FBB6"); //default User

        public TagConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _parentForm = parentForm;
            _logger = logger;
            _connectModel = connectModel;
            _baseConvert = new BaseConvert(parentForm,logger,connectModel);

            var mongoClient = new MongoClient(connectModel.ConnectionStringMG);
            _tmTasks_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<TM_Tasks>(typeof(TM_Tasks).Name);
            _pmProject_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<PM_Projects>(typeof(PM_Projects).Name);
        }

        public async Task<bool>ConvertDataTag(ParameterModelTag parametter)
        {
            var listTag = await GetListTagsByEntity(parametter.EntityNames);
            if(listTag?.Count > 0)
            {
                _parentForm.countTag.Text = listTag.Count.ToString();

                var listCreatedBy = listTag.Select(x=>x.CreatedBy).Distinct().ToList();
                Dictionary<string,Guid>  dicUsers = await _baseConvert.GetInfoUsers(listCreatedBy);

                var i = 0;
                var countSuccess = 0;
                
                IProgress<int> progress = new Progress<int>(value =>
                {
                    _parentForm.prgStatusTag.Value = value;
                });
                IProgress<string> countSuccessFunc = new Progress<string>(value =>
                {
                    _parentForm.convertSucTag.Text = value;
                });
                foreach (var item in listTag)
                {
                    var cr = item.CreatedBy?.ToLower() ?? "";
                    var crBy = DefaultUserId;
                    if (dicUsers.TryGetValue(cr, out var createdBY) &&
                        dicUsers.TryGetValue($@"pvoil\{cr}", out createdBY))
                    {
                        crBy = createdBY;
                    }

                    //1 item sinh car ddorng sqlTag
                    var listNew = CreateSQLTag(item,crBy);
                    if(listNew?.Count > 0)
                    {
                     var success = await UpsertTags(listNew);
                        if (success)
                        {
                            countSuccess++;
                            countSuccessFunc.Report(countSuccess.ToString());
                        }
                    }

                    i++;
                    int percentComplete = (i * 100) / listTag.Count;
                    progress.Report(percentComplete);
                }
            }
            return true;
        }

        public async Task<List<BS_Tags>> GetListTagsByEntity(List<string> listEntity)
        {
            var list = new List<BS_Tags>();
            try
            {
                using (var db = new NpgsqlConnection(_connectModel.ConnectionStringPG))
                {
                    await db.OpenAsync();

                    string sql = @"SELECT * FROM public.""BS_Tags""
                                    WHERE ""EntityName"" = ANY(@listEntity)";
                    
                    return (await db.QueryAsync<BS_Tags>(sql, new { listEntity })).ToList();                  
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return list;
            }
            return list;
        }

        public List<SQLTag> CreateSQLTag(BS_Tags tag, Guid createdBy)
        {
            var listTag = new List<SQLTag>();
            if(string.IsNullOrEmpty(tag.Value)) return listTag;
            var valueArr =  tag.Value.Split(";");
            var iconArr = tag.Icon.Split(";");
            var colorArr = tag.Color.Split(";");

            var idx = 0;
            var type = tag.EntityName == "PM_Projects" ? TagType.Project : TagType.Task;
            foreach (var item in valueArr) {
      
                var name = valueArr[idx]?.Trim();
                if (string.IsNullOrEmpty(name)) continue;
                var rawIcon = (idx < iconArr.Length) ? iconArr[idx] : null;
                var rawColor = (idx < colorArr.Length) ? colorArr[idx] : null;

                listTag.Add(new SQLTag()
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Name = name,
                    Slug = name.ToLower(),
                    Icon = rawIcon?.Trim() ?? "",
                    HexColor = rawColor?.Trim() ?? "",
                    CreatedBy = createdBy,
                    CreatedDate = tag.CreatedOn
                });
            }

            return listTag;
        }

        public async Task<bool> UpsertTags(List<SQLTag> tags)
        {
            try
            {
                string sql = @"
                MERGE qlcv.Tags AS target
                USING (
                    SELECT 
                            @Id AS Id, 
                            @Type AS Type, 
                            @Name AS Name, 
                            @Slug AS Slug, 
                            @Icon AS Icon, 
                            @HexColor AS HexColor, 
                            @CreatedBy AS CreatedBy, 
                            @CreatedDate AS CreatedDate
                    ) AS source
                ON (target.Name = source.Name AND target.Type = source.Type)
                WHEN MATCHED THEN
                    UPDATE SET 
                        target.Slug = source.Slug,
                        target.Icon = source.Icon,
                        target.HexColor = source.HexColor
                WHEN NOT MATCHED THEN
                    INSERT (Id, Type, Name, Slug, Icon, HexColor, CreatedBy, CreatedDate)
                    VALUES (
                        source.Id, 
                        source.Type, 
                        source.Name, 
                        source.Slug, 
                        source.Icon, 
                        source.HexColor, 
                        source.CreatedBy, 
                        source.CreatedDate
                    );";
                using (var conn = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    await conn.OpenAsync();
                    await conn.ExecuteAsync(sql, tags);
                }
                return true;
            }
            catch (Exception ex) {
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($@"Insert QLCV tags loi : {ex.Message}");
                return false;
            }
            
        }

        //____________________TASK_TAG___________________________
        public async Task<bool> ConvertTagsTasks(ParameterModelTag parameterModel)
        {
            try
            {
                var filterBuilder = Builders<TM_Tasks>.Filter;

                var filter = filterBuilder.And(
                            filterBuilder.Ne(x => x.ImportStatus, 3),
                            filterBuilder.Ne(x => x.IsTemplate, "1"),
                            filterBuilder.Ne(x => x.Category, "PBL"),
                            filterBuilder.Ne(x => x.Tags, ""),
                            filterBuilder.Ne(x => x.Tags, null)
                        );

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
                                         .ToListAsync();

                _parentForm.countTT.Text = (listTasks?.Count ?? 0).ToString();
                _parentForm.countSucTT.Text = "0";

                if (listTasks?.Count > 0)
                {
                    _parentForm.richTextBox1.AppendText($"Tìm thấy {listTasks.Count} công việc {(!string.IsNullOrEmpty(startCreatedDate) ? "từ " + startCreatedDate + " " : "")}{(!string.IsNullOrEmpty(endCreatedDate) ? "đến " + endCreatedDate : "")}\n");
                    var listUser = listTasks.Select(x=>x.CreatedBy).Distinct().ToList();

                    var dicUsers = await _baseConvert.GetInfoUsers(listUser);

                    var i = 0;
                    var countSuccess = 0;
                    IProgress<int> progress = new Progress<int>(value =>
                    {
                        _parentForm.progressBarTaskTag.Value = value; // Cập nhật thanh tiến trình
                    });
                    IProgress<string> countSuccessFunc = new Progress<string>(value =>
                    {
                        _parentForm.countSucTT.Text = value;
                    });

                    foreach (var item in listTasks)
                    {
                        var createdBy = item.CreatedBy?.ToLower() ?? "";
                        if (!dicUsers.TryGetValue(createdBy, out var createdById) &&
                           !dicUsers.TryGetValue($@"pvoil\{createdBy}", out createdById))
                        {
                            createdById = DefaultUserId;
                        }

                        //Chuyen dataa
                        var isSuccces = await AddTaskTag(item,createdById,true);
                        //end chuyeern
                        //UPDATE NGAY
                        if (isSuccces)
                        {
                            var updateFilter = Builders<TM_Tasks>.Filter.Eq(x => x.Id, item.Id);
                            var updateDefinition = Builders<TM_Tasks>.Update.Set(x => x.ImportStatus, 3);
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

        public async Task<bool> AddTaskTag(TM_Tasks task,Guid createdBy, bool createdNew = false)
        {
            try
            {
                var tags = task.Tags.Split(";",StringSplitOptions.RemoveEmptyEntries).ToList();
                var listSQLTag = await GetTagByNames(tags, TagType.Task, createdBy ,createdNew);
                if (listSQLTag?.Count > 0)
                {
                    var list = new List<SQLTagTask>();
                    foreach (var tagName in tags)
                    {
                        var tag = listSQLTag?.FirstOrDefault(x => x.Name == tagName);
                        if (tag == null) continue;
                        var tagTask = new SQLTagTask()
                        {
                            Id = Guid.NewGuid(),
                            TaskId = task.RecID,
                            TagId = tag.Id,
                            CreatedDate = task.CreatedOn,
                            CreatedBy = createdBy,
                            ActiveFlag =0
                        };

                        list.Add(tagTask);
                    }
                    if (list?.Count > 0)
                    {
                        var sql = @"
                             DELETE FROM qlcv.TaskTags WHERE TaskId = @TaskId;   --//Xóa cũ 
                             INSERT INTO qlcv.TaskTags ( 
                               [Id]
                              ,[TaskId]
                              ,[TagId]
                              ,[ActiveFlag]
                              ,[CreatedBy]
                              ,[UpdatedBy]
                              ,[CreatedDate]
                              ,[UpdatedDate]
                             )
                             VALUES (
                            @Id,@TaskId,@TagId,@ActiveFlag
                            ,@CreatedBy,@UpdatedBy,@CreatedDate,@UpdatedDate
                            )
                            ";
                        using(var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                        {
                            await db.OpenAsync();
                            await db.ExecuteAsync(sql,list);
                        }
                    }

                    return true;
                }
               
            } catch (Exception ex) { 
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"Lỗi ADD TASK TAG : { ex.Message}\n");
            }
            return false;
        }

        private async Task<List<SQLTag>> GetTagByNames(List<string> tagName ,TagType type,Guid createdBy ,bool createdNew = false)
        {
            var list = await GetTagsByNamesAndType(tagName,type);
            if(!createdNew) return list;
            var listExit = list.Select(x=>x.Name).ToList();

            var listNameNew = tagName.Where(x=> !listExit.Contains(x)).ToList();
            if (listNameNew?.Count > 0) {
                var listTagAdd = new List<SQLTag>();
                foreach (var name in listNameNew) {
                    listTagAdd.Add(new SQLTag()
                    {
                        Id = Guid.NewGuid(),
                        Type = type,
                        Name = name,
                        Slug = name.ToLower(),
                        ActiveFlag = 0,
                        HexColor = null,
                        Icon = null,
                        CreatedDate = DateTime.Now,
                        CreatedBy = createdBy
                    });
                }
                if (listTagAdd?.Count > 0)
                {
                    var result = await UpsertTags(listTagAdd);
                    if (result) list.AddRange(listTagAdd);
                }
            }

            return list;
        }

        public async Task<List<SQLTag>> GetTagsByNamesAndType(List<string> tagNames, TagType type)
        {
            if (tagNames == null || !tagNames.Any()) return new List<SQLTag>();
            using (var conn = new SqlConnection(_connectModel.ConnectionStringSQL))
            {
                string sql = @"SELECT * FROM qlcv.Tags 
                       WHERE Type = @Type AND Name IN @tagNames";

                var result = await conn.QueryAsync<SQLTag>(sql, new { Type=type, tagNames });
                return result.ToList();
            }
        }
        public async Task<List<SQLTag>> GetExistingTags(List<SQLTag> searchList)
        {
            if (searchList == null || !searchList.Any()) return new List<SQLTag>();

            using (var conn = new SqlConnection(_connectModel.ConnectionStringSQL))
            {
                var valueRows = new List<string>();
                var parameters = new DynamicParameters();

                for (int i = 0; i < searchList.Count; i++)
                {
                    valueRows.Add($"(@Name{i}, @Slug{i})");
                    parameters.Add($"Name{i}", searchList[i].Name);
                    parameters.Add($"Slug{i}", searchList[i].Slug);
                }

                 string sql = $@"
                SELECT t.* FROM qlcv.Tags t
                INNER JOIN (VALUES {string.Join(", ", valueRows)}) AS FilterTable(Name, Slug)
                    ON t.Name = FilterTable.Name AND t.Slug = FilterTable.Slug";

                var result = await conn.QueryAsync<SQLTag>(sql, parameters);
                return result.ToList();
            }
        }

        //----------------------PROJETC-TAG-------------------//
        public async Task<bool> ConvertTagsProject(ParameterModelTag parameterModel)
        {
            try
            {
                var filterBuilder = Builders<PM_Projects>.Filter;

                var filter = filterBuilder.And(
                            filterBuilder.Ne(x => x.ConvertStatus, 3),
                            filterBuilder.Ne(x => x.IsTemplate, "1"),
                            filterBuilder.Ne(x => x.Tags, ""),
                            filterBuilder.Ne(x => x.Tags, null)
                        );

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
                                         .ToListAsync();

                _parentForm.label27.Text = (listProjects?.Count ?? 0).ToString();
                _parentForm.label26.Text = "0";

                if (listProjects?.Count > 0)
                {
                    _parentForm.richTextBox1.AppendText($"Tìm thấy {listProjects.Count} dự án có TAG {(!string.IsNullOrEmpty(startCreatedDate) ? "từ " + startCreatedDate + " " : "")}{(!string.IsNullOrEmpty(endCreatedDate) ? "đến " + endCreatedDate : "")}\n");
                    var listUser = listProjects.Select(x => x.CreatedBy).Distinct().ToList();

                    var dicUsers = await _baseConvert.GetInfoUsers(listUser);

                    var i = 0;
                    var countSuccess = 0;
                    IProgress<int> progress = new Progress<int>(value =>
                    {
                        _parentForm.progressBar1.Value = value; // Cập nhật thanh tiến trình
                    });
                    IProgress<string> countSuccessFunc = new Progress<string>(value =>
                    {
                        _parentForm.label26.Text = value;
                    });

                    foreach (var item in listProjects)
                    {
                        var createdBy = item.CreatedBy?.ToLower() ?? "";
                        if (!dicUsers.TryGetValue(createdBy, out var createdById) &&
                           !dicUsers.TryGetValue($@"pvoil\{createdBy}", out createdById))
                        {
                            createdById = DefaultUserId;
                        }

                        //Chuyen dataa
                        var isSuccces = await AddProjectTag(item, createdById, true);
                        //end chuyeern
                        //UPDATE NGAY
                        if (isSuccces)
                        {
                            var updateFilter = Builders<PM_Projects>.Filter.Eq(x => x.Id, item.Id);
                            var updateDefinition = Builders<PM_Projects>.Update.Set(x => x.ConvertStatus, 3);
                            await _pmProject_Collection.UpdateOneAsync(updateFilter, updateDefinition);
                            countSuccess++;

                            countSuccessFunc.Report(countSuccess.ToString());
                        }

                        i++;
                        int percentComplete = (i * 100) / listProjects.Count;
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

        public async Task<bool> AddProjectTag(PM_Projects pro, Guid createdBy, bool createdNew = false)
        {
            try
            {
                var tags = pro.Tags.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList();
                var listSQLTag = await GetTagByNames(tags, TagType.Project, createdBy, createdNew);
                if (listSQLTag?.Count > 0)
                {
                    var list = new List<SQLTagProject>();
                    foreach (var tagName in tags)
                    {
                        var tag = listSQLTag?.FirstOrDefault(x => x.Name == tagName);
                        if (tag == null) continue;
                        var tagPro = new SQLTagProject()
                        {
                            Id = Guid.NewGuid(),
                            ProjectId = pro.RecID,
                            TagId = tag.Id,
                            CreatedDate = pro.CreatedOn,
                            CreatedBy = createdBy,
                            ActiveFlag = 0
                        };

                        list.Add(tagPro);
                    }
                    if (list?.Count > 0)
                    {
                        var sql = @"
                             DELETE FROM qlcv.ProjectTags WHERE ProjectId = @ProjectId;   --//Xóa cũ 
                             INSERT INTO qlcv.ProjectTags ( 
                               [Id]
                              ,[ProjectId]
                              ,[TagId]
                              ,[ActiveFlag]
                              ,[CreatedBy]
                              ,[UpdatedBy]
                              ,[CreatedDate]
                              ,[UpdatedDate]
                             )
                             VALUES (
                            @Id,@ProjectId,@TagId,@ActiveFlag
                            ,@CreatedBy,@UpdatedBy,@CreatedDate,@UpdatedDate
                            )
                            ";
                        using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                        {
                            await db.OpenAsync();
                            await db.ExecuteAsync(sql, list);
                        }
                    }

                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"Lỗi ADD TASK TAG : {ex.Message}\n");
            }
            return false;
        }
    }

}
