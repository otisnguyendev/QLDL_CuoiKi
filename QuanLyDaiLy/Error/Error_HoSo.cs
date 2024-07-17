using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Error
{
    public class Error_HoSo
    {
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        public void Exception_MaHoSo(string str, MySqlCommand commn,MySqlConnection sqlCNN)
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
            string strTableName = "HoSo";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN))
            {
                MessageBox.Show("Mã Hồ Sơ Này Đã Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_TenDaiLy(string str, MySqlCommand commn)
        {

            if (str.Length > 100 || str.Length == 0)
            {
                MessageBox.Show("Tên Đại Lý Không Được Lớn Hơn 100 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_DiaChi(string str, MySqlCommand commn)
        {
            if (str.Length > 50)
            {
                MessageBox.Show("Địa Chỉ Không Được Lớn Hơn 50 Kí Tự ", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_DienThoai(string str, MySqlCommand commn)
        {
            if (str.Length > 30)
            {
                MessageBox.Show("Điện Thoại Không Được Lớn Hơn 30 Kí Tự ", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_DiaChiMail(string str, MySqlCommand commn)
        {
            int intIndexEmail = str.IndexOf('@');
            if (str.Length > 0 && intIndexEmail == -1)
            {
                MessageBox.Show("Email Của Bạn Phải Có Kí Tự @", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }

            if (str.Length > 50)
            {
                MessageBox.Show("Email Không Được Lớn Hơn 50 Kí Tự ", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void Exception_MaHoSo_CN(string str, MySqlCommand commn, MySqlConnection sqlCNN)
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
            string strTableName = "HoSo";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN)==false)
            {
                MessageBox.Show("Mã Hồ Sơ Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }
    }
}
