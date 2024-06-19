using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbAdmin
{
    public string AdminId { get; set; } = null!;

    public string? UserId { get; set; }

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbUser? User { get; set; }
}
