using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbPaymentGateway
{
    public string GatewayId { get; set; } = null!;

    public string GatewayName { get; set; } = null!;

    public string GatewayDetails { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
