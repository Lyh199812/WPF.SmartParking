﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    [Table("role_menu")]
    public class RoleMenu
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("menu_id")]
        public int MenuId { get; set; }
    }
}
