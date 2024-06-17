using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbFarmerEquipment
{
    public string EquipmentId { get; set; } = null!;

    public string FarmerId { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbEquipment Equipment { get; set; } = null!;

    public virtual TbFarmer Farmer { get; set; } = null!;
}
