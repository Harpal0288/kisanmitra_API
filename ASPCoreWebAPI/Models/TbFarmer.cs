using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbFarmer
{
    public string FarmerId { get; set; } = null!;

    public string? UserId { get; set; }

    public string FarmSize { get; set; } = null!;

    public string FarmLocation { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public string? IrrigationMethod { get; set; }

    public string? SoilType { get; set; }

    public int? FarmingExperience { get; set; }

    public string MembershipStatus { get; set; } = null!;

    public DateTime? MembershipExpiry { get; set; }

    public string LanguagePreference { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<TbDiscussionForum> TbDiscussionForums { get; set; } = new List<TbDiscussionForum>();

    public virtual ICollection<TbFarmerCrop> TbFarmerCrops { get; set; } = new List<TbFarmerCrop>();

    public virtual ICollection<TbFarmerEquipment> TbFarmerEquipments { get; set; } = new List<TbFarmerEquipment>();

    public virtual ICollection<TbFarmerLibraryResource> TbFarmerLibraryResources { get; set; } = new List<TbFarmerLibraryResource>();

    public virtual ICollection<TbForumPost> TbForumPosts { get; set; } = new List<TbForumPost>();

    public virtual ICollection<TbQuery> TbQueries { get; set; } = new List<TbQuery>();

    public virtual TbUser? User { get; set; }
}
