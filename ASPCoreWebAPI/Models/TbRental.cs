using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbRental
{
    public string RentalId { get; set; } = null!;

    public string EquipmentId { get; set; } = null!;

    public DateOnly RentalStartDate { get; set; }

    public DateOnly RentalEndDate { get; set; }

    public int TotalCost { get; set; }

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbEquipment Equipment { get; set; } = null!;
}
