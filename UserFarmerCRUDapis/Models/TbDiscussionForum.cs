using System;
using System.Collections.Generic;

namespace UserFarmerCRUDapis.Models;

public partial class TbDiscussionForum
{
    public string ForumId { get; set; } = null!;

    public string FarmerId { get; set; } = null!;

    public string ForumName { get; set; } = null!;

    public DateTime InsertedDate { get; set; }

    public string InsertedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public virtual TbFarmer Farmer { get; set; } = null!;

    public virtual ICollection<TbForumPost> TbForumPosts { get; set; } = new List<TbForumPost>();
}
