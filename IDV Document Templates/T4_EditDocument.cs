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
    public partial class T4_EditDocument : MetroSetForm
    {
        public Templates ParetnFrm;
        public string CID = "0";
        public string SID = "0";
        public string DTID = "0";
        public string DID = "0";
        public T4_EditDocument()
        {
            InitializeComponent();
            PW_LBL.Visible = false;
            PW_GF.Visible = false;
        }

        private void T4_EditDocument_Load(object sender, EventArgs e)
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
                            var client = new RestClient(SRVR + "api/" + "DT_18_EditDocument" + "?" + "CID=" + CID + "&SID=" + SID + "&DTID=" + DTID + "&DID=" + DID + "&DN=" + TextBox1.Text + "&DC=" + TextBox2.Text);
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
                                ParetnFrm.dataGridView4.SelectedRows[0].Cells[2].Value = ResSep[1].Trim();
                                ParetnFrm.dataGridView4.SelectedRows[0].Cells[3].Value = ResSep[2].Trim();
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
