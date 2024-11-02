using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    /// <summary>
    /// 计费模式
    /// </summary>
    [Table("base_fee_model")]
    public class FeeMode
    {
        [Key]
        [Column("fee_model_id")]
        public int FeeModelId { get; set; }
        [Column("fee_model_name")]
        public string FeeModelName { get; set; }
    }
}
