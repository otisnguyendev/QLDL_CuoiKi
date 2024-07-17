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
    public partial class frmChiTietXuatHang : Form
    {
        public frmChiTietXuatHang()
        {
            InitializeComponent();
        }

        Modules.LoadCombobox loadCombo = new QuanLyDaiLy.Modules.LoadCombobox();
        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Error.Error_ChiTietXuatHang error = new QuanLyDaiLy.Error.Error_ChiTietXuatHang();

        private MySqlCommand commnd= null;

        private void frmChiTietXuatHang_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
                string sqlstr = "SELECT MaMatHang,TenMatHang FROM MatHang ORDER BY TenMatHang";
                string display = "TenMatHang";
                string value = "MaMatHang";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboTenMH_CTXH, sqlConn.sqlCNN);

                string sqlstr1 = "SELECT MaPhieuXuat FROM PhieuXuatHang ORDER BY MaPhieuXuat";
                string display1 = "MaPhieuXuat";
                string value1 = "MaPhieuXuat";
                loadCombo.Load_LoaiDaiLy(sqlstr1, display1, value1, comboMaPX_CTXH, sqlConn.sqlCNN);
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
            txtSoLuong_CTXH.Text = "";
            txtDonGia_CTXH.Text = "";
            txtDonVT_CTXH.Text = "";
            txtThanhTien_CTXH.Text = "";
            rtbGhiChu_CTXH.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strTenMatHang = comboTenMH_CTXH.SelectedValue.ToString();
            string strMaPX = comboMaPX_CTXH.Text.Trim();
            string strSoLuong = txtSoLuong_CTXH.Text.Trim();
            if (strSoLuong.Length == 0)
            {
                strSoLuong = "0";
            }
            string strDonGia = txtDonGia_CTXH.Text.Trim();
            string strDonVT = txtDonVT_CTXH.Text.Trim();
            string strThanhTien = txtThanhTien_CTXH.Text.Trim();
            string strGhiChu = rtbGhiChu_CTXH.Text.Trim();

            try
            {
                error.Exception_MaPhieuXuat(strMaPX, commnd, sqlConn.sqlCNN);
                error.Exception_SoLuong(strSoLuong, commnd);
                error.Exception_DonGia(strDonGia, commnd);
                error.Exception_DonViTinh(strDonVT, commnd);
                error.Exception_ThanhTien(strThanhTien, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                string sqlstr = "insert into ChiTietXuatHang values(N'" + strTenMatHang + "',N'" + strMaPX + "',N'" + strSoLuong + "',N'" + strDonGia + "',N'" + strDonVT + "',N'" + strThanhTien + "',N'" + strGhiChu + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strTenMatHang = comboTenMH_CTXH.SelectedValue.ToString();
            string strMaPX = comboMaPX_CTXH.Text.Trim();
            string strSoLuong = txtSoLuong_CTXH.Text.Trim();
            if (strSoLuong.Length == 0)
            {
                strSoLuong = "0";
            }
            string strDonGia = txtDonGia_CTXH.Text.Trim();
            string strDonVT = txtDonVT_CTXH.Text.Trim();
            string strThanhTien = txtThanhTien_CTXH.Text.Trim();
            string strGhiChu = rtbGhiChu_CTXH.Text.Trim();

            try
            {
                error.Exception_MaPhieuXuat_CN(strMaPX, commnd, sqlConn.sqlCNN);
                error.Exception_SoLuong(strSoLuong, commnd);
                error.Exception_DonGia(strDonGia, commnd);
                error.Exception_DonViTinh(strDonVT, commnd);
                error.Exception_ThanhTien(strThanhTien, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                string sqlstr = "update ChiTietXuatHang set MaMatHang = N'" + strTenMatHang + "',SoLuong=N'" + strSoLuong + "',DonGia=N'" + strDonGia + "',DonViTinh=N'" + strDonVT + "',ThanhTien=N'" + strThanhTien + "' ,GhiChu=N'" + strGhiChu + "'where MaPHieuXuat='" + strMaPX + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string strMaPX = comboMaPX_CTXH.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaPhieuXuat='" + strMaPX + "'";
                string strTableName = "ChiTietXuatHang";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN) == false)
                {
                    MessageBox.Show("Mã Phiếu Xuất Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                string sqlstr = "Delete From ChiTietXuatHang where MaPhieuXuat ='" + strMaPX + "'";
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
            if (str == "Tên Mặt Hàng")
            {
                strGetValue = "MaMatHang";
            }
            if (str == "Mã Phiếu Xuất")
            {
                strGetValue = "MaPhieuXuat";
            }
            if (str == "Số Lượng")
            {
                strGetValue = "SoLuong";
            }
            if (str == "Đơn Giá")
            {
                strGetValue = "DonGia";
            }
            if (str == "Đơn Vị Tính")
            {
                strGetValue = "DonViTinh";
            }
            if (str == "Thành Tiền")
            {
                strGetValue = "ThanhTien";
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
                //Tên Mặt Hàng
                DataGridTextBoxColumn grdColTenMatHang = new DataGridTextBoxColumn();
                grdColTenMatHang.MappingName = "TenMatHang";
                grdColTenMatHang.HeaderText = "Tên Mặt Hàng";
                grdColTenMatHang.NullText = "";
                grdColTenMatHang.Width = 80;
                grdColTenMatHang.Alignment = HorizontalAlignment.Center;

                //Mã Phiếu Xuất
                DataGridTextBoxColumn grdColMaPhieuXuat = new DataGridTextBoxColumn();
                grdColMaPhieuXuat.MappingName = "MaPhieuXuat";
                grdColMaPhieuXuat.HeaderText = "Mã Phiếu Xuất";
                grdColMaPhieuXuat.NullText = "";
                grdColMaPhieuXuat.Width = 150;
                grdColMaPhieuXuat.Alignment = HorizontalAlignment.Left;

                //Số Lượng
                DataGridTextBoxColumn grdColSoLuong = new DataGridTextBoxColumn();
                grdColSoLuong.MappingName = "SoLuong";
                grdColSoLuong.HeaderText = "Số Lượng";
                grdColSoLuong.NullText = "";
                grdColSoLuong.Width = 80;
                grdColSoLuong.Alignment = HorizontalAlignment.Center;

                //Đơn Giá
                DataGridTextBoxColumn grdColDonGia = new DataGridTextBoxColumn();
                grdColDonGia.MappingName = "DonGia";
                grdColDonGia.HeaderText = "Đơn Giá";
                grdColDonGia.NullText = "";
                grdColDonGia.Width = 150;
                grdColDonGia.Alignment = HorizontalAlignment.Left;

                //Đơn Vị Tính
                DataGridTextBoxColumn grdColDonViTinh = new DataGridTextBoxColumn();
                grdColDonViTinh.MappingName = "DonViTinh";
                grdColDonViTinh.HeaderText = "Đơn Vị Tính";
                grdColDonViTinh.NullText = "";
                grdColDonViTinh.Width = 80;
                grdColDonViTinh.Alignment = HorizontalAlignment.Center;

                //Thành Tiền
                DataGridTextBoxColumn grdColThanhTien = new DataGridTextBoxColumn();
                grdColThanhTien.MappingName = "ThanhTien";
                grdColThanhTien.HeaderText = "Thành Tiền";
                grdColThanhTien.NullText = "";
                grdColThanhTien.Width = 80;
                grdColThanhTien.Alignment = HorizontalAlignment.Center;

                //Ghi Chú
                DataGridTextBoxColumn grdColGhiChu = new DataGridTextBoxColumn();
                grdColGhiChu.MappingName = "GhiChu";
                grdColGhiChu.HeaderText = "Ghi Chú";
                grdColGhiChu.NullText = "";
                grdColGhiChu.Width = 80;
                grdColGhiChu.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColTenMatHang, grdColMaPhieuXuat, grdColSoLuong, grdColDonGia, grdColDonViTinh, grdColThanhTien, grdColGhiChu });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboTim_CTXH.Text);
            string strText = txtTim_CTXH.Text.Trim();
            string sqlstr;
            if (strCombobox == "MaMatHang")
            {
                sqlstr = "select * from MatHang,ChiTietXuatHang where MatHang.TenMatHang=N'" + strText + "' and ChiTietXuatHang.MaMatHang=(select MaMatHang from MatHang where MatHang.TenMatHang=N'" + strText + "')";
            }
            else
            {
                sqlstr = "select * from MatHang,ChiTietXuatHang where ChiTietXuatHang." + strCombobox + "=N'" + strText + "' and ChiTietXuatHang.MaMatHang =(select MaMatHang from ChiTietXuatHang where ChiTietXuatHang." + strCombobox + "=N'" + strText + "')";
            }
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }

        private void setControll(DataTable dtTable, int Index)
        {
            comboTenMH_CTXH.Text = dtTable.Rows[Index]["TenMatHang"].ToString();
            comboMaPX_CTXH.Text = dtTable.Rows[Index]["MaPhieuXuat"].ToString();
            txtSoLuong_CTXH.Text = dtTable.Rows[Index]["SoLuong"].ToString();
            txtDonGia_CTXH.Text = dtTable.Rows[Index]["DonGia"].ToString();
            txtDonVT_CTXH.Text = dtTable.Rows[Index]["DonViTinh"].ToString();
            txtThanhTien_CTXH.Text = dtTable.Rows[Index]["ThanhTien"].ToString();
            rtbGhiChu_CTXH.Text = dtTable.Rows[Index]["GhiChu"].ToString();
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