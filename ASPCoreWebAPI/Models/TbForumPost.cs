using System;
using System.Collections.Generic;

namespace ASPCoreWebAPI.Models;

public partial class TbForumPost
{
    public string PostId { get; set; } = null!;

    public string ForumId { get; set; } = null!;

    public string FarmerId { get; set; } = null!;

    public string PostText { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbFarmer Farmer { get; set; } = null!;

    public virtual TbDiscussionForum Forum { get; set; } = null!;
}
