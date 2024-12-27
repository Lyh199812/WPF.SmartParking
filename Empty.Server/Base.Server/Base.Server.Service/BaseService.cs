using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Base.Server.EFCore;
using Base.Server.IService;

namespace Base.Server.Service
{
    public class BaseService : IBaseService
    {
        public DbContext dbContext { get; set; }

        public BaseService(IDbContext idbConfig)
        {
            dbContext = idbConfig.GetDbContext();
        }
        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public void Delete<T>(int Id) where T : class
        {
            T t = this.Find<T>(Id);//也可以附加
            if (t == null) throw new Exception("t is null");
            this.dbContext.Set<T>().Remove(t);
            this.Commit();
        }

        public void Delete<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");
            this.dbContext.Set<T>().Attach(t);
            this.dbContext.Set<T>().Remove(t);
            this.Commit();
        }

        public void Delete<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.dbContext.Set<T>().Attach(t);
            }
            this.dbContext.Set<T>().RemoveRange(tList);
            this.Commit();
        }

        public T Find<T>(int id) where T : class
        {
            return this.dbContext.Set<T>().Find(id);
        }

        public T Insert<T>(T t) where T : class
        {
            this.dbContext.Set<T>().Add(t);
            this.Commit();
            return t;
        }

        public IEnumerable<T> Insert<T>(IEnumerable<T> tList) where T : class
        {
            this.dbContext.Set<T>().AddRange(tList);
            this.Commit();//写在这里  就不需要单独commit  不写就需要 
            return tList;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return this.dbContext.Set<T>().AsNoTracking().Where<T>(funcWhere);
        }

        public void Update<T>(T t) where T : class
        {
            if (t == null) throw new Exception("t is null");

            this.dbContext.Set<T>().Attach(t);//将数据附加到上下文，支持实体修改和新实体，重置为UnChanged
            this.dbContext.Entry<T>(t).State = EntityState.Modified;
            this.Commit();
            //this.dbContext.Entry<T>(t).State = EntityState.Detached;
        }

        public void Update<T>(IEnumerable<T> tList) where T : class
        {
            foreach (var t in tList)
            {
                this.dbContext.Set<T>().Attach(t);
                this.dbContext.Entry<T>(t).State = EntityState.Modified;
            }
            this.Commit();
        }

        public virtual void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }
    }
}
