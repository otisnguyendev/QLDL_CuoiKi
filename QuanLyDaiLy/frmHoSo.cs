using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy
{
    public partial class frmHoSo : Form
    {
        public frmHoSo()
        {
            InitializeComponent();
        }

        Modules.LoadCombobox loadCombo = new QuanLyDaiLy.Modules.LoadCombobox();
        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Error.Error_HoSo error = new QuanLyDaiLy.Error.Error_HoSo();

        private string sqlstr;
        MySqlCommand commnd= null;
        private void frmHoSo_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
                string sqlstr = "SELECT MaLoaiDaiLy, TenLoaiDaiLy FROM LoaiDaiLy ORDER BY MaLoaiDaiLy";
                string display = "TenLoaiDaiLy";
                string value = "MaLoaiDaiLy";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboLoaiDL, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ChuaKetNoiCSDL, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMaHS.Text = "";
            txtTenDL.Text = "";
            comboLoaiDL.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtDiaChiM.Text = "";
            txtTienNo.Text = "";
            dtpNgayTN.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      private void button3_Click(object sender, EventArgs e)
        {
            string strMaHoSo = txtMaHS.Text.Trim();
            string strTenDL = txtTenDL.Text.Trim();
            string strMaDL = comboLoaiDL.SelectedValue.ToString().Trim();
            string strDiaChi = txtDiaChi.Text.Trim();
            string strDienThoai = txtDienThoai.Text.Trim();
            string strMail = txtDiaChiM.Text.Trim();
            string strTienNo = txtTienNo.Text.Trim();
            int intXacNhan = 0;

            string sqtChuyenDoiNgayThang;
            if (dtpNgayTN.Checked)
            {
                DateTime dtNgayTN = dtpNgayTN.Value;
                sqtChuyenDoiNgayThang = dtNgayTN.ToString("yyyy-MM-dd");
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToString("yyyy-MM-dd");
            }

            try
            {
                error.Exception_MaHoSo(strMaHoSo, commnd, sqlConn.sqlCNN);
                error.Exception_TenDaiLy(strTenDL, commnd);
                error.Exception_DiaChi(strDiaChi, commnd);
                error.Exception_DienThoai(strDienThoai, commnd);
                error.Exception_DiaChiMail(strMail, commnd);
                error.Exception_TienNo(strTienNo, commnd);

                string sqlstr = $"INSERT INTO HoSo (MaHoSo, TenDaiLy, DienThoai, DiaChi, NgayTiepNhan, MaLoaiDaiLy, Email, TienNo, XacNhan) VALUES ('{strMaHoSo}', '{strTenDL}', '{strDienThoai}', '{strDiaChi}', '{sqtChuyenDoiNgayThang}', '{strMaDL}', '{strMail}', '{strTienNo}', {intXacNhan})";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }
    
        private void button7_Click(object sender, EventArgs e)
        {
            string strMaHoSo = txtMaHS.Text.Trim();
            string sqlstr = "UPDATE HoSo SET XacNhan = 1 WHERE MaHoSo='" + strMaHoSo + "'";
            global.SQL_Database(sqlstr, sqlConn.sqlCNN);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            string strMaHoSo = txtMaHS.Text.Trim();
            string sqlstr = "UPDATE HoSo SET XacNhan = 0 WHERE MaHoSo='" + strMaHoSo + "'";
            global.SQL_Database(sqlstr, sqlConn.sqlCNN);
        }

        string strGetValue;
        private string GetValue_Combobox(string str)
        {
            if (str == "Mã Hồ Sơ")
            {
                strGetValue = "MaHoSo";
            }
            if (str == "Tên Đại Lý")
            {
                strGetValue = "TenDaiLy";
            }
            if (str == "Loại Đại Lý")
            {
                strGetValue = "MaLoaiDaiLy";
            }
            if (str == "Địa Chỉ")
            {
                strGetValue = "DiaChi";
            }
            if (str == "Điện Thoại")
            {
                strGetValue = "DienThoai";
            }
            if (str == "Địa Chỉ Mail")
            {
                strGetValue = "Email";
            }
            if (str == "Tiền Nợ")
            {
                strGetValue = "TienNo";
            }
            if (str == "Ngày Tiếp Nhận")
            {
                strGetValue = "NgayTiepNhan";
            }
            return strGetValue;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboBox2.Text);
            string strText = txtTimKiemHoSo.Text.Trim();
            if (strCombobox == "NgayTiepNhan")
            {
                strText = global.Return_Time_ThangNgay(strText);
            }

            if (strCombobox == "MaLoaiDaiLy")
            {
                sqlstr = "SELECT * FROM HoSo INNER JOIN LoaiDaiLy ON HoSo.MaLoaiDaiLy = LoaiDaiLy.MaLoaiDaiLy WHERE LoaiDaiLy.TenLoaiDaiLy = '" + strText + "'";
            }
            else
            {
                sqlstr = "SELECT * FROM HoSo INNER JOIN LoaiDaiLy ON HoSo.MaLoaiDaiLy = LoaiDaiLy.MaLoaiDaiLy WHERE HoSo." + strCombobox + " = '" + strText + "'";
            }
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }

        private void FormatGrid(DataGrid dgIndex)
        {
            try
            {
                DataGridTableStyle grdTableStyle = new DataGridTableStyle();
                dgIndex.TableStyles.Clear();
                grdTableStyle = global.MyTableStyleCreate();

                //Mã Hồ Sơ
                DataGridTextBoxColumn grdColMaHoSo = new DataGridTextBoxColumn();
                grdColMaHoSo.MappingName = "MaHoSo";
                grdColMaHoSo.HeaderText = "Mã Hồ Sơ";
                grdColMaHoSo.NullText = "";
                grdColMaHoSo.Width = 80;
                grdColMaHoSo.Alignment = HorizontalAlignment.Center;

                //Tên Đại Lý
                DataGridTextBoxColumn grdColTenDaiLy = new DataGridTextBoxColumn();
                grdColTenDaiLy.MappingName = "TenDaiLy";
                grdColTenDaiLy.HeaderText = "Tên Đại Lý";
                grdColTenDaiLy.NullText = "";
                grdColTenDaiLy.Width = 100;
                grdColTenDaiLy.Alignment = HorizontalAlignment.Left;

                //Loại Đại Lý
                DataGridTextBoxColumn grdColLoaiDaiLy = new DataGridTextBoxColumn();
                grdColLoaiDaiLy.MappingName = "TenLoaiDaiLy";
                grdColLoaiDaiLy.HeaderText = "Tên Loại Đại Lý";
                grdColLoaiDaiLy.NullText = "";
                grdColLoaiDaiLy.Width = 80;
                grdColLoaiDaiLy.Alignment = HorizontalAlignment.Center;

                //Địa Chỉ
                DataGridTextBoxColumn grdColDiaChi = new DataGridTextBoxColumn();
                grdColDiaChi.MappingName = "DiaChi";
                grdColDiaChi.HeaderText = "Địa Chỉ";
                grdColDiaChi.NullText = "";
                grdColDiaChi.Width = 100;
                grdColDiaChi.Alignment = HorizontalAlignment.Center;

                //Điện Thoại
                DataGridTextBoxColumn grdColDienThoai = new DataGridTextBoxColumn();
                grdColDienThoai.MappingName = "DienThoai";
                grdColDienThoai.HeaderText = "Điện Thoại";
                grdColDienThoai.NullText = "";
                grdColDienThoai.Width = 80;
                grdColDienThoai.Alignment = HorizontalAlignment.Center;

                //Địa Chỉ Mail
                DataGridTextBoxColumn grdColEmail = new DataGridTextBoxColumn();
                grdColEmail.MappingName = "Email";
                grdColEmail.HeaderText = "Địa Chỉ Mail";
                grdColEmail.NullText = "";
                grdColEmail.Width = 120;
                grdColEmail.Alignment = HorizontalAlignment.Center;

                //Tiền Nợ
                DataGridTextBoxColumn grdColTienNo = new DataGridTextBoxColumn();
                grdColTienNo.MappingName = "TienNo";
                grdColTienNo.HeaderText = "Tiền Nợ";
                grdColTienNo.NullText = "";
                grdColTienNo.Width = 80;
                grdColTienNo.Alignment = HorizontalAlignment.Center;

                //Ngày Tiếp Nhận
                DataGridTextBoxColumn grdColNgayTiepNhan = new DataGridTextBoxColumn();
                grdColNgayTiepNhan.MappingName = "NgayTiepNhan";
                grdColNgayTiepNhan.HeaderText = "Ngày Tiếp Nhận";
                grdColNgayTiepNhan.NullText = "";
                grdColNgayTiepNhan.Width = 80;
                grdColNgayTiepNhan.Alignment = HorizontalAlignment.Center;

                //Xác Nhận
                DataGridTextBoxColumn grdColXacNhan = new DataGridTextBoxColumn();
                grdColXacNhan.MappingName = "XacNhan";
                grdColXacNhan.HeaderText = "Xác Nhận";
                grdColXacNhan.NullText = "";
                grdColXacNhan.Width = 40;
                grdColXacNhan.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaHoSo, grdColTenDaiLy, grdColLoaiDaiLy, grdColDiaChi, grdColDienThoai, grdColEmail, grdColTienNo, grdColNgayTiepNhan, grdColXacNhan });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void setControll(DataTable dtTable, int Index)
        {
            txtMaHS.Text = dtTable.Rows[Index]["MaHoSo"].ToString();
            txtTenDL.Text = dtTable.Rows[Index]["TenDaiLy"].ToString();
            comboLoaiDL.Text = dtTable.Rows[Index]["TenLoaiDaiLy"].ToString();
            txtDiaChi.Text = dtTable.Rows[Index]["DiaChi"].ToString();
            txtDienThoai.Text = dtTable.Rows[Index]["DienThoai"].ToString();
            txtDiaChiM.Text = dtTable.Rows[Index]["Email"].ToString();
            txtTienNo.Text = dtTable.Rows[Index]["TienNo"].ToString();
            dtpNgayTN.Text = dtTable.Rows[Index]["NgayTiepNhan"].ToString();
        }
        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            DataTable dtTable = (DataTable)dataGrid1.DataSource;
            int Index = dataGrid1.CurrentRowIndex;
            if ((dtTable != null) && (dtTable.Rows.Count > 0))
            {
                if (Index >= 0)
                {
                    setControll(dtTable, Index);
                }
            }

            dtTable = null;
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            string strMaHoSo = txtMaHS.Text.Trim();
            string strTenDL = txtTenDL.Text.Trim();
            string strMaDL = comboLoaiDL.SelectedValue.ToString().Trim();
            string strDiaChi = txtDiaChi.Text.Trim();
            string strDienThoai = txtDienThoai.Text.Trim();
            string strMail = txtDiaChiM.Text.Trim();
            string strTienNo = txtTienNo.Text.Trim();
            int intXacNhan = 0;

            string sqtChuyenDoiNgayThang;
            if (dtpNgayTN.Checked)
            {
                DateTime dtNgayTN = dtpNgayTN.Value;
                sqtChuyenDoiNgayThang = dtNgayTN.ToString("yyyy-MM-dd");
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToString("yyyy-MM-dd");
            }

            try
            {
                error.Exception_MaHoSo_CN(strMaHoSo, commnd, sqlConn.sqlCNN);
                error.Exception_TenDaiLy(strTenDL, commnd);
                error.Exception_DiaChi(strDiaChi, commnd);
                error.Exception_DienThoai(strDienThoai, commnd);
                error.Exception_DiaChiMail(strMail, commnd);
                error.Exception_TienNo(strTienNo, commnd);

                string sqlstr = $"UPDATE HoSo SET TenDaiLy = '{strTenDL}', DienThoai = '{strDienThoai}', DiaChi = '{strDiaChi}', NgayTiepNhan = '{sqtChuyenDoiNgayThang}', MaLoaiDaiLy = '{strMaDL}', Email = '{strMail}', TienNo = '{strTienNo}', XacNhan = {intXacNhan} WHERE MaHoSo = '{strMaHoSo}'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string strMaHoSo = txtMaHS.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaHoSo='" + strMaHoSo + "'";
                string strTableName = "HoSo";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN) == false)
                {
                    MessageBox.Show("Mã Hồ Sơ Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                string sqlstr1 = "Delete From CongNo where MaHoSo ='" + strMaHoSo + "'";
                global.SQL_Database(sqlstr1, sqlConn.sqlCNN);
                string sqlstr2 = "Delete From DoanhSo where MaHoSo ='" + strMaHoSo + "'";
                global.SQL_Database_NoMessage(sqlstr2, sqlConn.sqlCNN);
                string sqlstr3 = "Delete From PhieuXuatHang where MaHoSo ='" + strMaHoSo + "'";
                global.SQL_Database_NoMessage(sqlstr3, sqlConn.sqlCNN);
                string sqlstr4 = "Delete From PhieuThuTien where MaHoSo ='" + strMaHoSo + "'";
                global.SQL_Database_NoMessage(sqlstr4, sqlConn.sqlCNN);
                string sqlstr5 = "Delete From HoSo where MaHoSo ='" + strMaHoSo + "'";
                global.SQL_Database_NoMessage(sqlstr5, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}