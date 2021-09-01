using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_ThietBiTinHoc.Models
{
    public class GioHang
    {
        ShopQADataContext db = new ShopQADataContext();

        public string MASP { get; set; }

        public string TenSP { get; set; }

        public string sAnh { get; set; }

        public int sGiaBan { get; set; }

        public int sSoLuong { get; set; }
        public string SoLT { get; set; }
        public int sThanhTien
        {
            get { return sSoLuong * sGiaBan; }
        }

        public GioHang(string mtb)

        {  
            
            MASP = mtb;
            SANPHAM tb = db.SANPHAMs.FirstOrDefault(t => t.MASP == mtb);
            TenSP = tb.TENSP;
            sAnh = tb.HINHANH;
            sGiaBan = int.Parse(tb.DONGIA.ToString());
            sSoLuong = 1;
            SoLT = tb.TRANGTHAI;
        }
    }
}