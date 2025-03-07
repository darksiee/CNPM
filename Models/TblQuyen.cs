using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CNPM.Models;

[Table("tbl_Quyen")]
public partial class TblQuyen
{
    [Key]
    [Column("PK_sMaQuyen")]
    [StringLength(20)]
    [Unicode(false)]
    public string PkSMaQuyen { get; set; } = null!;

    [Column("sTenQuyen")]
    [StringLength(20)]
    public string? STenQuyen { get; set; }

    [InverseProperty("FkSMaQuyenNavigation")]
    public virtual ICollection<TblTaiKhoan> TblTaiKhoans { get; set; } = new List<TblTaiKhoan>();
}
