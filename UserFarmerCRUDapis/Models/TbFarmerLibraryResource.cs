using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbFarmerLibraryResource
{
    public string FarmerId { get; set; } = null!;

    public string FarmerResource { get; set; } = null!;

    public string InsertedBy { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual TbFarmer Farmer { get; set; } = null!;
}
