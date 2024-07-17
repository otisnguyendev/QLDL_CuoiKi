using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace QuanLyDaiLy.Error
{
    public class Error_ChangePass
    {
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        public void Exception_MatKhau(string str, MySqlCommand commn)
        {
            if (str.Length > 30)
            {
                MessageBox.Show("Mật Khẩu Chỉ Cho Phép Tối Đa 30 Kí Tự",strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }
    }
}
