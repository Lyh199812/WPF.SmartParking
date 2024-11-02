using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    [Table("record_info")]
    public class RecordInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public int RecordId { get; set; }
        [Column("auto_license")]
        public string AutoLicense { get; set; }
        [Column("enter_time")]
        public DateTime EnterTime { get; set; }

        [Column("leave_time")]
        public DateTime LeaveTime { get; set; }
        [Column("order_id")]
        public long OrderId { get; set; }
        [Column("fee_mode_id")]
        public int FeeModelId { get; set; } = 0;
        [Column("state")]
        public int State { get; set; } = 0;


        [Column("payable")]
        public double Payable { get; set; }// 应付
        [Column("payment")]
        public double Payment { get; set; }// 实付

        [Column("discount")]
        public double Discount { get; set; }// 折扣

        [Column("pay_type")]
        public int PayType { get; set; }
    }
}
