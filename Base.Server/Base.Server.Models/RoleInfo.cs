using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    [Table("roles")]
    public class RoleInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("role_name")]
        public string RoleName { get; set; }
        [DefaultValue(1)]
        public int state { get; set; }


        // 不向数据库映射
        [NotMapped]
        public List<int> UserIds { get; set; }
        [NotMapped]
        public List<int> MenuIds { get; set; }
    }
}
