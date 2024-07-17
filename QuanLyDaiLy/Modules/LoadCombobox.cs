using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Modules
{
    public class LoadCombobox
    {
        public void Load_LoaiDaiLy(string sqlstr, string display, string value, ComboBox combobox, MySqlConnection conn)
        {
            combobox.Items.Clear();
            // sqlstr = "SELECT MaLoaiDaiLy, TenLoaiDaiLy FROM LoaiDaiLy ORDER BY MaLoaiDaiLy";
            MySqlDataAdapter sqlAdp = new MySqlDataAdapter(sqlstr, conn);
            DataTable dtLoad = new DataTable();
            sqlAdp.Fill(dtLoad);
            if ((dtLoad != null) && (dtLoad.Rows.Count > 0))
            {
                combobox.DataSource = dtLoad;
                combobox.DisplayMember = display;
                // combobox.DisplayMember = "TenLoaiDaiLy";
                combobox.ValueMember = value;
            }
            dtLoad = null;
            sqlAdp.Dispose();
        }
    }
}
