using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Error
{
    public class Error_DoanhSo
    {
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        public void Exception_MaHoSo(string str, MySqlCommand commn, MySqlConnection sqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Hồ Sơ Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Hồ Sơ Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaHoSo='" + str + "'";
            string strTableName = "DoanhSo";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN))
            {
                MessageBox.Show("Mã Hồ Sơ Này Đã Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_TienNo(string str, MySqlCommand commn)
        {
            if (global.Test_Number(str) == false)
            {
                MessageBox.Show("Số Tiền Của Bạn Nhập Vào Phải Là Một Số", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 30)
            {
                MessageBox.Show("Số Tiền Của Bạn Nhập Vào Phải Nhỏ Hơn 30 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }
    }
}
