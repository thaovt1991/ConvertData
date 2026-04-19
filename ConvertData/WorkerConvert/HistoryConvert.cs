using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelMongo;
using ConvertData.Model.ModelSQL;
using ConvertData.WorkerConvert.Helper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.WorkerConvert
{
    public class HistoryConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;
        private BaseConvert _baseConvert;

        private IMongoCollection<BG_TrackLogs> _bgTrackLog_Collection;
        private IMongoCollection<TM_Tasks> _tmTasks_Collection;
        private IMongoCollection<PM_Projects> _pmProject_Collection;
        public HistoryConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _parentForm = parentForm;
            _logger = logger;
            _connectModel = connectModel;
            _baseConvert = new BaseConvert(parentForm, logger, connectModel);

            var mongoClient = new MongoClient(connectModel.ConnectionStringMG);
            _bgTrackLog_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<BG_TrackLogs>(typeof(BG_TrackLogs).Name);
            _tmTasks_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<TM_Tasks>(typeof(TM_Tasks).Name);
            _pmProject_Collection = mongoClient.GetDatabase(connectModel.DatabaseNameMG).GetCollection<PM_Projects>(typeof(PM_Projects).Name);

        }


        public async Task<List<BG_TrackLogs>> GetListTrackLogBy(string objectID, string objectType)
        {
            var list = new List<BG_TrackLogs>();
            try
            {
                var filterBuilder = Builders<BG_TrackLogs>.Filter;
                var filter = filterBuilder.Eq(x => x.ObjectID, objectID)
                            & filterBuilder.Eq(x => x.ObjectType, objectType);
                list = await _bgTrackLog_Collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"Lỗi lấy danh sách của đối tượng {objectID}-{objectType} \n");
            }
            return list;
        }

        private SQLHistory MapHistory(BG_TrackLogs trackLog, Dictionary<string,Guid> dicUsers)
        {
            var createdBy = trackLog.CreatedBy?.ToLower()??"";

            if(string.IsNullOrEmpty(createdBy) ||(!dicUsers.TryGetValue(createdBy,out var cretedCrr) && !dicUsers.TryGetValue($@"pvoil\{createdBy}", out cretedCrr)))
            {
                _parentForm.richTextBox1.AppendText($"User: {trackLog.CreatedBy} không có trong Core.User \n");
                return null;
            }
            if(!Guid.TryParse(trackLog.ObjectID,out var objectID))
            {
                return null;
            }

            var sqlHistory = new SQLHistory()
            {
                Id = trackLog.RecID,
                Comment = trackLog.Comment,
                ObjectId = objectID,
                ObjectType = trackLog.ObjectType == "PM_Projects" ? WorkObjectType.Project : (trackLog.ObjectType == "TM_Tasks" ? WorkObjectType.Task : WorkObjectType.TaskAssign),
                CreatedBy = cretedCrr,
                CreatedDate = trackLog.CreatedOn,
                Metadata = trackLog.Datas,
                RelatedUserId = null //nhu..
            };
            //Kiểu Type của his như ...
            return  ConvertType(sqlHistory, trackLog,dicUsers);         
        }

        //Dông bộ his dự án
        public async Task<bool> ConvertToHistorySQL(ParameterModelHistory parameterModel)
        {
            try
            {
                var filterBuilder = Builders<BG_TrackLogs>.Filter;
                var filter = filterBuilder.Eq(x => x.ObjectType, parameterModel.ObjectType)
                               & filterBuilder.Ne(x => x.ConvertStatus, "1");

                var startCreatedDate = parameterModel?.StartCreatedDate;
                var endCreatedDate = parameterModel?.EndCreatedDate;

                if (!string.IsNullOrEmpty(startCreatedDate))
                {
                    if (DateTime.TryParse(startCreatedDate, out DateTime start))
                    {
                       // var startUtc = start.Date.ToUniversalTime();
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
                var serializer = BsonSerializer.SerializerRegistry.GetSerializer<BG_TrackLogs>();
                var a = filter.Render(new RenderArgs<BG_TrackLogs>(serializer, BsonSerializer.SerializerRegistry));

                var listHistory = await _bgTrackLog_Collection.Find(filter)
                                         .Skip((parameterModel.Page - 1) * parameterModel.PageSize)
                                         .Limit(parameterModel.PageSize).ToListAsync();
                
                _parentForm.coutHisProject.Text = (listHistory?.Count ?? 0).ToString();
                _parentForm.countSucHisPro.Text = "0";
                
                if (listHistory?.Count > 0)
                {
                    var i = 0;
                    var countSuccess = 0;
                    IProgress<int> progress = new Progress<int>(value =>
                    {
                        switch (parameterModel.ObjectType)
                        {
                            case "PM_Projects":
                                _parentForm.progressBar3.Value = value; // Cập nhật thanh tiến trình
                                break;
                            case "TM_Tasks":
                                _parentForm.progressHisTask.Value = value;
                                break;
                        }
                       
                    });
                    IProgress<string> countSuccessFunc = new Progress<string>(value =>
                    {
                        switch (parameterModel.ObjectType)
                        {
                            case "PM_Projects":
                                _parentForm.countSucHisPro.Text = value;
                                break;
                            case "TM_Tasks":
                                _parentForm.countSucTaskTag.Text = value;
                                break;
                        }
                        
                    });
                    _parentForm.richTextBox1.AppendText($"Tìm thấy {listHistory.Count} lịch sử dự án {(!string.IsNullOrEmpty(startCreatedDate) ? "từ " + startCreatedDate + " " : "")}{(!string.IsNullOrEmpty(endCreatedDate) ? "đến " + endCreatedDate : "")}\n");
                    var dicUser = await _baseConvert.GetInfoUsers(null, true);//lấy all tolowcase

                    foreach(var item in listHistory)
                    {
                        var success = await AddSQLHistory(item, dicUser);

                        if (success)
                        {
                            var updateFilter = Builders<BG_TrackLogs>.Filter.Eq(x => x.Id, item.Id);
                            var updateDefinition = Builders<BG_TrackLogs>.Update.Set(x => x.ConvertStatus, "1");
                            await _bgTrackLog_Collection.UpdateOneAsync(updateFilter, updateDefinition);
                            countSuccess++;

                            countSuccessFunc.Report(countSuccess.ToString());
                        }

                        i++;
                        int percentComplete = (i * 100) / listHistory.Count;
                        progress.Report(percentComplete);

                    }

                }
                else
                {
                    _parentForm.richTextBox1.AppendText(($"Không có data nào được tìm thấy \n"));
                }

            }
            catch (Exception ex) { 
                _parentForm.richTextBox1.AppendText($@"Lỗi chuyển đổi lịch sử dự án {ex.Message} \n");
                 return false;
            }
            return true;
        }

        public async Task<bool> AddSQLHistory(BG_TrackLogs his,Dictionary<string,Guid> dicUsers)
        {
            try
            {
                var sqlHis = MapHistory(his ,dicUsers);
                if (sqlHis == null) return false;

                string sql = @"
                    DELETE FROM qlcv.Tasks WHERE Id = @Id; 
                    INSERT INTO qlcv.Histories (
                        Id, ObjectType, ObjectId, ActionType, 
                        OldValue, NewValue, RelatedUserId, 
                        Comment, Metadata, CreatedBy, CreatedDate
                    )
                    VALUES (
                        @Id, @ObjectType, @ObjectId, @ActionType, 
                        @OldValue, @NewValue, @RelatedUserId, 
                        @Comment, @Metadata, @CreatedBy, @CreatedDate
                    );";
                using (var db = new SqlConnection(_connectModel.ConnectionStringSQL))
                {
                    await db.OpenAsync();
                  var result  =  await db.ExecuteAsync(sql,sqlHis);
                  return result > 0;
                }

            }
            catch (Exception ex) {
                _logger.Error(ex);
                _parentForm.richTextBox1.AppendText($"lỗi add SQL History : {ex.Message} \n");
                return false;
            } 
        }
    
        //Quy dinh type = 1.Project, 2 task 3.assign task
        private SQLHistory ConvertType(SQLHistory sqlHis, BG_TrackLogs trackLog, Dictionary<string,Guid> dicUsers)
        {
            int objectType = trackLog.ObjectType == "PM_Projects" ? 1 : (trackLog.ObjectType == "TM_Tasks" ? 2 : 3);
            HistoryActionType actionTypeSQL = objectType == 1 ? HistoryActionType.ProjectUpdated : (objectType == 2 ? HistoryActionType.TaskUpdated : HistoryActionType.CommentUpdated);
            var codeMess = trackLog.Text;


            switch (trackLog.ActionType)
            {
            case "N":// new
                    actionTypeSQL= objectType == 1 ? HistoryActionType.ProjectCreated: (objectType == 2 ? HistoryActionType.TaskCreated : HistoryActionType.CommentAdded);
                    break;
            case "E": //edit

                //break;
            case "U": //update
                    actionTypeSQL = objectType == 1 ? HistoryActionType.ProjectUpdated : (objectType == 2 ? HistoryActionType.TaskUpdated : HistoryActionType.CommentUpdated);
                    break;
            case "C": //comment

                break;
            case "C1": // ghi chú

                break;
            case "C2": // chỉnh sửa gì đó (ActionType = ‘C2’ sẽ lấy hình từ BG_TrackLogs.Image không lấy từ valuelist)
                       //Hành động ở đây nhiu
                    switch (objectType)
                    {
                        case 1:
                            break;
                        case 2:
                            sqlHis.ActionType = HistoryActionType.TaskUpdated;
                            return ActionTypeTask(sqlHis ,trackLog,dicUsers);
                            break;
                        case 3:
                            //Như trò dua
                            break;

                    }
                break;
            case "C3": // ghi nhận thay đổi tình trạng chung của hệ thống

                break;
            case "T": // task

                break;
            case "A": // attachment

                break;
            case "S": // share

                break;
            case "D": // delete

                break;
            case "D1": // log xóa file đính kèm

                break;
            case "A0": // hủy yêu cầu xét duyệt

                break;
            case "A1": // khôi phục

                break;
            case "A2": // làm lại       

                break;
            case "A3": // phát hành

                break;
            case "A4": // từ chối

                break;
            case "A5": // duyệt

                break;
            case "RE": // yêu cầu cấp quyền

                break;
            case "V": // ‘V’ (Xử lý + UI giống như template ‘N’) 

                break;
            default:
                break; 
            }
            sqlHis.ActionType = actionTypeSQL;
            return sqlHis;
        }

        public SQLHistory ActionTypeTask(SQLHistory hisSql, BG_TrackLogs trackLog,Dictionary<string,Guid> dicUsers)
        {
            var textvales = JsonConvert.DeserializeObject<List<tmpDataValue>>(trackLog.TextValue ?? "[]");
            switch (trackLog.Text) {
                //Update Status
                case "HISTM001": //20
                case "HISTM002":
                case "HISTM010":                 
                    var status = textvales?.FirstOrDefault(x=>x.FieldName =="0")?.FieldValue;
                    switch (status)
                    {
                        case "3"://20
                            hisSql.ActionType = HistoryActionType.TaskStatusChanged;
                            break;
                        case "2"://30
                            hisSql.ActionType = HistoryActionType.TaskReported;
                            break;
                        case "10"://50
                        case "11": //80
                            hisSql.ActionType = HistoryActionType.TaskCancelled;
                            break;
                        case "1"://90
                            hisSql.ActionType = HistoryActionType.TaskCompleted;
                            break;
                        case "12"://95
                            hisSql.ActionType = HistoryActionType.TaskRejected;
                            break;
                    }
                    break;
                //Duyet
                case "HISTM003"://gui
                    hisSql.ActionType = HistoryActionType.TaskExtensionRequest;
                    break;
                case "HISTM005"://duỵet
                    var aproveExt = textvales?.FirstOrDefault(x => x.FieldName == "0")?.FieldValue;
                    hisSql.ActionType = aproveExt == "8" ?HistoryActionType.TaskExtensionApproved : HistoryActionType.TaskExtensionRejected;
                    break;
                case "HISTM006"://chuyển ngươi thục hiẹn 13
                case "HISTM007"://chuyển duyệt báo cáo 14 -p nghiejp vụ mới méo có chuyen duyejt nên dùng update
                    //13
                    hisSql.ActionType = trackLog.Text == "HISTM006" ? HistoryActionType.TaskReassigned :HistoryActionType.TaskUpdated;
                    var userChangesOld = textvales?.FirstOrDefault(x => x.FieldName == "3")?.FieldValue;
                    var userChanges= textvales?.FirstOrDefault(x => x.FieldName == "4")?.FieldValue;
                    if (!string.IsNullOrEmpty(userChanges))
                    {
                        userChanges = userChanges.ToLower();
                        if (dicUsers.TryGetValue(userChanges, out var userID) || dicUsers.TryGetValue($@"pvoil\{userChanges}", out userID)) {
                            hisSql.RelatedUserId = userID;
                            hisSql.NewValue = userID.ToString();
                        }
                        else
                        {
                            _parentForm.richTextBox1.AppendText($@"User {userChanges} không có trong cơ sở dữ liệu mới ! \n");
                        }
                    }
                    if (!string.IsNullOrEmpty(userChangesOld))
                    {
                        userChanges = userChangesOld.ToLower();
                        if (dicUsers.TryGetValue(userChangesOld, out var userIDOld) || dicUsers.TryGetValue($@"pvoil\{userChangesOld}", out userIDOld))
                        {
                            hisSql.OldValue = userIDOld.ToString();
                        }
                        else
                        {
                            _parentForm.richTextBox1.AppendText($@"User {userChangesOld} không có trong cơ sở dữ liệu mới ! \n");
                        }
                    }
                    break;
                case "HISTM008": //giao viec lại cho ai đó
                case "HISTM009": //Giao viec thay
                    hisSql.ActionType = HistoryActionType.TaskAssigned;
                    var userChanges2 = textvales?.FirstOrDefault(x => x.FieldName == "4")?.FieldValue;
                    if (!string.IsNullOrEmpty(userChanges2))
                    {
                        userChanges2 = userChanges2.ToLower();
                        if (dicUsers.TryGetValue(userChanges2, out var userID2) || dicUsers.TryGetValue($@"pvoil\{userChanges2}", out userID2))
                        {
                            hisSql.RelatedUserId = userID2;
                        }
                        else
                        {
                            _parentForm.richTextBox1.AppendText($@"User {userChanges2} không có trong cơ sở dữ liệu mới ! \n");
                        }
                    }
                    break;
                    //
            }

            return hisSql;
        }
    }
}
