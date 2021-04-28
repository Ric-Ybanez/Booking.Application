using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Booking.Application.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Booking.Application.DAL
{
    public class BookingAppContext : DbContext
    {
        public BookingAppContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }

        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<DAL.Models.Booking> Bookings { get; set; }
        public virtual DbSet<BookingType> BookingTypes { get; set; }
        public virtual DbSet<ChangeEvent> ChangeEvents { get; set; }
        public virtual DbSet<ChangeLog> ChangeLogs { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public int SaveChanges(int user = -1)
        {
            var auditEntries = BeforeSaveChanges(user);
            var result = base.SaveChanges();
            AfterSaveChangesAsync(auditEntries).Wait();
            return result;
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken), int user = -1)
        {
            var auditEntries = BeforeSaveChanges(user);
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await AfterSaveChangesAsync(auditEntries);
            return result;
        }

        public async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken), int user = -1)
        {
            var auditEntries = BeforeSaveChanges(user);
            var result = await base.SaveChangesAsync(cancellationToken);
            await AfterSaveChangesAsync(auditEntries);
            return result;
        }

        private IEnumerable<AuditEntry> BeforeSaveChanges(int user)
        {
            var now = DateTime.Now;
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries().
                Where(e => e.State != EntityState.Detached &&
                           e.State != EntityState.Unchanged &&
                           !(e.Entity is ChangeLog));

            var auditEntries = new List<AuditEntry>();
            foreach (var entry in entries)
            {
                var auditEntry = new AuditEntry(entry, now, user);
                auditEntry.TableName = entry.Metadata.Name;
                auditEntries.Add(auditEntry);

                if (entry.State == EntityState.Added)
                    auditEntry.Action = "INSERT";
                else if (entry.State == EntityState.Modified)
                    auditEntry.Action = "UPDATE";
                else if (entry.State == EntityState.Deleted)
                    auditEntry.Action = "DELETE";

                foreach (var prop in entry.Properties)
                {
                    if (prop.IsTemporary)
                    {
                        auditEntry.TempProps.Add(prop);
                        continue;
                    }
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.RowIds.Add(prop.CurrentValue.ToString());
                        continue;
                    }

                    string colName = prop.Metadata.Name;
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (prop.CurrentValue != null)
                            {
                                auditEntry.Values[colName] = (null, prop.CurrentValue.ToString());
                            }
                            break;
                        case EntityState.Deleted:
                            if (prop.OriginalValue != null)
                            {
                                auditEntry.Values[colName] = (prop.OriginalValue.ToString(), null);
                            }
                            break;
                        case EntityState.Modified:
                            if (prop.OriginalValue?.ToString() != prop.CurrentValue?.ToString())
                            {
                                auditEntry.Values[colName] = (prop.OriginalValue?.ToString(), prop.CurrentValue?.ToString());
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(a => !a.HasTempProps))
            {

                var changeLogs = auditEntry.GetChangeLogs();

                if (changeLogs.Any())
                {
                    var changeEvent = auditEntry.GetChangeEvent();
                    changeEvent.ChangeLogs = changeLogs;
                    ChangeEvents.Add(changeEvent);
                }
            }

            return auditEntries.Where(a => a.HasTempProps).ToList();
        }

        private async Task AfterSaveChangesAsync(IEnumerable<AuditEntry> auditEntries)
        {
            if (auditEntries == null || !auditEntries.Any()) return;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TempProps)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.RowIds.Add(prop.CurrentValue.ToString());
                    }
                    else
                    {
                        auditEntry.Values[prop.Metadata.Name] = (null, prop.CurrentValue?.ToString());
                    }
                }

                var changeLogs = auditEntry.GetChangeLogs();

                if (changeLogs.Any())
                {
                    var changeEvent = auditEntry.GetChangeEvent();
                    changeEvent.ChangeLogs = changeLogs;
                    ChangeEvents.Add(changeEvent);
                }
            }

            await base.SaveChangesAsync();
        }
    }

    public class AuditEntry
    {
        private readonly DateTime _created;
        private readonly int _createdBy;

        public AuditEntry(EntityEntry entry, DateTime created, int createdBy)
        {
            Entry = entry;
            _created = created;
            _createdBy = createdBy;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public List<string> RowIds { get; } = new List<string>();
        public Dictionary<string, (string oldVal, string newVal)> Values { get; } = new Dictionary<string, (string, string)>();
        public List<PropertyEntry> TempProps { get; } = new List<PropertyEntry>();
        public bool HasTempProps => TempProps.Any();

        public List<ChangeLog> GetChangeLogs()
        {
            var rowId = String.Join(",", RowIds);
            return Values.Select(v =>
            {
                var audit = new ChangeLog
                {
                    Operation = Action,
                    ColumnName = v.Key,
                    OldValue = v.Value.oldVal,
                    NewValue = v.Value.newVal,
                    TableName = TableName,
                    RowID = rowId
                };

                return audit;
            }).ToList();
        }

        public ChangeEvent GetChangeEvent()
        {
            var audit = new ChangeEvent
            {
                DateRecorded = _created,
                UserId = _createdBy
            };
            return audit;
        }
    }

}
