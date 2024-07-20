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
    public partial class frmPhieuThuTien : Form
    {
        public frmPhieuThuTien()
        {
            InitializeComponent();
        }

        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Modules.LoadCombobox loadCombo = new QuanLyDaiLy.Modules.LoadCombobox();
        Error.Error_PhieuThuTien error = new QuanLyDaiLy.Error.Error_PhieuThuTien();

        private MySqlCommand commnd= null;

        private void frmPhieuThuTien_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
                string sqlstr = "SELECT MaHoSo FROM HoSo ORDER BY MaHoSo";
                string display = "MaHoSo";
                string value = "MaHoSo";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboMaHS_PT, sqlConn.sqlCNN);
                string sqlstr1 = "SELECT MaPhieuXuat FROM PhieuXuatHang ORDER BY MaPhieuXuat";
                string value1 = "MaPhieuXuat";
                loadCombo.Load_LoaiDaiLy(sqlstr1, display, value1, comboMaPX_PT, sqlConn.sqlCNN);
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
            txtMaPT_PT.Text = "";
            comboMaHS_PT.Text="";
            comboMaPX_PT.Text="";
            dtpNgayTT_PT.Text="";
            txtSoTienT_PT.Text="";
            rtbGhiChu_PT.Text="";
        }
        //Download source code tại Sharecode.vn
        private void button3_Click(object sender, EventArgs e)
        {
            string strMaPT = txtMaPT_PT.Text.Trim();
            string strMaHS = comboMaHS_PT.Text.Trim();
            string strMaPX = comboMaPX_PT.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dtpNgayTT_PT.Checked)
            {
                DateTime dtNgayTN = dtpNgayTT_PT.Value;
                sqtChuyenDoiNgayThang = dtNgayTN.ToString("yyyy-MM-dd");
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToString("yyyy-MM-dd");
            }
            string strSoTienT = txtSoTienT_PT.Text.Trim();
            if (strSoTienT == "") strSoTienT = "0";
            string strGhiChu_PT = rtbGhiChu_PT.Text.Trim();
            try
            {
                error.Exception_MaPhieuThu(strMaPT, commnd, sqlConn.sqlCNN);
                error.Exception_SoTienThu(strSoTienT, commnd);
                error.Exception_GhiChu(strGhiChu_PT, commnd);
                string sqlstr = "Insert into PhieuThuTien values(N'" + strMaPT + "',N'" + strMaHS + "',N'" + strMaPX + "','" + sqtChuyenDoiNgayThang + "',N'" + strSoTienT + "',N'" + strGhiChu_PT + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strMaPT = txtMaPT_PT.Text.Trim();
            string strMaHS = comboMaHS_PT.Text.Trim();
            string strMaPX = comboMaPX_PT.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dtpNgayTT_PT.Checked)
            {
                DateTime dtNgayTN = dtpNgayTT_PT.Value;
                sqtChuyenDoiNgayThang = dtNgayTN.ToString("yyyy-MM-dd");
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToString("yyyy-MM-dd");
            }
            string strSoTienT = txtSoTienT_PT.Text.Trim();
            if (strSoTienT == "") strSoTienT = "0";
            string strGhiChu_PT = rtbGhiChu_PT.Text.Trim();

            try
            {
                error.Exception_MaPhieuThu_CN(strMaPT, commnd, sqlConn.sqlCNN);
                error.Exception_SoTienThu(strSoTienT, commnd);
                error.Exception_GhiChu(strGhiChu_PT, commnd);

                StringBuilder sqlBuilder = new StringBuilder("UPDATE PhieuThuTien SET ");
                List<string> setClauses = new List<string>();

                if (!string.IsNullOrEmpty(strMaHS))
                {
                    setClauses.Add("MaHoSo = @MaHoSo");
                }
                if (!string.IsNullOrEmpty(strMaPX))
                {
                    setClauses.Add("MaPhieuXuat = @MaPhieuXuat");
                }
                if (!string.IsNullOrEmpty(strSoTienT))
                {
                    setClauses.Add("SoTienThu = @SoTienThu");
                }
                if (!string.IsNullOrEmpty(strGhiChu_PT))
                {
                    setClauses.Add("GhiChu = @GhiChu");
                }
                if (dtpNgayTT_PT.Checked)
                {
                    setClauses.Add("NgayThuTien = @NgayThuTien");
                }

                if (setClauses.Count > 0)
                {
                    sqlBuilder.Append(string.Join(", ", setClauses));
                    sqlBuilder.Append(" WHERE MaPhieuThu = @MaPhieuThu");

                    string sqlstr = sqlBuilder.ToString();

                    using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=dbcuoiky;Uid=root;Pwd=123456;"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(sqlstr, conn))
                        {
                            if (!string.IsNullOrEmpty(strMaHS))
                            {
                                cmd.Parameters.AddWithValue("@MaHoSo", strMaHS);
                            }
                            if (!string.IsNullOrEmpty(strMaPX))
                            {
                                cmd.Parameters.AddWithValue("@MaPhieuXuat", strMaPX);
                            }
                            if (!string.IsNullOrEmpty(strSoTienT))
                            {
                                cmd.Parameters.AddWithValue("@SoTienThu", strSoTienT);
                            }
                            if (!string.IsNullOrEmpty(strGhiChu_PT))
                            {
                                cmd.Parameters.AddWithValue("@GhiChu", strGhiChu_PT);
                            }
                            if (dtpNgayTT_PT.Checked)
                            {
                                cmd.Parameters.AddWithValue("@NgayThuTien", sqtChuyenDoiNgayThang);
                            }
                            cmd.Parameters.AddWithValue("@MaPhieuThu", strMaPT);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strMess.ThaoTacThatBai + ": " + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string strMaPT = txtMaPT_PT.Text.Trim();
            try
            {
                string strChuoiDieuKien = "MaPhieuThu='" + strMaPT + "'";
                string strTableName = "PhieuTHuTien";
                if (global.Test_Database(strChuoiDieuKien, strTableName, sqlConn.sqlCNN) == false)
                {
                    MessageBox.Show("Mã Phiếu Thu Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    commnd.ExecuteNonQuery();
                }
                string sqlstr = "Delete From PhieuTHuTien where MaPhieuThu ='" + strMaPT + "'";
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
            if (str == "Mã Phiếu Thu")
            {
                strGetValue = "MaPhieuThu";
            }
            if (str == "Mã Hồ Sơ")
            {
                strGetValue = "MaHoSo";
            }
            if (str == "Mã Phiếu Xuất")
            {
                strGetValue = "MaPhieuXuat";
            }
            if (str == "Ngày Thu Tiền")
            {
                strGetValue = "NgayThuTien";
            }
            if (str == "Số Tiền Thu")
            {
                strGetValue = "SoTienTHu";
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
                //Mã Phiếu Thu
                DataGridTextBoxColumn grdColMaPhieuThu = new DataGridTextBoxColumn();
                grdColMaPhieuThu.MappingName = "MaPhieuThu";
                grdColMaPhieuThu.HeaderText = "Mã Phiếu Thu";
                grdColMaPhieuThu.NullText = "";
                grdColMaPhieuThu.Width = 80;
                grdColMaPhieuThu.Alignment = HorizontalAlignment.Center;

                //Mã Hồ Sơ
                DataGridTextBoxColumn grdColMaHoSo = new DataGridTextBoxColumn();
                grdColMaHoSo.MappingName = "MaHoSo";
                grdColMaHoSo.HeaderText = "Mã Hồ Sơ";
                grdColMaHoSo.NullText = "";
                grdColMaHoSo.Width = 150;
                grdColMaHoSo.Alignment = HorizontalAlignment.Left;

                //Mã Phiếu Xuất
                DataGridTextBoxColumn grdColMaPhieuXuat = new DataGridTextBoxColumn();
                grdColMaPhieuXuat.MappingName = "MaPhieuXuat";
                grdColMaPhieuXuat.HeaderText = "Mã Phiếu Xuất";
                grdColMaPhieuXuat.NullText = "";
                grdColMaPhieuXuat.Width = 80;
                grdColMaPhieuXuat.Alignment = HorizontalAlignment.Center;

                //Ngày Thu Tiền
                DataGridTextBoxColumn grdColNgayThuTien = new DataGridTextBoxColumn();
                grdColNgayThuTien.MappingName = "NgayThuTien";
                grdColNgayThuTien.HeaderText = "Ngày Thu Tiền";
                grdColNgayThuTien.NullText = "";
                grdColNgayThuTien.Width = 80;
                grdColNgayThuTien.Alignment = HorizontalAlignment.Center;

                //Số Tiền Thu
                DataGridTextBoxColumn grdColSoTienTHu = new DataGridTextBoxColumn();
                grdColSoTienTHu.MappingName = "SoTienTHu";
                grdColSoTienTHu.HeaderText = "Số Tiền Thu";
                grdColSoTienTHu.NullText = "";
                grdColSoTienTHu.Width = 80;
                grdColSoTienTHu.Alignment = HorizontalAlignment.Center;

                //Ghi Chú
                DataGridTextBoxColumn grdColGhiChu = new DataGridTextBoxColumn();
                grdColGhiChu.MappingName = "GhiChu";
                grdColGhiChu.HeaderText = "Ghi Chú";
                grdColGhiChu.NullText = "";
                grdColGhiChu.Width = 80;
                grdColGhiChu.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaPhieuThu, grdColMaHoSo, grdColMaPhieuXuat, grdColNgayThuTien, grdColSoTienTHu, grdColGhiChu });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboTim_PT.Text);
            string strText = txtTim_PT.Text.Trim();
            if (strCombobox == "NgayThuTien")
            {
                strText = global.Return_Time_ThangNgay(strText);
            }
            string sqlstr = "select * from PhieuThuTien where " + strCombobox + "=N'" + strText + "'";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }
        private void setControll(DataTable dtTable, int Index)
        {
            txtMaPT_PT.Text = dtTable.Rows[Index]["MaPhieuThu"].ToString();
            comboMaHS_PT.Text = dtTable.Rows[Index]["MaHoSo"].ToString();
            comboMaPX_PT.Text = dtTable.Rows[Index]["MaPhieuXuat"].ToString();
            dtpNgayTT_PT.Text = dtTable.Rows[Index]["NgayThuTien"].ToString();
            txtSoTienT_PT.Text = dtTable.Rows[Index]["SoTienTHu"].ToString();
            rtbGhiChu_PT.Text = dtTable.Rows[Index]["GhiChu"].ToString();
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