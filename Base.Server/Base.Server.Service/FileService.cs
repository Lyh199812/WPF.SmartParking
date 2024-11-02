using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.EFCore;

using Base.Server.IService;

namespace Base.Server.Service
{
    public class FileService : BaseService, IFileService
    {
        public FileService(IDbContext dbConfig) : base(dbConfig)
        {
        }
    }
}
