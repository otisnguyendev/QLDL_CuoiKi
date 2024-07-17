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
    public partial class frmMatHang : Form
    {
        public frmMatHang()
        {
            InitializeComponent();
        }

        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Error.Error_MatHang error = new QuanLyDaiLy.Error.Error_MatHang();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        MySqlCommand commnd= null;

        private void frmMatHang_Load(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtMatHang_CTMH.Text = "";
            txtTenMatHang_CTMH.Text="";
            txtDonViTinh_CTMH.Text="";
            txtDonGia_CTMH.Text="";
            rtbGhiChu_CTMH.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strMaMH = txtMatHang_CTMH.Text.Trim();
            string strTenMH = txtTenMatHang_CTMH.Text.Trim();
            string strDonViTinh = txtDonViTinh_CTMH.Text.Trim();
            string strDonGia = txtDonGia_CTMH.Text.Trim();
            string strGhiChu = rtbGhiChu_CTMH.Text.Trim();
            try
            {
                error.Exception_MaMatHang(strMaMH, commnd,sqlConn.sqlCNN);
                error.Exception_TenMatHang(strTenMH, commnd);
                error.Exception_DonViTinh(strDonViTinh, commnd);
                error.Exception_DonGia(strDonGia, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                string sqlstr = "insert into MatHang values(N'" + strMaMH + "',N'" + strTenMH + "',N'" + strDonViTinh + "','" + strDonGia + "',N'" + strGhiChu + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch 
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strMaMH = txtMatHang_CTMH.Text.Trim();
            string strTenMH = txtTenMatHang_CTMH.Text.Trim();
            string strDonViTinh = txtDonViTinh_CTMH.Text.Trim();
            string strDonGia = txtDonGia_CTMH.Text.Trim();
            string strGhiChu = rtbGhiChu_CTMH.Text.Trim();

            try
            {
                error.Exception_MaMatHang_CapNhap(strMaMH, commnd,sqlConn.sqlCNN);
                error.Exception_TenMatHang(strTenMH, commnd);
                error.Exception_DonViTinh(strDonViTinh, commnd);
                error.Exception_DonGia(strDonGia, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);

                string sqlstr = "update  MatHang set TenMatHang=N'" + strTenMH + "',DonViTinh=N'" + strDonViTinh + "',DonGia=N'" + strDonGia + "',GhiChu=N'" + strGhiChu + "' where MaMatHang=N'" + strMaMH + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strMaMH = txtMatHang_CTMH.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaMatHang='" + strMaMH + "'";
                string strTableName = "MatHang";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN) == false)
                {
                    MessageBox.Show("Mã Mặt Hàng Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                string sqlstr = "Delete From MatHang where MaMatHang ='" + strMaMH + "'";
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
            if (str == "Mã Mặt Hàng")
            {
                strGetValue = "MaMatHang";
            }
            if (str == "Tên Mặt Hàng")
            {
                strGetValue = "TenMatHang";
            }
            if (str == "Đơn Vị Tính")
            {
                strGetValue = "DonViTinh";
            }
            if (str == "Đơn Giá")
            {
                strGetValue = "DonGia";
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
                //Mã Mặt Hàng
                DataGridTextBoxColumn grdColMaMatHang = new DataGridTextBoxColumn();
                grdColMaMatHang.MappingName = "MaMatHang";
                grdColMaMatHang.HeaderText = "Mã Mặt Hàng";
                grdColMaMatHang.NullText = "";
                grdColMaMatHang.Width = 80;
                grdColMaMatHang.Alignment = HorizontalAlignment.Center;

                //Tên Mặt Hàng
                DataGridTextBoxColumn grdColTenMatHang = new DataGridTextBoxColumn();
                grdColTenMatHang.MappingName = "TenMatHang";
                grdColTenMatHang.HeaderText = "Tên Mặt Hàng";
                grdColTenMatHang.NullText = "";
                grdColTenMatHang.Width = 150;
                grdColTenMatHang.Alignment = HorizontalAlignment.Left;

                //Đơn Vị Tính
                DataGridTextBoxColumn grdColDonViTinh = new DataGridTextBoxColumn();
                grdColDonViTinh.MappingName = "DonViTinh";
                grdColDonViTinh.HeaderText = "Đơn Vị Tính";
                grdColDonViTinh.NullText = "";
                grdColDonViTinh.Width = 80;
                grdColDonViTinh.Alignment = HorizontalAlignment.Center;

                //Đơn Giá
                DataGridTextBoxColumn grdColDonGia = new DataGridTextBoxColumn();
                grdColDonGia.MappingName = "DonGia";
                grdColDonGia.HeaderText = "Đơn Giá";
                grdColDonGia.NullText = "";
                grdColDonGia.Width = 80;
                grdColDonGia.Alignment = HorizontalAlignment.Center;

                //Ghi Chú
                DataGridTextBoxColumn grdColGhiChu = new DataGridTextBoxColumn();
                grdColGhiChu.MappingName = "GhiChu";
                grdColGhiChu.HeaderText = "Ghi Chú";
                grdColGhiChu.NullText = "";
                grdColGhiChu.Width = 80;
                grdColGhiChu.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaMatHang, grdColTenMatHang, grdColDonViTinh, grdColDonGia, grdColGhiChu });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboTim_MatHang.Text);
            string strText = txtTimKiem_MatHang.Text.Trim();
            string sqlstr = "select * from MatHang where " + strCombobox + "=N'" + strText + "'";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }
        private void setControll(DataTable dtTable, int Index)
        {
            txtMatHang_CTMH.Text = dtTable.Rows[Index]["MaMatHang"].ToString();
            txtTenMatHang_CTMH.Text = dtTable.Rows[Index]["TenMatHang"].ToString();
            txtDonViTinh_CTMH.Text = dtTable.Rows[Index]["DonViTinh"].ToString();
            txtDonGia_CTMH.Text = dtTable.Rows[Index]["DonGia"].ToString();
            rtbGhiChu_CTMH.Text = dtTable.Rows[Index]["GhiChu"].ToString();
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