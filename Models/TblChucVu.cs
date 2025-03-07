using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_ChucVu")]
public partial class TblChucVu
{
    [Key]
    [Column("PK_sMaCV")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaCv { get; set; } = null!;

    [Column("sTenCV")]
    [StringLength(50)]
    public string? STenCv { get; set; }

    [InverseProperty("FkSMaCvNavigation")]
    public virtual ICollection<TblNhanVien> TblNhanViens { get; set; } = new List<TblNhanVien>();
}
