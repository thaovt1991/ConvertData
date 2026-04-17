using ConverData;
using ConvertData.Model;
using ConvertData.Model.ModelSQL;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Dapper;

namespace ConvertData.WorkerConvert.Helper
{
    public class BaseConvert
    {
        private Form1 _parentForm;
        private Logger _logger;
        private ConnectionModel _connectModel;

        private static Guid DefaultUserId = new("9B621C58-A5F6-F011-8D1E-54BF6477FBB6"); //default User

        public BaseConvert(Form1 parentForm, Logger logger, ConnectionModel connectModel)
        {
            _parentForm = parentForm;
            _logger = logger;
            _connectModel = connectModel;
        }

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
    }
}
