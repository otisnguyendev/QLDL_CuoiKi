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
    public partial class frmLoaiDaiLy : Form
    {
        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Error.Error_LoaiDaiLy error = new QuanLyDaiLy.Error.Error_LoaiDaiLy();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        MySqlCommand commnd= null;
        string strGetValue;
        private string sqlstr;
        
        public frmLoaiDaiLy()
        {
            InitializeComponent();
        }

        private void frmLoaiDaiLy_Load(object sender, EventArgs e)
        {
            try
            {
                btnCapNhap.Enabled = false;
                lblMaDLcu.Visible = false;
                lblMaDaiLyMoi.Visible = false;
                txtMaDLmoi.Visible = false;
                sqlConn.getConnectionString();
            }
            catch
            {
                MessageBox.Show(strMess.ChuaKetNoiCSDL, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region : Các button
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaLDL.Text = "";
            txtTenLDL.Text = "";
            rtbGhiChu.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strMaDL = txtMaLDL.Text.Trim();
            string strTenLoaiDL = txtTenLDL.Text.Trim();
            string strGhiChu = rtbGhiChu.Text.Trim();
            try
            {
                error.Exception_MaLoaiDL(strMaDL, commnd,sqlConn.sqlCNN);
                error.Exception_TenLoaiDL(strTenLoaiDL, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                sqlstr = "insert into LoaiDaiLy values(N'" + strMaDL + "',N'" + strTenLoaiDL + "',N'" + strGhiChu + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
           catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strMaDLCu = txtMaLDL.Text.Trim();
            string strMaDLMoi = txtMaDLmoi.Text.Trim();
            string strTenLoaiDL = txtTenLDL.Text.Trim();
            string strGhiChu = rtbGhiChu.Text.Trim();
            try
            {
                error.Exception_MaLoaiDL_CN_Cu(strMaDLCu, commnd,sqlConn.sqlCNN);
                error.Exception_MaLoaiDL_CN_Moi(strMaDLMoi, commnd, sqlConn.sqlCNN);
                error.Exception_TenLoaiDL(strTenLoaiDL, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);
                sqlstr = "update LoaiDaiLy set MaLoaiDaiLy = N'" + strMaDLMoi + "',TenLoaiDaily=N'" + strTenLoaiDL + "',GhiChu=N'" + strGhiChu + "' where MaLoaiDaily='" + strMaDLCu + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch 
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string strMaDL = txtMaLDL.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaLoaiDaiLy='" + strMaDL + "'";
                string strTableName = "LoaiDaiLy";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN)==false)
                {
                    MessageBox.Show("Mã Loại Đại Lý Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                sqlstr = "Delete From LoaiDaiLy where MaLoaiDaiLy ='" + strMaDL + "'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion
        #region : Thêm Bớt Chức Năng
        private void button4_Click(object sender, EventArgs e)
        {
            btnCapNhap.Enabled = true;
            lblMaDLcu.Visible = true;
            lblMaDaiLyMoi.Visible = true;
            txtMaDLmoi.Visible = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btnCapNhap.Enabled = false;
            lblMaDLcu.Visible = false;
            lblMaDaiLyMoi.Visible = false;
            txtMaDLmoi.Visible = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(cbTimKiem.Text);
            string strText = txtTimKiem.Text.Trim();
            //sqlstr = "select MaLoaiDaiLy as Mã_Loại_Đại_Lý, TenLoaiDaiLy as Tên_Loại_Đại_Lý,GhiChu as Ghi_Chú from LoaiDaiLy where " + strCombobox + "='" + strText + "'";
            //global.Seach_DataGrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            sqlstr = "select * from LoaiDaiLy where " + strCombobox + "=N'" + strText + "'";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }
        #endregion
        #region : Hàm Lấy Giá Trị Của Combocox
        private string GetValue_Combobox(string str)
        {
            if (str == "Mã Loại Đại Lý")
            {
                strGetValue = "MaLoaiDaiLy";
            }
            if (str == "Tên Loại Đại Lý")
            {
                strGetValue = "TenLoaiDaiLy";
            }
            return strGetValue;
        }
        #endregion
        
        #region : Định Dạng Bảng
        private void FormatGrid(DataGrid dgIndex)
        {
            try
            {
                DataGridTableStyle grdTableStyle = new DataGridTableStyle();
                dgIndex.TableStyles.Clear();
                grdTableStyle = global.MyTableStyleCreate();
                //MaLoaiDaiLy
                DataGridTextBoxColumn grdColMaLoaiDaiLy = new DataGridTextBoxColumn();
                grdColMaLoaiDaiLy.MappingName = "MaLoaiDaiLy";
                grdColMaLoaiDaiLy.HeaderText = "Mã Loại Đại Lý";
                grdColMaLoaiDaiLy.NullText = "";
                grdColMaLoaiDaiLy.Width = 80;
                grdColMaLoaiDaiLy.Alignment = HorizontalAlignment.Center;

                //TenLoaiDaiLy
                DataGridTextBoxColumn grdColTenLoaiDaiLy = new DataGridTextBoxColumn();
                grdColTenLoaiDaiLy.MappingName = "TenLoaiDaiLy";
                grdColTenLoaiDaiLy.HeaderText = "Tên Loại Đại Lý";
                grdColTenLoaiDaiLy.NullText = "";
                grdColTenLoaiDaiLy.Width = 150;
                grdColTenLoaiDaiLy.Alignment = HorizontalAlignment.Left;

                //GhiChu
                DataGridTextBoxColumn grdColGhiChu = new DataGridTextBoxColumn();
                grdColGhiChu.MappingName = "GhiChu";
                grdColGhiChu.HeaderText = "Ghi Chú";
                grdColGhiChu.NullText = "";
                grdColGhiChu.Width = 80;
                grdColGhiChu.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaLoaiDaiLy, grdColTenLoaiDaiLy, grdColGhiChu });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region "Event Grid"
        private void setControll(DataTable dtTable, int Index)
        {
            txtMaLDL.Text = dtTable.Rows[Index]["MaLoaiDaiLy"].ToString();
            txtTenLDL.Text = dtTable.Rows[Index]["TenLoaiDaiLy"].ToString();
            rtbGhiChu.Text = dtTable.Rows[Index]["GhiChu"].ToString();
        }

        private void dataGrid1_DataSourceChanged(object sender, EventArgs e)
        {
            //DataTable dtTable = (DataTable)dataGrid1.DataSource;
            //if ((dtTable != null) && (dtTable.Rows.Count > 0))
            //{
            //    setControll(dtTable, 0);
            //}
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (bolCheck) return;
            //if (bolLoader == false) return;
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
        #endregion
    }
}