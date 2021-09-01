using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_ThietBiTinHoc.Models
{
    public class XuLy
    {
        ShopQADataContext data = new ShopQADataContext();
        public string Them_Ma_HD()
        {
            List<HOADON> lst = data.HOADONs.OrderByDescending(s => s.MAHD).ToList();
            int Ma;
            if (lst.Count != 0)
                Ma = int.Parse(lst[0].MAHD.Remove(0, 2));
            else
                Ma = 0;
            if (Ma + 1 < 10)
                return "HD00" + (Ma + 1);
            if (Ma + 1 < 100)
                return "HD0" + (Ma + 1);
            return "HD" + (Ma + 1);
        }
        public string Them_Ma_KH()
        {
            List<KHACHHANG> lst = data.KHACHHANGs.OrderByDescending(s => s.MAKH).ToList();
            int Ma;
            if (lst.Count != 0)
                Ma = int.Parse(lst[0].MAKH.Remove(0, 2));
            else
                Ma = 0;
            if (Ma + 1 < 10)
                return "KH00" + (Ma + 1);
            if (Ma + 1 < 100)
                return "KH0" + (Ma + 1);
            return "KH" + (Ma + 1);
        }
        public bool themKH(string _maKH, string _tenKH, string _SDt, string _diaChi, string _gioitinh, string _Pass)
        {
            try
            {
                KHACHHANG kh = new KHACHHANG();
                kh.MAKH = _maKH;
                kh.TENKH = _tenKH;
                kh.SDT = _SDt;
                kh.DIACHI = _diaChi;
                kh.GIOITINH = _gioitinh;
                kh.PASS = _Pass;
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
        public KHACHHANG dangnhap(string _sdt,string _pass)
        {
            return data.KHACHHANGs.FirstOrDefault(n => n.SDT == _sdt && n.PASS == _pass);
        }
    }
}