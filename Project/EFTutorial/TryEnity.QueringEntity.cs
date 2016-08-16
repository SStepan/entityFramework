using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFTutorial
{
	public partial class TryEntity
	{
		public static void QueringEntity()
		{
			// Linq method syntax
			using (var context = new SchoolEntities()) {
				var Linq2Query = context.Students.Where(s => s.StudentName == "Bill");
				var student = Linq2Query.FirstOrDefault<Student>();
			}
			// linq query syntax
			using (var context = new SchoolEntities()) {
				var L2EQuery = from st in context.Students
							   where st.StudentName == "Bill"
							   select st;
				var student = L2EQuery.FirstOrDefault<Student>();
			}

			//Entity Sql
			using (var ctx = new SchoolEntities()) {
				string sqlString = "Select VALUE st FROM SchoolEntities.Students " +
							   "AS st WHERE st.StudentName == 'Bill'";
				var objctx = (ctx as IObjectContextAdapter).ObjectContext;

				ObjectQuery<Student> students = objctx.CreateQuery<Student>(sqlString);
				Student newStudent = students.First<Student>();
			}

			//Entity SQL with entityConnection and EntityCommand
			using (var con = new EntityConnection("name=SchoolEntities")) {
				con.Open();
				EntityCommand cmd = new EntityCommand();
				cmd.CommandText = "Select VALUE st FROM SchoolEntities.Students " +
							   "AS st WHERE st.StudentName == 'Bill'";
				Dictionary<int, string> dict = new Dictionary<int, string>();
				using (EntityDataReader rdr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection)) {
					while (rdr.Read()) {
						int a = rdr.GetInt32(0);
						string b = rdr.GetString(1);
						dict.Add(a, b);
					}
				}
			}

			//Native Sql
			using (var ctx = new SchoolEntities()) {
				var StudentName =
					ctx.Students.SqlQuery("Select studentid, studentname, standardid from Student where studentname = 'Bill'")
						.FirstOrDefault<Student>();
			}
		}
	}
}