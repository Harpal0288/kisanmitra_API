using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class VwGetConsultantsHaveAnswerToQueriesWithMultipleCategory
{
    public string ConsultantName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string QueryTitle { get; set; } = null!;

    public string AnswerText { get; set; } = null!;
}
