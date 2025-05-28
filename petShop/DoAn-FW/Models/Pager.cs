using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_FW.Models
{
    public class Pager
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pager() { }

        public Pager(int totalItems, int page, int pageSize = 10, int maxPages = 10)
        {
            // Đảm bảo các tham số hợp lệ
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            // Đảm bảo trang hiện tại không vượt quá tổng số trang
            if (currentPage > totalPages)
                currentPage = totalPages;

            // Đảm bảo trang hiện tại không nhỏ hơn 1
            if (currentPage < 1)
                currentPage = 1;

            // Tính toán trang bắt đầu và trang kết thúc
            int startPage = currentPage - (maxPages / 2);
            int endPage = currentPage + (maxPages / 2) - 1;

            if (startPage < 1)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > maxPages)
                {
                    startPage = endPage - maxPages + 1;
                }
            }

            // Gán các thuộc tính
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
