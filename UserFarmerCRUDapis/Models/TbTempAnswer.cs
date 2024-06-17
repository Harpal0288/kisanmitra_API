using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbTempAnswer
{
    public string AnswerId { get; set; } = null!;

    public string? QueryId { get; set; }

    public string? ConsultantId { get; set; }

    public string? AnswerText { get; set; }

    public string InsertedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public string TimeStamp { get; set; } = null!;
}
