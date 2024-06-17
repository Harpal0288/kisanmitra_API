using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbRole
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<TbUser> TbUsers { get; set; } = new List<TbUser>();
}
