using CardAPI.Entities;
using CardAPI.Models.DTOs.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardAPI.Data
{
    public class CardAPIContext : DbContext
    {
        public CardAPIContext(DbContextOptions<CardAPIContext> options) : base(options)
        {
        }

        public virtual DbSet<Cards> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cards>(ConfigureCards);
        }

        private void ConfigureCards(EntityTypeBuilder<Cards> builder)
        {
            builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cb => cb.Title)
                .HasMaxLength(255);

            builder.Property(cb => cb.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(cb => cb.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cb => cb.Address)
                .HasMaxLength(255);

            builder.Property(cb => cb.AddedDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}