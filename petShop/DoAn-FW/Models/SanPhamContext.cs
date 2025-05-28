using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient; // Cho SqlParameter của SQL Server thông thường
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace DoAn_FW.Models
{
    public class SanPhamContext
    {
        public string ConnectionString { get; set; }
        public SanPhamContext()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public SanPhamContext(string cs)
        {
            ConnectionString = cs;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }
        //Tin Tức
        public List<object> ListSPMoiNhat()
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT TOP 7 * FROM THONGTINSP ORDER BY MATTSP DESC";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaTTSP = int.Parse(reader["MaTTSP"].ToString()),
                            MaLoaiSP = int.Parse(reader["MaLoaiSP"].ToString()),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaTH = int.Parse(reader["MaTH"].ToString()),
                            Gia = long.Parse(reader["Gia"].ToString()),
                            GiaKM = long.Parse(reader["GiaKM"].ToString()),
                            SoLuong = reader["SoLuong"] != DBNull.Value ? int.Parse(reader["SoLuong"].ToString()) : 0,
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? float.Parse(reader["KhoiLuong"].ToString()) : 0,
                            DoTuoi = reader["DoTuoi"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            KichThuoc = reader["KichThuoc"].ToString(),
                            ThanhPhan = reader["ThanhPhan"].ToString(),
                            CongDung = reader["CongDung"].ToString(),
                            HuongDanSD = reader["HuongDanSD"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }


        public List<object> ListLoaiSP()
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open(); ;
                var sql = " SELECT * FROM LOAISP";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaLoaiSP = int.Parse(reader["MaLoaiSP"].ToString()),
                            TenLoaiSP = reader["TenLoaiSP"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<object> ListHang()
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open(); ;
                var sql = "SELECT * FROM THUONGHIEU";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaTH = int.Parse(reader["MaTH"].ToString()),
                            TenTH = reader["TenTH"].ToString(),
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<object> ListMauSac()
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM THONGTINSP";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mauSacValue = reader["MauSac"].ToString();

                        // Kiểm tra giá trị MauSac đã có trong list hay chưa
                        if (!string.IsNullOrEmpty(mauSacValue) && !list.Any(item => ((dynamic)item).MauSac == mauSacValue))
                        {
                            list.Add(new
                            {
                                MauSac = mauSacValue
                            });
                        }
                    }
                }
                conn.Close();
            }
            return list.OrderBy(item => ((dynamic)item).MauSac).ToList();
        }


        public List<object> ListDoTuoi()
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM THONGTINSP";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var doTuoiValue = reader["DoTuoi"].ToString();

                        // Kiểm tra giá trị DoTuoi đã có trong list hay chưa
                        if (!string.IsNullOrEmpty(doTuoiValue) && !list.Any(item => ((dynamic)item).DoTuoi == doTuoiValue))
                        {
                            list.Add(new
                            {
                                DoTuoi = doTuoiValue
                            });
                        }
                    }
                }
                conn.Close();
            }
            return list.OrderBy(item => ((dynamic)item).DoTuoi).ToList();
        }


        public List<object> ListXuatXu()
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM THONGTINSP";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var xuatXuValue = reader["XuatXu"].ToString();

                        // Kiểm tra giá trị XuatXu đã có trong list hay chưa
                        if (!string.IsNullOrEmpty(xuatXuValue) && !list.Any(item => ((dynamic)item).XuatXu == xuatXuValue))
                        {
                            list.Add(new
                            {
                                XuatXu = xuatXuValue
                            });
                        }
                    }
                }
                conn.Close();
            }
            return list.OrderBy(item => ((dynamic)item).XuatXu).ToList();
        }


        public List<object> FilterSanPham(int math, int currentProductId)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                // Update the SQL query to filter by MaTH
                var sql = "SELECT * FROM THONGTINSP WHERE MaTH = @Math AND MaTTSP != @CurrentProductId";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTH", math); // Add the parameter for filtering
                cmd.Parameters.AddWithValue("@CurrentProductId", currentProductId); // Exclude current product

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Convert.ToInt32(reader["MaLoaiSP"]),
                            MaTH = Convert.ToInt32(reader["MaTH"]),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            GiaKM = Convert.ToInt32(reader["GiaKM"]),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = Convert.ToSingle(reader["KhoiLuong"]),
                            DoTuoi = reader["DoTuoi"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            KichThuoc = reader["KichThuoc"].ToString(),
                            ThanhPhan = reader["ThanhPhan"].ToString(),
                            CongDung = reader["CongDung"].ToString(),
                            HuongDanSD = reader["HuongDanSD"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }


        public SanPhamViewModel ChiTietSP(int mattsp)
        {
            SanPhamViewModel sanpham = null;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM THONGTINSP t, LOAISP l, THUONGHIEU th WHERE t.MaLoaiSP = l.MaLoaiSP AND t.MaTH = th.MaTH AND t.MaTTSP = @mattsp";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mattsp", mattsp);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sanpham = new SanPhamViewModel
                        {
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            TenSP = reader["TenSP"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            MaLoaiSP = Convert.ToInt32(reader["MaLoaiSP"]),
                            MaTH = Convert.ToInt32(reader["MaTH"]),
                            Gia = Convert.ToInt32(reader["Gia"]),
                            GiaKM = Convert.ToInt32(reader["GiaKM"]),
                            SoLuong = Convert.ToInt32(reader["SoLuong"]),
                            MauSac = reader["MauSac"].ToString(),
                            KhoiLuong = Convert.ToSingle(reader["KhoiLuong"]),
                            DoTuoi = reader["DoTuoi"].ToString(),
                            XuatXu = reader["XuatXu"].ToString(),
                            KichThuoc = reader["KichThuoc"].ToString(),
                            ThanhPhan = reader["ThanhPhan"].ToString(),
                            CongDung = reader["CongDung"].ToString(),
                            HuongDanSD = reader["HuongDanSD"].ToString(),
                            TenTH = reader["TenTH"].ToString(),
                            TenLoaiSP = reader["TenLoaiSP"].ToString()
                        };
                    }
                }
                conn.Close();
            }
            return sanpham;
        }


        public string BuildFilterQuery(out List<Microsoft.Data.SqlClient.SqlParameter> parameters, List<string> MaLoaiSP, List<string> MauSac, List<string> DoTuoi, List<string> XuatXu, List<string> MaTH, string search, string sortOrder)
        {
            parameters = new List<Microsoft.Data.SqlClient.SqlParameter>();
            var query = new StringBuilder("SELECT * FROM thongtinsp WHERE 1=1");

            AppendFilter(query, parameters, "MaLoaiSP", MaLoaiSP);
            AppendFilter(query, parameters, "MauSac", MauSac);
            AppendFilter(query, parameters, "DoTuoi", DoTuoi);
            AppendFilter(query, parameters, "XuatXu", XuatXu);
            AppendFilter(query, parameters, "MaTH", MaTH);
            AppendFilterSearch(query, parameters, search);

            switch (sortOrder)
            {
                case "price_asc":
                    query.Append(" ORDER BY Gia ASC");
                    break;
                case "price_desc":
                    query.Append(" ORDER BY Gia DESC");
                    break;
                case "name_asc":
                    query.Append(" ORDER BY TenSP ASC");
                    break;
                case "name_desc":
                    query.Append(" ORDER BY TenSP DESC");
                    break;
                default:
                    break;
            }

            return query.ToString();
        }

        public void AppendFilter(StringBuilder query, List<Microsoft.Data.SqlClient.SqlParameter> parameters, string columnName, List<string> filterValues)
        {
            if (filterValues != null && filterValues.Count > 0)
            {
                query.Append($" AND {columnName} IN ({string.Join(", ", filterValues.Select((x, i) => $"@{columnName}{i}"))})");
                parameters.AddRange(filterValues.Select((x, i) => new Microsoft.Data.SqlClient.SqlParameter($"@{columnName}{i}", x)));
            }
        }

        public void AppendFilterXuatXu(StringBuilder query, List<Microsoft.Data.SqlClient.SqlParameter> parameters, List<string> filterOptions)
        {
            if (filterOptions != null && filterOptions.Count > 0)
            {
                query.Append($" AND XuatXu IN ({string.Join(", ", filterOptions.Select((x, i) => $"@XuatXu{i}"))})");
                parameters.AddRange(filterOptions.Select((x, i) => new Microsoft.Data.SqlClient.SqlParameter($"@XuatXu{i}", x)));
            }
        }


        public void AppendFilterSearch(StringBuilder query, List<Microsoft.Data.SqlClient.SqlParameter> parameters, string filterInfor)
        {
            if (!string.IsNullOrEmpty(filterInfor))
            {
                query.Append(" AND (TenSP LIKE @SearchTerm OR MauSac LIKE @SearchTerm OR DoTuoi LIKE @SearchTerm OR XuatXu LIKE @SearchTerm)");
                parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@SearchTerm", "%" + filterInfor + "%"));
            }
        }


        public List<object> FetchFilteredProducts(string query, List<Microsoft.Data.SqlClient.SqlParameter> parameters)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (var command = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                MaTTSP = reader["MaTTSP"] != DBNull.Value ? int.Parse(reader["MaTTSP"].ToString()) : 0,
                                TenSP = reader["TenSP"].ToString(),
                                HinhAnh = reader["HinhAnh"].ToString(),
                                MaLoaiSP = reader["MaLoaiSP"] != DBNull.Value ? int.Parse(reader["MaLoaiSP"].ToString()) : 0,
                                MaTH = reader["MaTH"] != DBNull.Value ? int.Parse(reader["MaTH"].ToString()) : 0,
                                Gia = reader["Gia"] != DBNull.Value ? long.Parse(reader["Gia"].ToString()) : 0,
                                GiaKM = reader["GiaKM"] != DBNull.Value ? long.Parse(reader["GiaKM"].ToString()) : 0,
                                SoLuong = reader["SoLuong"] != DBNull.Value ? int.Parse(reader["SoLuong"].ToString()) : 0,
                                MauSac = reader["MauSac"].ToString(),
                                KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? float.Parse(reader["KhoiLuong"].ToString()) : 0,
                                DoTuoi = reader["DoTuoi"].ToString(),
                                XuatXu = reader["XuatXu"].ToString(),
                                KichThuoc = reader["KichThuoc"].ToString(),
                                ThanhPhan = reader["ThanhPhan"].ToString(),
                                CongDung = reader["CongDung"].ToString(),
                                HuongDanSD = reader["HuongDanSD"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }


        public List<object> BinhLuans(int mattsp)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"SELECT b.MaBL, b.MaTTSP, b.MaKH, b.NoiDung, k.TenKH 
            FROM BinhLuan b
            JOIN KhachHang k ON b.MaKH = k.MaKH
            WHERE b.MaTTSP = @mattsp";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("mattsp", mattsp);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaBL = Convert.ToInt32(reader["MaBL"]),
                            MaTTSP = Convert.ToInt32(reader["MaTTSP"]),
                            MaKH = Convert.ToInt32(reader["MaKH"]),
                            NoiDung = reader["NoiDung"]?.ToString(),
                            TenKH = reader["TenKH"]?.ToString(),
                        });
                    }
                }
            }
            return list;
        }



        public List<object> ListHang(int maloaisp)
        {
            List<object> list = new List<object>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = @"SELECT th.MaTH, th.TenTH, COUNT(*) AS SL 
                   FROM thuonghieu th 
                   JOIN thongtinsp t ON th.MaTH = t.MaTH 
                   WHERE t.MaLoaiSP = @maloaisp 
                   GROUP BY th.MaTH, th.TenTH";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("maloaisp", maloaisp);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaTH = reader["MaTH"] != DBNull.Value ? Convert.ToInt32(reader["MaTH"]) : 0,
                            TenTH = reader["TenTH"]?.ToString() ?? string.Empty,
                            SL = reader["SL"] != DBNull.Value ? Convert.ToInt32(reader["SL"]) : 0
                        });
                    }
                }
            }
            return list;
        }

        public List<object> FilterSanPham(int? maloaisp)
        {
            List<object> list = new List<object>();

            if (!maloaisp.HasValue)
                return list;

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var sql = "SELECT * FROM THONGTINSP WHERE MaLoaiSP = @maloaisp ORDER BY MaTTSP DESC";

                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("maloaisp", maloaisp.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                MaTTSP = reader["MaTTSP"] != DBNull.Value ? Convert.ToInt32(reader["MaTTSP"]) : 0,
                                TenSP = reader["TenSP"]?.ToString() ?? string.Empty,
                                HinhAnh = reader["HinhAnh"]?.ToString() ?? string.Empty,
                                MaLoaiSP = reader["MaLoaiSP"] != DBNull.Value ? Convert.ToInt32(reader["MaLoaiSP"]) : 0,
                                MaTH = reader["MaTH"] != DBNull.Value ? Convert.ToInt32(reader["MaTH"]) : 0,
                                Gia = reader["Gia"] != DBNull.Value ? Convert.ToInt32(reader["Gia"]) : 0,
                                GiaKM = reader["GiaKM"] != DBNull.Value ? Convert.ToInt32(reader["GiaKM"]) : 0,
                                SoLuong = reader["SoLuong"] != DBNull.Value ? Convert.ToInt32(reader["SoLuong"]) : 0,
                                MauSac = reader["MauSac"]?.ToString() ?? string.Empty,
                                KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? Convert.ToSingle(reader["KhoiLuong"]) : 0,
                                DoTuoi = reader["DoTuoi"]?.ToString() ?? string.Empty,
                                XuatXu = reader["XuatXu"]?.ToString() ?? string.Empty,
                                KichThuoc = reader["KichThuoc"]?.ToString() ?? string.Empty,
                                ThanhPhan = reader["ThanhPhan"]?.ToString() ?? string.Empty,
                                CongDung = reader["CongDung"]?.ToString() ?? string.Empty,
                                HuongDanSD = reader["HuongDanSD"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ
                Console.WriteLine($"Lỗi khi lọc sản phẩm: {ex.Message}");
                return list;
            }
        }


        public List<object> SPLocTheoHangSP(int maloaisp, int? math)
        {
            List<object> list = new List<object>();

            if (!math.HasValue)
                return list;

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var sql = "SELECT * FROM THONGTINSP WHERE MaTH = @math AND MaLoaiSP = @maloaisp ORDER BY MaTTSP DESC";

                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("math", math.Value);
                    cmd.Parameters.AddWithValue("maloaisp", maloaisp);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                MaTTSP = reader["MaTTSP"] != DBNull.Value ? Convert.ToInt32(reader["MaTTSP"]) : 0,
                                MaLoaiSP = reader["MaLoaiSP"] != DBNull.Value ? Convert.ToInt32(reader["MaLoaiSP"]) : 0,
                                MaTH = reader["MaTH"] != DBNull.Value ? Convert.ToInt32(reader["MaTH"]) : 0,
                                TenSP = reader["TenSP"]?.ToString() ?? string.Empty,
                                HinhAnh = reader["HinhAnh"]?.ToString() ?? string.Empty,
                                SoLuong = reader["SoLuong"] != DBNull.Value ? Convert.ToInt32(reader["SoLuong"]) : 0,
                                MauSac = reader["MauSac"]?.ToString() ?? string.Empty,
                                Gia = reader["Gia"] != DBNull.Value ? Convert.ToInt64(reader["Gia"]) : 0,
                                GiaKM = reader["GiaKM"] != DBNull.Value ? Convert.ToInt64(reader["GiaKM"]) : 0,
                                KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? Convert.ToSingle(reader["KhoiLuong"]) : 0,
                                DoTuoi = reader["DoTuoi"]?.ToString() ?? string.Empty,
                                XuatXu = reader["XuatXu"]?.ToString() ?? string.Empty,
                                KichThuoc = reader["KichThuoc"]?.ToString() ?? string.Empty,
                                ThanhPhan = reader["ThanhPhan"]?.ToString() ?? string.Empty,
                                CongDung = reader["CongDung"]?.ToString() ?? string.Empty,
                                HuongDanSD = reader["HuongDanSD"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ
                Console.WriteLine($"Lỗi khi lọc sản phẩm theo hãng: {ex.Message}");
                return list;
            }
        }

        public List<object> SearchPK(string? search)
        {
            List<object> list = new List<object>();

            if (string.IsNullOrEmpty(search))
                return list;

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    var sql = "SELECT * FROM THONGTINSP WHERE TenSP LIKE @search AND MaLoaiSP = 3 ORDER BY MaTTSP DESC";

                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                MaTTSP = reader["MaTTSP"] != DBNull.Value ? Convert.ToInt32(reader["MaTTSP"]) : 0,
                                TenSP = reader["TenSP"]?.ToString() ?? string.Empty,
                                HinhAnh = reader["HinhAnh"]?.ToString() ?? string.Empty,
                                MaLoaiSP = reader["MaLoaiSP"] != DBNull.Value ? Convert.ToInt32(reader["MaLoaiSP"]) : 0,
                                MaTH = reader["MaTH"] != DBNull.Value ? Convert.ToInt32(reader["MaTH"]) : 0,
                                Gia = reader["Gia"] != DBNull.Value ? Convert.ToInt64(reader["Gia"]) : 0,
                                GiaKM = reader["GiaKM"] != DBNull.Value ? Convert.ToInt64(reader["GiaKM"]) : 0,
                                SoLuong = reader["SoLuong"] != DBNull.Value ? Convert.ToInt32(reader["SoLuong"]) : 0,
                                MauSac = reader["MauSac"]?.ToString() ?? string.Empty,
                                KhoiLuong = reader["KhoiLuong"] != DBNull.Value ? Convert.ToSingle(reader["KhoiLuong"]) : 0,
                                DoTuoi = reader["DoTuoi"]?.ToString() ?? string.Empty,
                                XuatXu = reader["XuatXu"]?.ToString() ?? string.Empty,
                                KichThuoc = reader["KichThuoc"]?.ToString() ?? string.Empty,
                                ThanhPhan = reader["ThanhPhan"]?.ToString() ?? string.Empty,
                                CongDung = reader["CongDung"]?.ToString() ?? string.Empty,
                                HuongDanSD = reader["HuongDanSD"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý ngoại lệ
                Console.WriteLine($"Lỗi khi tìm kiếm sản phẩm: {ex.Message}");
                return list;
            }
        }

    }
}