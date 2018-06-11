using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace LorikeetMApp.Data
{
	public class MemberDatabase : DbContext
    {
		protected string databasePath;

		protected MemberDatabase(string databasePath)
		{
			this.databasePath = databasePath;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }

		public DbSet<ModelsLinq.MemberSQLite> MemberItems { get; set; }
		public DbSet<ModelsLinq.ContactSQLite> ContactItems { get; set; }

		public static MemberDatabase Create(string databasePath)
		{
			try
			{
				var dbContext = new MemberDatabase(databasePath);
				dbContext.Database.EnsureCreated();
				dbContext.Database.Migrate();
				return dbContext;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<ModelsLinq.ContactSQLite> GetContactItemAsync(int contactID)
		{
			return await ContactItems.SingleAsync(item => item.ContactId == contactID);
		}

		public async Task<List<ModelsLinq.MemberSQLite>> GetMemberItemsAsync()
		{
			return await MemberItems.ToListAsync();
		}

		public async Task<List<ModelsLinq.ContactSQLite>> GetContactItemsAsync(int memberID)
		{
			var contactsFromDatabase = await ContactItems.ToListAsync();
			var contactsToReturn = (from c in contactsFromDatabase
			                        where c.MemberId == memberID
									select c).ToList();
			return contactsToReturn;
		}

		public async Task<List<ModelsLinq.ContactSQLite>> GetContactItemsAsync()
        {
            return await ContactItems.ToListAsync();
        }
        
		public async Task<int> SaveMemberItemAsync(ModelsLinq.MemberSQLite memberItem)
		{
			await MemberItems.AddAsync(memberItem);

			return await SaveChangesAsync();
		}

		public async Task<int> SaveContactItemAsync(ModelsLinq.ContactSQLite contactItem)
		{
			await ContactItems.AddAsync(contactItem);

			return await SaveChangesAsync();
		}

		public void DeleteMemberItems(List<ModelsLinq.MemberSQLite> memberList)
		{
			MemberItems.RemoveRange(memberList);

			SaveChanges();
		}

		public void DeleteContactItems(List<ModelsLinq.ContactSQLite> contactList)
		{
			ContactItems.RemoveRange(contactList);

			SaveChanges();
		}
      
		public static void Delete(string databasePath)
		{
			try {
				var dbContext = new MemberDatabase(databasePath);
                dbContext.Database.EnsureDeleted();
			}
            catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
    }
}
