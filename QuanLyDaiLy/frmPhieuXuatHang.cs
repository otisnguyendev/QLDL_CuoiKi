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
    public partial class frmPhieuXuatHang : Form
    {
        public frmPhieuXuatHang()
        {
            InitializeComponent();
        }

        Modules.LoadCombobox loadCombo = new QuanLyDaiLy.Modules.LoadCombobox();
        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Error.Error_PhieuXuatHang error = new QuanLyDaiLy.Error.Error_PhieuXuatHang();

        private MySqlCommand commnd = null;

        private void frmPhieuXuatHang_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
                string sqlstr = "SELECT MaHoSo FROM HoSo ORDER BY MaHoSo";
                string display = "MaHoSo";
                string value = "MaHoSo";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboMaHS_PhieuXuat, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ChuaKetNoiCSDL, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMaPX_PhieuXuat.Text = "";
            comboMaHS_PhieuXuat.Text = "";
            txtTongTX_PhieuXuat.Text="";
            dtpNgayLP_PhieuXuat.Text = "";
            rtbGhiChu.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strMaPX = txtMaPX_PhieuXuat.Text.Trim();
            string strMaHoSo = comboMaHS_PhieuXuat.Text.Trim();
            string strTongTienXuat = txtTongTX_PhieuXuat.Text.Trim();
            if (strTongTienXuat == "") strTongTienXuat = "0";
            string strNgayThang;
            string strGhiChu = rtbGhiChu.Text.Trim();
            string sqtChuyenDoiNgayThang;
            if (dtpNgayLP_PhieuXuat.Checked)
            {
                strNgayThang = dtpNgayLP_PhieuXuat.Text.Trim();
                sqtChuyenDoiNgayThang = global.Return_Time_ThangNgay(strNgayThang);
            }
            else
            {
                sqtChuyenDoiNgayThang = System.DateTime.Now.ToShortDateString();
            }
            try
            {
                error.Exception_PhieuXuatHang(strMaPX, commnd, sqlConn.sqlCNN);
                error.Exception_TongTienXuat(strTongTienXuat, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                string sqlstr = "insert into PhieuXuatHang values(N'" + strMaPX + "',N'" + strMaHoSo + "',N'" + strTongTienXuat + "',N'" + sqtChuyenDoiNgayThang + "',N'" + strGhiChu + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strMaPX = txtMaPX_PhieuXuat.Text.Trim();
            string strMaHoSo = comboMaHS_PhieuXuat.Text.Trim();
            string strTongTienXuat = txtTongTX_PhieuXuat.Text.Trim();
            if (strTongTienXuat == "") strTongTienXuat = "0";
            string strNgayThang;
            string strGhiChu = rtbGhiChu.Text.Trim();
            string sqtChuyenDoiNgayThang;
            if (dtpNgayLP_PhieuXuat.Checked)
            {
                strNgayThang = dtpNgayLP_PhieuXuat.Text.Trim();
                sqtChuyenDoiNgayThang = global.Return_Time_ThangNgay(strNgayThang);
            }
            else
            {
                sqtChuyenDoiNgayThang = System.DateTime.Now.ToShortDateString();
            }
            try
            {
                error.Exception_PhieuXuatHang_CN(strMaPX, commnd,sqlConn.sqlCNN);
                error.Exception_TongTienXuat(strTongTienXuat, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                string sqlstr = "update PhieuXuatHang set MaHoSo = N'" + strMaHoSo + "',TongTienXuat=N'" + strTongTienXuat + "',NgayLapPhieu='" + sqtChuyenDoiNgayThang + "' ,GhiChu=N'" + strGhiChu + "'where MaPhieuXuat='" + strMaPX + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strMaPX = txtMaPX_PhieuXuat.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaPhieuXuat='" + strMaPX + "'";
                string strTableName = "PhieuXuatHang";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN) == false)
                {
                    MessageBox.Show("Mã Phiếu Xuất Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                string sqlstr = "Delete From PhieuXuatHang where MaPhieuXuat ='" + strMaPX + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch 
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        string strGetValue;
        private string GetValue_Combobox(string str)
        {
            if (str == "Mã Phiếu Xuất")
            {
                strGetValue = "MaPhieuXuat";
            }
            if (str == "Mã Hồ Sơ")
            {
                strGetValue = "MaHoSo";
            }
            if (str == "Tổng Tiền Xuất")
            {
                strGetValue = "TongTienXuat";
            }
            if (str == "Ngày Lập Phiếu")
            {
                strGetValue = "NgayLapPhieu";
            }
            if (str == "Ghi Chú")
            {
                strGetValue = "GhiChu";
            }
            
            return strGetValue;
        }
        private void FormatGrid(DataGrid dgIndex)
        {
            try
            {
                DataGridTableStyle grdTableStyle = new DataGridTableStyle();
                dgIndex.TableStyles.Clear();
                grdTableStyle = global.MyTableStyleCreate();
                //Mã Phiếu Xuất
                DataGridTextBoxColumn grdColMaPhieuXuat = new DataGridTextBoxColumn();
                grdColMaPhieuXuat.MappingName = "MaPhieuXuat";
                grdColMaPhieuXuat.HeaderText = "Mã Phiếu Xuất";
                grdColMaPhieuXuat.NullText = "";
                grdColMaPhieuXuat.Width = 80;
                grdColMaPhieuXuat.Alignment = HorizontalAlignment.Center;

                //Mã Hồ Sơ
                DataGridTextBoxColumn grdColMaHoSo = new DataGridTextBoxColumn();
                grdColMaHoSo.MappingName = "MaHoSo";
                grdColMaHoSo.HeaderText = "Mã Hồ Sơ";
                grdColMaHoSo.NullText = "";
                grdColMaHoSo.Width = 100;
                grdColMaHoSo.Alignment = HorizontalAlignment.Left;

                //Tổng Tiền Xuất
                DataGridTextBoxColumn grdColTongTienXuat  = new DataGridTextBoxColumn();
                grdColTongTienXuat.MappingName = "TongTienXuat";
                grdColTongTienXuat.HeaderText = "Tổng Tiền Xuất";
                grdColTongTienXuat.NullText = "";
                grdColTongTienXuat.Width = 80;
                grdColTongTienXuat.Alignment = HorizontalAlignment.Center;

                //Ngày Lập Phiếu
                DataGridTextBoxColumn grdColNgayLapPhieu = new DataGridTextBoxColumn();
                grdColNgayLapPhieu.MappingName = "NgayLapPhieu";
                grdColNgayLapPhieu.HeaderText = "Ngày Lập Phiếu";
                grdColNgayLapPhieu.NullText = "";
                grdColNgayLapPhieu.Width = 100;
                grdColNgayLapPhieu.Alignment = HorizontalAlignment.Center;

                //Ghi Chú
                DataGridTextBoxColumn grdColGhiChu = new DataGridTextBoxColumn();
                grdColGhiChu.MappingName = "GhiChu";
                grdColGhiChu.HeaderText = "Ghi Chú";
                grdColGhiChu.NullText = "";
                grdColGhiChu.Width = 80;
                grdColGhiChu.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaPhieuXuat, grdColMaHoSo, grdColTongTienXuat, grdColNgayLapPhieu, grdColGhiChu });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboTimKiem_PX.Text);
            string strText = txtTimKiem_PX.Text.Trim();
            if (strCombobox == "NgayLapPhieu")
            {
                strText = global.Return_Time_ThangNgay(strText);
            }
            string sqlstr = "select * from PhieuXuatHang where PhieuXuatHang." + strCombobox + "=N'" + strText + "' ";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }

        private void setControll(DataTable dtTable, int Index)
        {
            txtMaPX_PhieuXuat.Text = dtTable.Rows[Index]["MaPhieuXuat"].ToString();
            comboMaHS_PhieuXuat.Text = dtTable.Rows[Index]["MaHoSo"].ToString();
            txtTongTX_PhieuXuat.Text = dtTable.Rows[Index]["TongTienXuat"].ToString();
            dtpNgayLP_PhieuXuat.Text = dtTable.Rows[Index]["NgayLapPhieu"].ToString();
            rtbGhiChu.Text = dtTable.Rows[Index]["GhiChu"].ToString();
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
    }
}