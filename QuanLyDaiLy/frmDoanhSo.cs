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
    public partial class frmDoanhSo : Form
    {
        public frmDoanhSo()
        {
            InitializeComponent();
        }

        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.LoadCombobox loadCombo = new QuanLyDaiLy.Modules.LoadCombobox();
        Error.Error_DoanhSo error = new QuanLyDaiLy.Error.Error_DoanhSo();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        private MySqlCommand commnd = null; 

        private void frmDoanhSo_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
                string sqlstr = "SELECT MaSo FROM HoSo ORDER BY MaSo";
                string display = "MaSo";
                string value = "MaSo";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboMaHS, sqlConn.sqlCNN);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            comboMaHS.Text = "";
            dateTimePicker1.Text = "";
            txtTongDS_DoanhSo.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strMaSo = comboMaHS.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dateTimePicker1.Checked)
            {
                strNgayThang = dateTimePicker1.Text.Trim();
                sqtChuyenDoiNgayThang = global.Return_Time_ThangNgay(strNgayThang);
            }
            else
            {
                sqtChuyenDoiNgayThang = System.DateTime.Now.ToShortDateString();
            }
            string strTongDS = txtTongDS_DoanhSo.Text.Trim();
            if (strTongDS == "") strTongDS = "0";

            try
            {
                error.Exception_MaHoSo(strMaSo, commnd, sqlConn.sqlCNN);
                error.Exception_TienNo(strTongDS, commnd);
                string sqlstr = "Insert into DoanhSo values(N'" + strMaSo + "','" + sqtChuyenDoiNgayThang + "','" + strTongDS + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboMaHS.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dateTimePicker1.Checked)
            {
                strNgayThang = dateTimePicker1.Text.Trim();
                sqtChuyenDoiNgayThang = global.Return_Time_ThangNgay(strNgayThang);
            }
            else
            {
                sqtChuyenDoiNgayThang = System.DateTime.Now.ToShortDateString();
            }
            string strTongDS = txtTongDS_DoanhSo.Text.Trim();

            try
            {
                error.Exception_TienNo(strTongDS, commnd);
                string sqlstr = "update DoanhSo set ThoiGian = '" + sqtChuyenDoiNgayThang + "',TongDoanhSo='" + strTongDS + "' where MaHoSo='" + strMaHoSo + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboMaHS.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaSo='" + strMaHoSo + "'";
                string strTableName = "DoanhSo";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN) == false)
                {
                    MessageBox.Show("Mã Loại Đại Lý Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                string sqlstr = "Delete From DoanhSo where MaSo ='" + strMaHoSo + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboTimKiem_DoanhSo.Text);
            string strText = txtTimKiem_DoanhSo.Text.Trim();
            if (strCombobox == "ThoiGian")
            {
                strText = global.Return_Time_ThangNgay(strText);
            }
            string sqlstr = "select * from DoanhSo where " + strCombobox + "=N'" + strText + "'";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }
        string strGetValue;
        private string GetValue_Combobox(string str)
        {
            if (str == "Mã Hồ Sơ")
            {
                strGetValue = "MaSo";
            }
            if (str == "Thời Gian")
            {
                strGetValue = "ThoiGian";
            }
            if (str == "Tổng Doanh Số")
            {
                strGetValue = "TongDoanhSo";
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
                //Mã Hồ Sơ
                DataGridTextBoxColumn grdColMaHoSo = new DataGridTextBoxColumn();
                grdColMaHoSo.MappingName = "MaSo";
                grdColMaHoSo.HeaderText = "Mã Hồ Sơ";
                grdColMaHoSo.NullText = "";
                grdColMaHoSo.Width = 80;
                grdColMaHoSo.Alignment = HorizontalAlignment.Center;

                //Thời Gian
                DataGridTextBoxColumn grdColThoiGian = new DataGridTextBoxColumn();
                grdColThoiGian.MappingName = "ThoiGian";
                grdColThoiGian.HeaderText = "Thời Gian";
                grdColThoiGian.NullText = "";
                grdColThoiGian.Width = 80;
                grdColThoiGian.Alignment = HorizontalAlignment.Left;

                //Tổng Doanh Số
                DataGridTextBoxColumn grdColTongDoanhSo = new DataGridTextBoxColumn();
                grdColTongDoanhSo.MappingName = "TongDoanhSo";
                grdColTongDoanhSo.HeaderText = "Tổng Doanh Số";
                grdColTongDoanhSo.NullText = "";
                grdColTongDoanhSo.Width = 150;
                grdColTongDoanhSo.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaHoSo, grdColThoiGian, grdColTongDoanhSo });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setControll(DataTable dtTable, int Index)
        {
            comboMaHS.Text = dtTable.Rows[Index]["MaSo"].ToString();
            dateTimePicker1.Text = dtTable.Rows[Index]["ThoiGian"].ToString();
            txtTongDS_DoanhSo.Text = dtTable.Rows[Index]["TongDoanhSo"].ToString();
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
