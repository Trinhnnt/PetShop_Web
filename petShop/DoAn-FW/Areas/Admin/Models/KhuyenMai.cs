using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_projectframeword_admin.Models
{
    public class KhuyenMai
    {
        public KhuyenMai()
        {
        }

        public int MaKM { get; set; }
        public byte SoPTKM { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int? TTienToiThieu { get; set; }

        public KhuyenMai(int maKM, byte soPTKM, DateTime tuNgay, DateTime denNgay, int tTienToiThieu)
        {
            MaKM = maKM;
            SoPTKM = soPTKM;
            TuNgay = tuNgay;
            DenNgay = denNgay;
            TTienToiThieu = tTienToiThieu;
        }
    }
}
