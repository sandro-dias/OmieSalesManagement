﻿using Domain.Entities;

namespace Domain.Shared
{
    public class PagedResult<T> where T : EntityBase
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
