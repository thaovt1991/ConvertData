using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelMongo;
using ConvertData.Model.ModelSQL;
using MongoDB.Driver;
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

        private IMongoCollection<BG_TrackLogs> _bgTrackLog_Collection;
        private IMongoCollection<TM_Tasks> _tmTasks_Collection;
        private IMongoCollection<PM_Projects> _pmProject_Collection;
        public HistoryConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _parentForm = parentForm;
            _logger = logger;
            _connectModel = connectModel;

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
                _parentForm.richTextBox1.AppendText($@"Lỗi lấy danh sách của đối tượng {objectID}-{objectType}\n");
            }
            return list;
        }

        private SQLHistory MapHistory(BG_TrackLogs trackLog, Dictionary<string,Guid> dicUsers)
        {
            var createdBy = trackLog.CreatedBy?.ToLower()??"";

            if(string.IsNullOrEmpty(createdBy) ||(!dicUsers.TryGetValue(createdBy,out var cretedCrr) && !dicUsers.TryGetValue($@"pvoil\{createdBy}", out cretedCrr)))
            {
                _parentForm.richTextBox1.AppendText($@"User: {trackLog.CreatedBy} không có trong Core.User =>>>> Next \n");
                return null;
            }

            var sqlHistory = new SQLHistory()
            {
                Id = trackLog.RecID,
                Comment=trackLog.Comment,
                ObjectType = trackLog.ObjectType == "PM_Projects"? WorkObjectType.Project :(trackLog.ObjectType == "TM_Tasks" ? WorkObjectType.Task:WorkObjectType.TaskAssign),
                CreatedBy = cretedCrr,
                Metadata = trackLog.Datas,
            };

            //Kiểu Type của his như ...



            return sqlHistory;
        }
    }
}
