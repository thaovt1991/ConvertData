using ConvertData.Model;
using ConvertData.WorkerConvert;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Core.Servers;
using NLog;
using System.Data;
using Npgsql;

namespace ConverData
{
    public partial class Form1 : Form
    {
        //SQL 
        private string _connectStringSQL = "";
        private SqlConnection? _sqlConnection; // Biến dùng chung cho SQL
        private bool isConnectedSql = false;    // Trạng thái kết nối SQL
        //Mogo
        private string _connectStringMG = "";
        private IMongoClient _client; // Biến dùng chung cho cả Form
        private string _databaseName = "";
        private bool isConnectedSourceMG = false;
        //Porgress
        private string _connectStringPG = "";
        private bool isConnectedSourcePG = false;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            numericPage.Value = 1;
            numericPage.Minimum = 1;
            //numericPage.Maximum = 1000;   // Tối đa 1000 dòng để tránh treo ram


            numericPageSize.Minimum = 1;
            numericPageSize.Maximum = 20000;   // Tối đa 20000 dòng để tránh treo ram
            numericPageSize.Value = 1; // 1000;  //t

            //Thiết lập hard code test
            //connectStringSQL.Text = _connectStringSQL = "Server=172.16.12.230\\MSSQLSERVER2025;Database=LV.Shell;User Id=sa;Password=Lv@123456";
            connectStringSQL.Text = _connectStringSQL = "Server=172.16.12.230\\MSSQLSERVER2025;Database=PVOIL.Shell;User Id=sa;Password=Lv@123456;MultipleActiveResultSets=true;Encrypt=False";
            connectStringMG.Text = _connectStringMG = "mongodb://admin:Lacviet%23123@172.16.7.34:37017"; //"mongodb://admin:Erm%402021@172.16.7.33:27017";
            connectionStringPG.Text = _connectStringPG = "Host=172.16.7.34;Port=5432;Database=developer;Username=postgres;Password=Erm@2021;Pooling=true;Minimum Pool Size=1;Maximum Pool Size=100;";
            databaseName.Text = _databaseName = "pvoil_Data";

            //Date picker
            // Đối với DateTimePicker bắt đầu
            //dateTimePicker1.Format = DateTimePickerFormat.Custom;
            //dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            //// Đối với DateTimePicker kết thúc
            //dateTimePicker2.Format = DateTimePickerFormat.Custom;
            //dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            //tASK
            numericPageTM.Value = 1;
            numericPageTM.Minimum = 1;
            //numericPageTM.Maximum = 1000;   // Tối đa 1000 dòng để tránh treo ram
            numericPageSizeTM.Minimum = 1;
            numericPageSizeTM.Maximum = 20000;   // Tối đa 20000 dòng để tránh treo ram
            numericPageSizeTM.Value = 1; // 1000;  //t
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //kiểm tra có // chuyển thành 1 /
            var connectionString = connectStringSQL.Text;
            if (connectionString.Contains(@"\\"))
            {
                connectionString = connectionString.Replace(@"\\", @"\");
            }
            this._connectStringSQL = connectionString;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this._connectStringMG = connectStringMG.Text;
        }



        private async void button4_Click(object sender, EventArgs e)
        {
            if (!isConnectedSourceMG)
            {
                await TestMongoConnection();

            }
            else
            {
                // --- LOGIC NGẮT KẾT NỐI ---
                _client = null; // Giải phóng object client MongoDB

                isConnectedSourceMG = false;
                bttConnectMG.Text = "Kiểm tra kết nối";
                bttConnectMG.BackColor = SystemColors.Control;

                connectStringMG.Enabled = true; // Cho phép nhập lại
                lblConnectMG.Text = "X"; // Mất tích xanh
                lblConnectMG.ForeColor = Color.Red;

                richTextBox1.AppendText($"{DateTime.Now}: Đã ngắt kết nối nguồn.\n");
            }

        }

