using Microsoft.EntityFrameworkCore;
using ORM_work.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ORM_work
{
    class Program
    {
        static async Task Main()
        {
            await TransactionAsync();
            
            using (var context = new FacultiesContext())
            {

                var result = context.Students.Join(context.Groups, s => s.GroupID, g => g.GroupId, (s, g) => new { s, g })
                                             .Join(context.Faculties, sgf => sgf.g.FacultyId, f => f.FacultyId, (sgf, f) => new { sgf, f })
                                             .Select(r => new {
                                                 r.sgf.s.FirstName,
                                                 r.sgf.s.LastName,
                                                 r.sgf.g.GroupName,
                                                 r.f.FacultyName
                                             });

                foreach (var item in result)
                {
                    Console.WriteLine("Student {0} {1} study at {2} in {3} ", item.FirstName, item.LastName, item.GroupName, item.FacultyName);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static async Task TransactionAsync()
        {
            using var context = new FacultiesContext();
            await using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var faculty = new Faculty() { FacultyName = "ApriorIT" };
                    context.Faculties.Add(faculty);

                    context.Groups.AddRange(new Entities.Group[] {
                       new Entities.Group() { GroupName = "Alfa", FacultyId = 1 },
                       new Entities.Group() { GroupName = "Bravo", FacultyId = 1 },
                       new Entities.Group() { GroupName = "Charlie", FacultyId = 1 },
                       new Entities.Group() { GroupName = "Delta", FacultyId = 1 }
                    });

                    context.Students.AddRange(new Entities.Student[] {
                     new Entities.Student() { FirstName = "Linus", LastName="Torvalds", GroupID = 1 },
                     new Entities.Student() { FirstName = "Donald", LastName="Knuth", GroupID = 2 },
                     new Entities.Student() { FirstName = "James", LastName="Gosling", GroupID = 3 },
                     new Entities.Student() { FirstName = "Andres", LastName="Heilsberg", GroupID = 4 },
                     new Entities.Student() { FirstName = "Bjarne", LastName="Stroustrup", GroupID = 1 },
                     new Entities.Student() { FirstName = "Vasja", LastName="Prosto", GroupID = 2 },
                     new Entities.Student() { FirstName = "Petro", LastName="Kozak", GroupID = 3 },
                     new Entities.Student() { FirstName = "Ivan", LastName="Kak-Ivan", GroupID = 4 }
                    });
                    context.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine("Whups! Bad entry");
                    throw;
                }
            }
        }
    }
}
