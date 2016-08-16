using System;
using System.ComponentModel;
using System.Runtime.Remoting.Contexts;

namespace EFTutorial
{
	public static partial class TryEntity
	{
		public static void EntityEntry()
		{
			using (var context  = new SchoolEntities())
			{
				var student = context.Students.Find(1);
				student.StudentName= "Edik";

				var entry = context.Entry(student);

				Console.WriteLine($"EnityName: {entry.Entity.GetType().FullName}");

				Console.WriteLine("Enity State: {0}", entry.State);

				Console.WriteLine("***** Property Values *****");

				foreach (var propertyName in entry.CurrentValues.PropertyNames)
				{
					Console.WriteLine($"PropertyName: {propertyName}");

					//original value
					var origVal = entry.OriginalValues[propertyName];
					Console.WriteLine($"Original value: {origVal}");

					//current value
					var currVal = entry.CurrentValues[propertyName];
					Console.WriteLine($"Current Value: {currVal}");
				}
			}
		}

		public static void CollectionEntity()
		{
			using (var context = new SchoolEntities())
			{
				var student = context.Students.Find(1);

				var entry = context.Entry(student);
				// get navigation property fot this entity
				entry.Collection(s => s.StudentCources);

				
			}
		}
	}
}