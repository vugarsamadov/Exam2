using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business.Pagination
{
    public class GenericPaginatedEntity<T>
    {
        public GenericPaginatedEntity(IEnumerable<T> data, int currentPage, int perPage, int rowCount)
        {
            Data = data;
            CurrentPage = currentPage;
            PerPage = perPage;
            RowCount = rowCount;
        }

        public IEnumerable<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int RowCount { get; set; }

        public int PageCount { get => (int)Math.Ceiling(RowCount * 1.0 / PerPage); }
        public bool HasNext { get => CurrentPage < PageCount; }
        public bool HasPrev { get => CurrentPage > 1; }

        public string? Next { get; set; }
        public string? Prev { get; set; }
        public string? Current { get; set; }
       
    }
}
