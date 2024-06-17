using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbCategory
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string InsertedBy { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual ICollection<TbQuery> TbQueries { get; set; } = new List<TbQuery>();
}
