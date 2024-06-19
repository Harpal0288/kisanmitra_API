using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class VwGetFarmerHavingEquipment
{
    public string FarmerName { get; set; } = null!;

    public string EquipmentName { get; set; } = null!;

    public string EquipmentType { get; set; } = null!;

    public string? EquipmentDescription { get; set; }

    public int RentPrice { get; set; }

    public byte AvailabiltyStatus { get; set; }

    public int Quantity { get; set; }
}
