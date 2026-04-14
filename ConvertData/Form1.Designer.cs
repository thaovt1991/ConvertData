namespace ConverData
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            nguon_du_lieu = new GroupBox();
            lblConnectSQL = new Label();
            connectStringSQL = new TextBox();
            label1 = new Label();
            groupBox1 = new GroupBox();
            databaseName = new TextBox();
            label7 = new Label();
            lblConnectMG = new Label();
            label2 = new Label();
            connectStringMG = new TextBox();
            bttConvertProject = new Button();
            bttConnectSQL = new Button();
            bttConnectMG = new Button();
            richTextBox1 = new RichTextBox();
            tabTasks = new TabPage();
            label3 = new Label();
            prgStatusTask = new ProgressBar();
            bttConvertTask = new Button();
            tabProject = new TabPage();
            numericPageSize = new NumericUpDown();
            numericPage = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            prgStatusProject = new ProgressBar();
            tabTable = new TabControl();
            nguon_du_lieu.SuspendLayout();
            groupBox1.SuspendLayout();
            tabTasks.SuspendLayout();
            tabProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPageSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPage).BeginInit();
            tabTable.SuspendLayout();
            SuspendLayout();
            // 
            // nguon_du_lieu
            // 
            nguon_du_lieu.Controls.Add(lblConnectSQL);
            nguon_du_lieu.Controls.Add(connectStringSQL);
            nguon_du_lieu.Controls.Add(label1);
            nguon_du_lieu.Location = new Point(48, 12);
            nguon_du_lieu.Name = "nguon_du_lieu";
            nguon_du_lieu.Size = new Size(664, 89);
            nguon_du_lieu.TabIndex = 0;
            nguon_du_lieu.TabStop = false;
            nguon_du_lieu.Text = "Nguồn dữ liệu SQL";
            nguon_du_lieu.Enter += groupBox1_Enter;
            // 
            // lblConnectSQL
            // 
            lblConnectSQL.AutoSize = true;
            lblConnectSQL.BorderStyle = BorderStyle.FixedSingle;
            lblConnectSQL.ForeColor = Color.Red;
            lblConnectSQL.Location = new Point(627, 25);
            lblConnectSQL.Name = "lblConnectSQL";
            lblConnectSQL.Size = new Size(16, 17);
            lblConnectSQL.TabIndex = 2;
            lblConnectSQL.Text = "X";
            // 
            // connectStringSQL
            // 
            connectStringSQL.Location = new Point(127, 21);
            connectStringSQL.Name = "connectStringSQL";
            connectStringSQL.Size = new Size(494, 23);
            connectStringSQL.TabIndex = 1;
            connectStringSQL.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 29);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "Link database";
            label1.Click += label1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(databaseName);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(lblConnectMG);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(connectStringMG);
            groupBox1.Location = new Point(48, 121);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(664, 89);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Nguồn  dữ liệu đến từ Mongo DB";
            groupBox1.Enter += groupBox1_Enter_1;
            // 
            // databaseName
            // 
            databaseName.Location = new Point(124, 52);
            databaseName.Name = "databaseName";
            databaseName.Size = new Size(166, 23);
            databaseName.TabIndex = 4;
            databaseName.TextChanged += textBox3_TextChanged_1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(17, 55);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 3;
            label7.Text = "Database Name";
            // 
            // lblConnectMG
            // 
            lblConnectMG.AutoSize = true;
            lblConnectMG.BorderStyle = BorderStyle.FixedSingle;
            lblConnectMG.ForeColor = Color.Red;
            lblConnectMG.Location = new Point(627, 23);
            lblConnectMG.Name = "lblConnectMG";
            lblConnectMG.Size = new Size(16, 17);
            lblConnectMG.TabIndex = 2;
            lblConnectMG.Text = "X";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 25);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 1;
            label2.Text = "Nguồn dữ liệu đến";
            // 
            // connectStringMG
            // 
            connectStringMG.Location = new Point(127, 17);
            connectStringMG.Name = "connectStringMG";
            connectStringMG.Size = new Size(494, 23);
            connectStringMG.TabIndex = 0;
            connectStringMG.TextChanged += textBox2_TextChanged;
            // 
            // bttConvertProject
            // 
            bttConvertProject.ForeColor = SystemColors.InfoText;
            bttConvertProject.Location = new Point(666, 196);
            bttConvertProject.Name = "bttConvertProject";
            bttConvertProject.Size = new Size(123, 23);
            bttConvertProject.TabIndex = 2;
            bttConvertProject.Text = "Đồng bộ dự án";
            bttConvertProject.UseVisualStyleBackColor = false;
            bttConvertProject.Click += ConverterProjectClick;
            // 
            // bttConnectSQL
            // 
            bttConnectSQL.Location = new Point(718, 22);
            bttConnectSQL.Name = "bttConnectSQL";
            bttConnectSQL.Size = new Size(129, 23);
            bttConnectSQL.TabIndex = 4;
            bttConnectSQL.Text = "Kiểm tra kết nối";
            bttConnectSQL.UseVisualStyleBackColor = true;
            bttConnectSQL.Click += btnConnectSql_Click;
            // 
            // bttConnectMG
            // 
            bttConnectMG.Location = new Point(718, 142);
            bttConnectMG.Name = "bttConnectMG";
            bttConnectMG.Size = new Size(129, 23);
            bttConnectMG.TabIndex = 6;
            bttConnectMG.Text = "Kiểm tra kết nối";
            bttConnectMG.UseVisualStyleBackColor = true;
            bttConnectMG.Click += button4_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(48, 496);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(803, 113);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // tabTasks
            // 
            tabTasks.Controls.Add(label3);
            tabTasks.Controls.Add(prgStatusTask);
            tabTasks.Controls.Add(bttConvertTask);
            tabTasks.Location = new Point(4, 24);
            tabTasks.Name = "tabTasks";
            tabTasks.Padding = new Padding(3);
            tabTasks.Size = new Size(795, 236);
            tabTasks.TabIndex = 1;
            tabTasks.Text = "Quản lý công việc";
            tabTasks.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(81, 209);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 2;
            label3.Text = "Tiến độ  ";
            // 
            // prgStatusTask
            // 
            prgStatusTask.Location = new Point(153, 209);
            prgStatusTask.Name = "prgStatusTask";
            prgStatusTask.Size = new Size(486, 15);
            prgStatusTask.TabIndex = 1;
            // 
            // bttConvertTask
            // 
            bttConvertTask.AutoEllipsis = true;
            bttConvertTask.Location = new Point(658, 200);
            bttConvertTask.Name = "bttConvertTask";
            bttConvertTask.Size = new Size(122, 24);
            bttConvertTask.TabIndex = 0;
            bttConvertTask.Text = "Đồng bộ công việc";
            bttConvertTask.UseVisualStyleBackColor = true;
            bttConvertTask.Click += bttConvertTask_ClickAsync;
            // 
            // tabProject
            // 
            tabProject.Controls.Add(numericPageSize);
            tabProject.Controls.Add(numericPage);
            tabProject.Controls.Add(label6);
            tabProject.Controls.Add(label5);
            tabProject.Controls.Add(label4);
            tabProject.Controls.Add(prgStatusProject);
            tabProject.Controls.Add(bttConvertProject);
            tabProject.Location = new Point(4, 24);
            tabProject.Name = "tabProject";
            tabProject.Padding = new Padding(3);
            tabProject.Size = new Size(795, 236);
            tabProject.TabIndex = 0;
            tabProject.Text = "Quản lý dự án";
            tabProject.UseVisualStyleBackColor = true;
            // 
            // numericPageSize
            // 
            numericPageSize.Location = new Point(90, 75);
            numericPageSize.Name = "numericPageSize";
            numericPageSize.Size = new Size(117, 23);
            numericPageSize.TabIndex = 9;
            numericPageSize.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // numericPage
            // 
            numericPage.Location = new Point(90, 32);
            numericPage.Name = "numericPage";
            numericPage.Size = new Size(117, 23);
            numericPage.TabIndex = 8;
            numericPage.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(29, 77);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 7;
            label6.Text = "PageSize";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 32);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 5;
            label5.Text = "Page";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(81, 204);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 4;
            label4.Text = "Tiến độ  ";
            // 
            // prgStatusProject
            // 
            prgStatusProject.Location = new Point(153, 204);
            prgStatusProject.Name = "prgStatusProject";
            prgStatusProject.Size = new Size(486, 15);
            prgStatusProject.TabIndex = 3;
            // 
            // tabTable
            // 
            tabTable.Controls.Add(tabProject);
            tabTable.Controls.Add(tabTasks);
            tabTable.Location = new Point(48, 216);
            tabTable.Name = "tabTable";
            tabTable.SelectedIndex = 0;
            tabTable.Size = new Size(803, 264);
            tabTable.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 633);
            Controls.Add(richTextBox1);
            Controls.Add(bttConnectMG);
            Controls.Add(bttConnectSQL);
            Controls.Add(tabTable);
            Controls.Add(groupBox1);
            Controls.Add(nguon_du_lieu);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ConvertData";
            Load += Form1_Load;
            nguon_du_lieu.ResumeLayout(false);
            nguon_du_lieu.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabTasks.ResumeLayout(false);
            tabTasks.PerformLayout();
            tabProject.ResumeLayout(false);
            tabProject.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPageSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPage).EndInit();
            tabTable.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox nguon_du_lieu;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox connectStringSQL;
        private Label label2;
        private TextBox connectStringMG;
        private Button bttConvertProject;
        private Button bttConnectSQL;
        private Button bttConnectMG;
        public RichTextBox richTextBox1;
        private Label lblConnectSQL;
        private Label lblConnectMG;
        private TabPage tabTasks;
        private TabPage tabProject;
        private TabControl tabTable;
        private Button bttConvertTask;
        private Label label3;
        public ProgressBar prgStatusTask;
        private Label label4;
        public ProgressBar prgStatusProject;
        private Label label5;
        private Label label6;
        private NumericUpDown numericPageSize;
        private NumericUpDown numericPage;
        private Label label7;
        private TextBox databaseName;
    }
}
