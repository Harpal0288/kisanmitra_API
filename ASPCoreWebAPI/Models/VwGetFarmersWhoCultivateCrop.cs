using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class VwGetFarmersWhoCultivateCrop
{
    public string FarmerName { get; set; } = null!;

    public string Crop { get; set; } = null!;

    public string FarmSize { get; set; } = null!;

    public string FarmLocation { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public string? IrrigationMethod { get; set; }

    public string? SoilType { get; set; }

    public int? FarmerExperience { get; set; }

    public string MembershipStatus { get; set; } = null!;

    public DateTime? MembershipExpiry { get; set; }

    public string LanguagePreference { get; set; } = null!;
}
