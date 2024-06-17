using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbPayment
{
    public string PaymentId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int Amount { get; set; }

    public string GatewayId { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbUser User { get; set; } = null!;
}
