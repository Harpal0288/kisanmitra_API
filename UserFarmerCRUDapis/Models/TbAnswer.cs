using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbAnswer
{
    public string AnswerId { get; set; } = null!;

    public string? QueryId { get; set; }

    public string AnswerText { get; set; } = null!;

    public string? ConsultantId { get; set; }

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime TimeStamp { get; set; }

    public virtual TbConsultant? Consultant { get; set; }

    public virtual TbQuery? Query { get; set; }
}
