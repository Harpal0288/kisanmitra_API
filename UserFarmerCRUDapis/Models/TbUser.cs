using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbUser
{
    public string? UserId { get; set; } = null!;

    public string? UserName { get; set; } = null!;

    public string? AadharNumber { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    //public string? Status { get; set; }

    public string? Password { get; set; } = null!;

    public string? RoleId { get; set; } = null!;

    public DateTime? InsertedDate { get; set; }

    public string? InsertedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; } = null!;

    public virtual TbRole? Role { get; set; } = null!;

    public virtual ICollection<TbAdmin> TbAdmins { get; set; } = new List<TbAdmin>();

    public virtual ICollection<TbConsultant> TbConsultants { get; set; } = new List<TbConsultant>();

    public virtual ICollection<TbFarmer> TbFarmers { get; set; } = new List<TbFarmer>();

    public virtual ICollection<TbPayment> TbPayments { get; set; } = new List<TbPayment>();

    public virtual ICollection<TbRating> TbRatings { get; set; } = new List<TbRating>();
}
