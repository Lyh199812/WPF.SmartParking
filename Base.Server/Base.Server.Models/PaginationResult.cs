using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    public class PaginationResult<T> : Result<T>
    {
        public PageInfo PageInfo { get; set; }
    }

    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int RecordCount { get; set; }
    }
}
