using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Common.Paging;
public class PagedList<T>
{

    public int TotalCount { get; private set; }
    public List<T> Items { get; set; }
    public PagedList(List<T> items, int count)
    {
        TotalCount = count;
        Items = items;
    }
    public async static Task<PagedList<T>> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count =await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count);
        
    }
}
