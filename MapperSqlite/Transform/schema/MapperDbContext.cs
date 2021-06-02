using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MapperSqlite.Transform.schema
{
    public partial class MapperDbContext : DbContext
    {
        public MapperDbContext()
        {
        }

        public MapperDbContext(DbContextOptions<MapperDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Ordersproduct> Ordersproducts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Representative> Representatives { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\code\\Mapper\\Mapper\\bin\\Debug\\net5.0\\MapperDb.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Birthdate)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("birthdate");

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("name");

                entity.Property(e => e.Repid)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("REPID");

                entity.Property(e => e.State)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("state");

                entity.Property(e => e.Telephone)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("telephone");

                entity.HasOne(d => d.Rep)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.Repid);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Clientid)
                    .HasColumnType("integer")
                    .HasColumnName("clientid");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("timestamp");

                entity.Property(e => e.Total)
                    .HasColumnType("real")
                    .HasColumnName("total");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Clientid)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Ordersproduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ordersproducts");

                entity.Property(e => e.Itemid)
                    .HasColumnType("integer")
                    .HasColumnName("itemid");

                entity.Property(e => e.Orderid)
                    .HasColumnType("integer")
                    .HasColumnName("orderid");

                entity.Property(e => e.Pricepaid)
                    .HasColumnType("real")
                    .HasColumnName("pricepaid");

                entity.Property(e => e.Qty)
                    .HasColumnType("integer")
                    .HasColumnName("qty");

                entity.HasOne(d => d.Item)
                    .WithMany()
                    .HasForeignKey(d => d.Itemid)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Category)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("category");

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("real")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<Representative>(entity =>
            {
                entity.HasKey(e => e.Repnumber);

                entity.ToTable("representatives");

                entity.Property(e => e.Repnumber)
                    .HasColumnType("nvarchar(20)")
                    .HasColumnName("repnumber");

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("phone");

                entity.Property(e => e.State)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("state");

                entity.Property(e => e.Telephone)
                    .HasColumnType("nvarchar(200)")
                    .HasColumnName("telephone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
