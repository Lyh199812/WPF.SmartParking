using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Server.Models;

namespace Base.Server.IService
{
    public interface IBaseInfoService : IBaseService
    {
        void SaveAutoColor(AutoColor autoColor);
        void SaveLicenseColor(LicenseColor licenseColor);
        void SaveFeeMode(FeeMode feeMode);
    }
}
