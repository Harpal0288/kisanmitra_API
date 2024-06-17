using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserFarmerCRUDapis.Models;

public partial class KisanmitraKisanContext : DbContext
{
    public KisanmitraKisanContext()
    {
    }

    public KisanmitraKisanContext(DbContextOptions<KisanmitraKisanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAdmin> TbAdmins { get; set; }

    public virtual DbSet<TbAnswer> TbAnswers { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbConsultant> TbConsultants { get; set; }

    public virtual DbSet<TbConsultantCertification> TbConsultantCertifications { get; set; }

    public virtual DbSet<TbConsultantLanguage> TbConsultantLanguages { get; set; }

    public virtual DbSet<TbDiscussionForum> TbDiscussionForums { get; set; }

    public virtual DbSet<TbEquipment> TbEquipments { get; set; }

    public virtual DbSet<TbFarmer> TbFarmers { get; set; }

    public virtual DbSet<TbFarmerCrop> TbFarmerCrops { get; set; }

    public virtual DbSet<TbFarmerEquipment> TbFarmerEquipments { get; set; }

    public virtual DbSet<TbFarmerLibraryResource> TbFarmerLibraryResources { get; set; }

    public virtual DbSet<TbForumPost> TbForumPosts { get; set; }

    public virtual DbSet<TbPayment> TbPayments { get; set; }

    public virtual DbSet<TbPaymentGateway> TbPaymentGateways { get; set; }

    public virtual DbSet<TbQuery> TbQueries { get; set; }

    public virtual DbSet<TbRating> TbRatings { get; set; }

    public virtual DbSet<TbRental> TbRentals { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbTempAnswer> TbTempAnswers { get; set; }

    public virtual DbSet<TbTempQuery> TbTempQueries { get; set; }

    public virtual DbSet<TbUser> TbUsers { get; set; }

    public virtual DbSet<VwGetConsultantsHaveAnswerToQueriesWithMultipleCategory> VwGetConsultantsHaveAnswerToQueriesWithMultipleCategories { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAdmin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__tb_Admin__43AA4141AED7FC14");

            entity.ToTable("tb_Admin");

            entity.Property(e => e.AdminId)
                .HasMaxLength(255)
                .HasColumnName("admin_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TbAdmins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tb_Admin__user_i__31EC6D26");
        });

        modelBuilder.Entity<TbAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__tb_Answe__337243184129FA66");

            entity.ToTable("tb_Answer");

            entity.Property(e => e.AnswerId)
                .HasMaxLength(255)
                .HasColumnName("answer_id");
            entity.Property(e => e.AnswerText)
                .HasMaxLength(1000)
                .HasColumnName("answer_text");
            entity.Property(e => e.ConsultantId)
                .HasMaxLength(255)
                .HasColumnName("consultant_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.QueryId)
                .HasMaxLength(255)
                .HasColumnName("query_id");
            entity.Property(e => e.TimeStamp)
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Consultant).WithMany(p => p.TbAnswers)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FK__tb_Answer__consu__6FE99F9F");

            entity.HasOne(d => d.Query).WithMany(p => p.TbAnswers)
                .HasForeignKey(d => d.QueryId)
                .HasConstraintName("FK__tb_Answer__query__6EF57B66");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tb_Categ__D54EE9B4AB68E74D");

            entity.ToTable("tb_Categories");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(255)
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TbConsultant>(entity =>
        {
            entity.HasKey(e => e.ConsultantId).HasName("PK__tb_Consu__680695C429C52BF8");

            entity.ToTable("tb_Consultants");

            entity.Property(e => e.ConsultantId)
                .HasMaxLength(255)
                .HasColumnName("consultant_id");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Expertise)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("expertise");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.SubscriptionExpiry)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("subscription_expiry");
            entity.Property(e => e.SubscriptionStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subscription_status");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TbConsultants)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tb_Consul__user___3E52440B");
        });

