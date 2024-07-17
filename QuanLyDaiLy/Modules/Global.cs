using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Modules
{
    public class Global
    {
        ConnectionString sqlConn = new ConnectionString();
        StringMessage strMess = new StringMessage();

        bool flag = false;

        // Hàm kiểm tra dữ liệu có trong Database hay không
        public bool Test_Database(string strChuoiDieuKien, string strTableName, MySqlConnection sqlCNN)
        {
            bool bolReturn = false;
            string strSQL = $"SELECT * FROM {strTableName.Trim()} WHERE {strChuoiDieuKien}";
            MySqlCommand sqlCmd = new MySqlCommand(strSQL, sqlCNN);
            MySqlDataReader sqlReader = sqlCmd.ExecuteReader();

            if (sqlReader.HasRows)
                bolReturn = true; // Tồn tại mẫu tin == True            

            sqlReader.Dispose();
            sqlReader.Close();
            return bolReturn;
        }

        // Hàm thực hiện câu lệnh SQL
        public void SQL_Database(string str, MySqlConnection conn)
        {
            if (MessageBox.Show(strMess.ThucHienThaoTac, strMess.TieuDe_Message, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                MySqlCommand commn = new MySqlCommand(str, conn);
                commn.ExecuteNonQuery();
                MessageBox.Show(strMess.ThaoTacThanhCong, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                flag = true;
            }
            else
            {
                MessageBox.Show(strMess.HuyThaoTac, strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SQL_Database_NoMessage(string str, MySqlConnection conn)
        {
            MySqlCommand commn = new MySqlCommand(str, conn);
            commn.ExecuteNonQuery();
        }

        // Lấy Datagrid
        public void Seach_DataGrid(string sqlstr, MySqlConnection conn, DataGrid dataGW)
        {
            if (MessageBox.Show("Bạn có muốn thực hiện thao tác này", "Quản Lý Đại Lý", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqlstr, conn);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet);
                dataGW.DataSource = dSet.Tables[0];
            }
            else
            {
                MessageBox.Show("Bạn Đã Huỷ Thao Tác", "Quản Lý Đại Lý", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Datagrid
        public DataGridTableStyle MyTableStyleCreate()
        {
            DataGridTableStyle MyStyle = new DataGridTableStyle
            {
                RowHeadersVisible = false,
                SelectionForeColor = Color.SlateGray,
                AlternatingBackColor = Color.Ivory,
                PreferredRowHeight = 10,
                RowHeaderWidth = 10,
                AllowSorting = false
            };
            return MyStyle;
        }

        public void LoadDataInToDatagrid(string str, MySqlConnection sqlCNN, DataGrid dgrid)
        {
            try
            {
                MySqlDataAdapter sqlADP = new MySqlDataAdapter(str, sqlCNN);
                DataTable dttable = new DataTable();
                sqlADP.Fill(dttable);
                dttable.DefaultView.AllowEdit = false;
                dttable.DefaultView.AllowNew = false;
                dttable.DefaultView.AllowDelete = false;
                dgrid.DataSource = dttable;
            }
            catch
            {
                MessageBox.Show("Ngày Tháng Của Bạn Nhập Vào Chưa Chính Xác", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Kiểm tra có phải kí tự là 1 số
        public bool Test_Number(string str)
        {
            int kt = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (((str[i].ToString()[0] >= '0') && str[i].ToString()[0] <= '9') || str[i].ToString()[0] == '.')
                {
                    kt = 0;
                }
                else
                {
                    kt = kt + 1;
                    break;
                }
            }
            return kt == 0;
        }

        // Hàm chuyển ngày tháng năm thành năm tháng ngày
        public string Return_Time_ThangNgay(string str)
        {
            int a = str.IndexOf('/', 0);
            int b = str.IndexOf('/', a + 1);
            string strNgay = str.Substring(0, a + 1);
            string strThang = str.Substring(a + 1, b - a);
            string strNam = str.Substring(b + 1, 4);
            return strThang + strNgay + strNam;
        }
    }
}
