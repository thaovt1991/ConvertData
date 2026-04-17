using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelPG;
using ConvertData.Model.ModelSQL;
using ConvertData.WorkerConvert.Helper;
using Dapper;
using NLog;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConvertData.WorkerConvert
{
    public class TagConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;
        private BaseConvert _baseConvert;

        private static Guid DefaultUserId = new("9B621C58-A5F6-F011-8D1E-54BF6477FBB6"); //default User

        public TagConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _parentForm = parentForm;
            _logger = logger;
            _connectModel = connectModel;
            _baseConvert = new BaseConvert(parentForm,logger,connectModel);
        }

        public async Task<bool>ConvertDataTag(ParameterModelTag parametter)
        {
            var listTag = await GetListTagsByEntity(parametter.EntityNames);
            if(listTag?.Count > 0)
            {
                var listSQLTag = new List<SQLTag>();
                var listCreatedBy = listTag.Select(x=>x.CreatedBy).ToList();
                Dictionary<string,Guid>  dicUsers = await _baseConvert.GetInfoUsers(listCreatedBy);
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
                    if(listNew?.Count > 0) listSQLTag.AddRange(listNew);
                }

                if (listSQLTag.Any())
                {
                    //Add - ve nha lam
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

    }
}
