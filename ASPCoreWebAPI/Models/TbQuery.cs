using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbQuery
{
    public string QueryId { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string FarmerId { get; set; } = null!;

    public string QueryTitle { get; set; } = null!;

    public string? QueryDescription { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public DateTime? TimeStamp { get; set; }

    public virtual TbCategory? Category { get; set; }

    public virtual TbFarmer Farmer { get; set; } = null!;

    public virtual ICollection<TbAnswer> TbAnswers { get; set; } = new List<TbAnswer>();

    public virtual ICollection<TbRating> TbRatings { get; set; } = new List<TbRating>();
}
