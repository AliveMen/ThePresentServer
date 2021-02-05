using Microsoft.EntityFrameworkCore;
using ThePresentServer.Data.Entities;

namespace ThePresentServer.Data.Repository
{
    public class ThePresentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=ThePresent2;Persist Security Info=True;User ID=virto;Password=virto;Connect Timeout=30");
            base.OnConfiguring(optionsBuilder);
        }

        public ThePresentDbContext(DbContextOptions<ThePresentDbContext> options)  : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserEntity>().ToTable("User").HasKey(x => x.Id);
            builder.Entity<UserEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
            builder.Entity<UserEntity>().HasIndex(i => i.Username).IsUnique().HasDatabaseName("IX_User_Name");
            builder.Entity<UserEntity>().HasOne(m => m.ParentUser).WithMany(x => x.Friends).HasForeignKey(x=>x.ParentUserId);


            builder.Entity<PresentEntity>().ToTable("Present").HasKey(x => x.Id);
            builder.Entity<PresentEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
            builder.Entity<PresentEntity>().HasOne(m => m.User).WithMany(x => x.Presents)
                .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<FriendEntity>().ToTable("Friend").HasKey(x => x.Id);
            //builder.Entity<FriendEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
            //builder.Entity<FriendEntity>().HasOne(m => m.User).WithMany(x => x.Friends)
            //    .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
