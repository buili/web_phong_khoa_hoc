using System;
using System.Collections.Generic;

namespace QLPhongKH.Models;

public partial class LoaiBaiViet
{
    public string? TenLoai { get; set; }

    public int MaLoai { get; set; }

    public int? ParentId { get; set; }

}
