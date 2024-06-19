using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbEquipment
{
    public string EquipmentId { get; set; } = null!;

    public string EquipmentName { get; set; } = null!;

    public string EquipmentType { get; set; } = null!;

    public string? EquipmentDescription { get; set; }

    public int RentPrice { get; set; }

    public byte AvailabilityStatus { get; set; }

    public int Quantity { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual ICollection<TbFarmerEquipment> TbFarmerEquipments { get; set; } = new List<TbFarmerEquipment>();

    public virtual ICollection<TbRental> TbRentals { get; set; } = new List<TbRental>();
}
