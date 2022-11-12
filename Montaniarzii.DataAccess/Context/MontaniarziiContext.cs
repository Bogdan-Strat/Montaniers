using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Montaniarzii.DataAccess;
using Montaniarzii.Entities.Entities;

#nullable disable

namespace Montaniarzii.DataAccess.Context
{
    public partial class MontaniarziiContext : DbContext
    {
        public MontaniarziiContext()
        {
        }

        public MontaniarziiContext(DbContextOptions<MontaniarziiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<AttractionXphoto> AttractionXphotos { get; set; }
        public virtual DbSet<AvatarPhoto> AvatarPhotos { get; set; }
        public virtual DbSet<Difficulty> Difficulties { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Marking> Markings { get; set; }
        public virtual DbSet<ParticipationInTrip> ParticipationInTrips { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Rescue> Rescues { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TrainStation> TrainStations { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripXattraction> TripXattractions { get; set; }
        public virtual DbSet<TripXphoto> TripXphotos { get; set; }
        public virtual DbSet<TypeAttraction> TypeAttractions { get; set; }
        public virtual DbSet<TypePost> TypePosts { get; set; }
        public virtual DbSet<TypePublicity> TypePublicities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Warning> Warnings { get; set; }
        public virtual DbSet<SuggestionAttraction> SuggestionAttractions { get; set; }
        public virtual DbSet<Like> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Initial Catalog=Montaniarzii;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.ToTable("Attraction");

                entity.Property(e => e.AttractionId).ValueGeneratedNever();

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 10)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 10)");

                entity.Property(e => e.Mountains).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TypeAttraction)
                    .WithMany(p => p.Attractions)
                    .HasForeignKey(d => d.TypeAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attraction_TypeAttraction");
            });

            modelBuilder.Entity<AttractionXphoto>(entity =>
            {
                entity.HasKey(e => new { e.AttractionId, e.PhotoId });

                entity.ToTable("AttractionXPhoto");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.AttractionXphotos)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionXPhoto_Attraction");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.AttractionXphotos)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionXPhoto_Photo");
            });

            modelBuilder.Entity<AvatarPhoto>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PhotoId });

                entity.ToTable("AvatarPhoto");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.AvatarPhotos)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AvatarPhoto_Photo");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AvatarPhotos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AvatarPhoto_User");
            });

            modelBuilder.Entity<Difficulty>(entity =>
            {
                entity.ToTable("Difficulty");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.FollowingUserId, e.FollowedUserId });

                entity.ToTable("Follow");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.FollowedUser)
                    .WithMany(p => p.FollowFollowedUsers)
                    .HasForeignKey(d => d.FollowedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Follow_UserFollowed");

                entity.HasOne(d => d.FollowingUser)
                    .WithMany(p => p.FollowFollowingUsers)
                    .HasForeignKey(d => d.FollowingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Follow_FollowingUser");
            });

            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TripId });

                entity.ToTable("Invitation");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invitation_Trip");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Invitations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invitation_User");
            });

            modelBuilder.Entity<Marking>(entity =>
            {
                entity.ToTable("Marking");

                entity.Property(e => e.MarkingName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ParticipationInTrip>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.TripId });

                entity.ToTable("ParticipationInTrip");

                entity.Property(e => e.ResponseDate).HasColumnType("datetime");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.ParticipationInTrips)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participation_Trip");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ParticipationInTrips)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Participation_User");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.PhotoId).ValueGeneratedNever();

                entity.Property(e => e.Path).IsRequired();
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");
            });

            modelBuilder.Entity<Rescue>(entity =>
            {
                entity.ToTable("Rescue");

                entity.Property(e => e.RescueId).ValueGeneratedNever();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.RescueName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TrainStation>(entity =>
            {
                entity.ToTable("TrainStation");

                entity.Property(e => e.TrainStationId).ValueGeneratedNever();

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 10)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 10)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.TripId).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Equipment)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TripDate).HasColumnType("datetime");

                entity.HasOne(d => d.Difficulty)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.DifficultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trip_Difficulty");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.RatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trip_Rating");

                entity.HasOne(d => d.TypePost)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TypePostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trip_TypePost");

                entity.HasOne(d => d.TypePublicity)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TypePublicityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trip_TypePublicity");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trip_User");
            });

            modelBuilder.Entity<TripXattraction>(entity =>
            {
                entity.HasKey(e => new { e.AttractionId, e.TripId, e.OrderNumber });

                entity.ToTable("TripXAttraction");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.TripXattractions)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripXAttraction_Attraction");

                entity.HasOne(d => d.Marking)
                    .WithMany(p => p.TripXattractions)
                    .HasForeignKey(d => d.MarkingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripXAttraction_Marking");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripXattractions)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripXAttraction_Trip");
            });

            modelBuilder.Entity<TripXphoto>(entity =>
            {
                entity.HasKey(e => new { e.TripId, e.PhotoId });

                entity.ToTable("TripXPhoto");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.TripXphotos)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripXPhoto_Photo");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripXphotos)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripXPhoto_Trip");
            });

            modelBuilder.Entity<TypeAttraction>(entity =>
            {
                entity.ToTable("TypeAttraction");

                entity.Property(e => e.TypaAttractionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TypePost>(entity =>
            {
                entity.ToTable("TypePost");

                entity.Property(e => e.TypePostName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TypePublicity>(entity =>
            {
                entity.ToTable("TypePublicity");

                entity.Property(e => e.TypePublicityName)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UK_EMAIL")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UK_USERNAME")
                    .IsUnique();

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.RegisteredDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Warning>(entity =>
            {
                entity.ToTable("Warning");

                entity.Property(e => e.WarningId).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Warnings)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Warning__Created__4F47C5E3");

            });

            modelBuilder.Entity<SuggestionAttraction>(entity =>
            {
                entity.ToTable("SuggestionAttraction");

                entity.Property(e => e.SuggestionAttractionId).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(12, 10)");

                entity.Property(e => e.Location).HasMaxLength(500);

                entity.Property(e => e.Longitude).HasColumnType("decimal(12, 10)");

                entity.Property(e => e.Mountains).HasMaxLength(100);

                entity.Property(e => e.AttractionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.TypeAttraction)
                    .WithMany(p => p.SuggestionAttractions)
                    .HasForeignKey(d => d.TypeAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuggestionAttraction_TypeAttraction");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SuggestionAttractions)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SuggestionAttraction_User");

            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasKey(e => new { e.TripId, e.UserId });

                entity.ToTable("Like");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TripLike");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLike");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
