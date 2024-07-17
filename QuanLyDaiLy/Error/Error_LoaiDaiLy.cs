using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Error
{
    public class Error_LoaiDaiLy
    {
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        public void Exception_MaLoaiDL(string str, MySqlCommand commn,MySqlConnection sqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Loại Đại Lý Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Loại Đại Lý Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaLoaiDaiLy='" + str + "'";
            string strTableName = "LoaiDaiLy";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN))
            {
                MessageBox.Show("Mã Loại Đại Lý Này Đã Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_TenLoaiDL(string str, MySqlCommand commn)
        {
            if (str.Length==0 || str.Length > 50)
            {
                MessageBox.Show("Tên Đại Lý Không Được Rỗng Hoặc Lớn Hơn 50 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_GhiChu(string str, MySqlCommand commn)
        {
            if (str.Length > 200)
            {
                MessageBox.Show("Ghi Chú Của Bạn không Được Dài Quá 200 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_MaLoaiDL_CN_Cu(string str, MySqlCommand commn, MySqlConnection sqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Loại Đại Lý Cũ Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Loại Đại Lý Cũ Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaLoaiDaiLy='" + str + "'";
            string strTableName = "LoaiDaiLy";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN)==false)
            {
                MessageBox.Show("Mã Loại Đại Lý Cũ Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }
        public void Exception_MaLoaiDL_CN_Moi(string str, MySqlCommand commn, MySqlConnection sqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Loại Đại Lý Mới Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Loại Đại Lý Mới Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaLoaiDaiLy='" + str + "'";
            string strTableName = "LoaiDaiLy";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN))
            {
                MessageBox.Show("Mã Loại Đại Lý Mới Này Đã Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }
    }
}