        private async Task TestMongoConnection(string connectionString = null)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString)) connectionString = this._connectStringMG;
                // 1. Khởi tạo Client với connection string của bạn
                _client = new MongoClient(connectionString);

                using (var cts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(5)))
                {
                    var dbList = await _client.ListDatabaseNamesAsync(cts.Token);
                    var listDatabase = await dbList.ToListAsync();

                    // MessageBox.Show("Kết nối MongoDB thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // --- LOGIC KẾT NỐI ---
                    isConnectedSourceMG = true;
                    bttConnectMG.Text = "Chỉnh sửa";
                    bttConnectMG.BackColor = Color.LightCoral;

                    connectStringMG.Enabled = false;

                    lblConnectMG.Text = "✔";
                    lblConnectMG.ForeColor = Color.Blue;

                    richTextBox1.AppendText($"{DateTime.Now}: Đã kết nối MongoDB thành công.\n");
                    _client = null; //chỉ là kiểm tra kết nối thôi nên conect xong tắt

                    //Kiểm tra databasename
                    if (!string.IsNullOrEmpty(_databaseName) && listDatabase.Contains(_databaseName))
                    {
                        richTextBox1.AppendText($"Đã thấy database name {_databaseName}.\n");
                        databaseName.Enabled = false;
                    }
                    else richTextBox1.AppendText($"Không tìm thấy database name {_databaseName}.\n");
                    ;
                }

            }
            catch (MongoConfigurationException ex)
            {
                richTextBox1.AppendText($"Chuỗi kết nối không đúng định dạng:: {ex.Message}\n");
                //MessageBox.Show("Chuỗi kết nối không đúng định dạng: " + ex.Message);
            }
            catch (TimeoutException)
            {
                richTextBox1.AppendText("Kết nối thất bại: Quá thời gian chờ (Timeout). Hãy kiểm tra IP và Port.");
                //MessageBox.Show("Kết nối thất bại: Quá thời gian chờ (Timeout). Hãy kiểm tra IP và Port.");
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("Lỗi kết nối: " + ex.Message);
                //MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }

        }

        private async Task GetDataCollection()
        {
            if (_client == null)
            {
                MessageBox.Show("Vui lòng kết nối trước!");
                return;
            }

            try
            {
                var database = _client.GetDatabase(_databaseName);
                var collection = database.GetCollection<BsonDocument>("TM_Tasks");
                //var options = new FindOptions<BsonDocument>
                //{
                //    BatchSize = 1000 // Cấu hình mỗi lần lấy 1000 dòng từ server
                //};
                using (var cursor = await collection.Find(new BsonDocument()).ToCursorAsync())
                {
                    while (await cursor.MoveNextAsync()) // Đọc từng lô (batch)
                    {
                        foreach (var doc in cursor.Current)
                        {
                            // Bước này bạn sẽ gọi hàm Insert sang SQL Server
                            await InsertToSqlServer(doc);
                        }
                    }
                }
                //// 3. Truy vấn lấy Top 100 bản ghi
                //var filter = new BsonDocument();
                //var documents = await collection.Find(filter).Limit(100).ToListAsync();

                //// 4. Hiển thị lên Log hoặc DataGridView
                //foreach (var doc in documents)
                //{

                //    richTextBox1.AppendText($"ID: {doc["_id"]}, Subject: {doc.GetValue("Subject", "N/A")}\n");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc dữ liệu: " + ex.Message);
            }
        }

        private async Task InsertToSqlServer(BsonDocument data)
        {

        }

        //SQL connect
        private async void btnConnectSql_Click(object sender, EventArgs e)
        {
            // Nếu đang kết nối -> Thực hiện Ngắt
            if (isConnectedSql)
            {
                if (_sqlConnection != null) _sqlConnection.Close();
                isConnectedSql = false;

                connectStringSQL.Enabled = true;
                lblConnectSQL.Text = "";           // Mất tích xanh
                bttConnectSQL.Text = "Kiểm tra kết nối";
                bttConnectSQL.BackColor = SystemColors.Control;

                richTextBox1.AppendText("🔌 Đã ngắt kết nối SQL Server.\n");
                return;
            }

            // Nếu chưa kết nối -> Thực hiện Kết nối
            string connStr = _connectStringSQL.Trim(); // Giả sử ô nhập tên là txtSqlConn
            richTextBox1.AppendText(" đang kiểm tra kết nối SQL Server...\n");

            bool success = await IsSqlConnected(connStr);

            if (success)
            {
                isConnectedSql = true;
                connectStringSQL.Enabled = false;      // Khóa ô nhập liệu
                bttConnectSQL.Text = "Chỉnh sửa";
                bttConnectSQL.BackColor = Color.LightCoral;

                lblConnectSQL.Text = "✔"; // Hiện tích xanh
                lblConnectSQL.ForeColor = Color.Blue;

                richTextBox1.AppendText("✅ Kết nối SQL Server thành công!\n");
                bttConnectSQL.Enabled = true;
                _sqlConnection.Close(); //ngat sau khi test ok
            }
            else
            {
                MessageBox.Show("Không thể kết nối đến SQL Server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private async Task<bool> IsSqlConnected(string connectionString)
        {
            try
            {
                _sqlConnection = new SqlConnection(connectionString);
                await _sqlConnection.OpenAsync();
                // Nếu mở được mà không lỗi thì trả về true
                return _sqlConnection.State == ConnectionState.Open;
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText($"❌ Lỗi SQL: {ex.Message}\n");
                richTextBox1.AppendText($"ConnectString SQL: {connectStringSQL}\n");
                return false;
            }
        }

        //private async Task BulkInsertToSql(DataTable dataTable)
        //{
        //    string sqlConnString = "Server=172.16.7.34;Database=CoDX;User Id=sa;Password=Erm@2021;Connection Timeout=10;Pooling=true;Max Pool Size=100;Encrypt=False;TrustServerCertificate=True;App=sql";

        //    using (SqlConnection connection = new SqlConnection(sqlConnString))
        //    {
        //        await connection.OpenAsync();
        //        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
        //        {
        //            // Tên bảng đích trong SQL Server
        //            bulkCopy.DestinationTableName = "Ten_Bang_Cua_Ban";

        //            try
        //            {
        //                // Ánh xạ cột: Cột ở DataTable -> Cột ở SQL Server (nếu tên khác nhau)
        //                // bulkCopy.ColumnMappings.Add("SourceCol", "DestCol");

        //                await bulkCopy.WriteToServerAsync(dataTable);
        //            }
        //            catch (Exception ex)
        //            {
        //                richTextBox1.AppendText($"Lỗi Bulk Insert: {ex.Message}\n");
        //            }
        //        }
        //    }
        //}

        private async void bttConvertTask_ClickAsync(object sender, EventArgs e)
        {
            bttConvertTask.Enabled = false;

            prgStatusTask.Value = 0;

            var connectModel = new ConnectionModel()
            {
                ConnectionStringPG = _connectStringPG,
                ConnectionStringMG = this._connectStringMG,
                ConnectionStringSQL = this._connectStringSQL,
                DatabaseNameMG = _databaseName ?? "developer_Data" //tesst
            };
            var parameterModel = new ParameterModelTask()
            {
                Page = !string.IsNullOrEmpty(numericPageTM.Text) ? Int32.Parse(numericPageTM.Text) : 1,
                PageSize = !string.IsNullOrEmpty(numericPageSizeTM.Text) ? Int32.Parse(numericPageSizeTM.Text) : 100,
                StartCreatedDate = dateTimePicker3.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                EndCreatedDate = dateTimePicker4.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                IsUpdateFull = true,
                IsUpdateTask = true,
                IsUpdateTaskRes = true
            };

            var coverter = await new TaskConvert(this, _logger, connectModel).ConvertDataTasks(parameterModel);

            //Test

            bttConvertTask.Enabled = true;
            richTextBox1.AppendText("Đồng bộ công việc hoàn tất! \n");
            //MessageBox.Show("Đồng bộ hoàn tất!");
        }

        private async Task StartSyncData(IProgress<int> progress)
        {
            // Giả sử bạn có 500 bản ghi cần chuyển
            int totalRecords = 500;

            for (int i = 1; i <= totalRecords; i++)
            {
                // Giả lập công việc chuyển dữ liệu (Ví dụ: Insert 1 dòng vào SQL)
                await Task.Delay(10); // Thay bằng code Insert thực tế của bạn

                // Tính % và báo cáo về giao diện
                int percentComplete = (i * 100) / totalRecords;
                progress.Report(percentComplete);
            }
        }
        // =========================DỰ ÁN====================
        #region Dự án
        private async void ConverterProjectClick(object sender, EventArgs e)
        {
            bttConvertProject.Enabled = false;
            //Xử lý nghiệp vụ đồng bộ
            //var progress = new Progress<int>(value =>
            //{
            //    prgStatusTask.Value = value; // Cập nhật thanh tiến trình
            //});
            prgStatusProject.Value = 0;

            var connectModel = new ConnectionModel()
            {
                ConnectionStringMG = this._connectStringMG,
                ConnectionStringSQL = this._connectStringSQL,
                DatabaseNameMG = _databaseName ?? "developer_Data" //tesst
            };
            var parameterModel = new ParameterModel()
            {
                Page = !string.IsNullOrEmpty(numericPage.Text) ? Int32.Parse(numericPage.Text) : 1,
                PageSize = !string.IsNullOrEmpty(numericPageSize.Text) ? Int32.Parse(numericPageSize.Text) : 100,
                StartCreatedDate = dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                EndCreatedDate = dateTimePicker2.Value.ToString("dd/MM/yyyy HH:mm:ss")
            };

            var coverter = await new ProjectConvert(this, _logger, connectModel).ConvertDataProject(parameterModel);

            //Test

            bttConvertProject.Enabled = true;
            richTextBox1.AppendText("Đồng bộ dự án hoàn tất! \n");

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //// Chỉ cho phép nhập chữ số và phím xóa (Control)
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true; // Chặn ký tự này lại
            //}
        }
        #endregion

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            _databaseName = databaseName.Text;
        }

        private async void bttConnectPG_Click(object sender, EventArgs e)
        {
            // Nếu đang kết nối -> Thực hiện Ngắt
            if (isConnectedSourcePG)
            {
                isConnectedSourcePG = false;
                connectionStringPG.Enabled = true;
                lblConnectPG.Text = "";           // Mất tích xanh
                bttConnectPG.Text = "Kiểm tra kết nối";
                bttConnectPG.BackColor = SystemColors.Control;

                richTextBox1.AppendText("🔌 Đã ngắt kết nối PostgreSQL Server.\n");
                return;
            }

            // Nếu chưa kết nối -> Thực hiện Kết nối
            string connStr = _connectStringPG.Trim();
            richTextBox1.AppendText(" đang kiểm tra kết nối PostgreSQL Server...\n");

            bool success = await IsPGSqlConnected(connStr);

            if (success)
            {
                isConnectedSourcePG = true;
                connectionStringPG.Enabled = false;      // Khóa ô nhập liệu
                bttConnectPG.Text = "Chỉnh sửa";
                bttConnectPG.BackColor = Color.LightCoral;

                lblConnectPG.Text = "✔"; // Hiện tích xanh
                lblConnectPG.ForeColor = Color.Blue;

                richTextBox1.AppendText("✅ Kết nối PostgreSQL thành công!\n");
                bttConnectPG.Enabled = true;
            }
            else
            {
                MessageBox.Show("Không thể kết nối đến PostgreSQL Server!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<bool> IsPGSqlConnected(string connectionString)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                await conn.OpenAsync();
                return true;
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText($"❌ Lỗi PostgreSQL: {ex.Message}\n");
                richTextBox1.AppendText($"ConnectString PostgreSQL: {connectStringSQL}\n");
                return false;
            }
        }

        private void connectionStringPG_TextChanged(object sender, EventArgs e)
        {
            this._connectStringPG = connectionStringPG.Text;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            convertTag.Enabled = false;

            prgStatusTask.Value = 0;

            var connectModel = new ConnectionModel()
            {
                ConnectionStringPG = _connectStringPG,
                ConnectionStringMG = this._connectStringMG,
                ConnectionStringSQL = this._connectStringSQL,
                DatabaseNameMG = _databaseName ?? "developer_Data" //tesst
            };
            var parameterModel = new ParameterModelTag()
            {
                //Page = !string.IsNullOrEmpty(numericPageTM.Text) ? Int32.Parse(numericPageTM.Text) : 1,
                //PageSize = !string.IsNullOrEmpty(numericPageSizeTM.Text) ? Int32.Parse(numericPageSizeTM.Text) : 100,
                //StartCreatedDate = dateTimePicker3.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                //EndCreatedDate = dateTimePicker4.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                EntityNames = new List<string>() { "TM_Tasks", "PM_Project" }
            };

            var coverter = await new TagConvert(this, _logger, connectModel).ConvertDataTag(parameterModel);

            //Test

            convertTag.Enabled = true;
            richTextBox1.AppendText("Đồng bộ Tag hoàn tất! \n");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            convertTaskTag.Enabled = false;

            progressBarTaskTag.Value = 0;

            var connectModel = new ConnectionModel()
            {
                ConnectionStringPG = _connectStringPG,
                ConnectionStringMG = this._connectStringMG,
                ConnectionStringSQL = this._connectStringSQL,
                DatabaseNameMG = _databaseName ?? "developer_Data" //tesst
            };
            var parameterModel = new ParameterModelTag()
            {
                //Page = !string.IsNullOrEmpty(numericPageTM.Text) ? Int32.Parse(numericPageTM.Text) : 1,
                //PageSize = !string.IsNullOrEmpty(numericPageSizeTM.Text) ? Int32.Parse(numericPageSizeTM.Text) : 100,
                StartCreatedDate = dateTimePicker6.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                EndCreatedDate = dateTimePicker5.Value.ToString("dd/MM/yyyy HH:mm:ss"),
                EntityNames = new List<string>() { "TM_Tasks", "PM_Project" }
            };

            var coverter = await new TagConvert(this, _logger, connectModel).ConvertTagsTasks(parameterModel);

            //Test

            convertTaskTag.Enabled = true;
            richTextBox1.AppendText("Đồng bộ Tag công việc hoàn tất! \n");
        }
    }
}
