using QL_ThietBiTinHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_ThietBiTinHoc.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        XuLy xl = new XuLy();
        ShopQADataContext db = new ShopQADataContext();

        public ActionResult Index()
        {
            return View();
        }

        private int Tongsoluong()
        {
            int tsl = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tsl = lst.Sum(n => n.sSoLuong);
            }
            return tsl;
        }
        private double Tongtien()
        {
            double tt = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tt = lst.Sum(t => t.sThanhTien);
            }
            return tt;
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstTB = Session["GioHang"] as List<GioHang>;
            if (lstTB == null)
            {
                lstTB = new List<GioHang>();
                Session["GioHang"] = lstTB;
            }
            return lstTB;
        }

        public ActionResult XemGioHang()
        {
            List<GioHang> lstTB = LayGioHang();
            ViewBag.Tongsoluong = Tongsoluong();
            ViewBag.Tongtien = Tongtien();
            return View(lstTB);

        }

        public ActionResult ThemGioHang(string ma)
        {

            List<GioHang> lstTB = LayGioHang();
            GioHang it = lstTB.FirstOrDefault(t => t.MASP == ma);

            if (it == null)
            {
                GioHang a = new GioHang(ma);
                lstTB.Add(a);
            }
            else
            {
                it.sSoLuong++;
            }
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaMatHang(string ma)
        {
            List<GioHang> lstTB = LayGioHang();
            GioHang a = lstTB.FirstOrDefault(t => t.MASP== ma);
            if (a != null)
            {
                lstTB.Remove(a);
            }
            return RedirectToAction("XemGioHang");
        }

        public ActionResult GiamGioHang(string ma)
        {

            List<GioHang> lstTB = LayGioHang();
            
            GioHang it = lstTB.FirstOrDefault(t => t.MASP == ma);

            if (it.sSoLuong == 1)
            {
                return RedirectToAction("XemGioHang");
            }
            else
            {
                it.sSoLuong--;
            }
            return RedirectToAction("XemGioHang");
        }

        public ActionResult GioHangPartial()
        {
            if (Tongsoluong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = Tongsoluong();
            ViewBag.TongTien = Tongtien();
            return PartialView();
        }

        public ActionResult XoaGioHang()
        {
            List<GioHang> lstTB = LayGioHang();
            lstTB.Clear();
            return RedirectToAction("XemGioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            List<GioHang> lstTB = LayGioHang();
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangKyVaDangNhap", "KhachHang");
            }
            if (Tongsoluong() == 0)
            {
                return RedirectToAction("DanhSachThietBi", "Home");
            }
            ViewBag.TongSoLuong = Tongsoluong();
            ViewBag.TongTien = Tongtien();
            return View(lstTB);
        }

        public ActionResult DatHang(FormCollection fc)
        {
            List<GioHang> lstTB = LayGioHang();
            
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangKyVaDangNhap", "KhachHang");
            }
            if (Tongsoluong() == 0)
            {
                return RedirectToAction("DanhSachThietBi", "Home");
            }
            HOADON ddh = new HOADON();
            ddh.MAHD = xl.Them_Ma_HD();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            ddh.MAKH = kh.MAKH;
            ddh.NGAYBD = DateTime.Now;
            ddh.TRANGTHAI = "Xác Nhận Đơn Hàng";
            ddh.TONGTIEN =  (int)Tongtien()-(((int)(Tongtien()*0.1)));
            ddh.DIACHI = kh.DIACHI;
            ddh.GIAMGIA = "10%";
            db.HOADONs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            foreach (var item in lstTB)
            {
                CT_HOADON ctdh = new CT_HOADON();
                ctdh.MAHD = ddh.MAHD;
                ctdh.MASP = item.MASP;
                ctdh.SOLUONG = item.sSoLuong;
                ctdh.DONGIA = item.sGiaBan;
                ctdh.GIAMGIA = "10%";
                ctdh.THANHTIEN = ddh.TONGTIEN;      
                db.CT_HOADONs.InsertOnSubmit(ctdh);
                db.SubmitChanges();
            }
            
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }

        public ActionResult HistoryBuy()
        {
            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            List<HOADON> lst = db.HOADONs.Where(h=>h.MAKH==kh.MAKH).ToList();
            return View(lst);
        }
    }
}
