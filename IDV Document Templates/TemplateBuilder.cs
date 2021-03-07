using MetroSet_UI.Forms;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace IDV_Document_Templates
{
    public partial class TemplateBuilder : MetroSetForm
    {
        private class TemplateBasicConfig
        {
            public string V1 { get; set; }
            public string V2 { get; set; }
            public string V3 { get; set; }
            public string V4 { get; set; }
            public string V5 { get; set; }
            public string V6 { get; set; }
            public string V7 { get; set; }
            public string V8 { get; set; }
            public string V9 { get; set; }
            public string V10 { get; set; }
            public string V11 { get; set; }
            public string V12 { get; set; }
            public string V13 { get; set; }
            public string V14 { get; set; }
            public string V15 { get; set; }
            public string V16 { get; set; }
            public string V17 { get; set; }
            public string V18 { get; set; }
            public string V19 { get; set; }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Public Variable :
        public string DocumentID = "0";
        string BaseData_CountryCode = "0";
        string BaseData_CountryName = "";
        string BaseData_StateCode = "0";
        string BaseData_StateName = "";
        string BaseData_DocumentTypeCode = "0";
        string BaseData_DocumentTypeName = "";
        string BaseData_DocumentCode = "0";
        string BaseData_DocumentName = "";
        string BaseData_Ins_User_Fullname = "";
        string BaseData_Ins_User_Date = "";
        string BaseData_Ins_User_Time = "";
        string BaseData_Update_User_Fullname = "";
        string BaseData_Update_User_Date = "";
        string BaseData_Update_User_Time = "";
        const int PageCounter = 8;
        int PageNumber = 0;
        bool FirstLoad = false;
        bool ObjectLoad = false;
        TransparentLabel P5_SelObject;
        TransparentLabel P6_SelObject;
        TransparentColor P7_SelObject;
        TransparentColor P8_SelObject;
        private Point MouseDownLocation;
        int FICounter = 0;
        int BICounter = 0;
        int FIColor = 0;
        int BIColor = 0;
        Size P5_SelObj_Size = new Size(0, 0);
        Size P6_SelObj_Size = new Size(0, 0);
        Point P5_SelObj_Location = new Point(0, 0);
        Point P6_SelObj_Location = new Point(0, 0);
        bool P7_AddNew = false;
        bool P8_AddNew = false;
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        public string ST_UID = "";
        public string ST_Server = "";
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Pre Init :
        public TemplateBuilder()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Form Function :
        private void Template_Wizard_Load(object sender, EventArgs e)
        {
            try
            {
                FirstLoad = true;
                this.Width = 1030;
                this.Height = 550;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.WindowState = FormWindowState.Maximized;
                ClearAll(1);
                timer1.Interval = 1000;
                timer1.Enabled = true;
                timer1.Start();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TemplateBuilder_Resize(object sender, EventArgs e)
        {
            if (this.Width <= 1030) { this.Width = 1030; }
            if (this.Height <= 550) { this.Height = 550; }
            if (PNL_1.Visible == true)
            {
                PNL_1.Left = (this.Width / 2) - (PNL_1.Width / 2);
                PNL_1.Top = (this.Height / 2) - (PNL_1.Height / 2);
            }
            if (PNL_2.Visible == true)
            {
                PNL_2.Left = 15;
                PNL_2.Top = 130;
                PNL_2.Width = this.Width - 30;
                PNL_2.Height = metroSetDivider3.Top - 136;
            }
            if (PNL_3.Visible == true)
            {
                PNL_3.Left = 15;
                PNL_3.Top = 130;
                PNL_3.Width = this.Width - 30;
                PNL_3.Height = metroSetDivider3.Top - 136;
            }
            if (PNL_4.Visible == true)
            {
                PNL_4.Left = 15;
                PNL_4.Top = 130;
                PNL_4.Width = this.Width - 30;
                PNL_4.Height = metroSetDivider3.Top - 136;
            }
            if (PNL_5.Visible == true)
            {
                PNL_5.Left = 15;
                PNL_5.Top = 130;
                PNL_5.Width = this.Width - 30;
                PNL_5.Height = metroSetDivider3.Top - 136;
            }
            if (PNL_6.Visible == true)
            {
                PNL_6.Left = 15;
                PNL_6.Top = 130;
                PNL_6.Width = this.Width - 30;
                PNL_6.Height = metroSetDivider3.Top - 136;
            }
            if (PNL_7.Visible == true)
            {
                PNL_7.Left = 15;
                PNL_7.Top = 130;
                PNL_7.Width = this.Width - 30;
                PNL_7.Height = metroSetDivider3.Top - 136;
            }
            if (PNL_8.Visible == true)
            {
                PNL_8.Left = 15;
                PNL_8.Top = 130;
                PNL_8.Width = this.Width - 30;
                PNL_8.Height = metroSetDivider3.Top - 136;
            }
        }
        //==========================================================================
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                FirstLoad = true;
                timer1.Stop();
                ClearAll(2);
                Show_Page(1);
                PNL_1_LBL_1.Text = "Reviewing template data";
                LBL_Country_V.Text = "Reviewing information";
                LBL_State_V.Text = "Reviewing information";
                LBL_DocumentType_V.Text = "Reviewing information";
                LBL_Document_V.Text = "Reviewing information";
                PNL_1_PB1.Value = 0;
                PNL_1_PB1.Maximum = 8;
                Application.DoEvents();
                bool TaskEnded = false;
                string ResposnResult = "";
                // Get Document Information :
                ResposnResult = "";
                TaskEnded = false;
                Task t1 = Task.Run(() =>
                {
                    try
                    {
                        string SRVR = ST_Server;
                        var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_20_GetDocumentInformation" + "?" + "DID=" + DocumentID);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        IRestResponse response = client.Execute(request);
                        ResposnResult = response.Content.ToString().Trim();
                        ResposnResult = ResposnResult.Replace("\"", "").Trim();
                        TaskEnded = true;
                    }
                    catch (Exception)
                    {
                        ResposnResult = "ERA";
                        TaskEnded = true;
                    }
                });
                while (TaskEnded == false) { Application.DoEvents(); }
                try { t1.Dispose(); } catch (Exception) { }
                if (ResposnResult != "ERA")
                {
                    string[] ResSep = ResposnResult.Split('_');
                    BaseData_CountryCode = ResSep[1];
                    BaseData_CountryName = ResSep[2];
                    BaseData_StateCode = ResSep[3];
                    BaseData_StateName = ResSep[4];
                    BaseData_DocumentTypeCode = ResSep[5];
                    BaseData_DocumentTypeName = ResSep[6];
                    BaseData_DocumentCode = ResSep[7];
                    BaseData_DocumentName = ResSep[8];
                    LBL_Country_V.Text = BaseData_CountryName;
                    LBL_State_V.Text = BaseData_StateName;
                    LBL_DocumentType_V.Text = BaseData_DocumentTypeName;
                    LBL_Document_V.Text = BaseData_DocumentName;
                    Relocate_LBL();
                    PNL_1_PB1.Value += 1;
                    Application.DoEvents();
                    // Get Latest Design :
                    PNL_1_LBL_1.Text = "Reviewing the latest design";
                    Application.DoEvents();
                    ResposnResult = "";
                    TaskEnded = false;
                    Task t2 = Task.Run(() =>
                    {
                        try
                        {
                            string SRVR = ST_Server;
                            var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_21_LoadTemplate" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            IRestResponse response = client.Execute(request);
                            ResposnResult = response.Content.ToString().Trim();
                            ResposnResult = ResposnResult.Replace("\"", "").Trim();
                            TaskEnded = true;
                        }
                        catch (Exception)
                        {
                            ResposnResult = "ERA";
                            TaskEnded = true;
                        }
                    });
                    while (TaskEnded == false) { Application.DoEvents(); }
                    try { t2.Dispose(); } catch (Exception) { }
                    PNL_1_PB1.Value += 1;
                    Application.DoEvents();
                    if (ResposnResult != "ERA")
                    {
                        // Get Base Insert Template Data :
                        string[] ResSep2 = ResposnResult.Split('_');
                        BaseData_Ins_User_Fullname = ResSep2[1];
                        BaseData_Ins_User_Date = ResSep2[2];
                        BaseData_Ins_User_Time = ResSep2[3];
                        BaseData_Update_User_Fullname = ResSep2[4];
                        BaseData_Update_User_Date = ResSep2[5];
                        BaseData_Update_User_Time = ResSep2[6];
                        P2_UI1.Text = BaseData_Ins_User_Fullname.Trim();
                        P2_UI2.Text = BaseData_Ins_User_Date.Trim() + " - " + BaseData_Ins_User_Time.Trim();
                        P2_UI3.Text = BaseData_Update_User_Fullname.Trim();
                        P2_UI4.Text = BaseData_Update_User_Date.Trim() + " - " + BaseData_Update_User_Time.Trim();
                        P2_UI1.Visible = true;
                        P2_UI1H.Visible = true;
                        P2_UI2.Visible = true;
                        P2_UI2H.Visible = true;
                        P2_UI3.Visible = true;
                        P2_UI3H.Visible = true;
                        P2_UI4.Visible = true;
                        P2_UI4H.Visible = true;
                        // Get Basic Configuration :
                        PNL_1_LBL_1.Text = "Reviewing configuration";
                        Application.DoEvents();
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t3 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_22_BasicConfigurationLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                ResposnResult = response.Content.ToString().Trim();
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                ResposnResult = "ERA";
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t3.Dispose(); } catch (Exception) { }
                        if (ResposnResult.Substring(0, 1) == "\"") { ResposnResult = ResposnResult.Substring(1, ResposnResult.Length - 1).Trim(); }
                        if (ResposnResult.Substring(ResposnResult.Length - 1, 1) == "\"") { ResposnResult = ResposnResult.Substring(0, ResposnResult.Length - 1).Trim(); }
                        ResposnResult = ResposnResult.Replace("\\", "");
                        if (ResposnResult != "ERA")
                        {
                            try
                            {
                                TemplateBasicConfig TBI = new TemplateBasicConfig();
                                TBI = Newtonsoft.Json.JsonConvert.DeserializeObject<TemplateBasicConfig>(ResposnResult);
                                P2_T1.Text = TBI.V1.Trim();
                                P2_T2.Text = TBI.V2.Trim();
                                P2_T3.Text = TBI.V3.Trim();
                                P2_S1.Switched = DigittoBool(TBI.V4);
                                P2_S2.Switched = DigittoBool(TBI.V5);
                                P2_S3.Switched = DigittoBool(TBI.V6);
                                P2_S4.Switched = DigittoBool(TBI.V7);
                                P2_S5.Switched = DigittoBool(TBI.V8);
                                P2_S6.Switched = DigittoBool(TBI.V9);
                                P2_S7.Switched = DigittoBool(TBI.V10);
                                P2_S8.Switched = DigittoBool(TBI.V11);
                                P2_S9.Switched = DigittoBool(TBI.V12);
                                P2_S10.Switched = DigittoBool(TBI.V13);
                                P2_S11.Switched = DigittoBool(TBI.V14);
                                P2_S12.Switched = DigittoBool(TBI.V15);
                                P2_S13.Switched = DigittoBool(TBI.V16);
                                P2_S14.Switched = DigittoBool(TBI.V17);
                                P2_T4.Text = TBI.V18.Trim();
                                P2_T5.Text = TBI.V19.Trim();
                            }
                            catch (Exception)
                            { }
                        }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Get Front Image :
                        PNL_1_LBL_1.Text = "Loading front image data";
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t4 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_23_FrontImageLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                MemoryStream ms = new MemoryStream(response.RawBytes, 0, response.RawBytes.Length);
                                ms.Write(response.RawBytes, 0, response.RawBytes.Length);
                                P3_Pic.Image = System.Drawing.Image.FromStream(ms, true);
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                P3_Pic.Image = null;
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t4.Dispose(); } catch (Exception) { }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Get Back Image :
                        PNL_1_LBL_1.Text = "Loading back image data";
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t5 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_24_BackImageLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                MemoryStream ms = new MemoryStream(response.RawBytes, 0, response.RawBytes.Length);
                                ms.Write(response.RawBytes, 0, response.RawBytes.Length);
                                P4_Pic.Image = System.Drawing.Image.FromStream(ms, true);
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                P4_Pic.Image = null;
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t5.Dispose(); } catch (Exception) { }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Load Front Image Elements :
                        PNL_1_LBL_1.Text = "Loading front image elements";
                        Application.DoEvents();
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t6 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_25_FIElementsLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                ResposnResult = response.Content.ToString().Trim();
                                if (ResposnResult.Substring(0, 1) == "\"") { ResposnResult = ResposnResult.Substring(1, ResposnResult.Length - 1).Trim(); }
                                if (ResposnResult.Substring(ResposnResult.Length - 1, 1) == "\"") { ResposnResult = ResposnResult.Substring(0, ResposnResult.Length - 1).Trim(); }
                                ResposnResult = ResposnResult.Replace("\\", "");
                                DataTable DTFIE = new DataTable();
                                DTFIE = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ResposnResult);
                                if (DTFIE.Rows != null)
                                {
                                    foreach (DataRow RW in DTFIE.Rows)
                                    {
                                        try
                                        {
                                            FICounter++;
                                            FI_TL.Parent = P5_IMG;
                                            FI_TR.Parent = P5_IMG;
                                            FI_BL.Parent = P5_IMG;
                                            FI_BR.Parent = P5_IMG;
                                            TransparentLabel LB = new TransparentLabel();
                                            LB.Name = "FI" + FICounter.ToString();
                                            LB.Visible = false;
                                            LB.Parent = P5_IMG;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.AutoSize = false;
                                            LB.Top = 0;
                                            LB.Left = 0;
                                            LB.Width = 10;
                                            LB.Height = 10;
                                            LB.Top = int.Parse(RW[1].ToString());
                                            LB.Left = int.Parse(RW[0].ToString());
                                            LB.Width = int.Parse(RW[8].ToString());
                                            LB.Height = int.Parse(RW[9].ToString());
                                            LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                                            LB.BackColor = Color.Transparent;
                                            LB.Cursor = Cursors.Default;
                                            LB.BringToFront();
                                            LB.OutputShow = false;
                                            LB.OutputTitle = "";
                                            LB.KeyActive = false;
                                            LB.KeyValue = "";
                                            LB.Similarity = "100";
                                            LB.OutputShow = DigittoBool(RW[10].ToString());
                                            LB.OutputTitle = RW[11].ToString();
                                            LB.KeyActive = DigittoBool(RW[12].ToString());
                                            LB.KeyValue = RW[13].ToString();
                                            LB.Similarity = RW[14].ToString();
                                            LB.OcrIndex = RW[15].ToString();
                                            LB.OcrPosition = RW[16].ToString();
                                            try
                                            {
                                                LB.DataProcessing = DigittoBool(RW[17].ToString());
                                                LB.TypeCode = int.Parse(RW[18].ToString());
                                                LB.SubstringStart = int.Parse(RW[19].ToString());
                                                LB.SubstringLength = int.Parse(RW[20].ToString());
                                                LB.SubstringLeft = DigittoBool(RW[21].ToString());
                                                LB.InputFormat = RW[22].ToString();
                                                LB.InputFormatSeprator = RW[23].ToString();
                                                LB.OutputFormat = RW[24].ToString();
                                                LB.OutputFormatSeprator = RW[25].ToString();
                                            }
                                            catch (Exception) { }
                                            LB.Text = "";
                                            LB.ForeColor = Color.Black;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.Click += new EventHandler(this.Trp_LB_Click);
                                            LB.MouseDown += new MouseEventHandler(this.TraLB_MouseDown);
                                            LB.MouseMove += new MouseEventHandler(this.TraLB_MouseMove);
                                            LB.MouseUp += new MouseEventHandler(this.TraLB_MouseUp);
                                            LB.Visible = true;
                                        }
                                        catch (Exception)
                                        { }
                                        Application.DoEvents();
                                    }
                                }
                                P5_Pan_Click(null, null);
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                ResposnResult = "ERA";
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t6.Dispose(); } catch (Exception) { }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Load Back Image Elements :
                        PNL_1_LBL_1.Text = "Loading back image elements";
                        Application.DoEvents();
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t7 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_26_BIElementsLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                ResposnResult = response.Content.ToString().Trim();
                                if (ResposnResult.Substring(0, 1) == "\"") { ResposnResult = ResposnResult.Substring(1, ResposnResult.Length - 1).Trim(); }
                                if (ResposnResult.Substring(ResposnResult.Length - 1, 1) == "\"") { ResposnResult = ResposnResult.Substring(0, ResposnResult.Length - 1).Trim(); }
                                ResposnResult = ResposnResult.Replace("\\", "");
                                DataTable DTFIE = new DataTable();
                                DTFIE = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ResposnResult);
                                if (DTFIE.Rows != null)
                                {
                                    foreach (DataRow RW in DTFIE.Rows)
                                    {
                                        try
                                        {
                                            BICounter++;
                                            BI_TL.Parent = P6_IMG;
                                            BI_TR.Parent = P6_IMG;
                                            BI_BL.Parent = P6_IMG;
                                            BI_BR.Parent = P6_IMG;
                                            TransparentLabel LB = new TransparentLabel();
                                            LB.Name = "BI" + BICounter.ToString();
                                            LB.Visible = false;
                                            LB.Parent = P6_IMG;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.AutoSize = false;
                                            LB.Top = 0;
                                            LB.Left = 0;
                                            LB.Width = 10;
                                            LB.Height = 10;
                                            LB.Top = int.Parse(RW[1].ToString());
                                            LB.Left = int.Parse(RW[0].ToString());
                                            LB.Width = int.Parse(RW[8].ToString());
                                            LB.Height = int.Parse(RW[9].ToString());
                                            LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                                            LB.BackColor = Color.Transparent;
                                            LB.Cursor = Cursors.Default;
                                            LB.BringToFront();
                                            LB.OutputShow = false;
                                            LB.OutputTitle = "";
                                            LB.KeyActive = false;
                                            LB.KeyValue = "";
                                            LB.Similarity = "100";
                                            LB.OutputShow = DigittoBool(RW[10].ToString());
                                            LB.OutputTitle = RW[11].ToString();
                                            LB.KeyActive = DigittoBool(RW[12].ToString());
                                            LB.KeyValue = RW[13].ToString();
                                            LB.Similarity = RW[14].ToString();
                                            LB.OcrIndex = RW[15].ToString();
                                            LB.OcrPosition = RW[16].ToString();
                                            try
                                            {
                                                LB.DataProcessing = DigittoBool(RW[17].ToString());
                                                LB.TypeCode = int.Parse(RW[18].ToString());
                                                LB.SubstringStart = int.Parse(RW[19].ToString());
                                                LB.SubstringLength = int.Parse(RW[20].ToString());
                                                LB.SubstringLeft = DigittoBool(RW[21].ToString());
                                                LB.InputFormat = RW[22].ToString();
                                                LB.InputFormatSeprator = RW[23].ToString();
                                                LB.OutputFormat = RW[24].ToString();
                                                LB.OutputFormatSeprator = RW[25].ToString();
                                            }
                                            catch (Exception) { }
                                            LB.Text = "";
                                            LB.ForeColor = Color.Black;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.Click += new EventHandler(this.Trp_LB_Click6);
                                            LB.MouseDown += new MouseEventHandler(this.TraLB_MouseDown6);
                                            LB.MouseMove += new MouseEventHandler(this.TraLB_MouseMove6);
                                            LB.MouseUp += new MouseEventHandler(this.TraLB_MouseUp6);
                                            LB.Visible = true;
                                        }
                                        catch (Exception)
                                        { }
                                        Application.DoEvents();
                                    }
                                }
                                P6_Pan_Click(null, null);
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                ResposnResult = "ERA";
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t7.Dispose(); } catch (Exception) { }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Load Front Image Color Picker :
                        PNL_1_LBL_1.Text = "Loading front image picked colors";
                        Application.DoEvents();
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t8 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_27_FIColorLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                ResposnResult = response.Content.ToString().Trim();
                                if (ResposnResult.Substring(0, 1) == "\"") { ResposnResult = ResposnResult.Substring(1, ResposnResult.Length - 1).Trim(); }
                                if (ResposnResult.Substring(ResposnResult.Length - 1, 1) == "\"") { ResposnResult = ResposnResult.Substring(0, ResposnResult.Length - 1).Trim(); }
                                ResposnResult = ResposnResult.Replace("\\", "");
                                DataTable DTFIE = new DataTable();
                                DTFIE = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ResposnResult);
                                if (DTFIE.Rows != null)
                                {
                                    foreach (DataRow RW in DTFIE.Rows)
                                    {
                                        try
                                        {
                                            FIColor++;
                                            TransparentColor LB = new TransparentColor();
                                            LB.Name = "FIC" + FIColor.ToString();
                                            LB.Visible = false;
                                            LB.Parent = P7_IMG;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.AutoSize = false;
                                            LB.Top = 0;
                                            LB.Left = 0;
                                            LB.Top = int.Parse(RW[1].ToString().Trim()) - 15;
                                            LB.Left = int.Parse(RW[0].ToString().Trim()) - 15;
                                            LB.Width = 30;
                                            LB.Height = 30;
                                            LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                                            LB.BackColor = Color.Transparent;
                                            LB.Cursor = Cursors.Default;
                                            LB.BringToFront();
                                            LB.Color_R = 0;
                                            LB.Color_G = 0;
                                            LB.Color_B = 0;
                                            LB.Color_R = int.Parse(RW[2].ToString().Trim());
                                            LB.Color_G = int.Parse(RW[3].ToString().Trim());
                                            LB.Color_B = int.Parse(RW[4].ToString().Trim());
                                            LB.Similarity = "100";
                                            LB.Similarity = RW[5].ToString().Trim();
                                            LB.Text = "X";
                                            LB.ForeColor = Color.Black;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.Click += new EventHandler(this.Trp_LB_Click7);
                                            LB.Visible = true;
                                            P7_AddNew = false;
                                        }
                                        catch (Exception)
                                        { }
                                        Application.DoEvents();
                                    }
                                }
                                P7_Pan_Click(null, null);
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                ResposnResult = "ERA";
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t8.Dispose(); } catch (Exception) { }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Load Back Image Color Picker :
                        PNL_1_LBL_1.Text = "Loading back image picked colors";
                        Application.DoEvents();
                        ResposnResult = "";
                        TaskEnded = false;
                        Task t9 = Task.Run(() =>
                        {
                            try
                            {
                                string SRVR = ST_Server;
                                var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_28_BIColorLoad" + "?" + "CID=" + BaseData_CountryCode + "&SID=" + BaseData_StateCode + "&DTID=" + BaseData_DocumentTypeCode + "&DID=" + BaseData_DocumentCode);
                                client.Timeout = -1;
                                var request = new RestRequest(Method.POST);
                                IRestResponse response = client.Execute(request);
                                ResposnResult = response.Content.ToString().Trim();
                                if (ResposnResult.Substring(0, 1) == "\"") { ResposnResult = ResposnResult.Substring(1, ResposnResult.Length - 1).Trim(); }
                                if (ResposnResult.Substring(ResposnResult.Length - 1, 1) == "\"") { ResposnResult = ResposnResult.Substring(0, ResposnResult.Length - 1).Trim(); }
                                ResposnResult = ResposnResult.Replace("\\", "");
                                DataTable DTFIE = new DataTable();
                                DTFIE = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(ResposnResult);
                                if (DTFIE.Rows != null)
                                {
                                    foreach (DataRow RW in DTFIE.Rows)
                                    {
                                        try
                                        {
                                            BIColor++;
                                            TransparentColor LB = new TransparentColor();
                                            LB.Name = "BIC" + BIColor.ToString();
                                            LB.Visible = false;
                                            LB.Parent = P8_IMG;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.AutoSize = false;
                                            LB.Top = 0;
                                            LB.Left = 0;
                                            LB.Top = int.Parse(RW[1].ToString().Trim()) - 15;
                                            LB.Left = int.Parse(RW[0].ToString().Trim()) - 15;
                                            LB.Width = 30;
                                            LB.Height = 30;
                                            LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                                            LB.BackColor = Color.Transparent;
                                            LB.Cursor = Cursors.Default;
                                            LB.BringToFront();
                                            LB.Color_R = 0;
                                            LB.Color_G = 0;
                                            LB.Color_B = 0;
                                            LB.Color_R = int.Parse(RW[2].ToString().Trim());
                                            LB.Color_G = int.Parse(RW[3].ToString().Trim());
                                            LB.Color_B = int.Parse(RW[4].ToString().Trim());
                                            LB.Similarity = "100";
                                            LB.Similarity = RW[5].ToString().Trim();
                                            LB.Text = "X";
                                            LB.ForeColor = Color.Black;
                                            LB.TransparentBackColor = Color.Transparent;
                                            LB.Click += new EventHandler(this.Trp_LB_Click8);
                                            LB.Visible = true;
                                            P8_AddNew = false;
                                        }
                                        catch (Exception)
                                        { }
                                        Application.DoEvents();
                                    }
                                }
                                P8_Pan_Click(null, null);
                                TaskEnded = true;
                            }
                            catch (Exception)
                            {
                                ResposnResult = "ERA";
                                TaskEnded = true;
                            }
                        });
                        while (TaskEnded == false) { Application.DoEvents(); }
                        try { t9.Dispose(); } catch (Exception) { }
                        PNL_1_PB1.Value += 1;
                        Application.DoEvents();
                        // Finalising :
                        PNL_1_PB1.Value = PNL_1_PB1.Maximum;
                        PNL_1_LBL_1.Text = "Loading template is completed";
                        Application.DoEvents();
                        Wait(1000);
                        PNL_1_LBL_1.Text = "Initialising template builder";
                        Application.DoEvents();
                        Wait(2000);
                    }
                    else
                    {
                        PNL_1_PB1.Value = PNL_1_PB1.Maximum;
                        PNL_1_LBL_1.Text = "No document design information has found";
                        Application.DoEvents();
                        Wait(1000);
                        PNL_1_LBL_1.Text = "Starting new template design";
                        Application.DoEvents();
                        Wait(1000);
                        PNL_1_PB1.Value = PNL_1_PB1.Maximum;
                        Application.DoEvents();
                        Wait(1000);
                    }
                }
                else
                {
                    PNL_1_LBL_1.Text = "Document information was not identified";
                    Application.DoEvents();
                    Wait(2000);
                    PNL_1_LBL_1.Text = "Closing the design form";
                    Application.DoEvents();
                    Wait(1000);
                    this.Close();
                }
                ObjectLoad = false;
                FirstLoad = false;
                Show_Page(2);
            }
            catch (Exception)
            {
                PNL_1_LBL_1.Text = "Document information was not identified";
                Application.DoEvents();
                Wait(2000);
                PNL_1_LBL_1.Text = "Closing the design form";
                Application.DoEvents();
                Wait(1000);
                this.Close();
            }
            FirstLoad = false;
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Basical Function :
        void ClearAll(int WID)
        {
            try
            {
                // Invisible All Pnael :
                PNL_1.Visible = false;
                PNL_2.Visible = false;
                PNL_3.Visible = false;
                PNL_4.Visible = false;
                PNL_5.Visible = false;
                PNL_6.Visible = false;
                PNL_7.Visible = false;
                PNL_8.Visible = false;
                // Form :
                BaseData_CountryCode = "0";
                BaseData_CountryName = "";
                BaseData_StateCode = "0";
                BaseData_StateName = "";
                BaseData_DocumentTypeCode = "0";
                BaseData_DocumentTypeName = "";
                BaseData_DocumentCode = "0";
                BaseData_DocumentName = "";
                BaseData_Ins_User_Fullname = "";
                BaseData_Ins_User_Date = "";
                BaseData_Ins_User_Time = "";
                BaseData_Update_User_Fullname = "";
                BaseData_Update_User_Date = "";
                BaseData_Update_User_Time = "";
                LBL_Step_Count.Text = PageCounter.ToString();
                LBL_Step_Num.Text = "0";
                LBL_Country_V.Text = "Reviewing information";
                LBL_State_V.Text = "Reviewing information";
                LBL_DocumentType_V.Text = "Reviewing information";
                LBL_Document_V.Text = "Reviewing information";
                Relocate_LBL();
                if (WID == 2)
                {
                    // Panel 1 - Loadding :
                    PNL_1_LBL_1.Text = "Reviewing template data";
                    PNL_1_PB1.Value = 0;
                    PNL_1_PB1.Maximum = 10;
                    // Panel 2 - Basic Configuration :
                    P2_T1.Text = "";
                    P2_T2.Text = "";
                    P2_T3.Text = "";
                    P2_T4.Text = "";
                    P2_T5.Text = "";
                    P2_UI1.Text = "";
                    P2_UI2.Text = "";
                    P2_UI3.Text = "";
                    P2_UI4.Text = "";
                    P2_UI1.Visible = false;
                    P2_UI1H.Visible = false;
                    P2_UI2.Visible = false;
                    P2_UI2H.Visible = false;
                    P2_UI3.Visible = false;
                    P2_UI3H.Visible = false;
                    P2_UI4.Visible = false;
                    P2_UI4H.Visible = false;
                    P2_S1.Switched = false;
                    P2_S2.Switched = false;
                    P2_S3.Switched = false;
                    P2_S4.Switched = false;
                    P2_S5.Switched = false;
                    P2_S6.Switched = false;
                    P2_S7.Switched = false;
                    P2_S8.Switched = false;
                    P2_S9.Switched = false;
                    P2_S10.Switched = false;
                    P2_S11.Switched = false;
                    P2_S12.Switched = false;
                    P2_S13.Switched = false;
                    P2_S14.Switched = false;
                    // Panel 3 - Template Front Image :
                    P3_Pic.Image = null;
                    // Panel 4 - Template Back Image :
                    P4_Pic.Image = null;
                    // Panel 5 - Front Image Elements :
                    ObjectLoad = true;
                    P5_IMG.Image = null;
                    P5_IMG.BackgroundImage = null;
                    P5_SelObject = null;
                    P5_T1.Text = "";
                    P5_T2.Text = "";
                    P5_T3.Text = "";
                    P5_T4.Text = "";
                    P5_T5.Text = "";
                    P5_T6.Text = "";
                    P5_T7.Text = "";
                    P5_T8.Text = "";
                    P5_T9.Text = "";
                    P5_T10.Text = "";
                    P5_T11.Text = "";
                    P5_T12.Text = "";
                    P5_T13.Text = "";
                    P5_T14.Text = "";
                    P5_T15.Text = "";
                    P5_T16.Text = "";
                    P5_T17.Text = "";
                    P5_T18.Text = "";
                    P5_T19.Text = "";
                    P5_C1.Checked = false;
                    P5_C2.Checked = false;
                    P5_C3.Checked = false;
                    P5_C4.Checked = false;
                    P5_D1.SelectedIndex = 0;
                    FI_TL.Visible = false;
                    FI_TR.Visible = false;
                    FI_BL.Visible = false;
                    FI_BR.Visible = false;
                    ObjectLoad = false;
                    P5_Pan_Click(null, null);
                    // Panel 6 - Back Image Elements :
                    ObjectLoad = true;
                    P6_IMG.Image = null;
                    P6_IMG.BackgroundImage = null;
                    P6_SelObject = null;
                    P6_T1.Text = "";
                    P6_T2.Text = "";
                    P6_T3.Text = "";
                    P6_T4.Text = "";
                    P6_T5.Text = "";
                    P6_T6.Text = "";
                    P6_T7.Text = "";
                    P6_T8.Text = "";
                    P6_T9.Text = "";
                    P6_T10.Text = "";
                    P6_T11.Text = "";
                    P6_T12.Text = "";
                    P6_T13.Text = "";
                    P6_T14.Text = "";
                    P6_T15.Text = "";
                    P6_T16.Text = "";
                    P6_T17.Text = "";
                    P6_T18.Text = "";
                    P6_T19.Text = "";
                    P6_C1.Checked = false;
                    P6_C2.Checked = false;
                    P6_C3.Checked = false;
                    P6_C3.Checked = false;
                    P6_D1.SelectedIndex = 0;
                    BI_TL.Visible = false;
                    BI_TR.Visible = false;
                    BI_BL.Visible = false;
                    BI_BR.Visible = false;
                    ObjectLoad = false;
                    P6_Pan_Click(null, null);
                    // Panel 7 - Front Image Color
                    ObjectLoad = true;
                    P7_IMG.Image = null;
                    P7_IMG.BackgroundImage = null;
                    P7_SelObject = null;
                    P7_T1.Text = "";
                    P7_T2.Text = "";
                    P7_T3.Text = "";
                    P7_T4.Text = "";
                    P7_Color.BackColor = PNL_7.BackColor;
                    P7_Color.Refresh();
                    ObjectLoad = false;
                    P7_Pan_Click(null, null);
                    // Panel 8 - Back Image Color
                    ObjectLoad = true;
                    P8_IMG.Image = null;
                    P8_IMG.BackgroundImage = null;
                    P8_SelObject = null;
                    P8_T1.Text = "";
                    P8_T2.Text = "";
                    P8_T3.Text = "";
                    P8_T4.Text = "";
                    P8_Color.BackColor = PNL_8.BackColor;
                    P8_Color.Refresh();
                    ObjectLoad = false;
                    P8_Pan_Click(null, null);
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        string BooltoDigit(bool BOLVAL)
        {
            string BolDigRes = "0";
            try
            {
                if (BOLVAL == true) { BolDigRes = "1"; } else { BolDigRes = "0"; }
            }
            catch (Exception)
            {
                BolDigRes = "0";
            }
            return BolDigRes;
        }
        //==========================================================================
        bool DigittoBool(string BOLVAL)
        {
            bool BolDigRes = false;
            try
            {
                if (BOLVAL.ToString().Trim() == "1") { BolDigRes = true; } else { BolDigRes = false; }
            }
            catch (Exception)
            {
                BolDigRes = false;
            }
            return BolDigRes;
        }
        //==========================================================================
        void Relocate_LBL()
        {
            try
            {
                LBL_Country_H.Left = 20; LBL_Country_H.Top = 97;
                LBL_Country_V.Left = LBL_Country_H.Left + LBL_Country_H.Width + 10; LBL_Country_V.Top = LBL_Country_H.Top;
                LBL_State_H.Left = LBL_Country_V.Left + LBL_Country_V.Width + 20; LBL_State_H.Top = LBL_Country_H.Top;
                LBL_State_V.Left = LBL_State_H.Left + LBL_State_H.Width + 10; LBL_State_V.Top = LBL_Country_H.Top;
                LBL_DocumentType_H.Left = LBL_State_V.Left + LBL_State_V.Width + 20; LBL_DocumentType_H.Top = LBL_Country_H.Top;
                LBL_DocumentType_V.Left = LBL_DocumentType_H.Left + LBL_DocumentType_H.Width + 10; LBL_DocumentType_V.Top = LBL_Country_H.Top;
                LBL_Document_H.Left = LBL_DocumentType_V.Left + LBL_DocumentType_V.Width + 20; LBL_Document_H.Top = LBL_Country_H.Top;
                LBL_Document_V.Left = LBL_Document_H.Left + LBL_Document_H.Width + 10; LBL_Document_V.Top = LBL_Country_H.Top;
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        public void Wait(int time)
        {
            Thread thread = new Thread(delegate ()
            {
                System.Threading.Thread.Sleep(time);
            });
            thread.Start();
            while (thread.IsAlive)
            {
                Application.DoEvents();
            }
        }
        //==========================================================================
        void Show_Page(int PageNum)
        {
            try
            {
                PNL_1.Visible = false;
                PNL_2.Visible = false;
                PNL_3.Visible = false;
                PNL_4.Visible = false;
                PNL_5.Visible = false;
                PNL_6.Visible = false;
                PNL_7.Visible = false;
                PNL_8.Visible = false;
                P5_C1.BackColor = Color.White;
                P5_C1.ForeColor = Color.Black;
                P5_C2.BackColor = Color.White;
                P5_C2.ForeColor = Color.Black;
                P5_C3.BackColor = Color.White;
                P5_C3.ForeColor = Color.Black;
                P5_C4.BackColor = Color.White;
                P5_C4.ForeColor = Color.Black;
                P6_C1.BackColor = Color.White;
                P6_C1.ForeColor = Color.Black;
                P6_C2.BackColor = Color.White;
                P6_C2.ForeColor = Color.Black;
                P6_C3.BackColor = Color.White;
                P6_C3.ForeColor = Color.Black;
                P6_C4.BackColor = Color.White;
                P6_C4.ForeColor = Color.Black;
                if (styleManager1.Style == MetroSet_UI.Design.Style.Dark)
                {
                    P5_C1.BackColor = Color.FromArgb(32, 32, 32);
                    P5_C1.ForeColor = Color.White;
                    P5_C2.BackColor = Color.FromArgb(32, 32, 32);
                    P5_C2.ForeColor = Color.White;
                    P5_C3.BackColor = Color.FromArgb(32, 32, 32);
                    P5_C3.ForeColor = Color.White;
                    P5_C4.BackColor = Color.FromArgb(32, 32, 32);
                    P5_C4.ForeColor = Color.White;
                    P6_C1.BackColor = Color.FromArgb(32, 32, 32);
                    P6_C1.ForeColor = Color.White;
                    P6_C2.BackColor = Color.FromArgb(32, 32, 32);
                    P6_C2.ForeColor = Color.White;
                    P6_C3.BackColor = Color.FromArgb(32, 32, 32);
                    P6_C3.ForeColor = Color.White;
                    P6_C4.BackColor = Color.FromArgb(32, 32, 32);
                    P6_C4.ForeColor = Color.White;
                }
                switch (PageNum)
                {
                    case 1: // Panel 1 - Loadding :
                        {
                            PNL_1.Left = (this.Width / 2) - (PNL_1.Width / 2);
                            PNL_1.Top = (this.Height / 2) - (PNL_1.Height / 2);
                            PNL_1.Visible = true;
                            PageNumber = 1;
                            break;
                        }
                    //----------------------------------------------------------
                    case 2: // Panel 2 - Basic Configuration :
                        {
                            PNL_2.Left = 15;
                            PNL_2.Top = 130;
                            PNL_2.Width = this.Width - 30;
                            PNL_2.Height = metroSetDivider3.Top - 136;
                            PNL_2.Visible = true;
                            PageNumber = 2;
                            break;
                        }
                    //----------------------------------------------------------
                    case 3: // Panel 3 - Template Front Image :
                        {
                            PNL_3.Left = 15;
                            PNL_3.Top = 130;
                            PNL_3.Width = this.Width - 30;
                            PNL_3.Height = metroSetDivider3.Top - 136;
                            PNL_3.Visible = true;
                            PageNumber = 3;
                            break;
                        }
                    //----------------------------------------------------------
                    case 4: // Panel 4 - Template Back Image :
                        {
                            PNL_4.Left = 15;
                            PNL_4.Top = 130;
                            PNL_4.Width = this.Width - 30;
                            PNL_4.Height = metroSetDivider3.Top - 136;
                            PNL_4.Visible = true;
                            PageNumber = 4;
                            break;
                        }
                    //----------------------------------------------------------
                    case 5: // Panel 5 - Front Image Elements :
                        {
                            try
                            {
                                P5_IMG.BackgroundImage = null;
                                P5_IMG.Image = null;
                                P5_IMG.Image = P3_Pic.Image;
                                int IMGW = P5_IMG.Width;
                                int IMGH = P5_IMG.Height;
                                P5_IMG.Image = null;
                                P5_IMG.BackgroundImage = P3_Pic.Image;
                                P5_IMG.Width = IMGW;
                                P5_IMG.Height = IMGH;
                            }
                            catch (Exception) { }
                            P5_D1.Items.Clear();
                            P5_D1.Items.Add("Alphabet and number");
                            P5_D1.Items.Add("Only alphabet");
                            P5_D1.Items.Add("Only number");
                            P5_D1.Items.Add("Address");
                            P5_D1.Items.Add("Date with full month name ( D:day - NTHN:month name - M:month - Y:year )");
                            P5_D1.Items.Add("Date with summary month name ( D:day - NTHN:month name - M:month - Y:year )");
                            P5_D1.Items.Add("Date - only number ( D:day - M:month - Y:year )");
                            P5_D1.Items.Add("Time ( H:hour - M:minutes - S:seconds");
                            P5_Pan_Click(null, null);
                            P5_T1.BackColor = P5_C1.BackColor;
                            P5_T1.ForeColor = P5_C1.ForeColor;
                            P5_T2.BackColor = P5_C1.BackColor;
                            P5_T2.ForeColor = P5_C1.ForeColor;
                            P5_T3.BackColor = P5_C1.BackColor;
                            P5_T3.ForeColor = P5_C1.ForeColor;
                            P5_T4.BackColor = P5_C1.BackColor;
                            P5_T4.ForeColor = P5_C1.ForeColor;
                            P5_T5.BackColor = P5_C1.BackColor;
                            P5_T5.ForeColor = P5_C1.ForeColor;
                            P5_T6.BackColor = P5_C1.BackColor;
                            P5_T6.ForeColor = P5_C1.ForeColor;
                            P5_T7.BackColor = P5_C1.BackColor;
                            P5_T7.ForeColor = P5_C1.ForeColor;
                            P5_T8.BackColor = P5_C1.BackColor;
                            P5_T8.ForeColor = P5_C1.ForeColor;
                            P5_T9.BackColor = P5_C1.BackColor;
                            P5_T9.ForeColor = P5_C1.ForeColor;
                            P5_T10.BackColor = P5_C1.BackColor;
                            P5_T10.ForeColor = P5_C1.ForeColor;
                            P5_T11.BackColor = P5_C1.BackColor;
                            P5_T11.ForeColor = P5_C1.ForeColor;
                            P5_T12.BackColor = P5_C1.BackColor;
                            P5_T12.ForeColor = P5_C1.ForeColor;
                            P5_T13.BackColor = P5_C1.BackColor;
                            P5_T13.ForeColor = P5_C1.ForeColor;
                            P5_T14.BackColor = P5_C1.BackColor;
                            P5_T14.ForeColor = P5_C1.ForeColor;
                            P5_T15.BackColor = P5_C1.BackColor;
                            P5_T15.ForeColor = P5_C1.ForeColor;
                            P5_T16.BackColor = P5_C1.BackColor;
                            P5_T16.ForeColor = P5_C1.ForeColor;
                            P5_T17.BackColor = P5_C1.BackColor;
                            P5_T17.ForeColor = P5_C1.ForeColor;
                            P5_T18.BackColor = P5_C1.BackColor;
                            P5_T18.ForeColor = P5_C1.ForeColor;
                            P5_T19.BackColor = P5_C1.BackColor;
                            P5_T19.ForeColor = P5_C1.ForeColor;
                            PNL_5.Left = 15;
                            PNL_5.Top = 130;
                            PNL_5.Width = this.Width - 30;
                            PNL_5.Height = metroSetDivider3.Top - 136;
                            PNL_5.Visible = true;
                            PageNumber = 5;
                            break;
                        }
                    //----------------------------------------------------------
                    case 6: // Panel 6 - Back Image Elements :
                        {
                            try
                            {
                                P6_IMG.BackgroundImage = null;
                                P6_IMG.Image = null;
                                P6_IMG.Image = P4_Pic.Image;
                                int IMGW = P6_IMG.Width;
                                int IMGH = P6_IMG.Height;
                                P6_IMG.Image = null;
                                P6_IMG.BackgroundImage = P4_Pic.Image;
                                P6_IMG.Width = IMGW;
                                P6_IMG.Height = IMGH;
                            }
                            catch (Exception) { }
                            P5_D1.Items.Clear();
                            P5_D1.Items.Add("Alphabet and number");
                            P5_D1.Items.Add("Only alphabet");
                            P5_D1.Items.Add("Only number");
                            P5_D1.Items.Add("Address");
                            P5_D1.Items.Add("Date with full month name ( D:day - NTHN:month name - M:month - Y:year )");
                            P5_D1.Items.Add("Date with summary month name ( D:day - NTHN:month name - M:month - Y:year )");
                            P5_D1.Items.Add("Date - only number ( D:day - M:month - Y:year )");
                            P5_D1.Items.Add("Time ( H:hour - M:minutes - S:seconds");
                            P6_Pan_Click(null, null);
                            P6_T1.BackColor = P6_C1.BackColor;
                            P6_T1.ForeColor = P6_C1.ForeColor;
                            P6_T2.BackColor = P6_C1.BackColor;
                            P6_T2.ForeColor = P6_C1.ForeColor;
                            P6_T3.BackColor = P6_C1.BackColor;
                            P6_T3.ForeColor = P6_C1.ForeColor;
                            P6_T4.BackColor = P6_C1.BackColor;
                            P6_T4.ForeColor = P6_C1.ForeColor;
                            P6_T5.BackColor = P6_C1.BackColor;
                            P6_T5.ForeColor = P6_C1.ForeColor;
                            P6_T6.BackColor = P6_C1.BackColor;
                            P6_T6.ForeColor = P6_C1.ForeColor;
                            P6_T7.BackColor = P6_C1.BackColor;
                            P6_T7.ForeColor = P6_C1.ForeColor;
                            P6_T8.BackColor = P6_C1.BackColor;
                            P6_T8.ForeColor = P6_C1.ForeColor;
                            P6_T9.BackColor = P6_C1.BackColor;
                            P6_T9.ForeColor = P6_C1.ForeColor;
                            P6_T10.BackColor = P6_C1.BackColor;
                            P6_T10.ForeColor = P6_C1.ForeColor;
                            P6_T11.BackColor = P6_C1.BackColor;
                            P6_T11.ForeColor = P6_C1.ForeColor;
                            P6_T12.BackColor = P6_C1.BackColor;
                            P6_T12.ForeColor = P6_C1.ForeColor;
                            P6_T13.BackColor = P6_C1.BackColor;
                            P6_T13.ForeColor = P6_C1.ForeColor;
                            P6_T14.BackColor = P6_C1.BackColor;
                            P6_T14.ForeColor = P6_C1.ForeColor;
                            P6_T15.BackColor = P6_C1.BackColor;
                            P6_T15.ForeColor = P6_C1.ForeColor;
                            P6_T16.BackColor = P6_C1.BackColor;
                            P6_T16.ForeColor = P6_C1.ForeColor;
                            P6_T17.BackColor = P6_C1.BackColor;
                            P6_T17.ForeColor = P6_C1.ForeColor;
                            P6_T18.BackColor = P6_C1.BackColor;
                            P6_T18.ForeColor = P6_C1.ForeColor;
                            P6_T19.BackColor = P6_C1.BackColor;
                            P6_T19.ForeColor = P6_C1.ForeColor;
                            PNL_6.Left = 15;
                            PNL_6.Top = 130;
                            PNL_6.Width = this.Width - 30;
                            PNL_6.Height = metroSetDivider3.Top - 136;
                            PNL_6.Visible = true;
                            PageNumber = 6;
                            break;
                        }
                    //----------------------------------------------------------
                    case 7: // Panel 7 - Front Image Color :
                        {
                            try
                            {
                                P7_IMG.BackgroundImage = null;
                                P7_IMG.Image = null;
                                P7_IMG.Image = P3_Pic.Image;
                                int IMGW = P7_IMG.Width;
                                int IMGH = P7_IMG.Height;
                                P7_IMG.Image = null;
                                P7_IMG.BackgroundImage = P3_Pic.Image;
                                P7_IMG.Width = IMGW;
                                P7_IMG.Height = IMGH;
                            }
                            catch (Exception) { }
                            P7_Pan_Click(null, null);
                            P7_T1.BackColor = P5_C1.BackColor;
                            P7_T1.ForeColor = P5_C1.ForeColor;
                            P7_T2.BackColor = P5_C1.BackColor;
                            P7_T2.ForeColor = P5_C1.ForeColor;
                            P7_T3.BackColor = P5_C1.BackColor;
                            P7_T3.ForeColor = P5_C1.ForeColor;
                            P7_T4.BackColor = P5_C1.BackColor;
                            P7_T4.ForeColor = P5_C1.ForeColor;
                            P7_Color.BackColor = P5_C1.BackColor;
                            P7_Color.Refresh();
                            PNL_7.Left = 15;
                            PNL_7.Top = 130;
                            PNL_7.Width = this.Width - 30;
                            PNL_7.Height = metroSetDivider3.Top - 136;
                            PNL_7.Visible = true;
                            PageNumber = 7;
                            break;
                        }
                    //----------------------------------------------------------
                    case 8: // Panel 8 - Back Image Color :
                        {
                            try
                            {
                                P8_IMG.BackgroundImage = null;
                                P8_IMG.Image = null;
                                P8_IMG.Image = P4_Pic.Image;
                                int IMGW = P8_IMG.Width;
                                int IMGH = P8_IMG.Height;
                                P8_IMG.Image = null;
                                P8_IMG.BackgroundImage = P4_Pic.Image;
                                P8_IMG.Width = IMGW;
                                P8_IMG.Height = IMGH;
                            }
                            catch (Exception) { }
                            P8_Pan_Click(null, null);
                            P8_T1.BackColor = P6_C1.BackColor;
                            P8_T1.ForeColor = P6_C1.ForeColor;
                            P8_T2.BackColor = P6_C1.BackColor;
                            P8_T2.ForeColor = P6_C1.ForeColor;
                            P8_T3.BackColor = P6_C1.BackColor;
                            P8_T3.ForeColor = P6_C1.ForeColor;
                            P8_T4.BackColor = P6_C1.BackColor;
                            P8_T4.ForeColor = P6_C1.ForeColor;
                            P8_Color.BackColor = P6_C1.BackColor;
                            P8_Color.Refresh();
                            PNL_8.Left = 15;
                            PNL_8.Top = 130;
                            PNL_8.Width = this.Width - 30;
                            PNL_8.Height = metroSetDivider3.Top - 136;
                            PNL_8.Visible = true;
                            PageNumber = 8;
                            break;
                        }
                }
                LBL_Step_Num.Text = PageNumber.ToString();
                LBL_Step_Count.Text = PageCounter.ToString();
            }
            catch (Exception)
            { }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Controlling Btn :
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstLoad == true) { return; }
                this.Close();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void Btn_Prev_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstLoad == true) { return; }
                if (PageNumber > 2) { PageNumber -= 1; } else { return; }
                Show_Page(PageNumber);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void Btn_Next_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstLoad == true) { return; }
                if (PageNumber < PageCounter) { PageNumber += 1; } else { return; }
                Show_Page(PageNumber);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstLoad == true) { return; }
                FirstLoad = true;
                string SP1 = "";
                string SP2 = "";
                string SP3 = "";
                string SP4 = "";
                string SP5 = "";
                string SP6 = "";
                string SP7 = "";
                bool WaitforRes = false;
                string APIRes = "";
                PNL_1.Visible = false;
                PNL_2.Visible = false;
                PNL_3.Visible = false;
                PNL_4.Visible = false;
                PNL_5.Visible = false;
                PNL_6.Visible = false;
                PNL_7.Visible = false;
                PNL_8.Visible = false;
                Application.DoEvents();
                PNL_1_PB1.Value = 0;
                PNL_1_PB1.Maximum = 8;
                PNL_1_LBL_1.Text = "";
                PNL_1_LBL_1.Text = "Preparing to save the document template";
                PNL_1.Visible = true;
                PNL_1.Enabled = true;
                TemplateBuilder_Resize(null, null);
                Application.DoEvents();
                Wait(1000);
                // Save Basic Configuration :
                PNL_1_LBL_1.Text = "Save configuration";
                Application.DoEvents();
                WaitforRes = true;
                APIRes = "";
                Task T1 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_29_BasicConfigurationSave");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "application/json");
                        string JsonBody = "";
                        JsonBody += "{";
                        JsonBody += "\"" + "V1" + "\": " + "\"" + P2_T1.Text.Trim() + "\",";
                        JsonBody += "\"" + "V2" + "\": " + "\"" + P2_T2.Text.Trim() + "\",";
                        JsonBody += "\"" + "V3" + "\": " + "\"" + P2_T3.Text.Trim() + "\",";
                        JsonBody += "\"" + "V4" + "\": " + "\"" + BooltoDigit(P2_S1.Switched) + "\",";
                        JsonBody += "\"" + "V5" + "\": " + "\"" + BooltoDigit(P2_S2.Switched) + "\",";
                        JsonBody += "\"" + "V6" + "\": " + "\"" + BooltoDigit(P2_S3.Switched) + "\",";
                        JsonBody += "\"" + "V7" + "\": " + "\"" + BooltoDigit(P2_S4.Switched) + "\",";
                        JsonBody += "\"" + "V8" + "\": " + "\"" + BooltoDigit(P2_S5.Switched) + "\",";
                        JsonBody += "\"" + "V9" + "\": " + "\"" + BooltoDigit(P2_S6.Switched) + "\",";
                        JsonBody += "\"" + "V10" + "\": " + "\"" + BooltoDigit(P2_S7.Switched) + "\",";
                        JsonBody += "\"" + "V11" + "\": " + "\"" + BooltoDigit(P2_S8.Switched) + "\",";
                        JsonBody += "\"" + "V12" + "\": " + "\"" + BooltoDigit(P2_S9.Switched) + "\",";
                        JsonBody += "\"" + "V13" + "\": " + "\"" + BooltoDigit(P2_S10.Switched) + "\",";
                        JsonBody += "\"" + "V14" + "\": " + "\"" + BooltoDigit(P2_S11.Switched) + "\",";
                        JsonBody += "\"" + "V15" + "\": " + "\"" + BooltoDigit(P2_S12.Switched) + "\",";
                        JsonBody += "\"" + "V16" + "\": " + "\"" + BooltoDigit(P2_S13.Switched) + "\",";
                        JsonBody += "\"" + "V17" + "\": " + "\"" + BooltoDigit(P2_S14.Switched) + "\",";
                        JsonBody += "\"" + "V18" + "\": " + "\"" + P2_T4.Text.Trim() + "\",";
                        JsonBody += "\"" + "V19" + "\": " + "\"" + P2_T5.Text.Trim() + "\"";
                        JsonBody += "}";
                        request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T1.Dispose(); } catch (Exception) { }
                APIRes = APIRes.Replace("\"", "").Trim();
                if (APIRes == "OK") { SP1 = "Done"; } else { SP1 = "Error"; }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Save Front Image :
                PNL_1_LBL_1.Text = "Saving front image template";
                Application.DoEvents();
                WaitforRes = true;
                APIRes = "";
                Task T2 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_30_FrontImageSave?W=" + P3_Pic.Width.ToString() + "&H=" + P3_Pic.Height);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "multipart/form-data");
                        ImageConverter converter = new ImageConverter();
                        byte[] P3IMG = (byte[])converter.ConvertTo(P3_Pic.Image, typeof(byte[]));
                        request.AddFile("files[testfile1.pot]", P3IMG, "FI");
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T2.Dispose(); } catch (Exception) { }
                APIRes = APIRes.Replace("\"", "").Trim();
                if (APIRes == "OK") { SP2 = "Done"; } else { SP2 = "Error"; }
                if (APIRes == "ER4") { SP2 = "No File"; }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Save Back Image :
                PNL_1_LBL_1.Text = "Saving back image template";
                Application.DoEvents();
                WaitforRes = true;
                APIRes = "";
                Task T3 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_31_BackImageSave?W=" + P4_Pic.Width.ToString() + "&H=" + P4_Pic.Height);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "multipart/form-data");
                        ImageConverter converter = new ImageConverter();
                        byte[] P4IMG = (byte[])converter.ConvertTo(P4_Pic.Image, typeof(byte[]));
                        request.AddFile("files[testfile1.pot]", P4IMG, "BI");
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T3.Dispose(); } catch (Exception) { }
                APIRes = APIRes.Replace("\"", "").Trim();
                if (APIRes == "OK") { SP3 = "Done"; } else { SP3 = "Error"; }
                if (APIRes == "ER4") { SP3 = "No File"; }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Save Front Image Elements :
                PNL_1_LBL_1.Text = "Saving front image template's elements";
                Application.DoEvents();
                // Delete Data :
                WaitforRes = true;
                APIRes = "";
                Task T4 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_32_FIElementsSave?DelData=1");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "application/json");
                        string JsonBody = "";
                        request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T4.Dispose(); } catch (Exception) { }
                SP4 = "Cleared";
                // Add Front Image Element :
                if (P3_Pic.Image != null)
                {
                    foreach (Control TRPL in P5_IMG.Controls)
                    {
                        if (TRPL is TransparentLabel)
                        {
                            TransparentLabel TRSel = (TransparentLabel)TRPL;
                            WaitforRes = true;
                            APIRes = "";
                            Task T5 = Task.Run(() =>
                            {
                                try
                                {
                                    var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_32_FIElementsSave?DelData=0");
                                    client.Timeout = -1;
                                    var request = new RestRequest(Method.POST);
                                    request.AddHeader("CID", BaseData_CountryCode);
                                    request.AddHeader("SID", BaseData_StateCode);
                                    request.AddHeader("DTID", BaseData_DocumentTypeCode);
                                    request.AddHeader("DID", BaseData_DocumentCode);
                                    request.AddHeader("UID", ST_UID);
                                    request.AddHeader("Content-Type", "application/json");
                                    string JsonBody = "";
                                    JsonBody += "{";
                                    JsonBody += "\"" + "V1" + "\": " + "\"" + TRSel.Left + "\",";
                                    JsonBody += "\"" + "V2" + "\": " + "\"" + TRSel.Top + "\",";
                                    JsonBody += "\"" + "V3" + "\": " + "\"" + TRSel.Left + "\",";
                                    JsonBody += "\"" + "V4" + "\": " + "\"" + (TRSel.Top + TRSel.Height).ToString() + "\",";
                                    JsonBody += "\"" + "V5" + "\": " + "\"" + (TRSel.Left + TRSel.Width).ToString() + "\",";
                                    JsonBody += "\"" + "V6" + "\": " + "\"" + TRSel.Top + "\",";
                                    JsonBody += "\"" + "V7" + "\": " + "\"" + (TRSel.Left + TRSel.Width).ToString() + "\",";
                                    JsonBody += "\"" + "V8" + "\": " + "\"" + (TRSel.Top + TRSel.Height).ToString() + "\",";
                                    JsonBody += "\"" + "V9" + "\": " + "\"" + TRPL.Width + "\",";
                                    JsonBody += "\"" + "V10" + "\": " + "\"" + TRSel.Height + "\",";
                                    JsonBody += "\"" + "V11" + "\": " + "\"" + BooltoDigit(TRSel.OutputShow) + "\",";
                                    JsonBody += "\"" + "V12" + "\": " + "\"" + TRSel.OutputTitle + "\",";
                                    JsonBody += "\"" + "V13" + "\": " + "\"" + BooltoDigit(TRSel.KeyActive) + "\",";
                                    JsonBody += "\"" + "V14" + "\": " + "\"" + TRSel.KeyValue + "\",";
                                    JsonBody += "\"" + "V15" + "\": " + "\"" + TRSel.Similarity + "\",";
                                    JsonBody += "\"" + "V16" + "\": " + "\"" + TRSel.OcrIndex + "\",";
                                    JsonBody += "\"" + "V17" + "\": " + "\"" + TRSel.OcrPosition + "\",";
                                    JsonBody += "\"" + "V18" + "\": " + "\"" + BooltoDigit(TRSel.DataProcessing) + "\",";
                                    JsonBody += "\"" + "V19" + "\": " + "\"" + TRSel.TypeCode + "\",";
                                    JsonBody += "\"" + "V20" + "\": " + "\"" + TRSel.SubstringStart + "\",";
                                    JsonBody += "\"" + "V21" + "\": " + "\"" + TRSel.SubstringLength + "\",";
                                    JsonBody += "\"" + "V22" + "\": " + "\"" + BooltoDigit(TRSel.SubstringLeft) + "\",";
                                    JsonBody += "\"" + "V23" + "\": " + "\"" + TRSel.InputFormat + "\",";
                                    JsonBody += "\"" + "V24" + "\": " + "\"" + TRSel.InputFormatSeprator + "\",";
                                    JsonBody += "\"" + "V25" + "\": " + "\"" + TRSel.OutputFormat + "\",";
                                    JsonBody += "\"" + "V26" + "\": " + "\"" + TRSel.OutputFormatSeprator + "\",";
                                    JsonBody += "}";
                                    request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                                    APIRes = client.Execute(request).Content.ToString();
                                    WaitforRes = false;
                                }
                                catch (Exception)
                                {
                                    APIRes = "ERA";
                                    WaitforRes = false;
                                }
                            });
                            while (WaitforRes == true) { Application.DoEvents(); }
                            try { T5.Dispose(); } catch (Exception) { }
                            APIRes = APIRes.Replace("\"", "").Trim();
                            if (SP4 != "Error")
                            {
                                if (APIRes == "OK") { SP4 = "Done"; } else { SP4 = "Error"; }
                            }
                        }
                    }
                }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Save Back Image Elements :
                PNL_1_LBL_1.Text = "Saving back image template's elements";
                Application.DoEvents();
                // Delete Data :
                WaitforRes = true;
                APIRes = "";
                Task T6 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_33_BIElementsSave?DelData=1");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "application/json");
                        string JsonBody = "";
                        request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T6.Dispose(); } catch (Exception) { }
                SP5 = "Cleared";
                // Add Back Image Element :
                if (P4_Pic.Image != null)
                {
                    foreach (Control TRPL in P6_IMG.Controls)
                    {
                        if (TRPL is TransparentLabel)
                        {
                            TransparentLabel TRSel = (TransparentLabel)TRPL;
                            WaitforRes = true;
                            APIRes = "";
                            Task T7 = Task.Run(() =>
                            {
                                try
                                {
                                    var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_33_BIElementsSave?DelData=0");
                                    client.Timeout = -1;
                                    var request = new RestRequest(Method.POST);
                                    request.AddHeader("CID", BaseData_CountryCode);
                                    request.AddHeader("SID", BaseData_StateCode);
                                    request.AddHeader("DTID", BaseData_DocumentTypeCode);
                                    request.AddHeader("DID", BaseData_DocumentCode);
                                    request.AddHeader("UID", ST_UID);
                                    request.AddHeader("Content-Type", "application/json");
                                    string JsonBody = "";
                                    JsonBody += "{";
                                    JsonBody += "\"" + "V1" + "\": " + "\"" + TRSel.Left + "\",";
                                    JsonBody += "\"" + "V2" + "\": " + "\"" + TRSel.Top + "\",";
                                    JsonBody += "\"" + "V3" + "\": " + "\"" + TRSel.Left + "\",";
                                    JsonBody += "\"" + "V4" + "\": " + "\"" + (TRSel.Top + TRSel.Height).ToString() + "\",";
                                    JsonBody += "\"" + "V5" + "\": " + "\"" + (TRSel.Left + TRSel.Width).ToString() + "\",";
                                    JsonBody += "\"" + "V6" + "\": " + "\"" + TRSel.Top + "\",";
                                    JsonBody += "\"" + "V7" + "\": " + "\"" + (TRSel.Left + TRSel.Width).ToString() + "\",";
                                    JsonBody += "\"" + "V8" + "\": " + "\"" + (TRSel.Top + TRSel.Height).ToString() + "\",";
                                    JsonBody += "\"" + "V9" + "\": " + "\"" + TRPL.Width + "\",";
                                    JsonBody += "\"" + "V10" + "\": " + "\"" + TRSel.Height + "\",";
                                    JsonBody += "\"" + "V11" + "\": " + "\"" + BooltoDigit(TRSel.OutputShow) + "\",";
                                    JsonBody += "\"" + "V12" + "\": " + "\"" + TRSel.OutputTitle + "\",";
                                    JsonBody += "\"" + "V13" + "\": " + "\"" + BooltoDigit(TRSel.KeyActive) + "\",";
                                    JsonBody += "\"" + "V14" + "\": " + "\"" + TRSel.KeyValue + "\",";
                                    JsonBody += "\"" + "V15" + "\": " + "\"" + TRSel.Similarity + "\",";
                                    JsonBody += "\"" + "V16" + "\": " + "\"" + TRSel.OcrIndex + "\",";
                                    JsonBody += "\"" + "V17" + "\": " + "\"" + TRSel.OcrPosition + "\",";
                                    JsonBody += "\"" + "V18" + "\": " + "\"" + BooltoDigit(TRSel.DataProcessing) + "\",";
                                    JsonBody += "\"" + "V19" + "\": " + "\"" + TRSel.TypeCode + "\",";
                                    JsonBody += "\"" + "V20" + "\": " + "\"" + TRSel.SubstringStart + "\",";
                                    JsonBody += "\"" + "V21" + "\": " + "\"" + TRSel.SubstringLength + "\",";
                                    JsonBody += "\"" + "V22" + "\": " + "\"" + BooltoDigit(TRSel.SubstringLeft) + "\",";
                                    JsonBody += "\"" + "V23" + "\": " + "\"" + TRSel.InputFormat + "\",";
                                    JsonBody += "\"" + "V24" + "\": " + "\"" + TRSel.InputFormatSeprator + "\",";
                                    JsonBody += "\"" + "V25" + "\": " + "\"" + TRSel.OutputFormat + "\",";
                                    JsonBody += "\"" + "V26" + "\": " + "\"" + TRSel.OutputFormatSeprator + "\",";
                                    JsonBody += "}";
                                    request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                                    APIRes = client.Execute(request).Content.ToString();
                                    WaitforRes = false;
                                }
                                catch (Exception)
                                {
                                    APIRes = "ERA";
                                    WaitforRes = false;
                                }
                            });
                            while (WaitforRes == true) { Application.DoEvents(); }
                            try { T7.Dispose(); } catch (Exception) { }
                            APIRes = APIRes.Replace("\"", "").Trim();
                            if (SP5 != "Error")
                            {
                                if (APIRes == "OK") { SP5 = "Done"; } else { SP5 = "Error"; }
                            }
                        }
                    }
                }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Save Front Image Color Pickers :
                PNL_1_LBL_1.Text = "Saving front image template's picked colors";
                Application.DoEvents();
                // Delete Data :
                WaitforRes = true;
                APIRes = "";
                Task T8 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_34_FIColorsSave?DelData=1");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "application/json");
                        string JsonBody = "";
                        request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T8.Dispose(); } catch (Exception) { }
                SP6 = "Cleared";
                // Add Front Image Color Picker :
                if (P3_Pic.Image != null)
                {
                    foreach (Control TRPL in P7_IMG.Controls)
                    {
                        if (TRPL is TransparentColor)
                        {
                            TransparentColor TRSel = (TransparentColor)TRPL;
                            WaitforRes = true;
                            APIRes = "";
                            Task T9 = Task.Run(() =>
                            {
                                try
                                {
                                    var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_34_FIColorsSave?DelData=0");
                                    client.Timeout = -1;
                                    var request = new RestRequest(Method.POST);
                                    request.AddHeader("CID", BaseData_CountryCode);
                                    request.AddHeader("SID", BaseData_StateCode);
                                    request.AddHeader("DTID", BaseData_DocumentTypeCode);
                                    request.AddHeader("DID", BaseData_DocumentCode);
                                    request.AddHeader("UID", ST_UID);
                                    request.AddHeader("Content-Type", "application/json");
                                    string JsonBody = "";
                                    JsonBody += "{";
                                    JsonBody += "\"" + "V1" + "\": " + "\"" + (TRSel.Left + 15).ToString() + "\",";
                                    JsonBody += "\"" + "V2" + "\": " + "\"" + (TRSel.Top + 15).ToString() + "\",";
                                    JsonBody += "\"" + "V3" + "\": " + "\"" + TRSel.Color_R + "\",";
                                    JsonBody += "\"" + "V4" + "\": " + "\"" + TRSel.Color_G + "\",";
                                    JsonBody += "\"" + "V5" + "\": " + "\"" + TRSel.Color_B + "\",";
                                    JsonBody += "\"" + "V6" + "\": " + "\"" + TRSel.Similarity + "\",";
                                    JsonBody += "}";
                                    request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                                    APIRes = client.Execute(request).Content.ToString();
                                    WaitforRes = false;
                                }
                                catch (Exception)
                                {
                                    APIRes = "ERA";
                                    WaitforRes = false;
                                }
                            });
                            while (WaitforRes == true) { Application.DoEvents(); }
                            try { T9.Dispose(); } catch (Exception) { }
                            APIRes = APIRes.Replace("\"", "").Trim();
                            if (SP6 != "Error")
                            {
                                if (APIRes == "OK") { SP6 = "Done"; } else { SP6 = "Error"; }
                            }
                        }
                    }
                }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Save Back Image Color Pickers :
                PNL_1_LBL_1.Text = "Saving back image template's picked colors";
                Application.DoEvents();
                // Delete Data :
                WaitforRes = true;
                APIRes = "";
                Task T10 = Task.Run(() =>
                {
                    try
                    {
                        var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_35_BIColorsSave?DelData=1");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("CID", BaseData_CountryCode);
                        request.AddHeader("SID", BaseData_StateCode);
                        request.AddHeader("DTID", BaseData_DocumentTypeCode);
                        request.AddHeader("DID", BaseData_DocumentCode);
                        request.AddHeader("UID", ST_UID);
                        request.AddHeader("Content-Type", "application/json");
                        string JsonBody = "";
                        request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                        APIRes = client.Execute(request).Content.ToString();
                        WaitforRes = false;
                    }
                    catch (Exception)
                    {
                        APIRes = "ERA";
                        WaitforRes = false;
                    }
                });
                while (WaitforRes == true) { Application.DoEvents(); }
                try { T10.Dispose(); } catch (Exception) { }
                SP7 = "Cleared";
                // Add Front Image Color Picker :
                if (P4_Pic.Image != null)
                {
                    foreach (Control TRPL in P8_IMG.Controls)
                    {
                        if (TRPL is TransparentColor)
                        {
                            TransparentColor TRSel = (TransparentColor)TRPL;
                            WaitforRes = true;
                            APIRes = "";
                            Task T11 = Task.Run(() =>
                            {
                                try
                                {
                                    var client = new RestClient(ST_Server + "api/DocBuilder/" + "DT_35_BIColorsSave?DelData=0");
                                    client.Timeout = -1;
                                    var request = new RestRequest(Method.POST);
                                    request.AddHeader("CID", BaseData_CountryCode);
                                    request.AddHeader("SID", BaseData_StateCode);
                                    request.AddHeader("DTID", BaseData_DocumentTypeCode);
                                    request.AddHeader("DID", BaseData_DocumentCode);
                                    request.AddHeader("UID", ST_UID);
                                    request.AddHeader("Content-Type", "application/json");
                                    string JsonBody = "";
                                    JsonBody += "{";
                                    JsonBody += "\"" + "V1" + "\": " + "\"" + (TRSel.Left + 15).ToString() + "\",";
                                    JsonBody += "\"" + "V2" + "\": " + "\"" + (TRSel.Top + 15).ToString() + "\",";
                                    JsonBody += "\"" + "V3" + "\": " + "\"" + TRSel.Color_R + "\",";
                                    JsonBody += "\"" + "V4" + "\": " + "\"" + TRSel.Color_G + "\",";
                                    JsonBody += "\"" + "V5" + "\": " + "\"" + TRSel.Color_B + "\",";
                                    JsonBody += "\"" + "V6" + "\": " + "\"" + TRSel.Similarity + "\",";
                                    JsonBody += "}";
                                    request.AddParameter("application/json", JsonBody, ParameterType.RequestBody);
                                    APIRes = client.Execute(request).Content.ToString();
                                    WaitforRes = false;
                                }
                                catch (Exception)
                                {
                                    APIRes = "ERA";
                                    WaitforRes = false;
                                }
                            });
                            while (WaitforRes == true) { Application.DoEvents(); }
                            try { T11.Dispose(); } catch (Exception) { }
                            APIRes = APIRes.Replace("\"", "").Trim();
                            if (SP7 != "Error")
                            {
                                if (APIRes == "OK") { SP7 = "Done"; } else { SP7 = "Error"; }
                            }
                        }
                    }
                }
                PNL_1_PB1.Value += 1;
                Application.DoEvents();
                Wait(500);
                // Waiting Section :
                PNL_1_LBL_1.Text = "Final review";
                PNL_1_PB1.Value += PNL_1_PB1.Maximum;
                Application.DoEvents();
                Wait(2000);
                if (MessageBox.Show("Dear User ...\r\nThe statuses of saving the template are :\r\n\r\n- Configuration : " + SP1 + "\r\n- Front Image : " + SP2 + "\r\n- Back Image : " + SP3 + "\r\n- Front Image Elements : " + SP4 + "\r\n- Back Image Elements : " + SP5 + "\r\n- Front Image Color Picker : " + SP6 + "\r\n- Back Image Color Picker : " + SP7 + "\r\n\r\nDo you want to close the template builder ?", "Template Save Result", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    Show_Page(2);
                }
            }
            catch (Exception)
            { }
            FirstLoad = false;
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Panel 3 - 4 Browser Btn :
        private void PN3BtnBrows_Click(object sender, EventArgs e)
        {
            try
            {
                P3_Pic.Image = null;
                OpenFileDialog FOD = new OpenFileDialog();
                FOD.Title = "Template Front Image Selector";
                FOD.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                FOD.FilterIndex = 2;
                FOD.RestoreDirectory = true;
                if (FOD.ShowDialog() == DialogResult.OK)
                {
                    P3_Pic.Image = new Bitmap(FOD.FileName);
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void PN4BtnBrows_Click(object sender, EventArgs e)
        {
            try
            {
                P4_Pic.Image = null;
                OpenFileDialog FOD = new OpenFileDialog();
                FOD.Title = "Template Back Image Selector";
                FOD.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                FOD.FilterIndex = 2;
                FOD.RestoreDirectory = true;
                if (FOD.ShowDialog() == DialogResult.OK)
                {
                    P4_Pic.Image = new Bitmap(FOD.FileName);
                }
            }
            catch (Exception)
            { }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Panel 5 - Front Image Elements Btn :
        private void P5_Pan_Click(object sender, EventArgs e)
        {
            try
            {
                if (P5_SelObject != null)
                {
                    P5_SelObject.Cursor = Cursors.Default;
                    P5_SelObject.transparentBackColor = Color.Transparent;
                    P5_SelObject.Invalidate();
                    P5_SelObject = null;
                }
                ObjectLoad = true;
                FI_TL.Visible = false;
                FI_TR.Visible = false;
                FI_BL.Visible = false;
                FI_BR.Visible = false;
                P5_T1.Text = "";
                P5_T2.Text = "";
                P5_T3.Text = "";
                P5_T4.Text = "";
                P5_T5.Text = "";
                P5_T6.Text = "";
                P5_T7.Text = "";
                P5_T8.Text = "";
                P5_T9.Text = "";
                P5_T10.Text = "";
                P5_T11.Text = "";
                P5_T12.Text = "";
                P5_T13.Text = "";
                P5_T14.Text = "";
                P5_T15.Text = "";
                P5_T16.Text = "";
                P5_T17.Text = "";
                P5_T18.Text = "";
                P5_T19.Text = "";
                P5_C1.Checked = false;
                P5_C2.Checked = false;
                P5_C3.Checked = false;
                P5_C4.Checked = false;
                P5_D1.SelectedIndex = 0;
                ObjectLoad = false;
                P5_Pan.Focus();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_IMG_Click(object sender, EventArgs e)
        {
            try { P5_Pan_Click(null, null); } catch (Exception) { }
        }
        //==========================================================================
        private void P5_Btn_New_Click(object sender, EventArgs e)
        {
            try
            {
                P5_Pan_Click(null, null);
                FICounter++;
                FI_TL.Parent = P5_IMG;
                FI_TR.Parent = P5_IMG;
                FI_BL.Parent = P5_IMG;
                FI_BR.Parent = P5_IMG;
                TransparentLabel LB = new TransparentLabel();
                LB.Name = "FI" + FICounter.ToString();
                LB.Visible = false;
                LB.Parent = P5_IMG;
                LB.TransparentBackColor = Color.Transparent;
                LB.AutoSize = false;
                LB.Top = 5;
                LB.Left = 5;
                LB.Width = 100;
                LB.Height = 50;
                LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                LB.BackColor = Color.Transparent;
                LB.Cursor = Cursors.SizeAll;
                LB.BringToFront();
                FI_TL.Top = LB.Top - 3;
                FI_TL.Left = LB.Left - 3;
                FI_TL.BringToFront();
                FI_TR.Top = LB.Top - 3;
                FI_TR.Left = LB.Left + LB.Width - 3;
                FI_TR.BringToFront();
                FI_BL.Top = LB.Top + LB.Height - 3;
                FI_BL.Left = LB.Left - 3;
                FI_BL.BringToFront();
                FI_BR.Top = LB.Top + LB.Height - 3;
                FI_BR.Left = LB.Left + LB.Width - 3;
                FI_BR.BringToFront();
                LB.KeyActive = false;
                LB.KeyValue = "";
                LB.OutputShow = true;
                LB.OutputTitle = "";
                LB.Similarity = "100";
                LB.OcrPosition = "L";
                LB.DataProcessing = false;
                LB.TypeCode = 1;
                LB.SubstringStart = 0;
                LB.SubstringLength = 0;
                LB.SubstringLeft = false;
                LB.InputFormat = "";
                LB.InputFormatSeprator = "";
                LB.OutputFormat = "";
                LB.OutputFormatSeprator = "";
                LB.Text = "";
                LB.ForeColor = Color.Black;
                LB.TransparentBackColor = Color.Red;
                LB.Click += new EventHandler(this.Trp_LB_Click);
                LB.MouseDown += new MouseEventHandler(this.TraLB_MouseDown);
                LB.MouseMove += new MouseEventHandler(this.TraLB_MouseMove);
                LB.MouseUp += new MouseEventHandler(this.TraLB_MouseUp);
                P5_SelObject = LB;
                ShowTrpLBPro();
                LB.Visible = true;
                FI_TL.Visible = true;
                FI_TR.Visible = true;
                FI_BL.Visible = true;
                FI_BR.Visible = true;
                LB.Invalidate();
                P5_T1.Focus();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (P5_SelObject != null)
                {
                    P5_SelObject.Dispose();
                    P5_SelObject = null;
                }
                P5_Pan_Click(null, null);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_Btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                P5_Pan_Click(null, null);
                foreach (Control TRPL in P5_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P5_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P5_Pan_Click(null, null);
                foreach (Control TRPL in P5_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P5_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P5_Pan_Click(null, null);
                foreach (Control TRPL in P5_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P5_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P5_Pan_Click(null, null);
                foreach (Control TRPL in P5_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P5_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            P5_Pan_Click(null, null);
        }
        //==========================================================================
        void ShowTrpLBPro()
        {
            try
            {
                ObjectLoad = true;
                P5_T1.Text = "";
                P5_T2.Text = "";
                P5_T3.Text = "";
                P5_T4.Text = "";
                P5_T5.Text = "";
                P5_T6.Text = "";
                P5_T7.Text = "";
                P5_T8.Text = "";
                P5_T9.Text = "";
                P5_T10.Text = "";
                P5_T11.Text = "";
                P5_T12.Text = "";
                P5_T13.Text = "";
                P5_T14.Text = "";
                P5_T15.Text = "";
                P5_T16.Text = "";
                P5_T17.Text = "";
                P5_T18.Text = "";
                P5_T19.Text = "";
                P5_C1.Checked = false;
                P5_C2.Checked = false;
                P5_C3.Checked = false;
                P5_C4.Checked = false;
                P5_D1.SelectedIndex = 0;
                if (P5_SelObject != null)
                {
                    P5_T1.Text = P5_SelObject.Left.ToString();
                    P5_T2.Text = P5_SelObject.Top.ToString();
                    P5_T3.Text = (P5_SelObject.Left + P5_SelObject.Width).ToString();
                    P5_T4.Text = (P5_SelObject.Top + P5_SelObject.Height).ToString();
                    P5_T5.Text = P5_SelObject.Top.ToString();
                    P5_T6.Text = P5_SelObject.Left.ToString();
                    P5_T7.Text = P5_SelObject.Width.ToString();
                    P5_T8.Text = P5_SelObject.Height.ToString();
                    P5_T9.Text = P5_SelObject.OutputTitle.Trim();
                    P5_T10.Text = P5_SelObject.KeyValue.Trim();
                    P5_T11.Text = P5_SelObject.Similarity;
                    P5_T12.Text = P5_SelObject.OcrIndex;
                    P5_T13.Text = P5_SelObject.OcrPosition;
                    P5_T14.Text = P5_SelObject.SubstringStart.ToString();
                    P5_T15.Text = P5_SelObject.SubstringLength.ToString();
                    P5_T16.Text = P5_SelObject.InputFormat.Trim();
                    P5_T17.Text = P5_SelObject.InputFormatSeprator;
                    P5_T18.Text = P5_SelObject.OutputFormat.Trim();
                    P5_T19.Text = P5_SelObject.OutputFormatSeprator;
                    P5_C1.Checked = P5_SelObject.OutputShow;
                    P5_C2.Checked = P5_SelObject.KeyActive;
                    P5_C3.Checked = P5_SelObject.DataProcessing;
                    P5_C4.Checked = P5_SelObject.SubstringLeft;
                    try
                    {
                        P5_D1.SelectedIndex = P5_SelObject.TypeCode - 1;
                    }
                    catch (Exception)
                    {
                        P5_D1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            { }
            ObjectLoad = false;
        }
        //==========================================================================
        private void Trp_LB_Click(object sender, EventArgs e)
        {
            try
            {
                TransparentLabel LB = (TransparentLabel)sender;
                if (P5_SelObject != null)
                {
                    if (P5_SelObject.Name.Trim() != LB.Name.Trim())
                    {
                        P5_Pan_Click(null, null);
                        FI_TL.Parent = P5_IMG;
                        FI_TR.Parent = P5_IMG;
                        FI_BL.Parent = P5_IMG;
                        FI_BR.Parent = P5_IMG;
                        LB.Cursor = Cursors.SizeAll;
                        LB.BringToFront();
                        FI_TL.Top = LB.Top - 3;
                        FI_TL.Left = LB.Left - 3;
                        FI_TL.BringToFront();
                        FI_TR.Top = LB.Top - 3;
                        FI_TR.Left = LB.Left + LB.Width - 3;
                        FI_TR.BringToFront();
                        FI_BL.Top = LB.Top + LB.Height - 3;
                        FI_BL.Left = LB.Left - 3;
                        FI_BL.BringToFront();
                        FI_BR.Top = LB.Top + LB.Height - 3;
                        FI_BR.Left = LB.Left + LB.Width - 3;
                        FI_BR.BringToFront();
                        LB.TransparentBackColor = Color.Red;
                        P5_SelObject = LB;
                        ShowTrpLBPro();
                        LB.Visible = true;
                        FI_TL.Visible = true;
                        FI_TR.Visible = true;
                        FI_BL.Visible = true;
                        FI_BR.Visible = true;
                        LB.Invalidate();
                    }
                }
                else
                {
                    FI_TL.Parent = P5_IMG;
                    FI_TR.Parent = P5_IMG;
                    FI_BL.Parent = P5_IMG;
                    FI_BR.Parent = P5_IMG;
                    LB.Cursor = Cursors.SizeAll;
                    LB.BringToFront();
                    FI_TL.Top = LB.Top - 3;
                    FI_TL.Left = LB.Left - 3;
                    FI_TL.BringToFront();
                    FI_TR.Top = LB.Top - 3;
                    FI_TR.Left = LB.Left + LB.Width - 3;
                    FI_TR.BringToFront();
                    FI_BL.Top = LB.Top + LB.Height - 3;
                    FI_BL.Left = LB.Left - 3;
                    FI_BL.BringToFront();
                    FI_BR.Top = LB.Top + LB.Height - 3;
                    FI_BR.Left = LB.Left + LB.Width - 3;
                    FI_BR.BringToFront();
                    LB.TransparentBackColor = Color.Red;
                    P5_SelObject = LB;
                    ShowTrpLBPro();
                    LB.Visible = true;
                    FI_TL.Visible = true;
                    FI_TR.Visible = true;
                    FI_BL.Visible = true;
                    FI_BR.Visible = true;
                    LB.Invalidate();
                }
                P5_T1.Focus();
            }
            catch (Exception)
            {
                P5_Pan_Click(null, null);
            }
        }
        //==========================================================================
        private void P5_C1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P5_SelObject != null)
                {
                    P5_SelObject.OutputShow = P5_C1.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_C2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P5_SelObject != null)
                {
                    P5_SelObject.KeyActive = P5_C2.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_C3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P5_SelObject != null)
                {
                    P5_SelObject.DataProcessing = P5_C3.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_C4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P5_SelObject != null)
                {
                    P5_SelObject.SubstringLeft = P5_C4.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_D1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P5_SelObject != null)
                {
                    P5_SelObject.TypeCode = P5_D1.SelectedIndex + 1;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TraLB_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (P5_SelObject == null) { Trp_LB_Click(sender, null); }
                    ObjectLoad = true;
                    TransparentLabel LB = sender as TransparentLabel;
                    if (P5_SelObject.Name.Trim() != LB.Name.Trim()) { Trp_LB_Click(sender, null); }
                    MouseDownLocation = e.Location;
                    LB.Left = e.X + LB.Left - MouseDownLocation.X;
                    LB.Top = e.Y + LB.Top - MouseDownLocation.Y;
                    LB.BringToFront();
                    FI_TL.Visible = false;
                    FI_TR.Visible = false;
                    FI_BL.Visible = false;
                    FI_BR.Visible = false;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TraLB_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (P5_SelObject == null) { return; }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    TransparentLabel LB = sender as TransparentLabel;
                    LB.Left = e.X + LB.Left - MouseDownLocation.X;
                    LB.Top = e.Y + LB.Top - MouseDownLocation.Y;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TraLB_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (P5_SelObject == null) { return; }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    TransparentLabel LB = sender as TransparentLabel;
                    LB.BringToFront();
                    FI_TL.Top = LB.Top - 3;
                    FI_TL.Left = LB.Left - 3;
                    FI_TL.BringToFront();
                    FI_TR.Top = LB.Top - 3;
                    FI_TR.Left = LB.Left + LB.Width - 3;
                    FI_TR.BringToFront();
                    FI_BL.Top = LB.Top + LB.Height - 3;
                    FI_BL.Left = LB.Left - 3;
                    FI_BL.BringToFront();
                    FI_BR.Top = LB.Top + LB.Height - 3;
                    FI_BR.Left = LB.Left + LB.Width - 3;
                    FI_BR.BringToFront();
                    FI_TL.Visible = true;
                    FI_TR.Visible = true;
                    FI_BL.Visible = true;
                    FI_BR.Visible = true;
                    ShowTrpLBPro();
                    ObjectLoad = false;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_T_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 13) { P5_SetValue(sender); }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_T_Leave(object sender, EventArgs e)
        {
            try
            {
                P5_SetValue(sender);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_TT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) { P5_SetValue(sender); }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P5_SetValue(object sender)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P5_SelObject != null)
                {
                    bool ReloadND = false;
                    System.Windows.Forms.TextBox Tx = sender as System.Windows.Forms.TextBox;
                    int TxVal = 0;
                    try { TxVal = int.Parse(Tx.Text); } catch (Exception) { }
                    switch (Tx.Name.ToUpper())
                    {
                        case "P5_T1":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P5_IMG.Width - 10)))
                                {
                                    P5_SelObject.Left = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T1.Text = P5_SelObject.Left.ToString();
                                }
                                break;
                            }
                        case "P5_T2":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P5_IMG.Height - 10)))
                                {
                                    P5_SelObject.Top = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T2.Text = P5_SelObject.Top.ToString();
                                }
                                break;
                            }
                        case "P5_T3":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P5_IMG.Width - 5)))
                                {
                                    if (TxVal >= (P5_SelObject.Left + 5))
                                    {
                                        P5_SelObject.Width = TxVal - P5_SelObject.Left;
                                        ReloadND = true;
                                    }
                                    else
                                    {
                                        P5_T3.Text = (P5_SelObject.Left + P5_SelObject.Width).ToString();
                                    }
                                }
                                else
                                {
                                    P5_T3.Text = (P5_SelObject.Left + P5_SelObject.Width).ToString();
                                }
                                break;
                            }
                        case "P5_T4":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P5_IMG.Height - 5)))
                                {
                                    if (TxVal >= (P5_SelObject.Top + 5))
                                    {
                                        P5_SelObject.Height = TxVal - P5_SelObject.Top;
                                        ReloadND = true;
                                    }
                                    else
                                    {
                                        P5_T4.Text = (P5_SelObject.Top + P5_SelObject.Height).ToString();
                                    }
                                }
                                else
                                {
                                    P5_T4.Text = (P5_SelObject.Top + P5_SelObject.Height).ToString();
                                }
                                break;
                            }
                        case "P5_T5":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P5_IMG.Height - 10)))
                                {
                                    P5_SelObject.Top = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T5.Text = P5_SelObject.Top.ToString();
                                }
                                break;
                            }
                        case "P5_T6":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P5_IMG.Width - 10)))
                                {
                                    P5_SelObject.Left = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T6.Text = P5_SelObject.Left.ToString();
                                }
                                break;
                            }
                        case "P5_T7":
                            {
                                if ((TxVal >= 5) && ((TxVal + P5_SelObject.Left) <= P5_IMG.Width))
                                {
                                    P5_SelObject.Width = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T7.Text = P5_SelObject.Width.ToString();
                                }
                                break;
                            }
                        case "P5_T8":
                            {
                                if ((TxVal >= 5) && ((TxVal + P5_SelObject.Height) <= P5_IMG.Height))
                                {
                                    P5_SelObject.Height = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T8.Text = P5_SelObject.Height.ToString();
                                }
                                break;
                            }
                        case "P5_T9":
                            {
                                P5_T9.Text = P5_T9.Text.Trim();
                                if (P5_T9.Text.Length > 255) { P5_T9.Text = P5_T9.Text.Substring(0, 254); }
                                P5_SelObject.OutputTitle = P5_T9.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T10":
                            {
                                P5_T10.Text = P5_T10.Text.Trim();
                                if (P5_T10.Text.Length > 255) { P5_T10.Text = P5_T10.Text.Substring(0, 254); }
                                P5_SelObject.KeyValue = P5_T10.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T11":
                            {
                                if (TxVal < 0) { TxVal = 0; }
                                if (TxVal > 100) { TxVal = 100; }
                                P5_SelObject.Similarity = TxVal.ToString();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T12":
                            {
                                if (TxVal < 0) { TxVal = 0; }
                                if (TxVal > 100) { TxVal = 100; }
                                P5_SelObject.OcrIndex = TxVal.ToString();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T13":
                            {
                                P5_T13.Text = P5_T13.Text.Trim().ToUpper();
                                if (P5_T13.Text.Length > 2) { P5_T13.Text = P5_T13.Text.Substring(0, 2); }
                                P5_SelObject.OcrPosition = P5_T13.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T14":
                            {
                                if ((TxVal >= 0) && (TxVal <= 9999))
                                {
                                    P5_SelObject.SubstringStart = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T14.Text = P5_SelObject.SubstringStart.ToString();
                                }
                                break;
                            }
                        case "P5_T15":
                            {
                                if ((TxVal >= 0) && (TxVal <= 9999))
                                {
                                    P5_SelObject.SubstringLength = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P5_T15.Text = P5_SelObject.SubstringLength.ToString();
                                }
                                break;
                            }
                        case "P5_T16":
                            {
                                P5_SelObject.InputFormat = P5_T16.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T17":
                            {
                                P5_SelObject.InputFormatSeprator = P5_T17.Text;
                                ReloadND = true;
                                break;
                            }
                        case "P5_T18":
                            {
                                P5_SelObject.OutputFormat = P5_T18.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P5_T19":
                            {
                                P5_SelObject.OutputFormatSeprator = P5_T19.Text;
                                ReloadND = true;
                                break;
                            }
                    }
                    if (ReloadND == true)
                    {
                        P5_SelObject.BringToFront();
                        FI_TL.Top = P5_SelObject.Top - 3;
                        FI_TL.Left = P5_SelObject.Left - 3;
                        FI_TL.BringToFront();
                        FI_TR.Top = P5_SelObject.Top - 3;
                        FI_TR.Left = P5_SelObject.Left + P5_SelObject.Width - 3;
                        FI_TR.BringToFront();
                        FI_BL.Top = P5_SelObject.Top + P5_SelObject.Height - 3;
                        FI_BL.Left = P5_SelObject.Left - 3;
                        FI_BL.BringToFront();
                        FI_BR.Top = P5_SelObject.Top + P5_SelObject.Height - 3;
                        FI_BR.Left = P5_SelObject.Left + P5_SelObject.Width - 3;
                        FI_BR.BringToFront();
                        FI_TL.Visible = true;
                        FI_TR.Visible = true;
                        FI_BL.Visible = true;
                        FI_BR.Visible = true;
                        P5_T1.Text = P5_SelObject.Left.ToString();
                        P5_T2.Text = P5_SelObject.Top.ToString();
                        P5_T3.Text = (P5_SelObject.Left + P5_SelObject.Width).ToString();
                        P5_T4.Text = (P5_SelObject.Top + P5_SelObject.Height).ToString();
                        P5_T5.Text = P5_SelObject.Top.ToString();
                        P5_T6.Text = P5_SelObject.Left.ToString();
                        P5_T7.Text = P5_SelObject.Width.ToString();
                        P5_T8.Text = P5_SelObject.Height.ToString();
                        P5_T9.Text = P5_SelObject.OutputTitle.Trim();
                        P5_T10.Text = P5_SelObject.KeyValue.Trim();
                        P5_T11.Text = P5_SelObject.Similarity;
                        P5_T12.Text = P5_SelObject.OcrIndex;
                        P5_T13.Text = P5_SelObject.OcrPosition;
                        P5_T14.Text = P5_SelObject.SubstringStart.ToString();
                        P5_T15.Text = P5_SelObject.SubstringLength.ToString();
                        P5_T16.Text = P5_SelObject.InputFormat.ToString();
                        P5_T17.Text = P5_SelObject.InputFormatSeprator.ToString();
                        P5_T18.Text = P5_SelObject.OutputFormat.ToString();
                        P5_T19.Text = P5_SelObject.OutputFormatSeprator.ToString();
                        P5_C1.Checked = P5_SelObject.OutputShow;
                        P5_C2.Checked = P5_SelObject.KeyActive;
                        P5_C3.Checked = P5_SelObject.DataProcessing;
                        P5_C4.Checked = P5_SelObject.SubstringLeft;
                        try { P5_D1.SelectedIndex = P5_SelObject.TypeCode - 1; } catch (Exception) { P5_D1.SelectedIndex = 0; }
                    }
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void FI_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (P5_SelObject == null) { return; }
                    P5_SelObj_Size.Width = P5_SelObject.Width;
                    P5_SelObj_Size.Height = P5_SelObject.Height;
                    P5_SelObj_Location.X = P5_SelObject.Left;
                    P5_SelObj_Location.Y = P5_SelObject.Top;
                    FI_TL.Visible = false;
                    FI_TR.Visible = false;
                    FI_BL.Visible = false;
                    FI_BR.Visible = false;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void FI_MouseMove(object sender, MouseEventArgs e)
        {
            if (P5_SelObject == null) { return; }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PictureBox PB = sender as PictureBox;
                switch (PB.Name)
                {
                    case "FI_TL": // Top - Left
                        {
                            if (P5_SelObject.Width >= 5)
                            {
                                P5_SelObject.Left = e.X + P5_SelObj_Location.X;
                                P5_SelObject.Width = P5_SelObj_Size.Width - e.X;
                                if (P5_SelObject.Width < 5) { P5_SelObject.Width = 5; }
                            }
                            if (P5_SelObject.Height >= 5)
                            {
                                P5_SelObject.Top = e.Y + P5_SelObj_Location.Y;
                                P5_SelObject.Height = P5_SelObj_Size.Height - e.Y;
                                if (P5_SelObject.Height < 5) { P5_SelObject.Height = 5; }
                            }
                            break;
                        }
                    case "FI_TR": // Top - Right
                        {
                            if ((e.X + P5_SelObj_Size.Width) > 5) { P5_SelObject.Width = e.X + P5_SelObj_Size.Width; }
                            if (P5_SelObject.Height >= 5)
                            {
                                P5_SelObject.Top = e.Y + P5_SelObj_Location.Y;
                                P5_SelObject.Height = P5_SelObj_Size.Height - e.Y;
                                if (P5_SelObject.Height < 5) { P5_SelObject.Height = 5; }
                            }
                            break;
                        }
                    case "FI_BR": // Bot - Right
                        {
                            if ((e.X + P5_SelObj_Size.Width) > 5) { P5_SelObject.Width = e.X + P5_SelObj_Size.Width; }
                            if ((e.Y + P5_SelObj_Size.Height) > 5) { P5_SelObject.Height = e.Y + P5_SelObj_Size.Height; }
                            break;
                        }
                    case "FI_BL": // Bot - Left
                        {
                            if (P5_SelObject.Width >= 5)
                            {
                                P5_SelObject.Left = e.X + P5_SelObj_Location.X;
                                P5_SelObject.Width = P5_SelObj_Size.Width - e.X;
                                if (P5_SelObject.Width < 5) { P5_SelObject.Width = 5; }
                            }
                            if ((e.Y + P5_SelObj_Size.Height) > 5) { P5_SelObject.Height = e.Y + P5_SelObj_Size.Height; }
                            break;
                        }
                }
            }
        }
        //==========================================================================
        private void FI_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                FI_TL.Parent = P5_IMG;
                FI_TR.Parent = P5_IMG;
                FI_BL.Parent = P5_IMG;
                FI_BR.Parent = P5_IMG;
                P5_SelObject.Cursor = Cursors.SizeAll;
                P5_SelObject.BringToFront();
                FI_TL.Top = P5_SelObject.Top - 3;
                FI_TL.Left = P5_SelObject.Left - 3;
                FI_TL.BringToFront();
                FI_TR.Top = P5_SelObject.Top - 3;
                FI_TR.Left = P5_SelObject.Left + P5_SelObject.Width - 3;
                FI_TR.BringToFront();
                FI_BL.Top = P5_SelObject.Top + P5_SelObject.Height - 3;
                FI_BL.Left = P5_SelObject.Left - 3;
                FI_BL.BringToFront();
                FI_BR.Top = P5_SelObject.Top + P5_SelObject.Height - 3;
                FI_BR.Left = P5_SelObject.Left + P5_SelObject.Width - 3;
                FI_BR.BringToFront();
                P5_SelObject.TransparentBackColor = Color.Red;
                ShowTrpLBPro();
                P5_SelObject.Visible = true;
                FI_TL.Visible = true;
                FI_TR.Visible = true;
                FI_BL.Visible = true;
                FI_BR.Visible = true;
                P5_SelObject.Invalidate();
                P5_SelObj_Size.Width = 0;
                P5_SelObj_Size.Height = 0;
                P5_SelObj_Location.X = 0;
                P5_SelObj_Location.Y = 0;
            }
            catch (Exception)
            { }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Panel 6 - Back Image Elements Btn :
        private void P6_Pan_Click(object sender, EventArgs e)
        {
            try
            {
                if (P6_SelObject != null)
                {
                    P6_SelObject.Cursor = Cursors.Default;
                    P6_SelObject.transparentBackColor = Color.Transparent;
                    P6_SelObject.Invalidate();
                    P6_SelObject = null;
                }
                ObjectLoad = true;
                BI_TL.Visible = false;
                BI_TR.Visible = false;
                BI_BL.Visible = false;
                BI_BR.Visible = false;
                P6_T1.Text = "";
                P6_T2.Text = "";
                P6_T3.Text = "";
                P6_T4.Text = "";
                P6_T5.Text = "";
                P6_T6.Text = "";
                P6_T7.Text = "";
                P6_T8.Text = "";
                P6_T9.Text = "";
                P6_T10.Text = "";
                P6_T11.Text = "";
                P6_T12.Text = "";
                P6_T13.Text = "";
                P6_T14.Text = "";
                P6_T15.Text = "";
                P6_T16.Text = "";
                P6_T17.Text = "";
                P6_T18.Text = "";
                P6_T19.Text = "";
                P6_C1.Checked = false;
                P6_C2.Checked = false;
                P6_C3.Checked = false;
                P6_C4.Checked = false;
                P6_D1.SelectedIndex = 0;
                ObjectLoad = false;
                P6_Pan.Focus();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_IMG_Click(object sender, EventArgs e)
        {
            try { P6_Pan_Click(null, null); } catch (Exception) { }
        }
        //==========================================================================
        private void P6_Btn_New_Click(object sender, EventArgs e)
        {
            try
            {
                P6_Pan_Click(null, null);
                BICounter++;
                BI_TL.Parent = P6_IMG;
                BI_TR.Parent = P6_IMG;
                BI_BL.Parent = P6_IMG;
                BI_BR.Parent = P6_IMG;
                TransparentLabel LB = new TransparentLabel();
                LB.Name = "BI" + BICounter.ToString();
                LB.Visible = false;
                LB.Parent = P6_IMG;
                LB.TransparentBackColor = Color.Transparent;
                LB.AutoSize = false;
                LB.Top = 5;
                LB.Left = 5;
                LB.Width = 100;
                LB.Height = 50;
                LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                LB.BackColor = Color.Transparent;
                LB.Cursor = Cursors.SizeAll;
                LB.BringToFront();
                BI_TL.Top = LB.Top - 3;
                BI_TL.Left = LB.Left - 3;
                BI_TL.BringToFront();
                BI_TR.Top = LB.Top - 3;
                BI_TR.Left = LB.Left + LB.Width - 3;
                BI_TR.BringToFront();
                BI_BL.Top = LB.Top + LB.Height - 3;
                BI_BL.Left = LB.Left - 3;
                BI_BL.BringToFront();
                BI_BR.Top = LB.Top + LB.Height - 3;
                BI_BR.Left = LB.Left + LB.Width - 3;
                BI_BR.BringToFront();
                LB.KeyActive = false;
                LB.KeyValue = "";
                LB.OutputShow = true;
                LB.OutputTitle = "";
                LB.Similarity = "100";
                LB.OcrPosition = "L";
                LB.DataProcessing = false;
                LB.TypeCode = 1;
                LB.SubstringStart = 0;
                LB.SubstringLength = 0;
                LB.SubstringLeft = false;
                LB.InputFormat = "";
                LB.InputFormatSeprator = "";
                LB.OutputFormat = "";
                LB.OutputFormatSeprator = "";
                LB.Text = "";
                LB.ForeColor = Color.Black;
                LB.TransparentBackColor = Color.Red;
                LB.Click += new EventHandler(this.Trp_LB_Click6);
                LB.MouseDown += new MouseEventHandler(this.TraLB_MouseDown6);
                LB.MouseMove += new MouseEventHandler(this.TraLB_MouseMove6);
                LB.MouseUp += new MouseEventHandler(this.TraLB_MouseUp6);
                P6_SelObject = LB;
                ShowTrpLBPro6();
                LB.Visible = true;
                BI_TL.Visible = true;
                BI_TR.Visible = true;
                BI_BL.Visible = true;
                BI_BR.Visible = true;
                LB.Invalidate();
                P6_T1.Focus();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (P6_SelObject != null)
                {
                    P6_SelObject.Dispose();
                    P6_SelObject = null;
                }
                P6_Pan_Click(null, null);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_Btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                P6_Pan_Click(null, null);
                foreach (Control TRPL in P6_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P6_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P6_Pan_Click(null, null);
                foreach (Control TRPL in P6_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P6_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P6_Pan_Click(null, null);
                foreach (Control TRPL in P6_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P6_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P6_Pan_Click(null, null);
                foreach (Control TRPL in P6_IMG.Controls)
                {
                    if (TRPL is TransparentLabel)
                    {
                        P6_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            P6_Pan_Click(null, null);
        }
        //==========================================================================
        void ShowTrpLBPro6()
        {
            try
            {
                ObjectLoad = true;
                P6_T1.Text = "";
                P6_T2.Text = "";
                P6_T3.Text = "";
                P6_T4.Text = "";
                P6_T5.Text = "";
                P6_T6.Text = "";
                P6_T7.Text = "";
                P6_T8.Text = "";
                P6_T9.Text = "";
                P6_T10.Text = "";
                P6_T11.Text = "";
                P6_T12.Text = "";
                P6_T13.Text = "";
                P6_T14.Text = "";
                P6_T15.Text = "";
                P6_T16.Text = "";
                P6_T17.Text = "";
                P6_T18.Text = "";
                P6_T19.Text = "";
                P6_C1.Checked = false;
                P6_C2.Checked = false;
                P6_C3.Checked = false;
                P6_C4.Checked = false;
                P6_D1.SelectedIndex = 0;
                if (P6_SelObject != null)
                {
                    P6_T1.Text = P6_SelObject.Left.ToString();
                    P6_T2.Text = P6_SelObject.Top.ToString();
                    P6_T3.Text = (P6_SelObject.Left + P6_SelObject.Width).ToString();
                    P6_T4.Text = (P6_SelObject.Top + P6_SelObject.Height).ToString();
                    P6_T5.Text = P6_SelObject.Top.ToString();
                    P6_T6.Text = P6_SelObject.Left.ToString();
                    P6_T7.Text = P6_SelObject.Width.ToString();
                    P6_T8.Text = P6_SelObject.Height.ToString();
                    P6_T9.Text = P6_SelObject.OutputTitle.Trim();
                    P6_T10.Text = P6_SelObject.KeyValue.Trim();
                    P6_T11.Text = P6_SelObject.Similarity;
                    P6_T12.Text = P6_SelObject.OcrIndex;
                    P6_T13.Text = P6_SelObject.OcrPosition;
                    P6_T14.Text = P6_SelObject.SubstringStart.ToString();
                    P6_T15.Text = P6_SelObject.SubstringLength.ToString();
                    P6_T16.Text = P6_SelObject.InputFormat;
                    P6_T17.Text = P6_SelObject.InputFormatSeprator;
                    P6_T18.Text = P6_SelObject.OutputFormat;
                    P6_T19.Text = P6_SelObject.OutputFormatSeprator;
                    P6_C1.Checked = P6_SelObject.OutputShow;
                    P6_C2.Checked = P6_SelObject.KeyActive;
                    P6_C3.Checked = P6_SelObject.DataProcessing;
                    P6_C4.Checked = P6_SelObject.SubstringLeft;
                    try
                    {
                        P6_D1.SelectedIndex = P6_SelObject.TypeCode - 1;
                    }
                    catch (Exception)
                    {
                        P6_D1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            { }
            ObjectLoad = false;
        }
        //==========================================================================
        private void Trp_LB_Click6(object sender, EventArgs e)
        {
            try
            {
                TransparentLabel LB = (TransparentLabel)sender;
                if (P6_SelObject != null)
                {
                    if (P6_SelObject.Name.Trim() != LB.Name.Trim())
                    {
                        P6_Pan_Click(null, null);
                        BI_TL.Parent = P6_IMG;
                        BI_TR.Parent = P6_IMG;
                        BI_BL.Parent = P6_IMG;
                        BI_BR.Parent = P6_IMG;
                        LB.Cursor = Cursors.SizeAll;
                        LB.BringToFront();
                        BI_TL.Top = LB.Top - 3;
                        BI_TL.Left = LB.Left - 3;
                        BI_TL.BringToFront();
                        BI_TR.Top = LB.Top - 3;
                        BI_TR.Left = LB.Left + LB.Width - 3;
                        BI_TR.BringToFront();
                        BI_BL.Top = LB.Top + LB.Height - 3;
                        BI_BL.Left = LB.Left - 3;
                        BI_BL.BringToFront();
                        BI_BR.Top = LB.Top + LB.Height - 3;
                        BI_BR.Left = LB.Left + LB.Width - 3;
                        BI_BR.BringToFront();
                        LB.TransparentBackColor = Color.Red;
                        P6_SelObject = LB;
                        ShowTrpLBPro6();
                        LB.Visible = true;
                        BI_TL.Visible = true;
                        BI_TR.Visible = true;
                        BI_BL.Visible = true;
                        BI_BR.Visible = true;
                        LB.Invalidate();
                    }
                }
                else
                {
                    BI_TL.Parent = P6_IMG;
                    BI_TR.Parent = P6_IMG;
                    BI_BL.Parent = P6_IMG;
                    BI_BR.Parent = P6_IMG;
                    LB.Cursor = Cursors.SizeAll;
                    LB.BringToFront();
                    BI_TL.Top = LB.Top - 3;
                    BI_TL.Left = LB.Left - 3;
                    BI_TL.BringToFront();
                    BI_TR.Top = LB.Top - 3;
                    BI_TR.Left = LB.Left + LB.Width - 3;
                    BI_TR.BringToFront();
                    BI_BL.Top = LB.Top + LB.Height - 3;
                    BI_BL.Left = LB.Left - 3;
                    BI_BL.BringToFront();
                    BI_BR.Top = LB.Top + LB.Height - 3;
                    BI_BR.Left = LB.Left + LB.Width - 3;
                    BI_BR.BringToFront();
                    LB.TransparentBackColor = Color.Red;
                    P6_SelObject = LB;
                    ShowTrpLBPro6();
                    LB.Visible = true;
                    BI_TL.Visible = true;
                    BI_TR.Visible = true;
                    BI_BL.Visible = true;
                    BI_BR.Visible = true;
                    LB.Invalidate();
                }
                P6_T1.Focus();
            }
            catch (Exception)
            {
                P6_Pan_Click(null, null);
            }
        }
        //==========================================================================
        private void P6_C1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P6_SelObject != null)
                {
                    P6_SelObject.OutputShow = P6_C1.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_C2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P6_SelObject != null)
                {
                    P6_SelObject.KeyActive = P6_C2.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_C3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P6_SelObject != null)
                {
                    P6_SelObject.DataProcessing = P6_C3.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_C4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P6_SelObject != null)
                {
                    P6_SelObject.SubstringLeft = P6_C4.Checked;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_D1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P6_SelObject != null)
                {
                    P6_SelObject.TypeCode = P6_D1.SelectedIndex + 1;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TraLB_MouseDown6(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (P6_SelObject == null) { Trp_LB_Click6(sender, null); }
                    ObjectLoad = true;
                    TransparentLabel LB = sender as TransparentLabel;
                    if (P6_SelObject.Name.Trim() != LB.Name.Trim()) { Trp_LB_Click6(sender, null); }
                    MouseDownLocation = e.Location;
                    LB.Left = e.X + LB.Left - MouseDownLocation.X;
                    LB.Top = e.Y + LB.Top - MouseDownLocation.Y;
                    LB.BringToFront();
                    BI_TL.Visible = false;
                    BI_TR.Visible = false;
                    BI_BL.Visible = false;
                    BI_BR.Visible = false;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TraLB_MouseMove6(object sender, MouseEventArgs e)
        {
            try
            {
                if (P6_SelObject == null) { return; }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    TransparentLabel LB = sender as TransparentLabel;
                    LB.Left = e.X + LB.Left - MouseDownLocation.X;
                    LB.Top = e.Y + LB.Top - MouseDownLocation.Y;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void TraLB_MouseUp6(object sender, MouseEventArgs e)
        {
            try
            {
                if (P6_SelObject == null) { return; }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    TransparentLabel LB = sender as TransparentLabel;
                    LB.BringToFront();
                    BI_TL.Top = LB.Top - 3;
                    BI_TL.Left = LB.Left - 3;
                    BI_TL.BringToFront();
                    BI_TR.Top = LB.Top - 3;
                    BI_TR.Left = LB.Left + LB.Width - 3;
                    BI_TR.BringToFront();
                    BI_BL.Top = LB.Top + LB.Height - 3;
                    BI_BL.Left = LB.Left - 3;
                    BI_BL.BringToFront();
                    BI_BR.Top = LB.Top + LB.Height - 3;
                    BI_BR.Left = LB.Left + LB.Width - 3;
                    BI_BR.BringToFront();
                    BI_TL.Visible = true;
                    BI_TR.Visible = true;
                    BI_BL.Visible = true;
                    BI_BR.Visible = true;
                    ShowTrpLBPro6();
                    ObjectLoad = false;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_T_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 13) { P6_SetValue(sender); }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_T_Leave(object sender, EventArgs e)
        {
            try
            {
                P6_SetValue(sender);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_TT_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) { P6_SetValue(sender); }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P6_SetValue(object sender)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P6_SelObject != null)
                {
                    bool ReloadND = false;
                    System.Windows.Forms.TextBox Tx = sender as System.Windows.Forms.TextBox;
                    int TxVal = 0;
                    try { TxVal = int.Parse(Tx.Text); } catch (Exception) { }
                    switch (Tx.Name.ToUpper())
                    {
                        case "P6_T1":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P6_IMG.Width - 10)))
                                {
                                    P6_SelObject.Left = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T1.Text = P6_SelObject.Left.ToString();
                                }
                                break;
                            }
                        case "P6_T2":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P6_IMG.Height - 10)))
                                {
                                    P6_SelObject.Top = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T2.Text = P6_SelObject.Top.ToString();
                                }
                                break;
                            }
                        case "P6_T3":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P6_IMG.Width - 5)))
                                {
                                    if (TxVal >= (P6_SelObject.Left + 5))
                                    {
                                        P6_SelObject.Width = TxVal - P6_SelObject.Left;
                                        ReloadND = true;
                                    }
                                    else
                                    {
                                        P6_T3.Text = (P6_SelObject.Left + P6_SelObject.Width).ToString();
                                    }
                                }
                                else
                                {
                                    P6_T3.Text = (P6_SelObject.Left + P6_SelObject.Width).ToString();
                                }
                                break;
                            }
                        case "P6_T4":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P6_IMG.Height - 5)))
                                {
                                    if (TxVal >= (P6_SelObject.Top + 5))
                                    {
                                        P6_SelObject.Height = TxVal - P6_SelObject.Top;
                                        ReloadND = true;
                                    }
                                    else
                                    {
                                        P6_T4.Text = (P6_SelObject.Top + P6_SelObject.Height).ToString();
                                    }
                                }
                                else
                                {
                                    P6_T4.Text = (P6_SelObject.Top + P6_SelObject.Height).ToString();
                                }
                                break;
                            }
                        case "P6_T5":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P6_IMG.Height - 10)))
                                {
                                    P6_SelObject.Top = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T5.Text = P6_SelObject.Top.ToString();
                                }
                                break;
                            }
                        case "P6_T6":
                            {
                                if ((TxVal >= 0) && (TxVal <= (P6_IMG.Width - 10)))
                                {
                                    P6_SelObject.Left = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T6.Text = P6_SelObject.Left.ToString();
                                }
                                break;
                            }
                        case "P6_T7":
                            {
                                if ((TxVal >= 5) && ((TxVal + P6_SelObject.Left) <= P6_IMG.Width))
                                {
                                    P6_SelObject.Width = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T7.Text = P6_SelObject.Width.ToString();
                                }
                                break;
                            }
                        case "P6_T8":
                            {
                                if ((TxVal >= 5) && ((TxVal + P6_SelObject.Height) <= P6_IMG.Height))
                                {
                                    P6_SelObject.Height = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T8.Text = P6_SelObject.Height.ToString();
                                }
                                break;
                            }
                        case "P6_T9":
                            {
                                P6_T9.Text = P6_T9.Text.Trim();
                                if (P6_T9.Text.Length > 255) { P6_T9.Text = P6_T9.Text.Substring(0, 254); }
                                P6_SelObject.OutputTitle = P6_T9.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T10":
                            {
                                P6_T10.Text = P6_T10.Text.Trim();
                                if (P6_T10.Text.Length > 255) { P6_T10.Text = P6_T10.Text.Substring(0, 254); }
                                P6_SelObject.KeyValue = P6_T10.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T11":
                            {
                                if (TxVal < 0) { TxVal = 0; }
                                if (TxVal > 100) { TxVal = 100; }
                                P6_SelObject.Similarity = TxVal.ToString();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T12":
                            {
                                if (TxVal < 0) { TxVal = 0; }
                                if (TxVal > 100) { TxVal = 100; }
                                P6_SelObject.OcrIndex = TxVal.ToString();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T13":
                            {
                                P6_T13.Text = P6_T13.Text.Trim().ToUpper();
                                if (P6_T13.Text.Length > 2) { P6_T13.Text = P6_T13.Text.Substring(0, 2); }
                                P6_SelObject.OcrPosition = P6_T13.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T14":
                            {
                                if ((TxVal >= 0) && (TxVal <= 9999))
                                {
                                    P6_SelObject.SubstringStart = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T14.Text = P6_SelObject.SubstringStart.ToString();
                                }
                                break;
                            }
                        case "P6_T15":
                            {
                                if ((TxVal >= 0) && (TxVal <= 9999))
                                {
                                    P6_SelObject.SubstringLength = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P6_T15.Text = P6_SelObject.SubstringLength.ToString();
                                }
                                break;
                            }
                        case "P6_T16":
                            {
                                P6_SelObject.InputFormat = P6_T16.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T17":
                            {
                                P6_SelObject.InputFormatSeprator = P6_T17.Text;
                                ReloadND = true;
                                break;
                            }
                        case "P6_T18":
                            {
                                P6_SelObject.OutputFormat = P6_T18.Text.Trim();
                                ReloadND = true;
                                break;
                            }
                        case "P6_T19":
                            {
                                P6_SelObject.OutputFormatSeprator = P6_T19.Text;
                                ReloadND = true;
                                break;
                            }
                    }
                    if (ReloadND == true)
                    {
                        P6_SelObject.BringToFront();
                        BI_TL.Top = P6_SelObject.Top - 3;
                        BI_TL.Left = P6_SelObject.Left - 3;
                        BI_TL.BringToFront();
                        BI_TR.Top = P6_SelObject.Top - 3;
                        BI_TR.Left = P6_SelObject.Left + P6_SelObject.Width - 3;
                        BI_TR.BringToFront();
                        BI_BL.Top = P6_SelObject.Top + P6_SelObject.Height - 3;
                        BI_BL.Left = P6_SelObject.Left - 3;
                        BI_BL.BringToFront();
                        BI_BR.Top = P6_SelObject.Top + P6_SelObject.Height - 3;
                        BI_BR.Left = P6_SelObject.Left + P6_SelObject.Width - 3;
                        BI_BR.BringToFront();
                        BI_TL.Visible = true;
                        BI_TR.Visible = true;
                        BI_BL.Visible = true;
                        BI_BR.Visible = true;
                        P6_T1.Text = P6_SelObject.Left.ToString();
                        P6_T2.Text = P6_SelObject.Top.ToString();
                        P6_T3.Text = (P6_SelObject.Left + P6_SelObject.Width).ToString();
                        P6_T4.Text = (P6_SelObject.Top + P6_SelObject.Height).ToString();
                        P6_T5.Text = P6_SelObject.Top.ToString();
                        P6_T6.Text = P6_SelObject.Left.ToString();
                        P6_T7.Text = P6_SelObject.Width.ToString();
                        P6_T8.Text = P6_SelObject.Height.ToString();
                        P6_T9.Text = P6_SelObject.OutputTitle.Trim();
                        P6_T10.Text = P6_SelObject.KeyValue.Trim();
                        P6_T11.Text = P6_SelObject.Similarity;
                        P6_T12.Text = P6_SelObject.OcrIndex;
                        P6_T13.Text = P6_SelObject.OcrPosition;
                        P6_T14.Text = P6_SelObject.SubstringStart.ToString();
                        P6_T15.Text = P6_SelObject.SubstringLength.ToString();
                        P6_T16.Text = P6_SelObject.InputFormat;
                        P6_T17.Text = P6_SelObject.InputFormatSeprator;
                        P6_T18.Text = P6_SelObject.OutputFormat;
                        P6_T19.Text = P6_SelObject.OutputFormatSeprator;
                        P6_C1.Checked = P6_SelObject.OutputShow;
                        P6_C2.Checked = P6_SelObject.KeyActive;
                        P6_C3.Checked = P6_SelObject.DataProcessing;
                        P6_C3.Checked = P6_SelObject.SubstringLeft;
                        try
                        {
                            P6_D1.SelectedIndex = P6_SelObject.TypeCode - 1;
                        }
                        catch (Exception)
                        {
                            P6_D1.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void BI_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (P6_SelObject == null) { return; }
                    P6_SelObj_Size.Width = P6_SelObject.Width;
                    P6_SelObj_Size.Height = P6_SelObject.Height;
                    P6_SelObj_Location.X = P6_SelObject.Left;
                    P6_SelObj_Location.Y = P6_SelObject.Top;
                    BI_TL.Visible = false;
                    BI_TR.Visible = false;
                    BI_BL.Visible = false;
                    BI_BR.Visible = false;
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void BI_MouseMove(object sender, MouseEventArgs e)
        {
            if (P6_SelObject == null) { return; }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PictureBox PB = sender as PictureBox;
                switch (PB.Name)
                {
                    case "BI_TL": // Top - Left
                        {
                            if (P6_SelObject.Width >= 5)
                            {
                                P6_SelObject.Left = e.X + P6_SelObj_Location.X;
                                P6_SelObject.Width = P6_SelObj_Size.Width - e.X;
                                if (P6_SelObject.Width < 5) { P6_SelObject.Width = 5; }
                            }
                            if (P6_SelObject.Height >= 5)
                            {
                                P6_SelObject.Top = e.Y + P6_SelObj_Location.Y;
                                P6_SelObject.Height = P6_SelObj_Size.Height - e.Y;
                                if (P6_SelObject.Height < 5) { P6_SelObject.Height = 5; }
                            }
                            break;
                        }
                    case "BI_TR": // Top - Right
                        {
                            if ((e.X + P6_SelObj_Size.Width) > 5) { P6_SelObject.Width = e.X + P6_SelObj_Size.Width; }
                            if (P6_SelObject.Height >= 5)
                            {
                                P6_SelObject.Top = e.Y + P6_SelObj_Location.Y;
                                P6_SelObject.Height = P6_SelObj_Size.Height - e.Y;
                                if (P6_SelObject.Height < 5) { P6_SelObject.Height = 5; }
                            }
                            break;
                        }
                    case "BI_BR": // Bot - Right
                        {
                            if ((e.X + P6_SelObj_Size.Width) > 5) { P6_SelObject.Width = e.X + P6_SelObj_Size.Width; }
                            if ((e.Y + P6_SelObj_Size.Height) > 5) { P6_SelObject.Height = e.Y + P6_SelObj_Size.Height; }
                            break;
                        }
                    case "BI_BL": // Bot - Left
                        {
                            if (P6_SelObject.Width >= 5)
                            {
                                P6_SelObject.Left = e.X + P6_SelObj_Location.X;
                                P6_SelObject.Width = P6_SelObj_Size.Width - e.X;
                                if (P6_SelObject.Width < 5) { P6_SelObject.Width = 5; }
                            }
                            if ((e.Y + P6_SelObj_Size.Height) > 5) { P6_SelObject.Height = e.Y + P6_SelObj_Size.Height; }
                            break;
                        }
                }
            }
        }
        //==========================================================================
        private void BI_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                BI_TL.Parent = P6_IMG;
                BI_TR.Parent = P6_IMG;
                BI_BL.Parent = P6_IMG;
                BI_BR.Parent = P6_IMG;
                P6_SelObject.Cursor = Cursors.SizeAll;
                P6_SelObject.BringToFront();
                BI_TL.Top = P6_SelObject.Top - 3;
                BI_TL.Left = P6_SelObject.Left - 3;
                BI_TL.BringToFront();
                BI_TR.Top = P6_SelObject.Top - 3;
                BI_TR.Left = P6_SelObject.Left + P6_SelObject.Width - 3;
                BI_TR.BringToFront();
                BI_BL.Top = P6_SelObject.Top + P6_SelObject.Height - 3;
                BI_BL.Left = P6_SelObject.Left - 3;
                BI_BL.BringToFront();
                BI_BR.Top = P6_SelObject.Top + P6_SelObject.Height - 3;
                BI_BR.Left = P6_SelObject.Left + P6_SelObject.Width - 3;
                BI_BR.BringToFront();
                P6_SelObject.TransparentBackColor = Color.Red;
                ShowTrpLBPro6();
                P6_SelObject.Visible = true;
                BI_TL.Visible = true;
                BI_TR.Visible = true;
                BI_BL.Visible = true;
                BI_BR.Visible = true;
                P6_SelObject.Invalidate();
                P6_SelObj_Size.Width = 0;
                P6_SelObj_Size.Height = 0;
                P6_SelObj_Location.X = 0;
                P6_SelObj_Location.Y = 0;
            }
            catch (Exception)
            { }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Panel 7 - Front Image Color :
        private void P7_Pan_Click(object sender, EventArgs e)
        {
            try
            {
                if (P7_SelObject != null)
                {
                    P7_SelObject.Cursor = Cursors.Default;
                    P7_SelObject.transparentBackColor = Color.Transparent;
                    P7_SelObject.Invalidate();
                    P7_SelObject = null;
                }
                ObjectLoad = true;
                P7_T1.Text = "";
                P7_T2.Text = "";
                P7_T3.Text = "";
                P7_T4.Text = "";
                P7_Color.BackColor = PNL_7.BackColor;
                P7_Color.Refresh();
                ObjectLoad = false;
                P7_AddNew = false;
                P7_IMG.Cursor = Cursors.Default;
                P7_Pan.Focus();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P7_IMG_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (P7_AddNew == true)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        try
                        {
                            P7_Pan_Click(null, null);
                            FIColor++;
                            TransparentColor LB = new TransparentColor();
                            LB.Name = "FIC" + FIColor.ToString();
                            LB.Visible = false;
                            LB.Parent = P7_IMG;
                            LB.TransparentBackColor = Color.Transparent;
                            LB.AutoSize = false;
                            LB.Top = e.Y - 15;
                            LB.Left = e.X - 15;
                            LB.Width = 30;
                            LB.Height = 30;
                            LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            LB.BackColor = Color.Transparent;
                            LB.Cursor = Cursors.SizeAll;
                            LB.BringToFront();
                            Bitmap bmp = new Bitmap(P7_IMG.BackgroundImage);
                            Color colour = bmp.GetPixel(e.X, e.Y);
                            LB.Color_R = colour.R;
                            LB.Color_G = colour.G;
                            LB.Color_B = colour.B;
                            P7_Color.BackColor = Color.FromArgb(LB.Color_R, LB.Color_G, LB.Color_B);
                            P7_Color.Refresh();
                            bmp.Dispose();
                            LB.Similarity = "100";
                            LB.Text = "X";
                            LB.ForeColor = Color.Black;
                            LB.TransparentBackColor = Color.Red;
                            LB.Click += new EventHandler(this.Trp_LB_Click7);
                            P7_SelObject = LB;
                            LB.Visible = true;
                            P7_AddNew = false;
                            P7_IMG.Cursor = Cursors.Default;
                            ShowTrpLB7();
                            P7_T1.Focus();
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        P7_Pan_Click(null, null);
                    }
                }
                else
                {
                    P7_Pan_Click(null, null);
                }
            }
            catch (Exception) { }
        }
        //==========================================================================
        private void P7_DeletePicker_Click(object sender, EventArgs e)
        {
            try
            {
                if (P7_SelObject != null)
                {
                    P7_SelObject.Dispose();
                    P7_SelObject = null;
                }
                P7_Pan_Click(null, null);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P7_ClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                P7_Pan_Click(null, null);
                foreach (Control TRPL in P7_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P7_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P7_Pan_Click(null, null);
                foreach (Control TRPL in P7_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P7_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P7_Pan_Click(null, null);
                foreach (Control TRPL in P7_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P7_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P7_Pan_Click(null, null);
                foreach (Control TRPL in P7_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P7_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            P7_Pan_Click(null, null);
        }
        //==========================================================================
        void ShowTrpLB7()
        {
            try
            {
                ObjectLoad = true;
                P7_T1.Text = "";
                P7_T2.Text = "";
                P7_T3.Text = "";
                P7_T4.Text = "";
                P7_Color.BackColor = PNL_7.BackColor;
                if (P7_SelObject != null)
                {
                    P7_T1.Text = P7_SelObject.Color_R.ToString();
                    P7_T2.Text = P7_SelObject.Color_G.ToString();
                    P7_T3.Text = P7_SelObject.Color_B.ToString();
                    P7_T4.Text = P7_SelObject.Similarity.ToString();
                    P7_Color.BackColor = Color.FromArgb(P7_SelObject.Color_R, P7_SelObject.Color_G, P7_SelObject.Color_B);
                }
            }
            catch (Exception)
            { }
            P7_Color.Refresh();
            ObjectLoad = false;
        }
        //==========================================================================
        private void P7_T_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 13) { P7_SetValue(sender); }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P7_T_Leave(object sender, EventArgs e)
        {
            try
            {
                P7_SetValue(sender);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P7_SetValue(object sender)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P7_SelObject != null)
                {
                    bool ReloadND = false;
                    System.Windows.Forms.TextBox Tx = sender as System.Windows.Forms.TextBox;
                    int TxVal = 0;
                    try { TxVal = int.Parse(Tx.Text); } catch (Exception) { }
                    switch (Tx.Name.ToUpper())
                    {
                        case "P7_T1":
                            {
                                if ((TxVal >= 0) && (TxVal <= 255))
                                {
                                    P7_SelObject.Color_R = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P7_T1.Text = P7_SelObject.Color_R.ToString();
                                }
                                break;
                            }
                        case "P7_T2":
                            {
                                if ((TxVal >= 0) && (TxVal <= 255))
                                {
                                    P7_SelObject.Color_G = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P7_T2.Text = P7_SelObject.Color_G.ToString();
                                }
                                break;
                            }
                        case "P7_T3":
                            {
                                if ((TxVal >= 0) && (TxVal <= 255))
                                {
                                    P7_SelObject.Color_B = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P7_T3.Text = P7_SelObject.Color_B.ToString();
                                }
                                break;
                            }
                        case "P7_T4":
                            {
                                if ((TxVal >= 0) && (TxVal <= 100))
                                {
                                    P7_SelObject.Similarity = TxVal.ToString();
                                    ReloadND = true;
                                }
                                else
                                {
                                    P7_T4.Text = P7_SelObject.Similarity;
                                }
                                break;
                            }
                    }
                    if (ReloadND == true)
                    {
                        P7_SelObject.BringToFront();
                        P7_T1.Text = P7_SelObject.Color_R.ToString();
                        P7_T2.Text = P7_SelObject.Color_G.ToString();
                        P7_T3.Text = P7_SelObject.Color_B.ToString();
                        P7_T4.Text = P7_SelObject.Similarity.ToString();
                        P7_Color.BackColor = Color.FromArgb(P7_SelObject.Color_R, P7_SelObject.Color_G, P7_SelObject.Color_B);
                        P7_Color.Refresh();
                    }
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P7_AddPicker_Click(object sender, EventArgs e)
        {
            try
            {
                P7_AddNew = true;
                P7_IMG.Cursor = Cursors.Cross;
            }
            catch (Exception)
            {
                P7_Pan_Click(null, null);
            }
        }
        //==========================================================================
        private void Trp_LB_Click7(object sender, EventArgs e)
        {
            try
            {
                TransparentColor LB = (TransparentColor)sender;
                if (P7_SelObject != null)
                {
                    if (P7_SelObject.Name.Trim() != LB.Name.Trim())
                    {
                        P7_Pan_Click(null, null);
                        LB.BringToFront();
                        LB.TransparentBackColor = Color.Red;
                        P7_SelObject = LB;
                        ShowTrpLB7();
                        LB.Visible = true;
                    }
                }
                else
                {
                    LB.BringToFront();
                    LB.TransparentBackColor = Color.Red;
                    P7_SelObject = LB;
                    ShowTrpLB7();
                    LB.Visible = true;
                }
                P7_T1.Focus();
            }
            catch (Exception)
            {
                P7_Pan_Click(null, null);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
        // Panel 8 - Back Image Color Btn :
        private void P8_Pan_Click(object sender, EventArgs e)
        {
            try
            {
                if (P8_SelObject != null)
                {
                    P8_SelObject.Cursor = Cursors.Default;
                    P8_SelObject.transparentBackColor = Color.Transparent;
                    P8_SelObject.Invalidate();
                    P8_SelObject = null;
                }
                ObjectLoad = true;
                P8_T1.Text = "";
                P8_T2.Text = "";
                P8_T3.Text = "";
                P8_T4.Text = "";
                P8_Color.BackColor = PNL_8.BackColor;
                P8_Color.Refresh();
                ObjectLoad = false;
                P8_AddNew = false;
                P8_IMG.Cursor = Cursors.Default;
                P8_Pan.Focus();
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P8_IMG_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (P8_AddNew == true)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        try
                        {
                            P8_Pan_Click(null, null);
                            BIColor++;
                            TransparentColor LB = new TransparentColor();
                            LB.Name = "BIC" + BIColor.ToString();
                            LB.Visible = false;
                            LB.Parent = P8_IMG;
                            LB.TransparentBackColor = Color.Transparent;
                            LB.AutoSize = false;
                            LB.Top = e.Y - 15;
                            LB.Left = e.X - 15;
                            LB.Width = 30;
                            LB.Height = 30;
                            LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            LB.BackColor = Color.Transparent;
                            LB.Cursor = Cursors.SizeAll;
                            LB.BringToFront();
                            Bitmap bmp = new Bitmap(P8_IMG.BackgroundImage);
                            Color colour = bmp.GetPixel(e.X, e.Y);
                            LB.Color_R = colour.R;
                            LB.Color_G = colour.G;
                            LB.Color_B = colour.B;
                            P8_Color.BackColor = Color.FromArgb(LB.Color_R, LB.Color_G, LB.Color_B);
                            P8_Color.Refresh();
                            bmp.Dispose();
                            LB.Similarity = "100";
                            LB.Text = "X";
                            LB.ForeColor = Color.Black;
                            LB.TransparentBackColor = Color.Red;
                            LB.Click += new EventHandler(this.Trp_LB_Click8);
                            P8_SelObject = LB;
                            LB.Visible = true;
                            P8_AddNew = false;
                            P8_IMG.Cursor = Cursors.Default;
                            ShowTrpLB8();
                            P8_T1.Focus();
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        P8_Pan_Click(null, null);
                    }
                }
                else
                {
                    P8_Pan_Click(null, null);
                }
            }
            catch (Exception) { }
        }
        //==========================================================================
        private void P8_DeletePicker_Click(object sender, EventArgs e)
        {
            try
            {
                if (P8_SelObject != null)
                {
                    P8_SelObject.Dispose();
                    P8_SelObject = null;
                }
                P8_Pan_Click(null, null);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P8_ClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                P8_Pan_Click(null, null);
                foreach (Control TRPL in P8_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P8_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P8_Pan_Click(null, null);
                foreach (Control TRPL in P8_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P8_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P8_Pan_Click(null, null);
                foreach (Control TRPL in P8_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P8_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            try
            {
                P8_Pan_Click(null, null);
                foreach (Control TRPL in P8_IMG.Controls)
                {
                    if (TRPL is TransparentColor)
                    {
                        P8_IMG.Controls.Remove(TRPL);
                        TRPL.Dispose();
                    }
                }
            }
            catch (Exception) { }
            P8_Pan_Click(null, null);
        }
        //==========================================================================
        void ShowTrpLB8()
        {
            try
            {
                ObjectLoad = true;
                P8_T1.Text = "";
                P8_T2.Text = "";
                P8_T3.Text = "";
                P8_T4.Text = "";
                P8_Color.BackColor = PNL_8.BackColor;
                if (P8_SelObject != null)
                {
                    P8_T1.Text = P8_SelObject.Color_R.ToString();
                    P8_T2.Text = P8_SelObject.Color_G.ToString();
                    P8_T3.Text = P8_SelObject.Color_B.ToString();
                    P8_T4.Text = P8_SelObject.Similarity.ToString();
                    P8_Color.BackColor = Color.FromArgb(P8_SelObject.Color_R, P8_SelObject.Color_G, P8_SelObject.Color_B);
                }
            }
            catch (Exception)
            { }
            P8_Color.Refresh();
            ObjectLoad = false;
        }
        //==========================================================================
        private void P8_T_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 13) { P8_SetValue(sender); }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P8_T_Leave(object sender, EventArgs e)
        {
            try
            {
                P8_SetValue(sender);
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P8_SetValue(object sender)
        {
            try
            {
                if (ObjectLoad == true) { return; }
                if (P8_SelObject != null)
                {
                    bool ReloadND = false;
                    System.Windows.Forms.TextBox Tx = sender as System.Windows.Forms.TextBox;
                    int TxVal = 0;
                    try { TxVal = int.Parse(Tx.Text); } catch (Exception) { }
                    switch (Tx.Name.ToUpper())
                    {
                        case "P8_T1":
                            {
                                if ((TxVal >= 0) && (TxVal <= 255))
                                {
                                    P8_SelObject.Color_R = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P8_T1.Text = P8_SelObject.Color_R.ToString();
                                }
                                break;
                            }
                        case "P8_T2":
                            {
                                if ((TxVal >= 0) && (TxVal <= 255))
                                {
                                    P8_SelObject.Color_G = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P8_T2.Text = P8_SelObject.Color_G.ToString();
                                }
                                break;
                            }
                        case "P8_T3":
                            {
                                if ((TxVal >= 0) && (TxVal <= 255))
                                {
                                    P8_SelObject.Color_B = TxVal;
                                    ReloadND = true;
                                }
                                else
                                {
                                    P8_T3.Text = P8_SelObject.Color_B.ToString();
                                }
                                break;
                            }
                        case "P8_T4":
                            {
                                if ((TxVal >= 0) && (TxVal <= 100))
                                {
                                    P8_SelObject.Similarity = TxVal.ToString();
                                    ReloadND = true;
                                }
                                else
                                {
                                    P8_T4.Text = P8_SelObject.Similarity;
                                }
                                break;
                            }
                    }
                    if (ReloadND == true)
                    {
                        P8_SelObject.BringToFront();
                        P8_T1.Text = P8_SelObject.Color_R.ToString();
                        P8_T2.Text = P8_SelObject.Color_G.ToString();
                        P8_T3.Text = P8_SelObject.Color_B.ToString();
                        P8_T4.Text = P8_SelObject.Similarity.ToString();
                        P8_Color.BackColor = Color.FromArgb(P8_SelObject.Color_R, P8_SelObject.Color_G, P8_SelObject.Color_B);
                        P8_Color.Refresh();
                    }
                }
            }
            catch (Exception)
            { }
        }
        //==========================================================================
        private void P8_AddPicker_Click(object sender, EventArgs e)
        {
            try
            {
                P8_AddNew = true;
                P8_IMG.Cursor = Cursors.Cross;
            }
            catch (Exception)
            {
                P8_Pan_Click(null, null);
            }
        }
        //==========================================================================
        private void Trp_LB_Click8(object sender, EventArgs e)
        {
            try
            {
                TransparentColor LB = (TransparentColor)sender;
                if (P8_SelObject != null)
                {
                    if (P8_SelObject.Name.Trim() != LB.Name.Trim())
                    {
                        P8_Pan_Click(null, null);
                        LB.BringToFront();
                        LB.TransparentBackColor = Color.Red;
                        P8_SelObject = LB;
                        ShowTrpLB8();
                        LB.Visible = true;
                    }
                }
                else
                {
                    LB.BringToFront();
                    LB.TransparentBackColor = Color.Red;
                    P8_SelObject = LB;
                    ShowTrpLB8();
                    LB.Visible = true;
                }
                P8_T1.Focus();
            }
            catch (Exception)
            {
                P8_Pan_Click(null, null);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------\\
        //-------------------------------------------------------------------------------------------------------------------//
    }
}
