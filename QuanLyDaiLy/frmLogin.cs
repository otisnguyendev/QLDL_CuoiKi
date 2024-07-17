using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyDaiLy
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();

        private int Number_Account = 2;
        private string strDangNhap;

        public int GetNumberAccount()
        {
            return Number_Account;
        }
        public string GetstrDangNhap()
        {
            return txtDangNhap.Text.Trim();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
            }
            catch
            {
                MessageBox.Show(strMess.ChuaKetNoiCSDL, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strDangNhap = txtDangNhap.Text.Trim();
            string strMatKhau = txtMatKhau.Text.Trim();
            string strChuoiDieuKien = "TenDangNhap='" + strDangNhap + "' and MatKhau='" + strMatKhau + "' and QuyenTruyCap ='" + Number_Account + "'";
            string strTableName = "TaiKhoan";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN))
            {
                MessageBox.Show(strMess.DangNhap_ThanhCong, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(strMess.DangNhap_ThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbGuest_CheckedChanged(object sender, EventArgs e)
        {
            Number_Account = 0;
        }

        private void rbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            Number_Account = 1;
        }
    }
}