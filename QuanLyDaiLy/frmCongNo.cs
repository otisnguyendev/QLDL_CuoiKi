using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy
{
    public partial class frmCongNo : Form
    {
        Modules.ConnectionString sqlConn = new QuanLyDaiLy.Modules.ConnectionString();
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.LoadCombobox loadCombo = new QuanLyDaiLy.Modules.LoadCombobox();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();
        Error.Error_CongNo error = new QuanLyDaiLy.Error.Error_CongNo();

        private MySqlCommand commnd = null;

        public frmCongNo()
        {
            InitializeComponent();
        }

        private void frmCongNo_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConn.getConnectionString();
                string sqlstr = "SELECT MaSo FROM HoSo ORDER BY MaSo";
                string display = "MaSo";
                string value = "MaSo";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboHoSo_CongNo, sqlConn.sqlCNN);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strMess.ChuaKetNoiCSDL + "\n" + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboHoSo_CongNo.Text = "";
            dtpThoiGian_CongNo.Text = "";
            txtNoDau.Text = "";
            txtPhatSinh.Text = "";
            txtNoDaTra.Text = "";
            txtNoCuoi.Text = "";
            rtbGhiChu.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboHoSo_CongNo.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dtpThoiGian_CongNo.Checked)
            {
                strNgayThang = dtpThoiGian_CongNo.Text.Trim();
                sqtChuyenDoiNgayThang = global.Return_Time_ThangNgay(strNgayThang);
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToShortDateString();
            }
            string strNoDau = txtNoDau.Text.Trim();
            string strPhatSinh = txtPhatSinh.Text.Trim();
            string strNoDaTra = txtNoDaTra.Text.Trim();
            string strNoCuoi = txtNoCuoi.Text.Trim();
            string strGhiChu = rtbGhiChu.Text.Trim();

            try
            {
                error.Exception_MaHoSo(strMaHoSo, commnd, sqlConn.sqlCNN);
                error.Exception_Tien(strNoDau, commnd);
                error.Exception_Tien(strPhatSinh, commnd);
                error.Exception_Tien(strNoDaTra, commnd);
                error.Exception_Tien(strNoCuoi, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);

                string sqlstr = "INSERT INTO CongNo (MaSo, ThoiGian, NoDau, PhatSinh, SoNoDaTra, NoCuoi, GhiChu) " +
                                $"VALUES ('{strMaHoSo}', '{sqtChuyenDoiNgayThang}', '{strNoDau}', '{strPhatSinh}', '{strNoDaTra}', '{strNoCuoi}', '{strGhiChu}')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
                MessageBox.Show(strMess.ThaoTacThanhCong, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strMess.ThaoTacThatBai + "\n" + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboHoSo_CongNo.Text.Trim();
            try
            {
                string sqlstr = $"DELETE FROM CongNo WHERE MaSo = '{strMaHoSo}'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
                MessageBox.Show(strMess.ThaoTacThanhCong, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strMess.ThaoTacThatBai + "\n" + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /*private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboBox2.Text);
            string strText = textBox7.Text.Trim();
            if (strCombobox == "NgayTiepNhan")
            {
                strText = global.Return_Time_ThangNgay(strText);
            }
            string sqlstr = "select * from CongNo where CongNo." + strCombobox + "='" + strText + "'";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dgv1);
            FormatGrid(dgv1);
        }*/

        private void FormatGrid(DataGridView dgIndex)
        {
            try
            {
                dgIndex.Columns["MaSo"].HeaderText = "Mã Hồ Sơ";
                dgIndex.Columns["ThoiGian"].HeaderText = "Thời Gian";
                dgIndex.Columns["NoDau"].HeaderText = "Nợ Đầu";
                dgIndex.Columns["PhatSinh"].HeaderText = "Phát Sinh";
                dgIndex.Columns["SoNoDaTra"].HeaderText = "Số Nợ Đã Trả";
                dgIndex.Columns["NoCuoi"].HeaderText = "Nợ Cuối";
                dgIndex.Columns["GhiChu"].HeaderText = "Ghi Chú";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView dgIndex = (DataGridView)sender;
            if (dgIndex.CurrentRow != null)
            {
                int Index = dgIndex.CurrentRow.Index;
                if (Index >= 0)
                {
                    setControll((DataTable)dgIndex.DataSource, Index);
                }
            }
        }

        private void setControll(DataTable dtTable, int Index)
        {
            comboHoSo_CongNo.Text = dtTable.Rows[Index]["MaSo"].ToString();
            dtpThoiGian_CongNo.Text = dtTable.Rows[Index]["ThoiGian"].ToString();
            txtNoDau.Text = dtTable.Rows[Index]["NoDau"].ToString();
            txtPhatSinh.Text = dtTable.Rows[Index]["PhatSinh"].ToString();
            txtNoDaTra.Text = dtTable.Rows[Index]["SoNoDaTra"].ToString();
            txtNoCuoi.Text = dtTable.Rows[Index]["NoCuoi"].ToString();
            rtbGhiChu.Text = dtTable.Rows[Index]["GhiChu"].ToString();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                string strNoDau = txtNoDau.Text.Trim();
                string strPhatSinh = txtPhatSinh.Text.Trim();
                string strNoDaTra = txtNoDaTra.Text.Trim();
                double dbNoDau = string.IsNullOrEmpty(strNoDau) ? 0 : Convert.ToDouble(strNoDau);
                double dbPhatSinh = string.IsNullOrEmpty(strPhatSinh) ? 0 : Convert.ToDouble(strPhatSinh);
                double dbNoDaTra = string.IsNullOrEmpty(strNoDaTra) ? 0 : Convert.ToDouble(strNoDaTra);
                double dbNoCuoi = (dbNoDau + dbPhatSinh) - dbNoDaTra;
                txtNoCuoi.Text = dbNoCuoi.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboHoSo_CongNo.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dtpThoiGian_CongNo.Checked)
            {
                strNgayThang = dtpThoiGian_CongNo.Text.Trim();
                sqtChuyenDoiNgayThang = global.Return_Time_ThangNgay(strNgayThang);
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToShortDateString();
            }
            string strNoDau = txtNoDau.Text.Trim();
            string strPhatSinh = txtPhatSinh.Text.Trim();
            string strNoDaTra = txtNoDaTra.Text.Trim();
            string strNoCuoi = txtNoCuoi.Text.Trim();
            string strGhiChu = rtbGhiChu.Text.Trim();

            try
            {
                error.Exception_MaHoSo_CN(strMaHoSo, commnd, sqlConn.sqlCNN);
                error.Exception_Tien(strNoDau, commnd);
                error.Exception_Tien(strPhatSinh, commnd);
                error.Exception_Tien(strNoDaTra, commnd);
                error.Exception_Tien(strNoCuoi, commnd);
                error.Exception_GhiChu(strGhiChu, commnd);

                string sqlstr = $"UPDATE CongNo SET ThoiGian = '{sqtChuyenDoiNgayThang}', NoDau = '{strNoDau}', PhatSinh = '{strPhatSinh}', " +
                                $"SoNoDaTra = '{strNoDaTra}', NoCuoi = '{strNoCuoi}', GhiChu = '{strGhiChu}' WHERE MaSo = '{strMaHoSo}'";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
                MessageBox.Show(strMess.ThaoTacThanhCong, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strMess.ThaoTacThatBai + "\n" + ex.Message, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
        }

        private string GetValue_Combobox(string str)
        {
            switch (str)
            {
                case "Mã Hồ Sơ":
                    return "MaHoSo";
                case "Thời Gian":
                    return "ThoiGian";
                case "Nợ Đầu":
                    return "NoDau";
                case "Phát Sinh":
                    return "PhatSinh";
                case "Số Nợ Đã Trả":
                    return "SoNoDaTra";
                case "Nợ Cuối":
                    return "NoCuoi";
                default:
                    return "";
            }
        }
    }
}
