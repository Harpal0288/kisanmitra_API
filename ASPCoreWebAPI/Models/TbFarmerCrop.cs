using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbFarmerCrop
{
    public string FarmarId { get; set; } = null!;

    public string Crop { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbFarmer Farmar { get; set; } = null!;
}
