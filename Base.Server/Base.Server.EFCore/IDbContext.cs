using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.EFCore
{
    public interface IDbContext
    {
        DbContext GetDbContext();
    }

}
