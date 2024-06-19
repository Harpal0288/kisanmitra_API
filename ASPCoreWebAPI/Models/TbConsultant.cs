using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbConsultant
{
    public string? UserId { get; set; }

    public string ConsultantId { get; set; } = null!;

    public string? Expertise { get; set; }

    public int? Experience { get; set; }

    public string SubscriptionStatus { get; set; } = null!;

    public DateTime? SubscriptionExpiry { get; set; }

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<TbAnswer> TbAnswers { get; set; } = new List<TbAnswer>();

    public virtual ICollection<TbConsultantCertification> TbConsultantCertifications { get; set; } = new List<TbConsultantCertification>();

    public virtual ICollection<TbConsultantLanguage> TbConsultantLanguages { get; set; } = new List<TbConsultantLanguage>();

    public virtual TbUser? User { get; set; }
}
