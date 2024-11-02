﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Base.Server.Models
{
    [Table("system_users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "user_id")]
        public int UserId { get; set; }
        [Column(name: "user_name")]
        public string UserName { get; set; }
        [Column(name: "password")]
        public string Password { get; set; }
        [Column(name: "user_icon")]
        public string UserIcon { get; set; }
        [Column("real_name")]
        public string RealName { get; set; }
        [Column(name: "age")]
        public int Age { get; set; }

        [DefaultValue(1)]
        public int state { get; set; }
        [Column("view_type")]
        public int ViewType { get; set; }
        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        public List<MenuModel> Menus { get; set; }
        [NotMapped]
        public List<RoleInfo> Roles { get; set; }
    }
}