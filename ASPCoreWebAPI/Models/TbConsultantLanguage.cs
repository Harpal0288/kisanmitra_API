using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbConsultantLanguage
{
    public string ConsultantId { get; set; } = null!;

    public string ConsultantLanguage { get; set; } = null!;

    public string InsertedBy { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual TbConsultant Consultant { get; set; } = null!;
}
