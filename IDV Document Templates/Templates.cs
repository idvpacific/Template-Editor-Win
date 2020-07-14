using MetroSet_UI.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace IDV_Document_Templates
{
    public partial class Templates : MetroSetForm
    {
        //--------------------------------------------------------------------------
        public string ST_UID = "";
        public string ST_UserFullname = "";
        public string ST_Server = "";
        public string ST_Username = "";
        public string ST_Password = "";
        //--------------------------------------------------------------------------
        bool FristLoad = false;
        bool SystemAuto = false;
        public bool Reload_Country_T1 = true;
        public bool Reload_Country_T3 = true;
        public bool Reload_State_T3 = true;
        public bool Reload_Type_T3 = true;
        public bool ReloadBtn = false;
        bool DocTypeFL = false;
        //--------------------------------------------------------------------------
        public Templates()
        {
            FristLoad = true;
            InitializeComponent();
            metroSetCheckBox1.Checked = false;
            styleManager1.Style = MetroSet_UI.Design.Style.Light;
            FristLoad = false;
        }
        //--------------------------------------------------------------------------
        private void Templates_Load(object sender, EventArgs e)
        {
            metroSetTabControl1.Visible = false;
            try
            {
                if (Interaction.GetSetting("IDV", "DocTemp", "DM", "FALSE").ToString().Trim().ToUpper() == "TRUE")
                {
                    metroSetCheckBox1.Checked = true;
                    styleManager1.Style = MetroSet_UI.Design.Style.Dark;
                }
            }
            catch (Exception) { }
            this.Width = 950;
            this.Height = 550;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            metroSetLabel1.Text = "";
            metroSetLabel1.Text = "Hi! " + ST_UserFullname.Trim();
            metroSetControlBox1.Visible = true;
            WG.Visible = false;
            metroSetTabControl1.SelectedIndex = 0;
            //------------------------------------------------------------
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("C1", "ID");
            dataGridView1.Columns.Add("C2", "Name");
            dataGridView1.Columns.Add("C2", "Code");
            dataGridView1.Columns.Add("C3", "Status");
            dataGridView1.Columns.Add("C4", "Date");
            dataGridView1.Columns.Add("C5", "Time");
            dataGridView1.Columns.Add("C6", "User");
            Set_DGV_WH(dataGridView1, 1);
            //------------------------------------------------------------
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Add("C1", "ID");
            dataGridView2.Columns.Add("C2", "Name");
            dataGridView2.Columns.Add("C2", "Code");
            dataGridView2.Columns.Add("C3", "Status");
            dataGridView2.Columns.Add("C4", "Date");
            dataGridView2.Columns.Add("C5", "Time");
            dataGridView2.Columns.Add("C6", "User");
            Set_DGV_WH(dataGridView2, 1);
            //------------------------------------------------------------
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Add("C1", "ID");
            dataGridView3.Columns.Add("C2", "Name");
            dataGridView3.Columns.Add("C2", "Code");
            dataGridView3.Columns.Add("C3", "Status");
            dataGridView3.Columns.Add("C4", "Date");
            dataGridView3.Columns.Add("C5", "Time");
            dataGridView3.Columns.Add("C6", "User");
            Set_DGV_WH(dataGridView3, 1);
            //------------------------------------------------------------
            dataGridView4.Rows.Clear();
            dataGridView4.Columns.Add("C1", "Type");
            dataGridView4.Columns.Add("C2", "ID");
            dataGridView4.Columns.Add("C3", "Name");
            dataGridView4.Columns.Add("C4", "Code");
            dataGridView4.Columns.Add("C5", "Status");
            dataGridView4.Columns.Add("C6", "Date");
            dataGridView4.Columns.Add("C7", "Time");
            dataGridView4.Columns.Add("C8", "User");
            Set_DGV_WH(dataGridView4, 2);
            //------------------------------------------------------------
            ChangeDGVBK();
            Reload_Country_T1 = true;
            Reload_Country_T3 = true;
            Reload_State_T3 = true;
            Reload_Type_T3 = true;
            DocTypeFL = false;
            ReloadBtn = false;
            timer1.Interval = 500;
            timer1.Enabled = true;
            timer1.Start();
            metroSetTabControl1.Visible = true;
        }
        //--------------------------------------------------------------------------
        // Needed Function :
        private void Set_DGV_WH(DataGridView DGV, int FillCol)
        {
            try
            {
                DGV.EditMode = DataGridViewEditMode.EditProgrammatically;
                DGV.AllowDrop = false;
                DGV.AllowUserToAddRows = false;
                DGV.AllowUserToDeleteRows = false;
                DGV.AllowUserToOrderColumns = false;
                DGV.AllowUserToResizeColumns = false;
                DGV.AllowUserToResizeRows = false;
                DGV.ReadOnly = true;
                DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                DGV.BorderStyle = BorderStyle.None;
                DGV.BackgroundColor = Color.White;
                DGV.RowHeadersVisible = false;
                DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                DGV.EnableHeadersVisualStyles = true;
                DGV.RowsDefaultCellStyle.BackColor = Color.AliceBlue;
                DGV.RowsDefaultCellStyle.ForeColor = Color.Black;
                DGV.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                DGV.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
                for (int i = 0; i < DGV.ColumnCount; i++) { DGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; if (i != FillCol) { DGV.Columns[i].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; } else { DGV.Columns[i].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft; } }
                DGV.AutoResizeColumnHeadersHeight();
                DGV.AutoResizeColumns();
                DGV.ColumnHeadersHeight = 40;
                DGV.RowTemplate.Height = 30;
                DGV.Columns[FillCol].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception)
            { }
        }
        private void ChangeDGVBK()
        {
            try
            {
                if (metroSetCheckBox1.Checked == true)
                {
                    dataGridView1.BackgroundColor = Color.FromArgb(32, 32, 32);
                    dataGridView2.BackgroundColor = Color.FromArgb(32, 32, 32);
                    dataGridView3.BackgroundColor = Color.FromArgb(32, 32, 32);
                    dataGridView4.BackgroundColor = Color.FromArgb(32, 32, 32);
                }
                else
                {
                    dataGridView1.BackgroundColor = Color.White;
                    dataGridView2.BackgroundColor = Color.White;
                    dataGridView3.BackgroundColor = Color.White;
                    dataGridView4.BackgroundColor = Color.White;
                }
            }
            catch (Exception)
            { }
        }
        private void metroSetCheckBox1_CheckedChanged(object sender)
        {
            ChangeDGVBK();
            if (FristLoad == true) { return; }
            if (metroSetCheckBox1.Checked == true)
            {
                styleManager1.Style = MetroSet_UI.Design.Style.Dark;
            }
            else
            {
                styleManager1.Style = MetroSet_UI.Design.Style.Light;
            }
            Interaction.SaveSetting("IDV", "DocTemp", "DM", metroSetCheckBox1.Checked.ToString().Trim().ToUpper());
        }
        private void Templates_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 950) { this.Width = 950; }
            if (this.Height <= 550) { this.Height = 550; }
            int THISW2 = this.Width - (24 + 24 + 24 + 10);
            CMB_T3_Country.Left = 3;
            CMB_T3_Country.Width = THISW2 / 3;
            CMB_T3_State.Left = CMB_T3_Country.Left + CMB_T3_Country.Width + 10;
            CMB_T3_State.Width = CMB_T3_Country.Width;
            CMB_T3_DocType.Left = CMB_T3_State.Left + CMB_T3_State.Width + 10;
            CMB_T3_DocType.Width = CMB_T3_State.Width;
            metroSetLabel5.Left = CMB_T3_Country.Left + 2;
            metroSetLabel6.Left = CMB_T3_State.Left + 2;
            metroSetLabel7.Left = CMB_T3_DocType.Left + 2;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            try
            {
                metroSetButton4_Click(null, null);
            }
            catch (Exception)
            { }
        }
        //--------------------------------------------------------------------------
        // Tab Panel Activity :
        private void metroSetTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (metroSetTabControl1.SelectedIndex)
                {
                    case 1:
                        {
                            if (Reload_Country_T1 == true)
                            {
                                SystemAuto = true;
                                CMB_T1_C1.Items.Clear();
                                CMB_T1_C1.DisplayMember = "Text";
                                CMB_T1_C1.ValueMember = "Value";
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    CMB_T1_C1.Items.Add(new { Text = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim(), Value = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() });
                                }
                                CMB_T1_C1.Sorted = true;
                                if (CMB_T1_C1.Items.Count > 0) { CMB_T1_C1.SelectedIndex = 0; }
                                Reload_Country_T1 = false;
                                SystemAuto = false;
                                metroSetButton5_Click(null, null);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (DocTypeFL == false) { DocTypeFL = true; metroSetButton9_Click(null, null); }
                            break;
                        }
                    case 3:
                        {
                            if (Reload_Country_T3 == true)
                            {
                                SystemAuto = true;
                                CMB_T3_Country.Items.Clear();
                                CMB_T3_Country.DisplayMember = "Text";
                                CMB_T3_Country.ValueMember = "Value";
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    CMB_T3_Country.Items.Add(new { Text = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim(), Value = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() });
                                }
                                CMB_T3_Country.Sorted = true;
                                if (CMB_T3_Country.Items.Count > 0) { CMB_T3_Country.SelectedIndex = 0; }
                                Reload_Country_T3 = false;
                                Reload_State_T3 = false;
                                SystemAuto = false;
                                CMB_T3_Country_SelectedIndexChanged(null, null);
                            }
                            if (Reload_State_T3 == true)
                            {
                                Reload_State_T3 = false;
                                CMB_T3_Country_SelectedIndexChanged(null, null);
                            }
                            if (Reload_Type_T3 == true)
                            {
                                try { CMB_T3_DocType.Items.Clear(); } catch (Exception) { }
                                try
                                {
                                    SystemAuto = true;
                                    Reload_Type_T3 = false;
                                    metroSetTabControl1.Enabled = false;
                                    WG.Visible = true;
                                    Application.DoEvents();
                                    bool TaskEnded = false;
                                    bool NoErr = false;
                                    string Res = "";
                                    Task t = Task.Run(() =>
                                    {
                                        try
                                        {
                                            string SRVR = ST_Server;
                                            var client = new RestClient(SRVR + "api/" + "DT_14_ReloadDocType" + "?" + "TP=1");
                                            client.Timeout = -1;
                                            var request = new RestRequest(Method.POST);
                                            IRestResponse response = client.Execute(request);
                                            Res = response.Content;
                                            Res = Res.Replace("\\\"", "\"");
                                            if (Res.Substring(0, 1) == "\"") { Res = Res.Substring(1, Res.Length - 1); }
                                            if (Res.Substring(Res.Length - 1, 1) == "\"") { Res = Res.Substring(0, Res.Length - 1); }
                                            NoErr = true;
                                            TaskEnded = true;
                                        }
                                        catch (Exception)
                                        {
                                            NoErr = false;
                                        }
                                    });
                                    while (TaskEnded == false) { Application.DoEvents(); }
                                    try { t.Dispose(); } catch (Exception) { }
                                    DataTable DTLC = (DataTable)JsonConvert.DeserializeObject(Res, (typeof(DataTable)));
                                    CMB_T3_DocType.Items.Clear();
                                    CMB_T3_DocType.DisplayMember = "Text";
                                    CMB_T3_DocType.ValueMember = "Value";
                                    foreach (DataRow RW in DTLC.Rows)
                                    {
                                        CMB_T3_DocType.Items.Add(new { Text = RW[1].ToString().Trim(), Value = RW[0].ToString().Trim() });
                                    }
                                    metroSetTabControl1.Enabled = true;
                                    WG.Visible = false;
                                    if (NoErr == false) { MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload document types, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }
                                catch (Exception)
                                {
                                    metroSetTabControl1.Enabled = true;
                                    WG.Visible = false;
                                    MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload document types, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                metroSetTabControl1.Enabled = true;
                                WG.Visible = false;
                                if (CMB_T3_DocType.Items.Count > 0) { CMB_T3_DocType.SelectedIndex = 0; }
                                SystemAuto = false;
                                metroSetButton13_Click(null, null);
                            }
                            break;
                        }
                }
            }
            catch (Exception)
            { }
        }
        //--------------------------------------------------------------------------
        // Country :
        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                T1_AddNewCountry Frm = new T1_AddNewCountry();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                ReloadBtn = false;
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView1, 1);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton4_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count < 1) { return; }
                this.Enabled = false;
                T1_EditCountry Frm = new T1_EditCountry();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                ReloadBtn = false;
                Frm.Country_ID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Trim();
                Frm.TextBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim();
                Frm.TextBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString().Trim();
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView1, 1);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton4_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        private void metroSetButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count < 1) { return; }
                metroSetTabControl1.Enabled = false;
                Application.DoEvents();
                bool retN = false;
                if (dataGridView1.SelectedRows[0].Cells[3].Value.ToString().Trim().ToLower() == "active")
                {
                    if (MessageBox.Show("Dear User\r\nThe country [ " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim() + " ] status is active now, Do you want to Disable this country ?", "Disable Country", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                else
                {
                    if (MessageBox.Show("Dear User\r\nThe country [ " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString().Trim() + " ] status is disable now, Do you want to Active this country ?", "Enable Country", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                if (retN == false)
                {
                    WG.Visible = true;
                    Application.DoEvents();
                    bool TaskEnded = false;
                    string Res = "";
                    Task t = Task.Run(() =>
                    {
                        try
                        {
                            string SRVR = ST_Server;
                            var client = new RestClient(SRVR + "api/" + "DT_05_StatusCountry" + "?" + "CID=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString().Trim());
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            IRestResponse response = client.Execute(request);
                            Res = response.Content.ToString().Trim();
                            Res = Res.Replace("\"", "");
                            TaskEnded = true;
                        }
                        catch (Exception)
                        {
                            Res = "ERA";
                            TaskEnded = true;
                        }
                    });
                    while (TaskEnded == false) { Application.DoEvents(); }
                    try { t.Dispose(); } catch (Exception) { }
                    string ResL = Res.Replace(" ", "").Replace("-", "").Trim();
                    if (ResL == "ERA")
                    {
                        MessageBox.Show("Dear User\r\nServer return an error to update status of country, After check please try again", "Server Error [Err1]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Res.ToUpper().IndexOf("OK") >= 0)
                        {
                            try
                            {
                                string[] ResSep = Res.Split('-');
                                dataGridView1.SelectedRows[0].Cells[3].Value = ResSep[1].Trim();

                            }
                            catch (Exception)
                            {
                                metroSetButton4_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Dear User\r\nServer return an error to update status of country, After check please try again", "Server Error [Err2]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            { }
            WG.Visible = false;
            metroSetTabControl1.Enabled = true;
        }
        private void metroSetButton4_Click(object sender, EventArgs e)
        {
            try
            {
                metroSetTabControl1.Enabled = false;
                dataGridView1.Rows.Clear();
                Reload_Country_T1 = true;
                Reload_Country_T3 = true;
                WG.Visible = true;
                Application.DoEvents();
                bool TaskEnded = false;
                bool NoErr = false;
                string Res = "";
                Task t = Task.Run(() =>
                {
                    try
                    {
                        string SRVR = ST_Server;
                        var client = new RestClient(SRVR + "api/" + "DT_03_ReloadCountry" + "?" + "TP=1");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);
                        Res = response.Content;
                        Res = Res.Replace("\\\"", "\"");
                        if (Res.Substring(0, 1) == "\"") { Res = Res.Substring(1, Res.Length - 1); }
                        if (Res.Substring(Res.Length - 1, 1) == "\"") { Res = Res.Substring(0, Res.Length - 1); }
                        NoErr = true;
                        TaskEnded = true;
                    }
                    catch (Exception)
                    {
                        NoErr = false;
                    }
                });
                while (TaskEnded == false) { Application.DoEvents(); }
                try { t.Dispose(); } catch (Exception) { }
                DataTable DTLC = (DataTable)JsonConvert.DeserializeObject(Res, (typeof(DataTable)));
                foreach (DataRow RW in DTLC.Rows)
                {
                    dataGridView1.Rows.Add(1);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = RW[0].ToString().Trim();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = RW[1].ToString().Trim();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = RW[2].ToString().Trim();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = RW[3].ToString().Trim();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[4].Value = RW[4].ToString().Trim();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[5].Value = RW[5].ToString().Trim();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[6].Value = RW[6].ToString().Trim();
                }
                Set_DGV_WH(dataGridView1, 1);
                ChangeDGVBK();
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                if (NoErr == false) { MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of countries, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception)
            {
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of countries, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //--------------------------------------------------------------------------
        // State :
        private void CMB_T1_C1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemAuto == true) { return; }
            if (CMB_T1_C1.Items.Count < 1) { return; }
            metroSetButton5_Click(null, null);
        }
        private void metroSetButton5_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if (SystemAuto == true) { return; }
            if (CMB_T1_C1.Items.Count < 1) { return; }
            try
            {
                int CID = int.Parse((CMB_T1_C1.SelectedItem as dynamic).Value);
                if (CID <= 100) { return; }
                metroSetTabControl1.Enabled = false;
                dataGridView2.Rows.Clear();
                Reload_State_T3 = true;
                WG.Visible = true;
                Application.DoEvents();
                bool TaskEnded = false;
                bool NoErr = false;
                string Res = "";
                Task t = Task.Run(() =>
                {
                    try
                    {
                        string SRVR = ST_Server;
                        var client = new RestClient(SRVR + "api/" + "DT_07_ReloadState" + "?" + "CID=" + CID.ToString());
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);
                        Res = response.Content;
                        Res = Res.Replace("\\\"", "\"");
                        if (Res.Substring(0, 1) == "\"") { Res = Res.Substring(1, Res.Length - 1); }
                        if (Res.Substring(Res.Length - 1, 1) == "\"") { Res = Res.Substring(0, Res.Length - 1); }
                        NoErr = true;
                        TaskEnded = true;
                    }
                    catch (Exception)
                    {
                        NoErr = false;
                    }
                });
                while (TaskEnded == false) { Application.DoEvents(); }
                try { t.Dispose(); } catch (Exception) { }
                DataTable DTLC = (DataTable)JsonConvert.DeserializeObject(Res, (typeof(DataTable)));
                foreach (DataRow RW in DTLC.Rows)
                {
                    dataGridView2.Rows.Add(1);
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = RW[0].ToString().Trim();
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = RW[1].ToString().Trim();
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = RW[2].ToString().Trim();
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = RW[3].ToString().Trim();
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = RW[4].ToString().Trim();
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value = RW[5].ToString().Trim();
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[6].Value = RW[6].ToString().Trim();
                }
                Set_DGV_WH(dataGridView2, 1);
                ChangeDGVBK();
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                if (NoErr == false) { MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of states, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception)
            {
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of states, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void metroSetButton8_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                T2_AddNewState Frm = new T2_AddNewState();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                Frm.Country_ID = (CMB_T1_C1.SelectedItem as dynamic).Value;
                ReloadBtn = false;
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView2, 1);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton5_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        private void metroSetButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count < 1) { return; }
                metroSetTabControl1.Enabled = false;
                Application.DoEvents();
                bool retN = false;
                if (dataGridView2.SelectedRows[0].Cells[3].Value.ToString().Trim().ToLower() == "active")
                {
                    if (MessageBox.Show("Dear User\r\nThe state [ " + dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Trim() + " ] status is active now, Do you want to Disable this state ?", "Disable State", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                else
                {
                    if (MessageBox.Show("Dear User\r\nThe state [ " + dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Trim() + " ] status is disable now, Do you want to Active this state ?", "Enable State", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                if (retN == false)
                {
                    WG.Visible = true;
                    Application.DoEvents();
                    bool TaskEnded = false;
                    string Res = "";
                    Task t = Task.Run(() =>
                    {
                        try
                        {
                            string SRVR = ST_Server;
                            var client = new RestClient(SRVR + "api/" + "DT_09_StatusState" + "?" + "SID=" + dataGridView2.SelectedRows[0].Cells[0].Value.ToString().Trim());
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            IRestResponse response = client.Execute(request);
                            Res = response.Content.ToString().Trim();
                            Res = Res.Replace("\"", "");
                            TaskEnded = true;
                        }
                        catch (Exception)
                        {
                            Res = "ERA";
                            TaskEnded = true;
                        }
                    });
                    while (TaskEnded == false) { Application.DoEvents(); }
                    try { t.Dispose(); } catch (Exception) { }
                    string ResL = Res.Replace(" ", "").Replace("-", "").Trim();
                    if (ResL == "ERA")
                    {
                        MessageBox.Show("Dear User\r\nServer return an error to update status of state, After check please try again", "Server Error [Err1]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Res.ToUpper().IndexOf("OK") >= 0)
                        {
                            try
                            {
                                string[] ResSep = Res.Split('-');
                                dataGridView2.SelectedRows[0].Cells[3].Value = ResSep[1].Trim();

                            }
                            catch (Exception)
                            {
                                metroSetButton5_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Dear User\r\nServer return an error to update status of state, After check please try again", "Server Error [Err2]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            { }
            WG.Visible = false;
            metroSetTabControl1.Enabled = true;
        }
        private void metroSetButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count < 1) { return; }
                this.Enabled = false;
                T2_EditSate Frm = new T2_EditSate();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                ReloadBtn = false;
                Frm.State_ID = dataGridView2.SelectedRows[0].Cells[0].Value.ToString().Trim();
                Frm.TextBox1.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString().Trim();
                Frm.TextBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString().Trim();
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView2, 1);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton5_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        //--------------------------------------------------------------------------
        // Document Type :
        private void metroSetButton12_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                T3_AddNewDocType Frm = new T3_AddNewDocType();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                ReloadBtn = false;
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView3, 1);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton9_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        private void metroSetButton11_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.Rows.Count < 1) { return; }
                this.Enabled = false;
                T3_EditDocType Frm = new T3_EditDocType();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                ReloadBtn = false;
                Frm.DocymentType_ID = dataGridView3.SelectedRows[0].Cells[0].Value.ToString().Trim();
                Frm.TextBox1.Text = dataGridView3.SelectedRows[0].Cells[1].Value.ToString().Trim();
                Frm.TextBox2.Text = dataGridView3.SelectedRows[0].Cells[2].Value.ToString().Trim();
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView3, 1);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton9_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        private void metroSetButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.Rows.Count < 1) { return; }
                metroSetTabControl1.Enabled = false;
                Application.DoEvents();
                bool retN = false;
                if (dataGridView3.SelectedRows[0].Cells[3].Value.ToString().Trim().ToLower() == "active")
                {
                    if (MessageBox.Show("Dear User\r\nThe document type [ " + dataGridView3.SelectedRows[0].Cells[1].Value.ToString().Trim() + " ] status is active now, Do you want to Disable this type ?", "Disable Document Type", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                else
                {
                    if (MessageBox.Show("Dear User\r\nThe document type [ " + dataGridView3.SelectedRows[0].Cells[1].Value.ToString().Trim() + " ] status is disable now, Do you want to Active this type ?", "Enable Document Type", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                if (retN == false)
                {
                    WG.Visible = true;
                    Application.DoEvents();
                    bool TaskEnded = false;
                    string Res = "";
                    Task t = Task.Run(() =>
                    {
                        try
                        {
                            string SRVR = ST_Server;
                            var client = new RestClient(SRVR + "api/" + "DT_13_StatusDocumentType" + "?" + "DID=" + dataGridView3.SelectedRows[0].Cells[0].Value.ToString().Trim());
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            IRestResponse response = client.Execute(request);
                            Res = response.Content.ToString().Trim();
                            Res = Res.Replace("\"", "");
                            TaskEnded = true;
                        }
                        catch (Exception)
                        {
                            Res = "ERA";
                            TaskEnded = true;
                        }
                    });
                    while (TaskEnded == false) { Application.DoEvents(); }
                    try { t.Dispose(); } catch (Exception) { }
                    string ResL = Res.Replace(" ", "").Replace("-", "").Trim();
                    if (ResL == "ERA")
                    {
                        MessageBox.Show("Dear User\r\nServer return an error to update status of document type, After check please try again", "Server Error [Err1]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Res.ToUpper().IndexOf("OK") >= 0)
                        {
                            try
                            {
                                string[] ResSep = Res.Split('-');
                                dataGridView3.SelectedRows[0].Cells[3].Value = ResSep[1].Trim();

                            }
                            catch (Exception)
                            {
                                metroSetButton4_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Dear User\r\nServer return an error to update status of document type, After check please try again", "Server Error [Err2]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            { }
            WG.Visible = false;
            metroSetTabControl1.Enabled = true;
        }
        private void metroSetButton9_Click(object sender, EventArgs e)
        {
            try
            {
                metroSetTabControl1.Enabled = false;
                dataGridView3.Rows.Clear();
                Reload_Type_T3 = true;
                WG.Visible = true;
                Application.DoEvents();
                bool TaskEnded = false;
                bool NoErr = false;
                string Res = "";
                Task t = Task.Run(() =>
                {
                    try
                    {
                        string SRVR = ST_Server;
                        var client = new RestClient(SRVR + "api/" + "DT_11_ReloadDocumentType" + "?" + "TP=1");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);
                        Res = response.Content;
                        Res = Res.Replace("\\\"", "\"");
                        if (Res.Substring(0, 1) == "\"") { Res = Res.Substring(1, Res.Length - 1); }
                        if (Res.Substring(Res.Length - 1, 1) == "\"") { Res = Res.Substring(0, Res.Length - 1); }
                        NoErr = true;
                        TaskEnded = true;
                    }
                    catch (Exception)
                    {
                        NoErr = false;
                    }
                });
                while (TaskEnded == false) { Application.DoEvents(); }
                try { t.Dispose(); } catch (Exception) { }
                DataTable DTLC = (DataTable)JsonConvert.DeserializeObject(Res, (typeof(DataTable)));
                foreach (DataRow RW in DTLC.Rows)
                {
                    dataGridView3.Rows.Add(1);
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[0].Value = RW[0].ToString().Trim();
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[1].Value = RW[1].ToString().Trim();
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[2].Value = RW[2].ToString().Trim();
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[3].Value = RW[3].ToString().Trim();
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[4].Value = RW[4].ToString().Trim();
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[5].Value = RW[5].ToString().Trim();
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[6].Value = RW[6].ToString().Trim();
                }
                Set_DGV_WH(dataGridView3, 1);
                ChangeDGVBK();
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                if (NoErr == false) { MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of document types, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception)
            {
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of document types, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //--------------------------------------------------------------------------
        // Documents :
        private void CMB_T3_Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            try { CMB_T3_State.Items.Clear(); } catch (Exception) { }
            if (SystemAuto == true) { return; }
            if (CMB_T3_Country.Items.Count < 1) { return; }
            SystemAuto = true;
            metroSetTabControl1.Enabled = false;
            WG.Visible = true;
            Application.DoEvents();
            try { CMB_T3_State.Items.Clear(); } catch (Exception) { }
            string CID = "0";
            try { CID = (CMB_T3_Country.SelectedItem as dynamic).Value; } catch (Exception) { }
            try
            {
                bool TaskEnded = false;
                bool NoErr = false;
                string Res = "";
                Task t = Task.Run(() =>
                {
                    try
                    {
                        string SRVR = ST_Server;
                        var client = new RestClient(SRVR + "api/" + "DT_15_ReloadStateCMB" + "?" + "CID=" + CID);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);
                        Res = response.Content;
                        Res = Res.Replace("\\\"", "\"");
                        if (Res.Substring(0, 1) == "\"") { Res = Res.Substring(1, Res.Length - 1); }
                        if (Res.Substring(Res.Length - 1, 1) == "\"") { Res = Res.Substring(0, Res.Length - 1); }
                        NoErr = true;
                        TaskEnded = true;
                    }
                    catch (Exception)
                    {
                        NoErr = false;
                    }
                });
                while (TaskEnded == false) { Application.DoEvents(); }
                try { t.Dispose(); } catch (Exception) { }
                DataTable DTLC = (DataTable)JsonConvert.DeserializeObject(Res, (typeof(DataTable)));
                CMB_T3_State.Items.Clear();
                CMB_T3_State.DisplayMember = "Text";
                CMB_T3_State.ValueMember = "Value";
                foreach (DataRow RW in DTLC.Rows)
                {
                    CMB_T3_State.Items.Add(new { Text = RW[1].ToString().Trim(), Value = RW[0].ToString().Trim() });
                }
                if (NoErr == false) { MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload states, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception)
            {
                MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload states, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            metroSetTabControl1.Enabled = true;
            WG.Visible = false;
            CMB_T3_State.Sorted = true;
            if (CMB_T3_State.Items.Count > 0) { CMB_T3_State.SelectedIndex = 0; }
            Reload_State_T3 = false;
            SystemAuto = false;
            CMB_T3_State_SelectedIndexChanged(null, null);
        }
        private void CMB_T3_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            if (SystemAuto == true) { return; }
            if (CMB_T3_State.Items.Count < 1) { return; }
            metroSetButton13_Click(null, null);
        }
        private void CMB_T3_DocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            if (SystemAuto == true) { return; }
            if (CMB_T3_DocType.Items.Count < 1) { return; }
            metroSetButton13_Click(null, null);
        }
        private void metroSetButton13_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Clear();
            if (SystemAuto == true) { return; }
            if (CMB_T3_Country.Items.Count < 1) { return; }
            if (CMB_T3_State.Items.Count < 1) { return; }
            if (CMB_T3_DocType.Items.Count < 1) { return; }
            if (Reload_Country_T3 == true) { return; }
            if (Reload_State_T3 == true) { return; }
            if (Reload_Type_T3 == true) { return; }
            try
            {
                dataGridView4.Rows.Clear();
                metroSetTabControl1.Enabled = false;
                WG.Visible = true;
                Application.DoEvents();
                bool TaskEnded = false;
                bool NoErr = false;
                string Res = "";
                string CID = "0";
                string SID = "0";
                string DTID = "0";
                try { CID = (CMB_T3_Country.SelectedItem as dynamic).Value; } catch (Exception) { }
                try { SID = (CMB_T3_State.SelectedItem as dynamic).Value; } catch (Exception) { }
                try { DTID = (CMB_T3_DocType.SelectedItem as dynamic).Value; } catch (Exception) { }
                Task t = Task.Run(() =>
                {
                    try
                    {
                        string SRVR = ST_Server;
                        var client = new RestClient(SRVR + "api/" + "DT_17_ReloadDocument" + "?" + "CID=" + CID + "&SID=" + SID + "&DTID=" + DTID);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);
                        Res = response.Content;
                        Res = Res.Replace("\\\"", "\"");
                        if (Res.Substring(0, 1) == "\"") { Res = Res.Substring(1, Res.Length - 1); }
                        if (Res.Substring(Res.Length - 1, 1) == "\"") { Res = Res.Substring(0, Res.Length - 1); }
                        NoErr = true;
                        TaskEnded = true;
                    }
                    catch (Exception)
                    {
                        NoErr = false;
                    }
                });
                while (TaskEnded == false) { Application.DoEvents(); }
                try { t.Dispose(); } catch (Exception) { }
                DataTable DTLC = (DataTable)JsonConvert.DeserializeObject(Res, (typeof(DataTable)));
                foreach (DataRow RW in DTLC.Rows)
                {
                    dataGridView4.Rows.Add(1);
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[0].Value = RW[0].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[1].Value = RW[1].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[2].Value = RW[2].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[3].Value = RW[3].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[4].Value = RW[4].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[5].Value = RW[5].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[6].Value = RW[6].ToString().Trim();
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].Cells[7].Value = RW[7].ToString().Trim();
                }
                Set_DGV_WH(dataGridView4, 2);
                ChangeDGVBK();
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                if (NoErr == false) { MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of documents, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception)
            {
                metroSetTabControl1.Enabled = true;
                WG.Visible = false;
                MessageBox.Show("Dear User ...\r\nThere has been a disruption during the reload table of documents, please try again", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void metroSetButton14_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.Rows.Count < 1) { return; }
                metroSetTabControl1.Enabled = false;
                Application.DoEvents();
                bool retN = false;
                if (dataGridView4.SelectedRows[0].Cells[4].Value.ToString().Trim().ToLower() == "active")
                {
                    if (MessageBox.Show("Dear User\r\nThe document [ " + dataGridView4.SelectedRows[0].Cells[2].Value.ToString().Trim() + " ] status is active now, Do you want to Disable this document ?", "Disable Document", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                else
                {
                    if (MessageBox.Show("Dear User\r\nThe document [ " + dataGridView4.SelectedRows[0].Cells[2].Value.ToString().Trim() + " ] status is disable now, Do you want to Active this document ?", "Enable Document", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) { retN = true; }
                }
                if (retN == false)
                {
                    WG.Visible = true;
                    Application.DoEvents();
                    bool TaskEnded = false;
                    string Res = "";
                    Task t = Task.Run(() =>
                    {
                        try
                        {
                            string SRVR = ST_Server;
                            var client = new RestClient(SRVR + "api/" + "DT_19_StatusDocument" + "?" + "DID=" + dataGridView4.SelectedRows[0].Cells[1].Value.ToString().Trim());
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            IRestResponse response = client.Execute(request);
                            Res = response.Content.ToString().Trim();
                            Res = Res.Replace("\"", "");
                            TaskEnded = true;
                        }
                        catch (Exception)
                        {
                            Res = "ERA";
                            TaskEnded = true;
                        }
                    });
                    while (TaskEnded == false) { Application.DoEvents(); }
                    try { t.Dispose(); } catch (Exception) { }
                    string ResL = Res.Replace(" ", "").Replace("-", "").Trim();
                    if (ResL == "ERA")
                    {
                        MessageBox.Show("Dear User\r\nServer return an error to update status of document, After check please try again", "Server Error [Err1]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (Res.ToUpper().IndexOf("OK") >= 0)
                        {
                            try
                            {
                                string[] ResSep = Res.Split('-');
                                dataGridView4.SelectedRows[0].Cells[4].Value = ResSep[1].Trim();

                            }
                            catch (Exception)
                            {
                                metroSetButton13_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Dear User\r\nServer return an error to update status of document, After check please try again", "Server Error [Err2]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception)
            { }
            WG.Visible = false;
            metroSetTabControl1.Enabled = true;
        }
        private void metroSetButton16_Click(object sender, EventArgs e)
        {
            try
            {
                string CID = "0";
                string SID = "0";
                string DTID = "0";
                try { CID = (CMB_T3_Country.SelectedItem as dynamic).Value; } catch (Exception) { }
                try { SID = (CMB_T3_State.SelectedItem as dynamic).Value; } catch (Exception) { }
                try { DTID = (CMB_T3_DocType.SelectedItem as dynamic).Value; } catch (Exception) { }
                if (CID == "0") { MessageBox.Show("Dear User\r\nServer return an error to add new document, No country founded", "Country Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (SID == "0") { MessageBox.Show("Dear User\r\nServer return an error to add new document, No state founded", "State Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (DTID == "0") { MessageBox.Show("Dear User\r\nServer return an error to add new document, No document type founded", "Document Type Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                this.Enabled = false;
                T4_AddNewDocument Frm = new T4_AddNewDocument();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                Frm.CID = CID;
                Frm.SID = SID;
                Frm.DTID = DTID;
                ReloadBtn = false;
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView4, 2);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton13_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        private void metroSetButton15_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.Rows.Count < 1) { return; }
                string CID = "0";
                string SID = "0";
                string DTID = "0";
                try { CID = (CMB_T3_Country.SelectedItem as dynamic).Value; } catch (Exception) { }
                try { SID = (CMB_T3_State.SelectedItem as dynamic).Value; } catch (Exception) { }
                try { DTID = (CMB_T3_DocType.SelectedItem as dynamic).Value; } catch (Exception) { }
                if (CID == "0") { MessageBox.Show("Dear User\r\nServer return an error to edit document, No country founded", "Country Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (SID == "0") { MessageBox.Show("Dear User\r\nServer return an error to edit document, No state founded", "State Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (DTID == "0") { MessageBox.Show("Dear User\r\nServer return an error to edit document, No document type founded", "Document Type Not Founded", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                this.Enabled = false;
                T4_EditDocument Frm = new T4_EditDocument();
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ParetnFrm = this;
                Frm.CID = CID;
                Frm.SID = SID;
                Frm.DTID = DTID;
                Frm.DID = dataGridView4.SelectedRows[0].Cells[1].Value.ToString().Trim();
                Frm.TextBox1.Text = dataGridView4.SelectedRows[0].Cells[2].Value.ToString().Trim();
                Frm.TextBox2.Text = dataGridView4.SelectedRows[0].Cells[3].Value.ToString().Trim();
                ReloadBtn = false;
                Frm.ShowDialog();
                Set_DGV_WH(dataGridView4, 2);
                ChangeDGVBK();
                this.Enabled = true;
                if (ReloadBtn == true) { metroSetButton13_Click(null, null); }
            }
            catch (Exception)
            { }
        }
        //--------------------------------------------------------------------------
        // Wizard :
        private void metroSetButton17_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.Rows.Count < 1) { return; }
                string DID = dataGridView4.SelectedRows[0].Cells[1].Value.ToString().Trim();
                if (DID.Trim() == "") { return; }
                if (DID.Trim() == "0") { return; }
                TemplateBuilder Frm = new TemplateBuilder();
                Frm.ST_Server = ST_Server;
                Frm.ST_UID = ST_UID;
                Frm.DocumentID = DID;
                Frm.styleManager1.Style = styleManager1.Style;
                Frm.ShowDialog();
            }
            catch (Exception)
            { } 
        }
        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                metroSetButton17_Click(null, null);
            }
            catch (Exception)
            {}
        }
        //--------------------------------------------------------------------------
    }
}
