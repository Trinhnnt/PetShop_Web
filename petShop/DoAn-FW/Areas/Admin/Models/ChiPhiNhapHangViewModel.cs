using System;

namespace DoAn_FW.Areas.Admin.Models
{
    public class ChiPhiNhapHangViewModel
    {
        public int MaPN { get; set; }
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public int TongTienTT { get; set; }
        public DateTime NgayLapPN { get; set; }
    }

}
