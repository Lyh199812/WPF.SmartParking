using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    public class Result<T>
    {
        public int State { get; set; } = 200;
        public string ExceptionMessage { get; set; }
        public T Data { get; set; }
    }
}
