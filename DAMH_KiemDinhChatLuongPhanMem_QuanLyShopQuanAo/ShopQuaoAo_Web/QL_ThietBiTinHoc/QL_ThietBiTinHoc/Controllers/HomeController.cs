using QL_ThietBiTinHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;
using PagedList.Mvc;
namespace QL_ThietBiTinHoc.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        ShopQADataContext db = new ShopQADataContext();

        public ActionResult Index()
        {
            List<SANPHAM> lst = db.SANPHAMs.ToList();
            return View(lst);
        }
        public ActionResult DanhSachThietBi()//int ? page
        {
            List<SANPHAM> lst = db.SANPHAMs.OrderBy(s => s.DONGIA).ToList();
            return View(lst);
        }

        public ActionResult ChiTietThietBi(string ma)
        {
            List<SANPHAM> lst = db.SANPHAMs.ToList();
            SANPHAM tb = lst.FirstOrDefault(t => t.MASP == ma);
            return View(tb);
        }
        public ActionResult DanhMucLoai()
        {
            List<LOAI> lst = db.LOAIs.ToList();
            return PartialView(lst);
        }

        public ActionResult DanhMucNhaSanXuat()
        {
            List<KHACHHANG> lst = db.KHACHHANGs.ToList();
            return PartialView(lst);
        }

        public ActionResult HienThiThietBiTheoLoai(string ma)
        {
            List<SANPHAM> lst = db.SANPHAMs.ToList();
            List<SANPHAM> lst2 = lst.Where(t => t.MALOAI == ma).ToList();
            return View("DanhSachThietBi", lst2);
        }


        public ActionResult GioiThieu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TimKiem(FormCollection fc)
        {
            var tukhoa = fc["timkiem"];
            List<SANPHAM> lst = db.SANPHAMs.ToList();
            List<SANPHAM> lst2 = lst.Where(t => t.TENSP.Contains(tukhoa) == true).ToList();
            return View("DanhSachThietBi", lst2);
        }

        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult LienHe()
        {
            return View();
        }
    }
}
