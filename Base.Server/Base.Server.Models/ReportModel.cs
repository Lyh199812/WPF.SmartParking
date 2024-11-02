using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    public class ReportModel
    {
        public DateTime LeaveTime { get; set; }// 日期
        public int OrderCount { get; set; }// 订单数
        public double Payable { get; set; }// 应付
        public double PaymentCash { get; set; }//现金
        public double PaymentElec { get; set; }//电子

    }
}