        modelBuilder.Entity<TbConsultantCertification>(entity =>
        {
            entity.HasKey(e => e.CertificationNumber).HasName("PK__tb_Consu__1B9BADCFB5DE8E3F");

            entity.ToTable("tb_ConsultantCertification");

            entity.Property(e => e.CertificationNumber)
                .HasMaxLength(255)
                .HasColumnName("certification_number");
            entity.Property(e => e.ConsultantId)
                .HasMaxLength(255)
                .HasColumnName("consultant_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Consultant).WithMany(p => p.TbConsultantCertifications)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FK__tb_Consul__consu__6A30C649");
        });

        modelBuilder.Entity<TbConsultantLanguage>(entity =>
        {
            entity.HasKey(e => new { e.ConsultantId, e.ConsultantLanguage }).HasName("PK__tb_Consu__1CDB456F20A417C8");

            entity.ToTable("tb_ConsultantLanguage");

            entity.Property(e => e.ConsultantId)
                .HasMaxLength(255)
                .HasColumnName("consultant_id");
            entity.Property(e => e.ConsultantLanguage)
                .HasMaxLength(50)
                .HasColumnName("consultant_language");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Consultant).WithMany(p => p.TbConsultantLanguages)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Consul__consu__656C112C");
        });

        modelBuilder.Entity<TbDiscussionForum>(entity =>
        {
            entity.HasKey(e => e.ForumId).HasName("PK__tb_Discu__69A2FA58D8A3C19D");

            entity.ToTable("tb_DiscussionForum");

            entity.Property(e => e.ForumId)
                .HasMaxLength(255)
                .HasColumnName("forum_id");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.ForumName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("forum_name");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TbDiscussionForums)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Discus__farme__74AE54BC");
        });

        modelBuilder.Entity<TbEquipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PK__tb_Equip__197068AF0D467F85");

            entity.ToTable("tb_Equipment");

            entity.Property(e => e.EquipmentId)
                .HasMaxLength(255)
                .HasColumnName("equipment_id");
            entity.Property(e => e.AvailabilityStatus).HasColumnName("availability_status");
            entity.Property(e => e.EquipmentDescription)
                .HasMaxLength(1000)
                .HasColumnName("equipment_description");
            entity.Property(e => e.EquipmentName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("equipment_name");
            entity.Property(e => e.EquipmentType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("equipment_type");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RentPrice).HasColumnName("rent_price");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TbFarmer>(entity =>
        {
            entity.HasKey(e => e.FarmerId).HasName("PK__tb_Farme__C615582529A2E6B3");

            entity.ToTable("tb_Farmers");

            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.FarmLocation)
                .HasMaxLength(255)
                .HasColumnName("farm_location");
            entity.Property(e => e.FarmSize)
                .HasMaxLength(250)
                .HasColumnName("farm_size");
            entity.Property(e => e.FarmingExperience)
                .HasDefaultValue(0)
                .HasColumnName("farming_experience");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.IrrigationMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("irrigation_method");
            entity.Property(e => e.LanguagePreference)
                .HasMaxLength(255)
                .HasColumnName("language_preference");
            entity.Property(e => e.MembershipExpiry)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("datetime")
                .HasColumnName("membership_expiry");
            entity.Property(e => e.MembershipStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("membership_status");
            entity.Property(e => e.PinCode)
                .HasMaxLength(255)
                .HasColumnName("pin_code");
            entity.Property(e => e.SoilType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("soil_type");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TbFarmers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tb_Farmer__user___38996AB5");
        });

        modelBuilder.Entity<TbFarmerCrop>(entity =>
        {
            entity.HasKey(e => new { e.FarmarId, e.Crop }).HasName("PK__tb_Farme__1B9206E9E8719B1C");

            entity.ToTable("tb_FarmerCrop");

            entity.Property(e => e.FarmarId)
                .HasMaxLength(255)
                .HasColumnName("farmar_id");
            entity.Property(e => e.Crop)
                .HasMaxLength(255)
                .HasColumnName("crop");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Farmar).WithMany(p => p.TbFarmerCrops)
                .HasForeignKey(d => d.FarmarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Farmer__updat__52593CB8");
        });

        modelBuilder.Entity<TbFarmerEquipment>(entity =>
        {
            entity.HasKey(e => new { e.EquipmentId, e.FarmerId }).HasName("PK__tb_Farme__55113D2DAF1F1003");

            entity.ToTable("tb_FarmerEquipment");

            entity.Property(e => e.EquipmentId)
                .HasMaxLength(255)
                .HasColumnName("equipment_id");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Equipment).WithMany(p => p.TbFarmerEquipments)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Farmer__updat__47DBAE45");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TbFarmerEquipments)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Farmer__farme__48CFD27E");
        });

        modelBuilder.Entity<TbFarmerLibraryResource>(entity =>
        {
            entity.HasKey(e => new { e.FarmerId, e.FarmerResource }).HasName("PK__tb_Farme__31BED6777574C161");

            entity.ToTable("tb_FarmerLibraryResource");

            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.FarmerResource)
                .HasMaxLength(255)
                .HasColumnName("farmer_resource");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TbFarmerLibraryResources)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Farmer__farme__571DF1D5");
        });

        modelBuilder.Entity<TbForumPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__tb_Forum__3ED78766E2F60448");

            entity.ToTable("tb_ForumPost");

            entity.Property(e => e.PostId)
                .HasMaxLength(255)
                .HasColumnName("post_id");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.ForumId)
                .HasMaxLength(255)
                .HasColumnName("forum_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.PostText)
                .HasMaxLength(1500)
                .HasColumnName("post_text");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TbForumPosts)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_ForumP__farme__7A672E12");

            entity.HasOne(d => d.Forum).WithMany(p => p.TbForumPosts)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_ForumP__forum__797309D9");
        });

        modelBuilder.Entity<TbPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__tb_Payme__ED1FC9EA361258DA");

            entity.ToTable("tb_Payment");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(255)
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.GatewayId)
                .HasMaxLength(255)
                .HasColumnName("gateway_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.TbPayments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Paymen__user___06CD04F7");
        });

        modelBuilder.Entity<TbPaymentGateway>(entity =>
        {
            entity.HasKey(e => e.GatewayId).HasName("PK__tb_Payme__0AF5B00BB068C095");

            entity.ToTable("tb_PaymentGateway");

            entity.Property(e => e.GatewayId)
                .HasMaxLength(255)
                .HasColumnName("gateway_id");
            entity.Property(e => e.GatewayDetails)
                .HasMaxLength(255)
                .HasColumnName("gateway_details");
            entity.Property(e => e.GatewayName)
                .HasMaxLength(255)
                .HasColumnName("gateway_name");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TbQuery>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__tb_Query__E793E349F0263CA1");

            entity.ToTable("tb_Query");

            entity.Property(e => e.QueryId)
                .HasMaxLength(255)
                .HasColumnName("query_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(255)
                .HasColumnName("category_id");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.QueryDescription)
                .HasMaxLength(255)
                .HasColumnName("query_description");
            entity.Property(e => e.QueryTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("query_title");
            entity.Property(e => e.TimeStamp)
                .HasColumnType("datetime")
                .HasColumnName("time_stamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Category).WithMany(p => p.TbQueries)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__tb_Query__catego__60A75C0F");

            entity.HasOne(d => d.Farmer).WithMany(p => p.TbQueries)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Query__farmer__5FB337D6");
        });

        modelBuilder.Entity<TbRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__tb_Ratin__D35B278BBBC733EA");

            entity.ToTable("tb_Rating");

            entity.Property(e => e.RatingId)
                .HasMaxLength(255)
                .HasColumnName("rating_id");
            entity.Property(e => e.QueryId)
                .HasMaxLength(255)
                .HasColumnName("query_id");
            entity.Property(e => e.RatingComment)
                .HasMaxLength(255)
                .HasColumnName("rating_comment");
            entity.Property(e => e.RatingValue).HasColumnName("rating_value");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Query).WithMany(p => p.TbRatings)
                .HasForeignKey(d => d.QueryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Rating__query__7D439ABD");

            entity.HasOne(d => d.User).WithMany(p => p.TbRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Rating__user___7E37BEF6");
        });

        modelBuilder.Entity<TbRental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__tb_Renta__67DB611B512A52C5");

            entity.ToTable("tb_Rental");

            entity.Property(e => e.RentalId)
                .HasMaxLength(255)
                .HasColumnName("rental_id");
            entity.Property(e => e.EquipmentId)
                .HasMaxLength(255)
                .HasColumnName("equipment_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.RentalEndDate).HasColumnName("rental_end_date");
            entity.Property(e => e.RentalStartDate).HasColumnName("rental_start_date");
            entity.Property(e => e.TotalCost).HasColumnName("total_cost");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");

            entity.HasOne(d => d.Equipment).WithMany(p => p.TbRentals)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Rental__updat__4D94879B");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tb_Role__760965CCC0372B9F");

            entity.ToTable("tb_Role");

            entity.HasIndex(e => e.RoleName, "UQ__tb_Role__783254B1C806BEB0").IsUnique();

            entity.Property(e => e.RoleId)
                .HasMaxLength(255)
                .HasColumnName("role_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
        });

        modelBuilder.Entity<TbTempAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__tb_TempA__33724318221596F2");

            entity.ToTable("tb_TempAnswer");

            entity.Property(e => e.AnswerId)
                .HasMaxLength(255)
                .HasColumnName("answer_id");
            entity.Property(e => e.AnswerText)
                .HasMaxLength(1000)
                .HasColumnName("answer_text");
            entity.Property(e => e.ConsultantId)
                .HasMaxLength(255)
                .HasColumnName("consultant_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.QueryId)
                .HasMaxLength(255)
                .HasColumnName("query_id");
            entity.Property(e => e.TimeStamp)
                .HasMaxLength(255)
                .HasColumnName("time_stamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<TbTempQuery>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__tb_TempQ__E793E349F1D5D6DB");

            entity.ToTable("tb_TempQuery");

            entity.Property(e => e.QueryId)
                .HasMaxLength(255)
                .HasColumnName("query_id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(255)
                .HasColumnName("category_id");
            entity.Property(e => e.FarmerId)
                .HasMaxLength(255)
                .HasColumnName("farmer_id");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.QueryDescription)
                .HasMaxLength(255)
                .HasColumnName("query_description");
            entity.Property(e => e.QueryTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("query_title");
            entity.Property(e => e.TimeStamp)
                .HasMaxLength(255)
                .HasColumnName("time_stamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<TbUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tb_Users__B9BE370F99E55C94");

            entity.ToTable("tb_Users");

            entity.HasIndex(e => e.AadharNumber, "UQ__tb_Users__9CF469850DF86CA9").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__tb_Users__AB6E616402FD6D4D").IsUnique();

            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");
            entity.Property(e => e.AadharNumber)
                .HasMaxLength(255)
                .HasColumnName("aadhar_number");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.InsertedBy)
                .HasMaxLength(255)
                .HasColumnName("inserted_by");
            entity.Property(e => e.InsertedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("inserted_date");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId)
                .HasMaxLength(255)
                .HasColumnName("role_id");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.TbUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tb_Users__role_i__2D27B809");
        });

        modelBuilder.Entity<VwGetConsultantsHaveAnswerToQueriesWithMultipleCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Get_ConsultantsHaveAnswerToQueriesWithMultipleCategories");

            entity.Property(e => e.AnswerText)
                .HasMaxLength(1000)
                .HasColumnName("answer_text");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.ConsultantName)
                .HasMaxLength(255)
                .HasColumnName("Consultant Name");
            entity.Property(e => e.QueryTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("query_title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
