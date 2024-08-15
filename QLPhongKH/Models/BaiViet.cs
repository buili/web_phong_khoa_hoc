using System;
using System.Collections.Generic;

namespace QLPhongKH.Models;

public partial class BaiViet
{
    public string? TenBaiViet { get; set; }

    public int? LoaiBaiViet { get; set; }

    public string? NoiDung { get; set; }

    public string? TacGia { get; set; }

    public DateOnly? NgayDang { get; set; }

    public string? HinhAnh { get; set; }

    public string? TomTat { get; set; }

    public int MaBaiViet { get; set; }
}
