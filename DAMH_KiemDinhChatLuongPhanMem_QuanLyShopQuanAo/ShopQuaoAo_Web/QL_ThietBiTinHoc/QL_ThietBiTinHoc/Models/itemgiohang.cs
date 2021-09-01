using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_ThietBiTinHoc.Models
{
    public class itemgiohang
    {
        public string Masp { get; set; }
        public string tensp { get; set; }
        public string hinhanh { get; set; }
        public int soluong { get; set; }
        public int dongia { get; set; }
        public int thanhtien
        {

            get { return soluong * dongia; }
        }
        ShopQADataContext data = new ShopQADataContext();
        public itemgiohang(string ms)
        {
            SANPHAM sp = data.SANPHAMs.FirstOrDefault(s => s.MASP == ms);
            if (sp != null)
            {
                Masp = sp.MASP;
                hinhanh = sp.HINHANH;
                tensp = sp.TENSP;
                dongia = (int)sp.DONGIA;
                soluong = 1;
            }
        }
    }
}