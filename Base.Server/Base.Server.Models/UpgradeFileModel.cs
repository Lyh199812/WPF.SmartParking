using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Server.Models
{
    [Table("upgrade_file")]
    public class UpgradeFileModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("file_id")]
        public int FileId { get; set; }
        [Column("file_name")]
        public string FileName { get; set; }
        [Column("file_md5")]
        public string FileMd5 { get; set; }
        [Column("file_path")]
        public string FilePath { get; set; }
        [Column("upload_time")]
        public DateTime UploadTime { get; set; }// "2022-01-24 20:00:00"
        [Column("state")]
        public int state { get; set; }
        [Column("length")]
        public int Length { get; set; }
    }
}
