using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class VwGetUsersDataWithRoleOfAdmin
{
    public string Name { get; set; } = null!;

    public string AadhaarNumber { get; set; } = null!;

    public string Role { get; set; } = null!;
}
