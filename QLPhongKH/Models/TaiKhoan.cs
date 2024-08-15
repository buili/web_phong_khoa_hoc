using System;
using System.Collections.Generic;

namespace QLPhongKH.Models;

public partial class TaiKhoan
{
    public string? Ten { get; set; }

    public string Email { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public int Id { get; set; }
}
