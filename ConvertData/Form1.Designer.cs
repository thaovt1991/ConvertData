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
            label10 = new Label();
            dateTimePicker3 = new DateTimePicker();
            label11 = new Label();
            dateTimePicker4 = new DateTimePicker();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            label12 = new Label();
            label13 = new Label();
            nguon_du_lieu.SuspendLayout();
            groupBox1.SuspendLayout();
            tabTasks.SuspendLayout();
            tabProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericPageSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPage).BeginInit();
            tabTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // nguon_du_lieu
            // 
            nguon_du_lieu.Controls.Add(lblConnectSQL);
            nguon_du_lieu.Controls.Add(connectStringSQL);
            nguon_du_lieu.Controls.Add(label1);
            nguon_du_lieu.Location = new Point(55, 16);
            nguon_du_lieu.Margin = new Padding(3, 4, 3, 4);
            nguon_du_lieu.Name = "nguon_du_lieu";
            nguon_du_lieu.Padding = new Padding(3, 4, 3, 4);
            nguon_du_lieu.Size = new Size(759, 119);
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
            lblConnectSQL.Location = new Point(717, 33);
            lblConnectSQL.Name = "lblConnectSQL";
            lblConnectSQL.Size = new Size(20, 22);
            lblConnectSQL.TabIndex = 2;
            lblConnectSQL.Text = "X";
            // 
            // connectStringSQL
            // 
            connectStringSQL.Location = new Point(145, 28);
            connectStringSQL.Margin = new Padding(3, 4, 3, 4);
            connectStringSQL.Name = "connectStringSQL";
            connectStringSQL.Size = new Size(564, 27);
            connectStringSQL.TabIndex = 1;
            connectStringSQL.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 39);
            label1.Name = "label1";
            label1.Size = new Size(100, 20);
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
            groupBox1.Location = new Point(55, 161);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(759, 119);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Nguồn  dữ liệu đến từ Mongo DB";
            groupBox1.Enter += groupBox1_Enter_1;
            // 
            // databaseName
            // 
            databaseName.Location = new Point(142, 69);
            databaseName.Margin = new Padding(3, 4, 3, 4);
            databaseName.Name = "databaseName";
            databaseName.Size = new Size(189, 27);
            databaseName.TabIndex = 4;
            databaseName.TextChanged += textBox3_TextChanged_1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(19, 73);
            label7.Name = "label7";
            label7.Size = new Size(116, 20);
            label7.TabIndex = 3;
            label7.Text = "Database Name";
            // 
            // lblConnectMG
            // 
            lblConnectMG.AutoSize = true;
            lblConnectMG.BorderStyle = BorderStyle.FixedSingle;
            lblConnectMG.ForeColor = Color.Red;
            lblConnectMG.Location = new Point(717, 31);
            lblConnectMG.Name = "lblConnectMG";
            lblConnectMG.Size = new Size(20, 22);
            lblConnectMG.TabIndex = 2;
            lblConnectMG.Text = "X";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 33);
            label2.Name = "label2";
            label2.Size = new Size(133, 20);
            label2.TabIndex = 1;
            label2.Text = "Nguồn dữ liệu đến";
            // 
            // connectStringMG
            // 
            connectStringMG.Location = new Point(145, 23);
            connectStringMG.Margin = new Padding(3, 4, 3, 4);
            connectStringMG.Name = "connectStringMG";
            connectStringMG.Size = new Size(564, 27);
            connectStringMG.TabIndex = 0;
            connectStringMG.TextChanged += textBox2_TextChanged;
            // 
            // bttConvertProject
            // 
            bttConvertProject.ForeColor = SystemColors.InfoText;
            bttConvertProject.Location = new Point(736, 261);
            bttConvertProject.Margin = new Padding(3, 4, 3, 4);
            bttConvertProject.Name = "bttConvertProject";
            bttConvertProject.Size = new Size(166, 31);
            bttConvertProject.TabIndex = 2;
            bttConvertProject.Text = "Đồng bộ dự án";
            bttConvertProject.UseVisualStyleBackColor = false;
            bttConvertProject.Click += ConverterProjectClick;
            // 
            // bttConnectSQL
            // 
            bttConnectSQL.Location = new Point(821, 29);
            bttConnectSQL.Margin = new Padding(3, 4, 3, 4);
            bttConnectSQL.Name = "bttConnectSQL";
            bttConnectSQL.Size = new Size(147, 31);
            bttConnectSQL.TabIndex = 4;
            bttConnectSQL.Text = "Kiểm tra kết nối";
            bttConnectSQL.UseVisualStyleBackColor = true;
            bttConnectSQL.Click += btnConnectSql_Click;
            // 
            // bttConnectMG
            // 
            bttConnectMG.Location = new Point(821, 189);
            bttConnectMG.Margin = new Padding(3, 4, 3, 4);
            bttConnectMG.Name = "bttConnectMG";
            bttConnectMG.Size = new Size(147, 31);
            bttConnectMG.TabIndex = 6;
            bttConnectMG.Text = "Kiểm tra kết nối";
            bttConnectMG.UseVisualStyleBackColor = true;
            bttConnectMG.Click += button4_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(55, 661);
            richTextBox1.Margin = new Padding(3, 4, 3, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(917, 149);
            richTextBox1.TabIndex = 7;
            richTextBox1.Text = "";
            // 
            // tabTasks
            // 
            tabTasks.Controls.Add(numericUpDown1);
            tabTasks.Controls.Add(numericUpDown2);
            tabTasks.Controls.Add(label12);
            tabTasks.Controls.Add(label13);
            tabTasks.Controls.Add(label11);
            tabTasks.Controls.Add(dateTimePicker4);
            tabTasks.Controls.Add(label10);
            tabTasks.Controls.Add(dateTimePicker3);
            tabTasks.Controls.Add(label3);
            tabTasks.Controls.Add(prgStatusTask);
            tabTasks.Controls.Add(bttConvertTask);
            tabTasks.Location = new Point(4, 29);
            tabTasks.Margin = new Padding(3, 4, 3, 4);
            tabTasks.Name = "tabTasks";
            tabTasks.Padding = new Padding(3, 4, 3, 4);
            tabTasks.Size = new Size(909, 319);
            tabTasks.TabIndex = 1;
            tabTasks.Text = "Quản lý công việc";
            tabTasks.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(93, 279);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 2;
            label3.Text = "Tiến độ  ";
            // 
            // prgStatusTask
            // 
            prgStatusTask.Location = new Point(175, 279);
            prgStatusTask.Margin = new Padding(3, 4, 3, 4);
            prgStatusTask.Name = "prgStatusTask";
            prgStatusTask.Size = new Size(555, 20);
            prgStatusTask.TabIndex = 1;
            // 
            // bttConvertTask
            // 
            bttConvertTask.AutoEllipsis = true;
            bttConvertTask.Location = new Point(736, 267);
            bttConvertTask.Margin = new Padding(3, 4, 3, 4);
            bttConvertTask.Name = "bttConvertTask";
            bttConvertTask.Size = new Size(167, 32);
            bttConvertTask.TabIndex = 0;
            bttConvertTask.Text = "Đồng bộ công việc";
            bttConvertTask.UseVisualStyleBackColor = true;
            bttConvertTask.Click += bttConvertTask_ClickAsync;
            // 
            // tabProject
            // 
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
            tabProject.Location = new Point(4, 29);
            tabProject.Margin = new Padding(3, 4, 3, 4);
            tabProject.Name = "tabProject";
            tabProject.Padding = new Padding(3, 4, 3, 4);
            tabProject.Size = new Size(909, 319);
            tabProject.TabIndex = 0;
            tabProject.Text = "Quản lý dự án";
            tabProject.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(291, 105);
            label9.Name = "label9";
            label9.Size = new Size(70, 20);
            label9.TabIndex = 13;
            label9.Text = "End Date";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(386, 103);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(257, 27);
            dateTimePicker2.TabIndex = 12;
            dateTimePicker2.ValueChanged += dateTimePicker2_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(291, 45);
            label8.Name = "label8";
            label8.Size = new Size(76, 20);
            label8.TabIndex = 11;
            label8.Text = "Start Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(386, 43);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(257, 27);
            dateTimePicker1.TabIndex = 10;
            // 
            // numericPageSize
            // 
            numericPageSize.Location = new Point(103, 100);
            numericPageSize.Margin = new Padding(3, 4, 3, 4);
            numericPageSize.Name = "numericPageSize";
            numericPageSize.Size = new Size(134, 27);
            numericPageSize.TabIndex = 9;
            numericPageSize.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // numericPage
            // 
            numericPage.Location = new Point(103, 43);
            numericPage.Margin = new Padding(3, 4, 3, 4);
            numericPage.Name = "numericPage";
            numericPage.Size = new Size(134, 27);
            numericPage.TabIndex = 8;
            numericPage.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(33, 103);
            label6.Name = "label6";
            label6.Size = new Size(68, 20);
            label6.TabIndex = 7;
            label6.Text = "PageSize";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 43);
            label5.Name = "label5";
            label5.Size = new Size(41, 20);
            label5.TabIndex = 5;
            label5.Text = "Page";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(93, 272);
            label4.Name = "label4";
            label4.Size = new Size(67, 20);
            label4.TabIndex = 4;
            label4.Text = "Tiến độ  ";
            // 
            // prgStatusProject
            // 
            prgStatusProject.Location = new Point(175, 272);
            prgStatusProject.Margin = new Padding(3, 4, 3, 4);
            prgStatusProject.Name = "prgStatusProject";
            prgStatusProject.Size = new Size(555, 20);
            prgStatusProject.TabIndex = 3;
            // 
            // tabTable
            // 
            tabTable.Controls.Add(tabProject);
            tabTable.Controls.Add(tabTasks);
            tabTable.Location = new Point(55, 288);
            tabTable.Margin = new Padding(3, 4, 3, 4);
            tabTable.Name = "tabTable";
            tabTable.SelectedIndex = 0;
            tabTable.Size = new Size(917, 352);
            tabTable.TabIndex = 3;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(251, 59);
            label10.Name = "label10";
            label10.Size = new Size(76, 20);
            label10.TabIndex = 13;
            label10.Text = "Start Date";
            // 
            // dateTimePicker3
            // 
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Location = new Point(333, 62);
            dateTimePicker3.Name = "dateTimePicker3";
            dateTimePicker3.Size = new Size(257, 27);
            dateTimePicker3.TabIndex = 12;
            dateTimePicker3.ValueChanged += dateTimePicker3_ValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(246, 117);
            label11.Name = "label11";
            label11.Size = new Size(70, 20);
            label11.TabIndex = 15;
            label11.Text = "End Date";
            // 
            // dateTimePicker4
            // 
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.Location = new Point(333, 115);
            dateTimePicker4.Name = "dateTimePicker4";
            dateTimePicker4.Size = new Size(257, 27);
            dateTimePicker4.TabIndex = 14;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(93, 115);
            numericUpDown1.Margin = new Padding(3, 4, 3, 4);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(134, 27);
            numericUpDown1.TabIndex = 19;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(93, 57);
            numericUpDown2.Margin = new Padding(3, 4, 3, 4);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(134, 27);
            numericUpDown2.TabIndex = 18;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(17, 117);
            label12.Name = "label12";
            label12.Size = new Size(68, 20);
            label12.TabIndex = 17;
            label12.Text = "PageSize";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(17, 57);
            label13.Name = "label13";
            label13.Size = new Size(41, 20);
            label13.TabIndex = 16;
            label13.Text = "Page";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 844);
            Controls.Add(richTextBox1);
            Controls.Add(bttConnectMG);
            Controls.Add(bttConnectSQL);
            Controls.Add(tabTable);
            Controls.Add(groupBox1);
            Controls.Add(nguon_du_lieu);
            Margin = new Padding(3, 4, 3, 4);
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
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
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
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label12;
        private Label label13;
        private Label label11;
        private DateTimePicker dateTimePicker4;
    }
}
