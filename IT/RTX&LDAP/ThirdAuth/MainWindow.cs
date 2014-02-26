using RTXSAPILib;
using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace ThirdAuth
{
    public partial class MainWindow : Form
    {
        public delegate void UpdateDisplayControl(string strAuthContent);
        UpdateDisplayControl updateAuthResult;
        UpdateDisplayControl updateSychResult;

        private NotifyIcon app_notify = null;
        private ContextMenu notify_menu = null;

        //����һ��������
        private RTXSAPILib.RTXSAPIRootObj RootObj;
        //����һ���û���֤����
        private RTXSAPILib.RTXSAPIUserAuthObj UserAuthObj;
        //�û��������
        private RTXSAPILib.IRTXSAPIUserManager UserManagerObj;
        //���Ź������
        private RTXSAPILib.IRTXSAPIDeptManager DeptManagerObj;
        //
        private RTXSAPILib.IRTXSAPIOrgManager OrgsManagerObj;
        //Ӧ�õ�GUID
        private string _AppGUID = "{193947E5-E990-4af8-A954-D358B385F069}";
        //Ӧ�õ����� 
        private string _AppName = "RTX_LDAP_Auth";
        //������IP��ַ
        private string _ServerIP = "127.0.0.1";
        //�������˿�
        private Int16 _ServerPort = 8006;
        //��֤���ؽ��
        public Int32 nRet;
        //��������״̬
        private bool _AppRunning = false;

        //����
        private string _DomainName = "127.0.0.1/DC=intranet,DC=123u,DC=com";
        private string _UsersDomainName = "127.0.0.1/OU=users,OU=123u,DC=intranet,DC=123u,DC=com";
        private string _AdminAccount = ConfigurationManager.AppSettings["AD_ACCOUNT"].ToString();
        private string _AdminPasswd = ConfigurationManager.AppSettings["AD_PASSWD"].ToString();

        //
        private Thread sychronThread;
        private ManualResetEvent exitSychronThreadEvent = new ManualResetEvent(false);

        public MainWindow()
        {
            InitializeComponent();

            app_notify = new NotifyIcon();
            app_notify.Icon = new Icon(SystemIcons.Application, 40, 40);
            app_notify.Icon = (Icon)Properties.Resources.ResourceManager.GetObject("myappicon");
            app_notify.Visible = true;
            app_notify.DoubleClick += new EventHandler(NotifyIcon_DoubleClicked);

            notify_menu = new System.Windows.Forms.ContextMenu();
            MenuItem exit_item = new MenuItem("�˳�");
            notify_menu.MenuItems.Add(exit_item);
            exit_item.Click += new EventHandler(NotifyMenuExitItem_Clicked);

            app_notify.ContextMenu = notify_menu;

            //����������
            RootObj = new RTXSAPILib.RTXSAPIRootObj();
            //���÷�����IP
            RootObj.ServerIP = _ServerIP;
            //���÷������˿�
            RootObj.ServerPort = _ServerPort;

            //ͨ�������󴴽��û���֤����
            UserAuthObj = RootObj.UserAuthObj;
            //�����û���֤��Ӧ�¼�
            UserAuthObj.OnRecvUserAuthRequest += new _IRTXSAPIUserAuthObjEvents_OnRecvUserAuthRequestEventHandler(UserAuthObj_OnRecvUserAuthRequest);
            //����Ӧ��GUID
            UserAuthObj.AppGUID = _AppGUID;
            //����Ӧ����
            UserAuthObj.AppName = _AppName;

            UserManagerObj = RootObj.UserManager;
            DeptManagerObj = RootObj.DeptManager;
            OrgsManagerObj = ((RTXSAPIRootObj2)RootObj).OrgstructManager;

            updateAuthResult = new UpdateDisplayControl(AuthCompleted);
            updateSychResult = new UpdateDisplayControl(SychCompleted);

            try
            {
                //ע��Ӧ��
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ����ע��Ӧ��..." + Environment.NewLine;
                UserAuthObj.RegisterApp();
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| Ӧ��ע��ɹ�..." + Environment.NewLine;
                //����Ӧ��
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ��������Ӧ��..." + Environment.NewLine; ;
                UserAuthObj.StartApp("", 8);
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| Ӧ�������ɹ�..." + Environment.NewLine;

                // ����ͬ���߳�
                sychronThread = new Thread(new ThreadStart(AccountSychronization));
                sychronThread.Start();

                _AppRunning = true;
            }
            catch (COMException ex)
            {
                _AppRunning = false;
                this.authExecuteResultTxtBox.Text += ex.Message;
            }
        }

        //ȡ�����е��û�
        protected void LoadAllRtxUsers()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("users");
            dt.Columns.Add("real_name");
            dt.Columns.Add("username");
            dt.Columns.Add("auth_type");

            try
            {
                string all_users = RootObj.QueryUsersByState("online") + RootObj.QueryUsersByState("offline") + RootObj.QueryUsersByState("away");

                int count = 0;
                foreach (string user in all_users.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                {
                    string user_realname = "";
                    string mobile = "";
                    string email = "";
                    string phone = "";
                    int gender = 0;
                    int auth_type = 0;
                    UserManagerObj.GetUserBasicInfo(user, out user_realname, out gender, out mobile, out email, out phone, out auth_type);
                    DataRow dr = dt.NewRow();
                    dr["real_name"] = user_realname;
                    dr["username"] = user;
                    dr["auth_type"] = Convert.ToBoolean(auth_type) ? "��������֤" : "������֤";
                    dt.Rows.Add(dr);
                    count++;
                }

                ds.Tables.Add(dt);

                this.allUserListDataGridView.DataSource = ds;
                this.allUserListDataGridView.DataMember = "users";

                this.userCountLabel.Text = "��ǰ���� " + count + " λ�û�";
            }
            catch (COMException ex)
            {
                this.manageResultTextBox.Text = DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| �쳣: " + ex.Message + Environment.NewLine;
            }
        }

        protected void NotifyMenuExitItem_Clicked(object sender, EventArgs e)
        {
            ShutdownApplication();
        }

        protected void NotifyIcon_DoubleClicked(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        // ��ʾͬ�����
        public void SychCompleted(string strSychResult)
        {
            if (this.sychronizationResultTxtBox.TextLength >= 1406)
            {
                string old_msg = this.sychronizationResultTxtBox.Text;
                string[] old_msg_arr = old_msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string[] new_msg_arr = new string[old_msg_arr.Length - 10];
                Array.Copy(old_msg_arr, 10, new_msg_arr, 0, old_msg_arr.Length - 10);
                this.sychronizationResultTxtBox.Clear();
                this.sychronizationResultTxtBox.Text += String.Join(Environment.NewLine, new_msg_arr) + Environment.NewLine;
            }
            this.sychronizationResultTxtBox.Text += strSychResult;
        }

        //��ʾ��֤���
        public void AuthCompleted(string strAuthContent)
        {
            if (this.authExecuteResultTxtBox.TextLength >= 1406)
            {
                string old_msg = this.authExecuteResultTxtBox.Text;
                string[] old_msg_arr = old_msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string[] new_msg_arr = new string[old_msg_arr.Length - 10];
                Array.Copy(old_msg_arr, 0, new_msg_arr, 0, 4);
                Array.Copy(old_msg_arr, 4, new_msg_arr, 4, old_msg_arr.Length - 14);
                this.authExecuteResultTxtBox.Clear();
                this.authExecuteResultTxtBox.Text += String.Join(Environment.NewLine, new_msg_arr) + Environment.NewLine;
            }
            this.authExecuteResultTxtBox.Text += strAuthContent;
        }

        public void UserAuthObj_OnRecvUserAuthRequest(string bstrUserName, string bstrPwd, out RTXSAPI_USERAUTH_RESULT pResult)
        {
            DirectoryEntry entry = new DirectoryEntry();

            string strAuthContent = "";
            string ldap_uname = bstrUserName.Substring(0, bstrUserName.IndexOf('@'));

            try
            {
                entry.Path = string.Format("LDAP://{0}", _DomainName);
                entry.Username = ldap_uname;
                entry.Password = bstrPwd;
                entry.AuthenticationType = AuthenticationTypes.Secure;
                entry.RefreshCache();

                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(samaccountname=" + bstrUserName.Substring(0, bstrUserName.IndexOf('@')) + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    nRet = 100;
                }
                else
                {
                    nRet = 0;
                }
            }
            catch
            {
                nRet = 2;
            }

            strAuthContent += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| UserName: " + bstrUserName.Substring(0, bstrUserName.IndexOf('@')) + "; Result: " + nRet + Environment.NewLine;

            this.authExecuteResultTxtBox.BeginInvoke(updateAuthResult, strAuthContent);

            if (nRet == 0)
                pResult = RTXSAPI_USERAUTH_RESULT.RTXSAPI_USERAUTH_RESULT_OK;//������֤�ɹ����ͻ��˽�������¼
            else if (nRet == 1)
                pResult = RTXSAPI_USERAUTH_RESULT.RTXSAPI_USERAUTH_RESULT_FAIL;//������֤ʧ�ܣ��ͻ��˽�������¼ʧ����ʾ
            else if (nRet == 2)
                pResult = RTXSAPI_USERAUTH_RESULT.RTXSAPI_USERAUTH_RESULT_ERRPWD;//�����û����������֤ʧ�ܣ��ͻ��˽������û����������ʾ
            else
                pResult = RTXSAPI_USERAUTH_RESULT.RTXSAPI_USERAUTH_RESULT_ERRNOUSER;//������֤ʧ�ܣ��ͻ��˵�����Ӧ��ʾ
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_AppRunning)
            {
                if (e.CloseReason == CloseReason.WindowsShutDown)
                {
                    ShutdownApplication();
                }
                else
                {
                    e.Cancel = true;
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = false;
                }
            }
        }

        private void ShutdownApplication()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.tabControl1.SelectedTab = tabPage1;
            try
            {
                // ֹͣӦ��
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ����ֹͣӦ��..." + Environment.NewLine; ;
                UserAuthObj.StopApp();
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| Ӧ��ֹͣ�ɹ�..." + Environment.NewLine;
                // ע��Ӧ��
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ����ע��Ӧ��..." + Environment.NewLine;
                UserAuthObj.UnRegisterApp();
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| Ӧ��ע���ɹ�..." + Environment.NewLine;
                // �˳�ͬ���߳�
                exitSychronThreadEvent.Set();
                sychronThread.Join();

                _AppRunning = false;
            }
            catch(COMException ex)
            {
                this.authExecuteResultTxtBox.Text += DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| �쳣:" + ex.Message + Environment.NewLine;
            }
            Application.Exit();
        }

        private void setUserAuthTypeLocal_Click(object sender, EventArgs e)
        {
            SetUserAuthTypes(false);
        }

        private void setUserAuthTypeThird_Click(object sender, EventArgs e)
        {
            SetUserAuthTypes(true);
        }

        protected void SetUserAuthTypes(bool authType)
        {
            try
            {
                this.manageResultTextBox.Clear();
                foreach (DataGridViewRow row in allUserListDataGridView.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        if (UserAuthObj.QueryUserAuthType(row.Cells[2].Value.ToString()) == authType)
                        {
                            this.manageResultTextBox.Text += row.Cells[1].Value.ToString() + " û�б仯." + Environment.NewLine;
                        }
                        else
                        {
                            UserAuthObj.SetUserAuthType(row.Cells[2].Value.ToString(), authType);
                            this.manageResultTextBox.Text += row.Cells[1].Value.ToString() + " ���óɹ�." + Environment.NewLine;
                        }
                    }
                }
            }
            catch(COMException ex)
            {
                this.manageResultTextBox.Text = ex.Message;
            }
            finally
            {
                LoadAllRtxUsers();
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name == authTypeSetTabPage.Name)
            {
                LoadAllRtxUsers();
            }
        }

        protected void AccountSychronization()
        {
            DirectoryEntry root_entry = new DirectoryEntry();
            root_entry.Path = string.Format("LDAP://{0}", _UsersDomainName);
            root_entry.Username = _AdminAccount;
            root_entry.Password = _AdminPasswd;
            root_entry.AuthenticationType = AuthenticationTypes.Secure;

            while (true)
            {
                if (exitSychronThreadEvent.WaitOne(0))
                {
                    break;
                }
                this.sychronizationResultTxtBox.BeginInvoke(updateSychResult, DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ͬ����ʼ..." + Environment.NewLine);
                try
                {
                    root_entry.RefreshCache();
                    DoSychronizationToRTX(root_entry, null);
                    DoSychronizationCleanRTX(root_entry);
                }
                catch (Exception ex)
                {
                    this.sychronizationResultTxtBox.BeginInvoke(updateSychResult, DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| " + ex.Message + Environment.NewLine);
                }
                this.sychronizationResultTxtBox.BeginInvoke(updateSychResult, DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ͬ������..." + Environment.NewLine);
                Thread.Sleep(5 * 60 * 1000);
            }
        }

        protected void DoSychronizationCleanRTX(DirectoryEntry root_entry)
        {
            string all_users = RootObj.QueryUsersByState("online") + RootObj.QueryUsersByState("offline") + RootObj.QueryUsersByState("away");
            foreach (string user in all_users.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                DirectorySearcher search = new DirectorySearcher(root_entry);
                search.Filter = "(&(objectclass=user)(samaccountname=" + user.Substring(0, user.IndexOf('@')) + "))";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (result == null)
                {
                    UserManagerObj.DeleteUser(user);
                }
            }
        }

        protected void DoSychronizationToRTX(DirectoryEntry entry, string parentId)
        {
            if (parentId == null)
            {
                string rootOuName = entry.Name;
                byte[] bGUID = entry.Properties["objectGUID"][0] as byte[];
                string id = BitConverter.ToString(bGUID);
                DoSychronizationToRTX(entry, id);
            }
            else
            {
                foreach (DirectoryEntry subEntry in entry.Children)
                {
                    string entrySchemaClsName = subEntry.SchemaClassName;

                    string[] arr = subEntry.Name.Split('=');
                    string categoryStr = arr[0];
                    string nameStr = arr[1];
                    string id = string.Empty;

                    string dept_name = nameStr;
                    DirectoryEntry tmp_entry = subEntry.Parent;
                    while (!string.Format("LDAP://{0}", _UsersDomainName).Equals(tmp_entry.Path))
                    {
                        dept_name = tmp_entry.Name.Split('=')[1] + "\\" + dept_name;
                        tmp_entry = tmp_entry.Parent;
                    }

                    if (subEntry.Properties.Contains("objectGUID"))
                    {
                        byte[] bGUID = subEntry.Properties["objectGUID"][0] as byte[];
                        id = BitConverter.ToString(bGUID);
                    }
                    switch (entrySchemaClsName)
                    {
                        case "organizationalUnit":
                            if (!DeptManagerObj.IsDeptExist(dept_name))
                            {
                                if (dept_name.IndexOf("\\") == -1)
                                {
                                    DeptManagerObj.AddDept(nameStr, null);
                                }
                                else
                                {
                                    DeptManagerObj.AddDept(nameStr, dept_name.Substring(0, dept_name.LastIndexOf("\\")));
                                }
                            }
                            DoSychronizationToRTX(subEntry, id);
                            break;
                        case "user":
                            string accountName = string.Empty;
                            string telephonenumber = string.Empty;
                            if (subEntry.Properties.Contains("samaccountname"))
                            {
                                accountName = subEntry.Properties["samaccountname"][0].ToString() + "@123u.com";
                                if (subEntry.Properties.Contains("telephonenumber"))
                                {
                                    telephonenumber = subEntry.Properties["telephonenumber"][0].ToString();
                                }
                                if (UserManagerObj.IsUserExist(accountName))
                                {
                                    UserManagerObj.SetUserBasicInfo(accountName, nameStr, -1, telephonenumber, accountName, null, -1);

                                    string old_depts = DeptManagerObj.GetUserDepts(accountName);
                                    XmlDocument xmlDoc = new XmlDocument();
                                    xmlDoc.LoadXml(old_depts);

                                    string xpath = "Departments";
                                    var nodes = xmlDoc.SelectNodes(xpath);

                                    foreach (XmlNode childrenNode in nodes)
                                    {
                                        string old_dept = childrenNode.SelectSingleNode("Department").Attributes["Name"].Value.ToString();
                                        old_dept = old_dept.Substring(0, old_dept.LastIndexOf("\\"));
                                        if (dept_name.Substring(0, dept_name.LastIndexOf("\\")).Equals(old_dept))
                                        {

                                        }
                                        else
                                        {
                                            DeptManagerObj.DelUserFromDept(accountName, old_dept);
                                        }
                                    }
                                }
                                else
                                {
                                    UserManagerObj.AddUser(accountName, 1);
                                    OrgsManagerObj.SetOrgUser(accountName, "����");
                                    UserManagerObj.SetUserBasicInfo(accountName, nameStr, -1, telephonenumber, accountName, null, 1);
                                    this.sychronizationResultTxtBox.BeginInvoke(updateSychResult, DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "| ͬ���˻�:" + accountName + Environment.NewLine);
                                }
                                DeptManagerObj.AddUserToDept(accountName, "", dept_name.Substring(0, dept_name.LastIndexOf("\\")), false);
                            }
                            break;
                    }
                }
            }
        }
    }
}