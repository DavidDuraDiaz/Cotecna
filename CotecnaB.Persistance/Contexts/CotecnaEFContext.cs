using CotecnaB.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CotecnaB.Persistance.Contexts
{
    public class CotecnaEFContext : DbContext
    {
        public CotecnaEFContext(DbContextOptions<CotecnaEFContext> options)
            : base(options)
        {
        }

        public DbSet<Inspection> Inspection { get; set; }
        public DbSet<Inspector> Inspector { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BaseEntity>().Property<bool>("Active");
            builder.Entity<BaseEntity>().HasQueryFilter(m => EF.Property<bool>(m, "Active") == false);

            builder.Entity<InspectionInspector>()
                .HasKey(ii => new { ii.InspectionId, ii.InspectorId, ii.InspectionDate });

            builder.Entity<InspectionInspector>()
                .HasOne(ii => ii.Inspection)
                .WithMany(ition => ition.InspectionInspector)
                .HasForeignKey(ii => ii.InspectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<InspectionInspector>()
                .HasOne(ii => ii.Inspector)
                .WithMany(itor => itor.InspectionInspector)
                .HasForeignKey(ii => ii.InspectorId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["Active"] = false;
                            entry.CurrentValues["Created"] = DateTime.Today;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues["Active"] = true;
                            entry.CurrentValues["Modified"] = DateTime.Today;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["Active"] = true;
                            entry.CurrentValues["Deleted"] = DateTime.Today;
                            break;
                    }
                }
            }
        }

        //>TODO
        //validate "No more than 1 inspection per day / inspector"
        //protected override DbEntityValidationResult ValidateEntity(
        //                            DbEntityEntry entityEntry,
        //                            IDictionary<object, object> items)
        //{
        //    var result = base.ValidateEntity(entityEntry, items);

        //    CustomValidate(result);
        //    return result;
        //}

        //private void CustomValidate(DbEntityValidationResult result)
        //{
        //    ValidateInspections(result);
        //}

        //private void ValidateInspections(DbEntityValidationResult result)
        //{
        //    var c = result.Entry.Entity as Inspection;
        //    if (c == null)
        //        return;

        //    IEnumerable<Inspection> toValidate = Inspection.Where(a => a.InspectionDate == c.InspectionDate).AsEnumerable();

        //    if (toValidate != null && toValidate.Any( a => a.Inspectors.Select(o => new { o.Id }).Intersect(c.Inspectors.Select(o=> new { o.Id })).Count() == 0))

        //    {
        //        result.ValidationErrors.Add(new DbValidationError("Inspection", "No more than 1 inspection per day / inspector"));
        //    }
        //}

    }
}
