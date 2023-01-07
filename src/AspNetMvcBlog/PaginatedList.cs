using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace AspNetMvcBlog
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IList<T> items, int pageIndex, int pageSize)
        {
            int count = items.Count;
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
        
        public PaginatedList(IQueryable<T> items, int pageIndex, int pageSize)
        {
            int count = items.Count();
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }
        
        public PaginatedList(IEnumerable<T> items, int pageIndex, int pageSize)
        {
            int count = items.Count();
            this.PageIndex = pageIndex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, pageIndex, pageSize);
        }

    }
    public static class PaginatedListExtension
    {
        public static PaginatedList<T> ToPaginetedList<T>(this IList<T> source, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(source, pageIndex, pageSize);
        }

        public static PaginatedList<T> ToPaginetedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(source, pageIndex, pageSize);
        }

        public static PaginatedList<T> ToPaginetedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PaginatedList<T>(source, pageIndex, pageSize);
        }
    }
}