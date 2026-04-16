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
            bttConnectSQL = new Button();
            label1 = new Label();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            lblConnectPG = new Label();
            bttConnectPG = new Button();
            connectionStringPG = new TextBox();
            label16 = new Label();
            groupBox2 = new GroupBox();
            label7 = new Label();
            bttConnectMG = new Button();
            lblConnectMG = new Label();
            databaseName = new TextBox();
            connectStringMG = new TextBox();
            label2 = new Label();
            bttConvertProject = new Button();
            richTextBox1 = new RichTextBox();
            tabTasks = new TabPage();
            convertSucTask = new Label();
            countTask = new Label();
            label18 = new Label();
            label19 = new Label();
            numericPageSizeTM = new NumericUpDown();
            numericPageTM = new NumericUpDown();
            label12 = new Label();
            label13 = new Label();
            label11 = new Label();
            dateTimePicker4 = new DateTimePicker();
            label10 = new Label();
            dateTimePicker3 = new DateTimePicker();
            label3 = new Label();
            prgStatusTask = new ProgressBar();
            bttConvertTask = new Button();
            tabProject = new TabPage();
            convertSucPr = new Label();
            countPr = new Label();
            label15 = new Label();
            label14 = new Label();
            label9 = new Label();
            dateTimePicker2 = new DateTimePicker();
            label8 = new Label();
            dateTimePicker1 = new DateTimePicker();
            numericPageSize = new NumericUpDown();
            numericPage = new NumericUpDown();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            prgStatusProject = new ProgressBar();
            tabTable = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            nguon_du_lieu.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            tabTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPageSizeTM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPageTM).BeginInit();
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
            nguon_du_lieu.Controls.Add(bttConnectSQL);
            nguon_du_lieu.Controls.Add(label1);
            nguon_du_lieu.Location = new Point(48, 12);
            nguon_du_lieu.Name = "nguon_du_lieu";
            nguon_du_lieu.Size = new Size(824, 54);
            nguon_du_lieu.TabIndex = 0;
            nguon_du_lieu.TabStop = false;
            nguon_du_lieu.Text = "NEW PVOIL-Nguồn dữ liệu SQL";
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
            // bttConnectSQL
            // 
            bttConnectSQL.Location = new Point(673, 21);
            bttConnectSQL.Name = "bttConnectSQL";
            bttConnectSQL.Size = new Size(129, 23);
            bttConnectSQL.TabIndex = 4;
            bttConnectSQL.Text = "Kiểm tra kết nối";
            bttConnectSQL.UseVisualStyleBackColor = true;
            bttConnectSQL.Click += btnConnectSql_Click;
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
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Location = new Point(48, 72);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(824, 169);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "OLD PVOIL-Nguồn  dữ liệu ";
            groupBox1.Enter += groupBox1_Enter_1;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(lblConnectPG);
            groupBox3.Controls.Add(bttConnectPG);
            groupBox3.Controls.Add(connectionStringPG);
            groupBox3.Controls.Add(label16);
            groupBox3.Location = new Point(9, 111);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(809, 53);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "PostgreSQL";
            // 
            // lblConnectPG
            // 
            lblConnectPG.AutoSize = true;
            lblConnectPG.BorderStyle = BorderStyle.FixedSingle;
            lblConnectPG.ForeColor = Color.Red;
            lblConnectPG.Location = new Point(618, 19);
            lblConnectPG.Name = "lblConnectPG";
            lblConnectPG.Size = new Size(16, 17);
            lblConnectPG.TabIndex = 5;
            lblConnectPG.Text = "X";
            // 
            // bttConnectPG
            // 
            bttConnectPG.Location = new Point(664, 15);
            bttConnectPG.Name = "bttConnectPG";
            bttConnectPG.Size = new Size(129, 23);
            bttConnectPG.TabIndex = 6;
            bttConnectPG.Text = "Kiểm tra kết nối";
            bttConnectPG.UseVisualStyleBackColor = true;
            bttConnectPG.Click += bttConnectPG_Click;
            // 
            // connectionStringPG
            // 
            connectionStringPG.Location = new Point(112, 16);
            connectionStringPG.Name = "connectionStringPG";
            connectionStringPG.Size = new Size(494, 23);
            connectionStringPG.TabIndex = 3;
            connectionStringPG.TextChanged += connectionStringPG_TextChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(0, 19);
            label16.Name = "label16";
            label16.Size = new Size(106, 15);
            label16.TabIndex = 2;
            label16.Text = "Nguồn dữ liệu đến";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(bttConnectMG);
            groupBox2.Controls.Add(lblConnectMG);
            groupBox2.Controls.Add(databaseName);
            groupBox2.Controls.Add(connectStringMG);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(6, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(812, 83);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "MongoDB";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 56);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 3;
            label7.Text = "Database Name";
            // 
            // bttConnectMG
            // 
            bttConnectMG.Location = new Point(667, 24);
            bttConnectMG.Name = "bttConnectMG";
            bttConnectMG.Size = new Size(129, 23);
            bttConnectMG.TabIndex = 6;
            bttConnectMG.Text = "Kiểm tra kết nối";
            bttConnectMG.UseVisualStyleBackColor = true;
            bttConnectMG.Click += button4_Click;
            // 
            // lblConnectMG
            // 
            lblConnectMG.AutoSize = true;
            lblConnectMG.BorderStyle = BorderStyle.FixedSingle;
            lblConnectMG.ForeColor = Color.Red;
            lblConnectMG.Location = new Point(621, 19);
            lblConnectMG.Name = "lblConnectMG";
            lblConnectMG.Size = new Size(16, 17);
            lblConnectMG.TabIndex = 2;
            lblConnectMG.Text = "X";
            // 
            // databaseName
            // 
            databaseName.Location = new Point(121, 48);
            databaseName.Name = "databaseName";
            databaseName.Size = new Size(166, 23);
            databaseName.TabIndex = 4;
            databaseName.TextChanged += textBox3_TextChanged_1;
            // 
            // connectStringMG
            // 
            connectStringMG.Location = new Point(121, 16);
            connectStringMG.Name = "connectStringMG";
            connectStringMG.Size = new Size(494, 23);
            connectStringMG.TabIndex = 0;
            connectStringMG.TextChanged += textBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 24);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 1;
            label2.Text = "Nguồn dữ liệu đến";
            // 
            // bttConvertProject
            // 
            bttConvertProject.ForeColor = SystemColors.InfoText;
            bttConvertProject.Location = new Point(644, 196);
            bttConvertProject.Name = "bttConvertProject";
            bttConvertProject.Size = new Size(145, 23);
            bttConvertProject.TabIndex = 2;
            bttConvertProject.Text = "Đồng bộ dự án";
            bttConvertProject.UseVisualStyleBackColor = false;
            bttConvertProject.Click += ConverterProjectClick;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(44, 528);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(822, 113);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // tabTasks
            // 
            tabTasks.Controls.Add(convertSucTask);
            tabTasks.Controls.Add(countTask);
            tabTasks.Controls.Add(label18);
            tabTasks.Controls.Add(label19);
            tabTasks.Controls.Add(numericPageSizeTM);
            tabTasks.Controls.Add(numericPageTM);
            tabTasks.Controls.Add(label12);
            tabTasks.Controls.Add(label13);
            tabTasks.Controls.Add(label11);
            tabTasks.Controls.Add(dateTimePicker4);
            tabTasks.Controls.Add(label10);
            tabTasks.Controls.Add(dateTimePicker3);
            tabTasks.Controls.Add(label3);
            tabTasks.Controls.Add(prgStatusTask);
            tabTasks.Controls.Add(bttConvertTask);
            tabTasks.Location = new Point(4, 24);
            tabTasks.Name = "tabTasks";
            tabTasks.Padding = new Padding(3);
            tabTasks.Size = new Size(820, 236);
            tabTasks.TabIndex = 1;
            tabTasks.Text = "Quản lý công việc";
            tabTasks.UseVisualStyleBackColor = true;
            // 
            // convertSucTask
            // 
            convertSucTask.AutoSize = true;
            convertSucTask.FlatStyle = FlatStyle.Popup;
            convertSucTask.ForeColor = Color.Blue;
            convertSucTask.Location = new Point(493, 181);
            convertSucTask.Name = "convertSucTask";
            convertSucTask.Size = new Size(13, 15);
            convertSucTask.TabIndex = 23;
            convertSucTask.Text = "0";
            // 
            // countTask
            // 
            countTask.AutoSize = true;
            countTask.Location = new Point(229, 181);
            countTask.Name = "countTask";
            countTask.Size = new Size(13, 15);
            countTask.TabIndex = 22;
            countTask.Text = "0";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(364, 181);
            label18.Name = "label18";
            label18.Size = new Size(123, 15);
            label18.TabIndex = 21;
            label18.Text = "Đồng bộ thành công: ";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(153, 181);
            label19.Name = "label19";
            label19.Size = new Size(70, 15);
            label19.TabIndex = 20;
            label19.Text = "Tổng cộng :";
            // 
            // numericPageSizeTM
            // 
            numericPageSizeTM.Location = new Point(81, 67);
            numericPageSizeTM.Name = "numericPageSizeTM";
            numericPageSizeTM.Size = new Size(117, 23);
            numericPageSizeTM.TabIndex = 19;
            // 
            // numericPageTM
            // 
            numericPageTM.Location = new Point(81, 24);
            numericPageTM.Name = "numericPageTM";
            numericPageTM.Size = new Size(117, 23);
            numericPageTM.TabIndex = 18;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(11, 69);
            label12.Name = "label12";
            label12.Size = new Size(53, 15);
            label12.TabIndex = 17;
            label12.Text = "PageSize";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(15, 26);
            label13.Name = "label13";
            label13.Size = new Size(33, 15);
            label13.TabIndex = 16;
            label13.Text = "Page";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(219, 69);
            label11.Name = "label11";
            label11.Size = new Size(54, 15);
            label11.TabIndex = 15;
            label11.Text = "End Date";
            // 
            // dateTimePicker4
            // 
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.Location = new Point(281, 69);
            dateTimePicker4.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker4.Name = "dateTimePicker4";
            dateTimePicker4.Size = new Size(225, 23);
            dateTimePicker4.TabIndex = 14;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(215, 26);
            label10.Name = "label10";
            label10.Size = new Size(58, 15);
            label10.TabIndex = 13;
            label10.Text = "Start Date";
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Location = new Point(281, 20);
            dateTimePicker3.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(225, 23);
            dateTimePicker3.TabIndex = 12;
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
            bttConvertTask.Location = new Point(644, 200);
            bttConvertTask.Name = "bttConvertTask";
            bttConvertTask.Size = new Size(146, 24);
            bttConvertTask.TabIndex = 0;
            bttConvertTask.Text = "Đồng bộ công việc";
            bttConvertTask.UseVisualStyleBackColor = true;
            bttConvertTask.Click += bttConvertTask_ClickAsync;
            // 
            // tabProject
            // 
            tabProject.Controls.Add(convertSucPr);
            tabProject.Controls.Add(countPr);
            tabProject.Controls.Add(label15);
            tabProject.Controls.Add(label14);
            tabProject.Controls.Add(label9);
            tabProject.Controls.Add(dateTimePicker2);
            tabProject.Controls.Add(label8);
            tabProject.Controls.Add(dateTimePicker1);
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
            tabProject.Size = new Size(820, 236);
            tabProject.TabIndex = 0;
            tabProject.Text = "Quản lý dự án";
            tabProject.UseVisualStyleBackColor = true;
            // 
            // convertSucPr
            // 
            convertSucPr.AutoSize = true;
            convertSucPr.FlatStyle = FlatStyle.Popup;
            convertSucPr.ForeColor = Color.Blue;
            convertSucPr.Location = new Point(492, 178);
            convertSucPr.Name = "convertSucPr";
            convertSucPr.Size = new Size(13, 15);
            convertSucPr.TabIndex = 17;
            convertSucPr.Text = "0";
            // 
            // countPr
            // 
            countPr.AutoSize = true;
            countPr.Location = new Point(228, 178);
            countPr.Name = "countPr";
            countPr.Size = new Size(13, 15);
            countPr.TabIndex = 16;
            countPr.Text = "0";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(363, 178);
            label15.Name = "label15";
            label15.Size = new Size(123, 15);
            label15.TabIndex = 15;
            label15.Text = "Đồng bộ thành công: ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(152, 178);
            label14.Name = "label14";
            label14.Size = new Size(70, 15);
            label14.TabIndex = 14;
            label14.Text = "Tổng cộng :";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(255, 79);
            label9.Name = "label9";
            label9.Size = new Size(54, 15);
            label9.TabIndex = 13;
            label9.Text = "End Date";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(338, 77);
            dateTimePicker2.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(225, 23);
            dateTimePicker2.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(255, 34);
            label8.Name = "label8";
            label8.Size = new Size(58, 15);
            label8.TabIndex = 11;
            label8.Text = "Start Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(338, 32);
            dateTimePicker1.Margin = new Padding(3, 2, 3, 2);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(225, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // numericPageSize
            // 
            numericPageSize.Location = new Point(90, 75);
            numericPageSize.Name = "numericPageSize";
            numericPageSize.Size = new Size(117, 23);
            numericPageSize.TabIndex = 9;
            // 
            // numericPage
            // 
            numericPage.Location = new Point(90, 32);
            numericPage.Name = "numericPage";
            numericPage.Size = new Size(117, 23);
            numericPage.TabIndex = 8;
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
            tabTable.Controls.Add(tabPage1);
            tabTable.Controls.Add(tabPage2);
            tabTable.Location = new Point(44, 247);
            tabTable.Name = "tabTable";
            tabTable.SelectedIndex = 0;
            tabTable.Size = new Size(828, 264);
            tabTable.TabIndex = 3;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabTag";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(820, 236);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Tag";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabHistory";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(820, 236);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Lịch sử";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 665);
            Controls.Add(richTextBox1);
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
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tabTasks.ResumeLayout(false);
            tabTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericPageSizeTM).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPageTM).EndInit();
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
        private Label label8;
        private DateTimePicker dateTimePicker1;
        private Label label9;
        private DateTimePicker dateTimePicker2;
        private Label label10;
        private DateTimePicker dateTimePicker3;
        private NumericUpDown numericPageSizeTM;
        private NumericUpDown numericPageTM;
        private Label label12;
        private Label label13;
        private Label label11;
        private DateTimePicker dateTimePicker4;
        public Label convertSucPr;
        public Label countPr;
        private Label label15;
        private Label label14;
        public Label convertSucTask;
        public Label countTask;
        private Label label18;
        private Label label19;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox connectionStringPG;
        private Label label16;
        private Label lblConnectPG;
        private Button bttConnectPG;
        private TabControl Tag;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}
