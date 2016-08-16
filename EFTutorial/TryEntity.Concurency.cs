using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFTutorial
{
	public static partial class TryEntity
	{
		public static void Concurency()
		{
			Student student1 = null;
			Student student2 = null;

			using (var context = new SchoolEntities())
			{
				context.Configuration.ProxyCreationEnabled = false;
				student1 = context.Students.Where(s => s.StandardId == 1).Single();
			}

			using (var context = new SchoolEntities()) {
				context.Configuration.ProxyCreationEnabled = false;
				student2 = context.Students.Where(s => s.StandardId == 1).Single();
			}

			student1.StudentName = "Valera";
			student2.StudentName = "Sane";

			using (var context = new SchoolEntities())
			{
				try
				{
					context.Entry(student1).State = EntityState.Modified;
					context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException ex)
				{
					Console.WriteLine("Opt Concurency");
				}
			}

			using (var context = new SchoolEntities())
			{
				try
				{
					context.Entry(student2).State = EntityState.Modified;
					context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException ex)
				{
					Console.WriteLine("Opt Concurency");
				}
			}
		}
	}
}