using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ContactsContext.ReadModel.SqlEf6
{

    public class ContactsDb : DbContext
    {
        static ContactsDb()
        {
            Database.SetInitializer(new ContactsDbInitializer());
        }

        public DbSet<ContactDao> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder m)
        {
            base.OnModelCreating(m);

            var contact = m.Entity<ContactDao>();
            contact.Property(e => e.Code).IsUnicode(false);
            contact.Property(e => e.PicturePath).IsUnicode(false);
        }
    }

    public class ContactsDbInitializer : 
        DropCreateDatabaseIfModelChanges<ContactsDb>
    {
        protected override void Seed(ContactsDb context)
        {
            base.Seed(context);
        }
    }

    public static class ContactsDbNames
    {
        public const string Schema = "ContactsBC";
        public const string ContactTable = "Contact";
    }

    [Table(ContactsDbNames.ContactTable, Schema = ContactsDbNames.Schema)]
    public class ContactDao
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ContactId { get; set; }

        [MaxLength(10)] // ANSI - no need for unicode
        public string Code { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(250)] // ANSI - no need for unicode
        public string PicturePath { get; set; }
    }
}
