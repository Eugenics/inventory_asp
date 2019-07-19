using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace inventory_dot_core.Classes.Paging
{
    public class PaginatedList<T> : List<T>
    {
        /*
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public string SortOder { get; private set; }

        public PaginatedList(List<T> items, string sortOder, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            SortOder = sortOder;

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get => PageIndex > 1;
        }

        public bool HasNextPage
        {
            get => PageIndex < TotalPages;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
        */
    }
}
