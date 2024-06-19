using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbConsultantCertification
{
    public string? ConsultantId { get; set; }

    public string CertificationNumber { get; set; } = null!;

    public string InsertedBy { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual TbConsultant? Consultant { get; set; }
}
