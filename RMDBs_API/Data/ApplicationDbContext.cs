using Microsoft.EntityFrameworkCore;
using RMDBs_API.Model;

namespace RMDBs_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext)
        {
        }

        // DbSets for other entities
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<ActorMovieAward> ActorMovieAwards { get; set; }
        public DbSet<AwardCategory> AwardCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieMedia1> MovieMedias1 { get; set; }
        public DbSet<Popularity> Popularities { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ReceiverType> ReceiverTypes { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<User> Users { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Ensure Enum Property Conversion Works
            modelBuilder.Entity<Movie>()
                .Property(m => m.productionStatus)
                .HasConversion<string>();

            // ✅ Ensure Popularity Table Uses Current Timestamp
            modelBuilder.Entity<Popularity>(entity =>
            {
                entity.ToTable("Popularity");
                entity.Property(e => e.RecordedDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Default timestamp
            });

            // ✅ Ensure UserRating Has Correct Relationship With Users
            modelBuilder.Entity<UserRating>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Ensure Comments Have Correct Relationship With Users
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}