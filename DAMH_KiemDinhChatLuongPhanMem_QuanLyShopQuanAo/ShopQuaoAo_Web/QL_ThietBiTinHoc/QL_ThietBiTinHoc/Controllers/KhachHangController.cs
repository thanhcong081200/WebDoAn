using QL_ThietBiTinHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_ThietBiTinHoc.Controllers
{
    public class KhachHangController : Controller
    {
        //
        // GET: /KhachHang/

        XuLy xl = new XuLy();

        public ActionResult DangKyVaDangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection fc)
        {

            string ma = xl.Them_Ma_KH();
            if(xl.themKH(ma, fc["hoten"], fc["dienthoai"], fc["diachi"], fc["gioitinh"], fc["matkhaudangky"]))
            {
                ViewData["Loi"] = "Đăng Ký Thành Công";
            }
            else
                ViewData["Loi"] = "Đăng Ký  Không Thành Công";

            return View("DangKyVaDangNhap");
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection fc)
        {
            var tendn = fc["taikhoandangnhap"];
            var matkhau = fc["matkhaudangnhap"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi"] = "Phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi"] = "Phải nhập mật khẩu";
            }
            
            else
            {
                KHACHHANG kh = xl.dangnhap(tendn, matkhau);
                if (kh != null)
                {
                    Session["Taikhoan"] = kh;
                    if (Session["GioHang"] != null)
                    {
                        return RedirectToAction("XemGioHang", "GioHang");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewData["Loi"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View("DangKyVaDangNhap");
                }
            }
            return View("DangKyVaDangNhap");
        }

        public ActionResult DangXuat()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
