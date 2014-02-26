namespace ThirdAuth
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.authExecuteResultTxtBox = new System.Windows.Forms.TextBox();
            this.authTypeSetTabPage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.allUserListDataGridView = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.manageResultTextBox = new System.Windows.Forms.TextBox();
            this.userCountLabel = new System.Windows.Forms.Label();
            this.setUserAuthTypeLocal = new System.Windows.Forms.Button();
            this.setUserAuthTypeThird = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.sychronizationResultTxtBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.authTypeSetTabPage.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.allUserListDataGridView)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.authTypeSetTabPage);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 562);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.authExecuteResultTxtBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 536);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "认证日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // authExecuteResultTxtBox
            // 
            this.authExecuteResultTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.authExecuteResultTxtBox.Location = new System.Drawing.Point(3, 3);
            this.authExecuteResultTxtBox.Margin = new System.Windows.Forms.Padding(6);
            this.authExecuteResultTxtBox.MaxLength = 0;
            this.authExecuteResultTxtBox.Multiline = true;
            this.authExecuteResultTxtBox.Name = "authExecuteResultTxtBox";
            this.authExecuteResultTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.authExecuteResultTxtBox.Size = new System.Drawing.Size(770, 530);
            this.authExecuteResultTxtBox.TabIndex = 0;
            // 
            // authTypeSetTabPage
            // 
            this.authTypeSetTabPage.Controls.Add(this.splitContainer1);
            this.authTypeSetTabPage.Location = new System.Drawing.Point(4, 22);
            this.authTypeSetTabPage.Name = "authTypeSetTabPage";
            this.authTypeSetTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.authTypeSetTabPage.Size = new System.Drawing.Size(776, 536);
            this.authTypeSetTabPage.TabIndex = 1;
            this.authTypeSetTabPage.Text = "认证方式管理";
            this.authTypeSetTabPage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.allUserListDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(770, 530);
            this.splitContainer1.SplitterDistance = 372;
            this.splitContainer1.TabIndex = 0;
            // 
            // allUserListDataGridView
            // 
            this.allUserListDataGridView.AllowUserToAddRows = false;
            this.allUserListDataGridView.AllowUserToDeleteRows = false;
            this.allUserListDataGridView.AllowUserToResizeColumns = false;
            this.allUserListDataGridView.AllowUserToResizeRows = false;
            this.allUserListDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.allUserListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.allUserListDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column4,
            this.Column1,
            this.Column3});
            this.allUserListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allUserListDataGridView.Location = new System.Drawing.Point(0, 0);
            this.allUserListDataGridView.Name = "allUserListDataGridView";
            this.allUserListDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.allUserListDataGridView.RowHeadersVisible = false;
            this.allUserListDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.allUserListDataGridView.RowTemplate.Height = 23;
            this.allUserListDataGridView.Size = new System.Drawing.Size(770, 372);
            this.allUserListDataGridView.TabIndex = 0;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.FalseValue = "False";
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.TrueValue = "True";
            this.Column2.Width = 5;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "real_name";
            this.Column4.HeaderText = "姓名";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 54;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "username";
            this.Column1.HeaderText = "用户名";
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "auth_type";
            this.Column3.HeaderText = "认证方式";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.manageResultTextBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.userCountLabel);
            this.splitContainer2.Panel2.Controls.Add(this.setUserAuthTypeLocal);
            this.splitContainer2.Panel2.Controls.Add(this.setUserAuthTypeThird);
            this.splitContainer2.Size = new System.Drawing.Size(770, 154);
            this.splitContainer2.SplitterDistance = 100;
            this.splitContainer2.TabIndex = 1;
            // 
            // manageResultTextBox
            // 
            this.manageResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manageResultTextBox.Location = new System.Drawing.Point(0, 0);
            this.manageResultTextBox.Multiline = true;
            this.manageResultTextBox.Name = "manageResultTextBox";
            this.manageResultTextBox.ReadOnly = true;
            this.manageResultTextBox.Size = new System.Drawing.Size(770, 100);
            this.manageResultTextBox.TabIndex = 0;
            // 
            // userCountLabel
            // 
            this.userCountLabel.AutoSize = true;
            this.userCountLabel.Location = new System.Drawing.Point(316, 17);
            this.userCountLabel.Name = "userCountLabel";
            this.userCountLabel.Size = new System.Drawing.Size(0, 12);
            this.userCountLabel.TabIndex = 1;
            // 
            // setUserAuthTypeLocal
            // 
            this.setUserAuthTypeLocal.Location = new System.Drawing.Point(141, 17);
            this.setUserAuthTypeLocal.Name = "setUserAuthTypeLocal";
            this.setUserAuthTypeLocal.Size = new System.Drawing.Size(102, 26);
            this.setUserAuthTypeLocal.TabIndex = 0;
            this.setUserAuthTypeLocal.Text = "设置为本地认证";
            this.setUserAuthTypeLocal.UseVisualStyleBackColor = true;
            this.setUserAuthTypeLocal.Click += new System.EventHandler(this.setUserAuthTypeLocal_Click);
            // 
            // setUserAuthTypeThird
            // 
            this.setUserAuthTypeThird.Location = new System.Drawing.Point(18, 17);
            this.setUserAuthTypeThird.Name = "setUserAuthTypeThird";
            this.setUserAuthTypeThird.Size = new System.Drawing.Size(102, 26);
            this.setUserAuthTypeThird.TabIndex = 0;
            this.setUserAuthTypeThird.Text = "设置为三方认证";
            this.setUserAuthTypeThird.UseVisualStyleBackColor = true;
            this.setUserAuthTypeThird.Click += new System.EventHandler(this.setUserAuthTypeThird_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.sychronizationResultTxtBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 536);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "同步日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // sychronizationResultTxtBox
            // 
            this.sychronizationResultTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sychronizationResultTxtBox.Location = new System.Drawing.Point(3, 3);
            this.sychronizationResultTxtBox.MaxLength = 0;
            this.sychronizationResultTxtBox.Multiline = true;
            this.sychronizationResultTxtBox.Name = "sychronizationResultTxtBox";
            this.sychronizationResultTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.sychronizationResultTxtBox.Size = new System.Drawing.Size(770, 530);
            this.sychronizationResultTxtBox.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTX三方认证&数据同步";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.authTypeSetTabPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.allUserListDataGridView)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage authTypeSetTabPage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox authExecuteResultTxtBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button setUserAuthTypeThird;
        private System.Windows.Forms.Button setUserAuthTypeLocal;
        private System.Windows.Forms.TextBox sychronizationResultTxtBox;
        private System.Windows.Forms.DataGridView allUserListDataGridView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox manageResultTextBox;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label userCountLabel;


    }
}

