using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PagedList;

namespace DoAn_FW.Models
{
    public class TinTucContext
    {
        public string ConnectionString { get; set; }
        public TinTucContext()
        {
            ConnectionString = "Data Source=localhost;Initial Catalog=website_petShop;Integrated Security=True";
        }
        public TinTucContext(string cs)
        {
            ConnectionString = cs;
        }
        private Microsoft.Data.SqlClient.SqlConnection GetConnection()
        {
            return new Microsoft.Data.SqlClient.SqlConnection(ConnectionString);
        }

        //Tin Tức
        public List<TinTuc> ListAllTinTuc()
        {
            List<TinTuc> list = new List<TinTuc>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM TINTUC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TinTuc()
                        {
                            MaTinTuc = Convert.ToInt32(reader["MaTinTuc"]),
                            HinhBia = reader["HinhBia"].ToString(),
                            TieuDe = reader["TieuDe"].ToString(),
                            NoiDung = reader["NoiDung"].ToString(),
                            Link = reader["Link"].ToString(),
                            TrangThai = Convert.ToInt32(reader["TrangThai"])
                        });
                    }
                }
            }
            return list;
        }

        public IEnumerable<TinTuc> ListPagingTinTuc(int page, int pageSize)
        {
            List<TinTuc> list = new List<TinTuc>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM TINTUC ORDER BY MaTinTuc DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TinTuc()
                        {
                            MaTinTuc = Convert.ToInt32(reader["MaTinTuc"]),
                            HinhBia = reader["HinhBia"].ToString(),
                            TieuDe = reader["TieuDe"].ToString(),
                            NoiDung = reader["NoiDung"].ToString(),
                            Link = reader["Link"].ToString(),
                            TrangThai = Convert.ToInt32(reader["TrangThai"])
                        });
                    }
                }
            }
            return list.ToPagedList(page, pageSize);
        }

        public TinTuc GetTinTucById(int id)
        {
            TinTuc tinTuc = null;
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM TINTUC WHERE MaTinTuc = @id";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tinTuc = new TinTuc()
                        {
                            MaTinTuc = Convert.ToInt32(reader["MaTinTuc"]),
                            HinhBia = reader["HinhBia"].ToString(),
                            TieuDe = reader["TieuDe"].ToString(),
                            NoiDung = reader["NoiDung"].ToString(),
                            Link = reader["Link"].ToString(),
                            TrangThai = Convert.ToInt32(reader["TrangThai"])
                        };
                    }
                }
            }
            return tinTuc;
        }

        public int ThemTinTuc(TinTuc tinTuc)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "INSERT INTO TINTUC(HinhBia, TieuDe, NoiDung, Link, TrangThai) " +
                          "VALUES(@HinhBia, @TieuDe, @NoiDung, @Link, @TrangThai); SELECT SCOPE_IDENTITY();";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HinhBia", tinTuc.HinhBia);
                cmd.Parameters.AddWithValue("@TieuDe", tinTuc.TieuDe);
                cmd.Parameters.AddWithValue("@NoiDung", tinTuc.NoiDung);
                cmd.Parameters.AddWithValue("@Link", tinTuc.Link);
                cmd.Parameters.AddWithValue("@TrangThai", tinTuc.TrangThai);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int CapNhatTinTuc(TinTuc tinTuc)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE TINTUC SET HinhBia = @HinhBia, TieuDe = @TieuDe, " +
                          "NoiDung = @NoiDung, Link = @Link, TrangThai = @TrangThai " +
                          "WHERE MaTinTuc = @MaTinTuc";

                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTinTuc", tinTuc.MaTinTuc);
                cmd.Parameters.AddWithValue("@HinhBia", tinTuc.HinhBia);
                cmd.Parameters.AddWithValue("@TieuDe", tinTuc.TieuDe);
                cmd.Parameters.AddWithValue("@NoiDung", tinTuc.NoiDung);
                cmd.Parameters.AddWithValue("@Link", tinTuc.Link);
                cmd.Parameters.AddWithValue("@TrangThai", tinTuc.TrangThai);

                return cmd.ExecuteNonQuery();
            }
        }

        public int XoaTinTuc(int maTinTuc)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "DELETE FROM TINTUC WHERE MaTinTuc = @MaTinTuc";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTinTuc", maTinTuc);

                return cmd.ExecuteNonQuery();
            }
        }

        public int CapNhatTrangThai(int maTinTuc, int trangThai)
        {
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "UPDATE TINTUC SET TrangThai = @TrangThai WHERE MaTinTuc = @MaTinTuc";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaTinTuc", maTinTuc);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                return cmd.ExecuteNonQuery();
            }
        }

        public List<TinTuc> TimKiemTinTuc(string tuKhoa)
        {
            List<TinTuc> list = new List<TinTuc>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM TINTUC WHERE TieuDe LIKE @TuKhoa OR NoiDung LIKE @TuKhoa";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TinTuc()
                        {
                            MaTinTuc = Convert.ToInt32(reader["MaTinTuc"]),
                            HinhBia = reader["HinhBia"].ToString(),
                            TieuDe = reader["TieuDe"].ToString(),
                            NoiDung = reader["NoiDung"].ToString(),
                            Link = reader["Link"].ToString(),
                            TrangThai = Convert.ToInt32(reader["TrangThai"])
                        });
                    }
                }
            }
            return list;
        }

        public List<TinTuc> GetTinTucByTrangThai(int trangThai)
        {
            List<TinTuc> list = new List<TinTuc>();
            using (Microsoft.Data.SqlClient.SqlConnection conn = GetConnection())
            {
                conn.Open();
                var sql = "SELECT * FROM TINTUC WHERE TrangThai = @TrangThai ORDER BY MaTinTuc DESC";
                Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TinTuc()
                        {
                            MaTinTuc = Convert.ToInt32(reader["MaTinTuc"]),
                            HinhBia = reader["HinhBia"].ToString(),
                            TieuDe = reader["TieuDe"].ToString(),
                            NoiDung = reader["NoiDung"].ToString(),
                            Link = reader["Link"].ToString(),
                            TrangThai = Convert.ToInt32(reader["TrangThai"])
                        });
                    }
                }
            }
            return list;
        }
    }
}
