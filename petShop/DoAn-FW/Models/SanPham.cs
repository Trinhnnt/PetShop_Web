using DoAn_FW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DoAn_FW.Models
{
    public class SanPham
    {
        private int maTTSP;
        private string tenSP;
        private string hinhAnh;
        private int maLoaiSP;
        private int maTH;
        private int gia;
        private int giaKM;
        private int soLuong;
        private string mauSac;
        private float khoiLuong;
        private string doTuoi;
        private string xuatXu;
        private string kichThuoc;
        private string thanhPhan;
        private string congDung;
        private string huongDanSD;

        public int MaTTSP
        {
            get { return maTTSP; }
            set { maTTSP = value; }
        }
        public string TenSP
        {
            get { return tenSP; }
            set { tenSP = value; }
        }
        public string HinhAnh
        {
            get { return hinhAnh; }
            set { hinhAnh = value; }
        }
        public int MaLoaiSP
        {
            get { return maLoaiSP; }
            set { maLoaiSP = value; }
        }
        public int MaTH
        {
            get { return maTH; }
            set { maTH = value; }
        }
        public int Gia
        {
            get { return gia; }
            set { gia = value; }
        }
        public int GiaKM
        {
            get { return giaKM; }
            set { giaKM = value; }
        }
        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        public string MauSac
        {
            get { return mauSac; }
            set { mauSac = value; }
        }
        public float KhoiLuong
        {
            get { return khoiLuong; }
            set { khoiLuong = value; }
        }
        public string DoTuoi
        {
            get { return doTuoi; }
            set { doTuoi = value; }
        }
        public string XuatXu
        {
            get { return xuatXu; }
            set { xuatXu = value; }
        }
        public string KichThuoc
        {
            get { return kichThuoc; }
            set { kichThuoc = value; }
        }
        public string ThanhPhan
        {
            get { return thanhPhan; }
            set { thanhPhan = value; }
        }
        public string CongDung
        {
            get { return congDung; }
            set { congDung = value; }
        }
        public string HuongDanSD
        {
            get { return huongDanSD; }
            set { huongDanSD = value; }
        }
        public string KhoiLuongFormatted => $"{KhoiLuong:0.##} kg";
    }
}
