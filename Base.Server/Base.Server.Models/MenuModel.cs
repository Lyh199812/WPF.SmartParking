using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    [Table("menus")]
    public class MenuModel
    {
        [Key]
        [Column("menu_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        [Column("menu_header")]
        public string MenuHeader { get; set; }
        [Column("target_view")]
        public string TargetView { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }

        // 字体图标
        // e618
        // XAML:  #&e618
        // C#  :  \ue618  ->   转换   64   A
        [Column("menu_icon")]
        public string MenuIcon { get; set; }
        [Column("index")]
        public int Index { get; set; }
        [Column("menu_type")]
        public int MenuType { get; set; }
        [Column("state")]
        [DefaultValue(1)]
        public int State { get; set; }
    }
}
