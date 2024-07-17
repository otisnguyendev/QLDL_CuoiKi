using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace QuanLyDaiLy.Error
{
    public class Error_ChiTietXuatHang
    {
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        Modules.Global global = new QuanLyDaiLy.Modules.Global();

        public void Exception_MaPhieuXuat(string str, MySqlCommand commn, MySqlConnection sqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Phiếu Xuất Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Phiếu Xuất Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaPhieuXuat='" + str + "'";
            string strTableName = "ChiTietXuatHang";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN))
            {
                MessageBox.Show("Mã Phiếu Xuất Này Đã Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_SoLuong(string str, MySqlCommand commn)
        {
            if (global.Test_Number(str) == false)
            {
                MessageBox.Show("Số Lượng Của Bạn Nhập Vào Phải Là Một Số", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 30)
            {
                MessageBox.Show("Số Lượng Của Bạn Nhập Vào Phải Nhỏ Hơn 9 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_DonGia(string str, MySqlCommand commn)
        {
            if (str.Length > 30)
            {
                MessageBox.Show("Đơn Giá Của Bạn không Được Dài Quá 30 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_DonViTinh(string str, MySqlCommand commn)
        {
            if (str.Length > 20)
            {
                MessageBox.Show("Đơn Vị Tính Của Bạn không Được Dài Quá 20 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
        }

        public void Exception_ThanhTien(string str, MySqlCommand commn)
        {
            if (str.Length > 30)
            {
                MessageBox.Show("Số Tiền Của Bạn không Được Dài Quá 30 Kí Tự", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void Exception_MaPhieuXuat_CN(string str, MySqlCommand commn, MySqlConnection sqlCNN)
        {
            int indexMaLoaiDL = str.IndexOf(' ');
            if (indexMaLoaiDL > -1)
            {
                MessageBox.Show("Mã Phiếu Xuất Không Được Có Khoảng Trắng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            if (str.Length > 10 || str.Length == 0)
            {
                MessageBox.Show("Mã Phiếu Xuất Không Được Lớn Hơn 10 Kí Tự Hoặc Rỗng", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                commn.ExecuteNonQuery();
            }
            string strChuoiDieuKien = "MaPhieuXuat='" + str + "'";
            string strTableName = "ChiTietXuatHang";
            if (global.Test_Database(strChuoiDieuKien, strTableName, sqlCNN)==false)
            {
                MessageBox.Show("Mã Phiếu Xuất Này Không Tồn Tại", strMess.TieuDe_Message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                commn.ExecuteNonQuery();
            }
        }
    }
}
