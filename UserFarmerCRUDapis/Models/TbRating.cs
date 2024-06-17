using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbRating
{
    public string RatingId { get; set; } = null!;

    public string QueryId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int? RatingValue { get; set; }

    public string? RatingComment { get; set; }

    public virtual TbQuery Query { get; set; } = null!;

    public virtual TbUser User { get; set; } = null!;
}
