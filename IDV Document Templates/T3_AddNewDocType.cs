using MetroSet_UI.Forms;
using Microsoft.VisualBasic;
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
    public partial class T3_AddNewDocType : MetroSetForm
    {
        public Templates ParetnFrm;
        public T3_AddNewDocType()
        {
            InitializeComponent();
            PW_LBL.Visible = false;
            PW_GF.Visible = false;
        }

        private void T3_AddNewDocType_Load(object sender, EventArgs e)
        {
            this.Width = 523;
            this.Height = 338;
            this.StartPosition = FormStartPosition.CenterScreen;
            PW_GF.Visible = false;
            PW_LBL.Visible = false;
            metroSetButton1.Enabled = true;
            metroSetButton1.Enabled = true;
            metroSetLabel4.Text = "";
            metroSetLabel5.Text = "";
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        private void metroSetButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void metroSetButton2_Click(object sender, EventArgs e)
        {
            try
            {
                PW_GF.Visible = true;
                PW_LBL.Visible = true;
                metroSetButton1.Enabled = false;
                metroSetButton1.Enabled = false;
                metroSetLabel4.Text = "";
                metroSetLabel5.Text = "";
                Application.DoEvents();
                bool NoError = true;
                bool CallTempReloadBtn = false;
                TextBox1.Text = TextBox1.Text.Replace("-", "").Trim();
                TextBox2.Text = TextBox2.Text.Replace("-", "").Trim().ToUpper();
                if (TextBox1.Text == "") { metroSetLabel4.Text = "(Required)"; NoError = false; }
                if (TextBox2.Text == "") { metroSetLabel5.Text = "(Required)"; NoError = false; }
                if (NoError == true)
                {
                    bool TaskEnded = false;
                    string Res = "";
                    Task t = Task.Run(() =>
                    {
                        try
                        {
                            string SRVR = ParetnFrm.ST_Server;
                            var client = new RestClient(SRVR + "api/DocBuilder/" + "DT_10_AddNewDocumentType" + "?" + "DTN=" + TextBox1.Text + "&DTC=" + TextBox2.Text + "&UI=" + ParetnFrm.ST_UID);
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
                        NoError = false;
                        metroSetLabel4.Text = "Server return an error [ERA]";
                    }
                    else
                    {
                        if (Res.ToUpper().IndexOf("OK") >= 0)
                        {
                            try
                            {
                                string[] ResSep = Res.Split('-');
                                ParetnFrm.dataGridView3.Rows.Add(1);
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[0].Value = ResSep[1].Trim();
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[1].Value = ResSep[2].Trim();
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[2].Value = ResSep[3].Trim();
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[3].Value = ResSep[4].Trim();
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[4].Value = ResSep[5].Trim();
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[5].Value = ResSep[6].Trim();
                                ParetnFrm.dataGridView3.Rows[ParetnFrm.dataGridView3.Rows.Count - 1].Cells[6].Value = ResSep[7].Trim();
                            }
                            catch (Exception)
                            {
                                CallTempReloadBtn = true;
                            }
                        }
                        else
                        {
                            NoError = false;
                            try
                            {
                                string[] ResSep = Res.Split('-');
                                metroSetLabel4.Text = ResSep[1].Trim();
                                metroSetLabel5.Text = ResSep[2].Trim();
                            }
                            catch (Exception)
                            {
                                metroSetLabel4.Text = "Server return an error [ERA Local]";
                            }
                        }
                    }
                }
                if (NoError == true)
                {
                    ParetnFrm.Reload_State_T3 = true;
                    ParetnFrm.ReloadBtn = CallTempReloadBtn;
                    metroSetButton1_Click(null, null);
                }
                else
                {
                    PW_GF.Visible = false;
                    PW_LBL.Visible = false;
                    metroSetButton1.Enabled = true;
                    metroSetButton1.Enabled = true;
                }
            }
            catch (Exception)
            {
                PW_GF.Visible = false;
                PW_LBL.Visible = false;
                metroSetButton1.Enabled = true;
                metroSetButton1.Enabled = true;
            }
        }
    }
}
