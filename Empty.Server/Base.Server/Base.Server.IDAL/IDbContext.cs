using Microsoft.EntityFrameworkCore;
using System;


namespace Base.Server.IDAL
{
    public interface IDbContext
    {
        DbContext GetDbContext();
    }
}
