using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ESS.SFS.DAL
{
    public partial class SmartFamilySurveyContext : DbContext
    {
       
        public SmartFamilySurveyContext(DbContextOptions<SmartFamilySurveyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCity> TblCity { get; set; }
        public virtual DbSet<TblDemographic> TblDemographic { get; set; }
        public virtual DbSet<TblFamilyStatus> TblFamilyStatus { get; set; }
        public virtual DbSet<TblForm> TblForm { get; set; }
        public virtual DbSet<TblGender> TblGender { get; set; }
        public virtual DbSet<TblLanguages> TblLanguages { get; set; }
        public virtual DbSet<TblPayment> TblPayment { get; set; }
        public virtual DbSet<TblPool> TblPool { get; set; }
        public virtual DbSet<TblPoolSurvey> TblPoolSurvey { get; set; }
        public virtual DbSet<TblSurvey> TblSurvey { get; set; }
        public virtual DbSet<TblSurveyPoolCity> TblSurveyPoolCity { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserSurvey> TblUserSurvey { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCity>(entity =>
            {
                entity.ToTable("tblCity");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<TblDemographic>(entity =>
            {
                entity.ToTable("tblDemographic");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TblFamilyStatus>(entity =>
            {
                entity.ToTable("tblFamilyStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblForm>(entity =>
            {
                entity.ToTable("tblForm");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(15);
            });

            modelBuilder.Entity<TblGender>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblLanguages>(entity =>
            {
                entity.ToTable("tblLanguages");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.ShortName).HasMaxLength(10);
            });

            modelBuilder.Entity<TblPayment>(entity =>
            {
                entity.ToTable("tblPayment");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPool>(entity =>
            {
                entity.ToTable("tblPool");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityIds)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FamilyStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PoolName).HasMaxLength(50);
            });

            modelBuilder.Entity<TblPoolSurvey>(entity =>
            {
                entity.ToTable("tblPoolSurvey");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<TblSurvey>(entity =>
            {
                entity.ToTable("tblSurvey");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<TblSurveyPoolCity>(entity =>
            {
                entity.ToTable("tblSurveyPoolCity");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.SurveyPoolId).HasColumnName("SurveyPoolID");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.JobTittle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Language).HasMaxLength(50);

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.TblUser)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("FK_tblUser_tblCity");
            });

            modelBuilder.Entity<TblUserSurvey>(entity =>
            {
                entity.ToTable("tblUserSurvey");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompletedDate).HasColumnType("datetime");

                entity.Property(e => e.SentDate).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
