using MetroSet_UI.Forms;
using Microsoft.VisualBasic;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IDV_Document_Templates
{
    public partial class Login : MetroSetForm
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                if (Interaction.GetSetting("IDV", "DocTemp", "DM", "").ToString().Trim() == "")
                {
                    Interaction.SaveSetting("IDV", "DocTemp", "DM", "TRUE");
                }
            }
            catch (Exception)
            {
                Interaction.SaveSetting("IDV", "DocTemp", "DM", "TRUE");
            }
            try
            {
                if (Interaction.GetSetting("IDV", "DocTemp", "DM", "FALSE").ToString().Trim().ToUpper() == "TRUE")
                {
                    styleManager1.Style = MetroSet_UI.Design.Style.Dark;
                }
                else
                {
                    styleManager1.Style = MetroSet_UI.Design.Style.Light;
                }
            }
            catch (Exception) { }
            this.Width = 360;
            this.Height = 451;
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBox1.Enabled = true;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Staging Server [ Scanner ]");
            comboBox1.Items.Add("Production Server [ Icore ]");
            label1.Enabled = true;
            label2.Enabled = true;
            label3.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            checkBox1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button2.Visible = true;
            button1.Text = "Admin Login";
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            checkBox1.Checked = false;
            try
            {
                comboBox1.SelectedIndex = int.Parse(Interaction.GetSetting("IDV", "DocTemp", "DD", "0").ToString().Trim());
                textBox1.Text = Interaction.GetSetting("IDV", "DocTemp", "US", "").ToString().Trim();
                textBox2.Text = "";
                if (Interaction.GetSetting("IDV", "DocTemp", "CB", "False").ToString().Trim().ToUpper() == "TRUE") { checkBox1.Checked = true; }
            }
            catch (Exception)
            { }
            textBox1.BackColor = comboBox1.BackgroundColor;
            textBox2.BackColor = comboBox1.BackgroundColor;
            textBox1.ForeColor = comboBox1.ForeColor;
            textBox2.ForeColor = comboBox1.ForeColor;
            textBox1.Focus();
            textBox1.SelectAll();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Dear User ...\r\nPlease enter your IDV administrator username", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Dear User ...\r\nPlease enter your IDV administrator password", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked == true)
            {
                Interaction.SaveSetting("IDV", "DocTemp", "DD", comboBox1.SelectedIndex.ToString());
                Interaction.SaveSetting("IDV", "DocTemp", "US", textBox1.Text);
                Interaction.SaveSetting("IDV", "DocTemp", "CB", "True");
            }
            else
            {
                Interaction.SaveSetting("IDV", "DocTemp", "DD", "0");
                Interaction.SaveSetting("IDV", "DocTemp", "US", "");
                Interaction.SaveSetting("IDV", "DocTemp", "CB", "False");
            }
            this.Enabled = false;
            label1.Enabled = false;
            label2.Enabled = false;
            label3.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            checkBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button2.Visible = false;
            button1.Text = "Please Wait ...";
            Application.DoEvents();
            try
            {
                string SRVR = "";
                if (comboBox1.SelectedIndex == 0)
                {
                    SRVR = ConfigurationSettings.AppSettings["IDVST"].ToString().Trim() + "/";
                }
                else
                {
                    SRVR = ConfigurationSettings.AppSettings["IDVPD"].ToString().Trim() + "/";
                }
                var client = new RestClient(SRVR + "api/" + "DT_01_Authentication" + "?" + "USN=" + textBox1.Text + "&PSW=" + textBox2.Text);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);
                string Res = response.Content.ToString().Trim();
                Res = Res.Replace("\"", "");
                if (Res != "ER1")
                {
                    if (Res != "ER2")
                    {
                        if (Res != "ER3")
                        {
                            if (Res.IndexOf("EMASOK") >= 0)
                            {
                                string[] SepRes = Res.Split('_');
                                if (SepRes.Length == 3)
                                {
                                    this.Visible = false;
                                    Templates Tmp = new Templates();
                                    Tmp.ST_UID = SepRes[1].Trim();
                                    Tmp.ST_UserFullname = SepRes[2].Trim();
                                    Tmp.ST_Server = SRVR;
                                    Tmp.ST_Username = textBox1.Text;
                                    Tmp.ST_Password = textBox2.Text;
                                    Tmp.ShowDialog();
                                    this.Close();
                                    Application.Exit();
                                }
                                else
                                {
                                    this.Enabled = true;
                                    label1.Enabled = true;
                                    label2.Enabled = true;
                                    label3.Enabled = true;
                                    comboBox1.Enabled = true;
                                    textBox1.Enabled = true;
                                    textBox2.Enabled = true;
                                    checkBox1.Enabled = true;
                                    button1.Enabled = true;
                                    button2.Enabled = true;
                                    button2.Visible = true;
                                    button1.Text = "Admin Login";
                                    MessageBox.Show("Dear User ...\r\nUsername or password not valid, After check please try again", "Authentication Failed [ER4]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                this.Enabled = true;
                                label1.Enabled = true;
                                label2.Enabled = true;
                                label3.Enabled = true;
                                comboBox1.Enabled = true;
                                textBox1.Enabled = true;
                                textBox2.Enabled = true;
                                checkBox1.Enabled = true;
                                button1.Enabled = true;
                                button2.Enabled = true;
                                button2.Visible = true;
                                button1.Text = "Admin Login";
                                MessageBox.Show("Dear User ...\r\nUsername or password not valid, After check please try again", "Authentication Failed [ER4]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            this.Enabled = true;
                            label1.Enabled = true;
                            label2.Enabled = true;
                            label3.Enabled = true;
                            comboBox1.Enabled = true;
                            textBox1.Enabled = true;
                            textBox2.Enabled = true;
                            checkBox1.Enabled = true;
                            button1.Enabled = true;
                            button2.Enabled = true;
                            button2.Visible = true;
                            button1.Text = "Admin Login";
                            MessageBox.Show("Dear User ...\r\nUsername or password not valid, After check please try again", "Authentication Failed [ER3]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        this.Enabled = true;
                        label1.Enabled = true;
                        label2.Enabled = true;
                        label3.Enabled = true;
                        comboBox1.Enabled = true;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        checkBox1.Enabled = true;
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button2.Visible = true;
                        button1.Text = "Admin Login";
                        MessageBox.Show("Dear User ...\r\nUsername or password not valid, After check please try again", "Authentication Failed [ER2]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    this.Enabled = true;
                    label1.Enabled = true;
                    label2.Enabled = true;
                    label3.Enabled = true;
                    comboBox1.Enabled = true;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    checkBox1.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button2.Visible = true;
                    button1.Text = "Admin Login";
                    MessageBox.Show("Dear User ...\r\nIDV server not responded, After check please try again", "Authentication Failed [ER1]", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {
                this.Enabled = true;
                label1.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                checkBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button2.Visible = true;
                button1.Text = "Admin Login";
                MessageBox.Show("Dear User ...\r\nIDV administrator security value not valid, After check please try again", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox2.Refresh();
            styleManager1.MetroForm.Refresh();
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { textBox1.Focus(); }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { textBox2.Focus(); }
        }
        [Obsolete]
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1_Click(null, null); }
        }
        [Obsolete]
        private void checkBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1_Click(null, null); }
        }
    }
}
