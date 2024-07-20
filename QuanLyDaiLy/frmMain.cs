using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace QuanLyDaiLy
{
    public partial class frmMain : Form
    {
        Modules.StringMessage strMess = new QuanLyDaiLy.Modules.StringMessage();
        public static string strTenDangNhap;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            PhanQuyen_Khack();
        }
        public void PhanQuyen_Khack()
        {
            Logout.Enabled = false;
            ChangePass.Enabled = false;
            ThayDoiQuyDinh.Enabled = false;
            AnThanhCongCu.Enabled = false;
            HoSoDaiLy.Enabled = false;
            CongNoDaiLy.Enabled = false;
            DoanhSoDaiLy.Enabled = false;
            PhieuThuTien.Enabled = false;
            PhieuXuatHang.Enabled = false;
        }
        public void PhanQuyen_DaiLy()
        {
            Logout.Enabled = true;
            ChangePass.Enabled = true;
            ThayDoiQuyDinh.Enabled = false;
            AnThanhCongCu.Enabled = true;
            HoSoDaiLy.Enabled = false;
            CongNoDaiLy.Enabled = false;
            DoanhSoDaiLy.Enabled = false;
            PhieuThuTien.Enabled = false;
            PhieuXuatHang.Enabled = false;
        }
        public void PhanQuyen_Administrator()
        {
            Logout.Enabled = true;
            ChangePass.Enabled = true;
            ThayDoiQuyDinh.Enabled = true;
            AnThanhCongCu.Enabled = true;
            HoSoDaiLy.Enabled = true;
            CongNoDaiLy.Enabled = true;
            DoanhSoDaiLy.Enabled = true;
            PhieuThuTien.Enabled = true;
            PhieuXuatHang.Enabled = true;
        }
        #region : ShowForm
        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin lg = new frmLogin();
            lg.ShowDialog();
            if (lg.GetNumberAccount() == 0)
            {
                this.PhanQuyen_DaiLy();
                //toolStripTaiKhoan.Text = lg.GetstrDangNhap();
                //strTenDangNhap = toolStripTaiKhoan.Text;
            }
            else if (lg.GetNumberAccount() == 1)
            {
                this.PhanQuyen_Administrator();
                //toolStripTaiKhoan.Text = lg.GetstrDangNhap();
                //strTenDangNhap = toolStripTaiKhoan.Text;
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePass cp = new frmChangePass();
            cp.MdiParent = this;
            cp.Show();
        }

        private void hồSơĐạiLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoSo hs = new frmHoSo();
            hs.MdiParent = this;
            hs.Show();
        }

        private void côngNợĐạiLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCongNo cn = new frmCongNo();
            cn.MdiParent = this;
            cn.Show();
        }

        private void doanhSốĐạiLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoanhSo ds = new frmDoanhSo();
            ds.MdiParent = this;
            ds.Show();
        }

        private void phiếuThuTiềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPhieuThuTien ptt = new frmPhieuThuTien();
            ptt.MdiParent = this;
            ptt.Show();
        }

        private void phiếuXuấtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPhieuXuatHang pxh = new frmPhieuXuatHang();
            pxh.MdiParent = this;
            pxh.Show();
        }

        private void chiTiếtXuấtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChiTietXuatHang ctxh = new frmChiTietXuatHang();
            ctxh.MdiParent = this;
            ctxh.Show();
        }

        private void chiTiếtMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMatHang mh = new frmMatHang();
            mh.MdiParent = this;
            mh.Show();
        }

        private void cậpNhậpLoạiĐạiLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoaiDaiLy ldl = new frmLoaiDaiLy();
            ldl.MdiParent = this;
            ldl.Show();
        }
        #endregion

        private void Logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn Có Muốn Thoát Khỏi Tài Khoản Này", strMess.TieuDe_Message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.PhanQuyen_Khack();
            }
            
        }

        public void toolStripTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void thôngTinSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTinSP tt = new frmThongTinSP();
            tt.MdiParent = this;
            tt.Show();
        }

        private void quyChếMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuyDinhMatHang q = new frmQuyDinhMatHang();
            q.MdiParent = this;
            q.Show();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}