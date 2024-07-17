using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy
{
    public partial class frmChangePass : Form
    {
        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Error.Error_ChangePass error = new QuanLyDaiLy.Error.Error_ChangePass();

        private MySqlCommand commnd = null;
        private string sqlstr;

        public frmChangePass()
        {
            InitializeComponent();
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strMess.ChuaKetNoiCSDL + "\n" + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strDangNhap = frmMain.strTenDangNhap;
            string strMatKhau = txtMatKhauCu.Text.Trim();
            string strChuoiDieuKien = $"TenDangNhap='{strDangNhap}' and MatKhau='{strMatKhau}'";
            string strTableName = "TaiKhoan";

            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN))
            {
                string strMatKhauMoi = txtMatKhauMoi.Text.Trim();
                string strLapLaiMatKhau = txtNhapLaiMK.Text.Trim();
                try
                {
                    error.Exception_MatKhau(strMatKhauMoi, commnd);
                    error.Exception_MatKhau(strLapLaiMatKhau, commnd);

                    if (!(strMatKhauMoi == "" || strLapLaiMatKhau == ""))
                    {
                        if (strMatKhauMoi == strLapLaiMatKhau && strMatKhauMoi != strMatKhau)
                        {
                            sqlstr = $"update TaiKhoan set MatKhau='{strMatKhauMoi}' where TenDangNhap='{strDangNhap}'";
                            global.SQL_Database(sqlstr, sqlConn.sqlCNN);
                            MessageBox.Show(strMess.ThaoTacThanhCong, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(strMess.MatKhauKhac, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu mới của bạn không được rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(strMess.MatKhauCu, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
