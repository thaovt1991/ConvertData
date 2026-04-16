using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelPG;
using Dapper;
using NLog;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConvertData.WorkerConvert
{
    public class TagConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;

        public TagConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _parentForm = parentForm;
            _logger = logger;
            _connectModel = connectModel;
        }

        public async Task<bool>ConvertDataTag(ParameterModelTag parametter)
        {
            var listTag = await GetListTagsByEntity(parametter.EntityNames);
            if(listTag?.Count > 0)
            {
                foreach (var item in listTag)
                {
                    //1 item sinh car ddorng sqlTag
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
                                    WHERE ""UserID"" = ANY(@listEntity)";
                    
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

    }
}
