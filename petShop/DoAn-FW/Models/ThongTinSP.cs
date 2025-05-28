using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class ThongTinSP
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

        public virtual int MaTTSP
        {
            get { return maTTSP; }
            set { maTTSP = value; }
        }
        public virtual string TenSP
        {
            get { return tenSP; }
            set { tenSP = value; }
        }
        public virtual string HinhAnh
        {
            get { return hinhAnh; }
            set { hinhAnh = value; }
        }
        public virtual int MaLoaiSP
        {
            get { return maLoaiSP; }
            set { maLoaiSP = value; }
        }
        public virtual int MaTH
        {
            get { return maTH; }
            set { maTH = value; }
        }
        public virtual int Gia
        {
            get { return gia; }
            set { gia = value; }
        }
        public virtual int GiaKM
        {
            get { return giaKM; }
            set { giaKM = value; }
        }
        public virtual int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        public virtual string? MauSac
        {
            get { return mauSac; }
            set { mauSac = value; }
        }
        public virtual float KhoiLuong
        {
            get { return khoiLuong; }
            set { khoiLuong = value; }
        }
        public virtual string? DoTuoi
        {
            get { return doTuoi; }
            set { doTuoi = value; }
        }
        public virtual string XuatXu
        {
            get { return xuatXu; }
            set { xuatXu = value; }
        }
        public virtual string KichThuoc
        {
            get { return kichThuoc; }
            set { kichThuoc = value; }
        }
        public virtual string ThanhPhan
        {
            get { return thanhPhan; }
            set { thanhPhan = value; }
        }
        public virtual string CongDung
        {
            get { return congDung; }
            set { congDung = value; }
        }
        public virtual string HuongDanSD
        {
            get { return huongDanSD; }
            set { huongDanSD = value; }
        }
    }
}
