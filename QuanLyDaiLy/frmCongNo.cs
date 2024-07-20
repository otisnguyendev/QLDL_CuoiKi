using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
                string sqlstr = "SELECT MaHoSo FROM HoSo ORDER BY MaHoSo";
                string display = "MaHoSo";
                string value = "MaHoSo";
                loadCombo.Load_LoaiDaiLy(sqlstr, display, value, comboHoSo_CongNo, sqlConn.sqlCNN);
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
                DateTime dtpTG_CN = dtpThoiGian_CongNo.Value;
                sqtChuyenDoiNgayThang = dtpTG_CN.ToString("yyyy-MM-dd");
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToString("yyyy-MM-dd");
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
                string sqlstr = "Insert into CongNo (MaHoSo, ThoiGian, NoDau, PhatSinh, SoDuCuoi, NoCuoi, GhiChu) values(N'" + strMaHoSo + "','" + sqtChuyenDoiNgayThang + "','" + strNoDau + "','" + strPhatSinh + "','" + strNoDaTra + "','" + strNoCuoi + "',N'" + strGhiChu + "')";
                global.SQL_Database(sqlstr, sqlConn.sqlCNN);
            }
            catch
            {
                MessageBox.Show(strMess.ThaoTacThatBai, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtNoDaTra_TextChanged(object sender, EventArgs e)
        {
            //string strNoDau = txtNoDau.Text;
            //string strPhatSinh = txtPhatSinh.Text;
            //string strNoDaTra = txtNoDaTra.Text;

            //double dbNoDau = Convert.ToDouble(strNoDau);
            //double dbPhatSinh = Convert.ToDouble(strPhatSinh);
            //double dbNoDaTra = Convert.ToDouble(strNoDaTra);
            //double dbNoCuoi = (dbNoDau + dbPhatSinh) - dbNoDaTra;
            //txtNoCuoi.Text = dbNoCuoi.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboHoSo_CongNo.Text.Trim();
            try
            {
                string sqlstr = "Delete From CongNo where MaHoSo =N'" + strMaHoSo + "'";
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
            if (str == "Mã Hồ Sơ")
            {
                strGetValue = "MaHoSo";
            }
            if (str == "Thời Gian")
            {
                strGetValue = "ThoiGian";
            }
            if (str == "Nợ Đầu")
            {
                strGetValue = "NoDau";
            }
            if (str == "Phát Sinh")
            {
                strGetValue = "PhatSinh";
            }
            if (str == "Số Nợ Đã Trả")
            {
                strGetValue = "SoDuCuoi";
            }
            if (str == "Nợ Cuối")
            {
                strGetValue = "NoCuoi";
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
                grdColMaHoSo.MappingName = "MaHoSo";
                grdColMaHoSo.HeaderText = "Mã Hồ Sơ";
                grdColMaHoSo.NullText = "";
                grdColMaHoSo.Width = 80;
                grdColMaHoSo.Alignment = HorizontalAlignment.Center;

                //Thời Gian
                DataGridTextBoxColumn grdColThoiGian = new DataGridTextBoxColumn();
                grdColThoiGian.MappingName = "ThoiGian";
                grdColThoiGian.HeaderText = "Thời Gian";
                grdColThoiGian.NullText = "";
                grdColThoiGian.Width = 100;
                grdColThoiGian.Alignment = HorizontalAlignment.Left;

                //Nợ Đầu
                DataGridTextBoxColumn grdColNoDau = new DataGridTextBoxColumn();
                grdColNoDau.MappingName = "NoDau";
                grdColNoDau.HeaderText = "Nợ Đầu";
                grdColNoDau.NullText = "";
                grdColNoDau.Width = 80;
                grdColNoDau.Alignment = HorizontalAlignment.Center;

                //Nợ Phát Sinh
                DataGridTextBoxColumn grdColPhatSinh = new DataGridTextBoxColumn();
                grdColPhatSinh.MappingName = "Nợ Phát Sinh";
                grdColPhatSinh.HeaderText = "PhatSinh";
                grdColPhatSinh.NullText = "";
                grdColPhatSinh.Width = 100;
                grdColPhatSinh.Alignment = HorizontalAlignment.Center;

                //Số Nợ Đã Trả
                DataGridTextBoxColumn grdColSoDuCuoi = new DataGridTextBoxColumn();
                grdColSoDuCuoi.MappingName = "SoDuCuoi";
                grdColSoDuCuoi.HeaderText = "Số Nợ Đã Trả";
                grdColSoDuCuoi.NullText = "";
                grdColSoDuCuoi.Width = 80;
                grdColSoDuCuoi.Alignment = HorizontalAlignment.Center;

                //Nợ Cuối
                DataGridTextBoxColumn grdColNoCuoi = new DataGridTextBoxColumn();
                grdColNoCuoi.MappingName = "NoCuoi";
                grdColNoCuoi.HeaderText = "Nợ Cuối";
                grdColNoCuoi.NullText = "";
                grdColNoCuoi.Width = 120;
                grdColNoCuoi.Alignment = HorizontalAlignment.Center;

                //Ghi Chú
                DataGridTextBoxColumn grdColGhiChu = new DataGridTextBoxColumn();
                grdColGhiChu.MappingName = "GhiChu";
                grdColGhiChu.HeaderText = "Ghi Chú";
                grdColGhiChu.NullText = "";
                grdColGhiChu.Width = 80;
                grdColGhiChu.Alignment = HorizontalAlignment.Center;

                grdTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[] { grdColMaHoSo, grdColThoiGian, grdColNoDau, grdColPhatSinh, grdColSoDuCuoi, grdColNoCuoi, grdColGhiChu });
                dgIndex.TableStyles.Add(grdTableStyle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string strCombobox = GetValue_Combobox(comboBox2.Text);
            string strText = textBox7.Text.Trim();
            if (strCombobox == "NgayTiepNhan")
            {
                strText = global.Return_Time_ThangNgay(strText);
            }
            string sqlstr = "select * from CongNo where CongNo." + strCombobox + "='" + strText + "'";
            global.LoadDataInToDatagrid(sqlstr, sqlConn.sqlCNN, dataGrid1);
            FormatGrid(dataGrid1);
        }

        private void setControll(DataTable dtTable, int Index)
        {
            comboHoSo_CongNo.Text = dtTable.Rows[Index]["MaHoSo"].ToString();
            dtpThoiGian_CongNo.Text = dtTable.Rows[Index]["ThoiGian"].ToString();
            txtNoDau.Text = dtTable.Rows[Index]["NoDau"].ToString();
            txtPhatSinh.Text = dtTable.Rows[Index]["PhatSinh"].ToString();
            txtNoDaTra.Text = dtTable.Rows[Index]["SoDuCuoi"].ToString();
            txtNoCuoi.Text = dtTable.Rows[Index]["NoCuoi"].ToString();
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

        private void button22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string strNoDau = txtNoDau.Text;
            string strPhatSinh = txtPhatSinh.Text;
            string strNoDaTra = txtNoDaTra.Text;
            if (strNoDau == "") strNoDau = "0";
            if (strPhatSinh == "") strPhatSinh = "0";
            if (strNoDaTra == "") strNoDaTra = "0";
            double dbNoDau = Convert.ToDouble(strNoDau);
            double dbPhatSinh = Convert.ToDouble(strPhatSinh);
            double dbNoDaTra = Convert.ToDouble(strNoDaTra);
            double dbNoCuoi = (dbNoDau + dbPhatSinh) - dbNoDaTra;
            txtNoCuoi.Text = dbNoCuoi.ToString();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string strMaHoSo = comboHoSo_CongNo.Text.Trim();
            string strNgayThang;
            string sqtChuyenDoiNgayThang;
            if (dtpThoiGian_CongNo.Checked)
            {
                DateTime dtpTG_CN = dtpThoiGian_CongNo.Value;
                sqtChuyenDoiNgayThang = dtpTG_CN.ToString("yyyy-MM-dd");
            }
            else
            {
                sqtChuyenDoiNgayThang = DateTime.Now.ToString("yyyy-MM-dd");
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

                StringBuilder sqlBuilder = new StringBuilder("UPDATE CongNo SET ");
                List<string> setClauses = new List<string>();

                if (!string.IsNullOrEmpty(strNoDau))
                {
                    setClauses.Add("NoDau = @NoDau");
                }
                if (!string.IsNullOrEmpty(strPhatSinh))
                {
                    setClauses.Add("PhatSinh = @PhatSinh");
                }
                if (!string.IsNullOrEmpty(strNoDaTra))
                {
                    setClauses.Add("SoDuCuoi = @SoDuCuoi");
                }
                if (!string.IsNullOrEmpty(strNoCuoi))
                {
                    setClauses.Add("NoCuoi = @NoCuoi");
                }
                if (!string.IsNullOrEmpty(strGhiChu))
                {
                    setClauses.Add("GhiChu = @GhiChu");
                }
                if (dtpThoiGian_CongNo.Checked)
                {
                    setClauses.Add("ThoiGian = @ThoiGian");
                }

                if (setClauses.Count > 0)
                {
                    sqlBuilder.Append(string.Join(", ", setClauses));
                    sqlBuilder.Append(" WHERE MaHoSo = @MaHoSo");

                    string sqlstr = sqlBuilder.ToString();

                    using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=dbcuoiky;Uid=root;Pwd=123456;"))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(sqlstr, conn))
                        {
                            if (!string.IsNullOrEmpty(strNoDau))
                            {
                                cmd.Parameters.AddWithValue("@NoDau", strNoDau);
                            }
                            if (!string.IsNullOrEmpty(strPhatSinh))
                            {
                                cmd.Parameters.AddWithValue("@PhatSinh", strPhatSinh);
                            }
                            if (!string.IsNullOrEmpty(strNoDaTra))
                            {
                                cmd.Parameters.AddWithValue("@SoDuCuoi", strNoDaTra);
                            }
                            if (!string.IsNullOrEmpty(strNoCuoi))
                            {
                                cmd.Parameters.AddWithValue("@NoCuoi", strNoCuoi);
                            }
                            if (!string.IsNullOrEmpty(strGhiChu))
                            {
                                cmd.Parameters.AddWithValue("@GhiChu", strGhiChu);
                            }
                            if (dtpThoiGian_CongNo.Checked)
                            {
                                cmd.Parameters.AddWithValue("@ThoiGian", sqtChuyenDoiNgayThang);
                            }
                            cmd.Parameters.AddWithValue("@MaHoSo", strMaHoSo);

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
    }
}