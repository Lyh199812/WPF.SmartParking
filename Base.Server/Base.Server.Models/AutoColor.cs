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
    /// 车辆颜色
    /// </summary>
    [Table("base_license_color")]
    public class AutoColor
    {
        [Key]
        [Column("color_id")]
        public int ColorId { get; set; }
        [Column("color_name")]
        public string ColorName { get; set; }
    }
}
