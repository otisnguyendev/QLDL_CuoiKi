using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace QuanLyDaiLy.Error
{
    public class Error_MatHang
    {

        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        public void Exception_MaMatHang(string str, MySqlCommand commn,MySqlConnection SqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Mặt Hàng Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Mặt Hàng  Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaMatHang='" + str + "'";
            string strTableName = "MatHang";
            if (global.Test_Database(strChuoiDieuKien, strTableName, SqlCNN))
            {
                MessageBox.Show("Mã Mặt Hàng  Này Đã Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_TenMatHang(string str, MySqlCommand commn)
        {
            if (str.Length > 50)
            {
                MessageBox.Show("Tên Mặt Hàng Không Được Lớn Hơn 50 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_DonViTinh(string str, MySqlCommand commn)
        {
            if (str.Length > 30)
            {
                MessageBox.Show("Đơn Vị Tính Không Được Lớn Hơn 30 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }
        public void Exception_DonGia(string str, MySqlCommand commn)
        {
            if (str.Length>30)
            {
                MessageBox.Show("Đơn Gia Không Được Lớn Hơn 30 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_GhiChu(string str, MySqlCommand commn)
        {
            if (str.Length > 200)
            {
                MessageBox.Show("Ghi Chú Không Được Lớn Hơn 200 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_MaMatHang_CapNhap(string str, MySqlCommand commn, MySqlConnection SqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Mặt Hàng Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Mặt Hàng  Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }

            string strChuoiDieuKien = "MaMatHang='" + str + "'";
            string strTableName = "MatHang";
            if (global.Test_Database(strChuoiDieuKien, strTableName, SqlCNN)==false)
            {
                MessageBox.Show("Mã Mặt Hàng  Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }
    }
}
