using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbTempQuery
{
    public string QueryId { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string FarmerId { get; set; } = null!;

    public string QueryTitle { get; set; } = null!;

    public string? QueryDescription { get; set; }

    public string InsertedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public string TimeStamp { get; set; } = null!;
}
