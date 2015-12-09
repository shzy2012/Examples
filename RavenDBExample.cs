using System.Collections.Generic;
using System.Linq;
using Raven.Client.Document;

namespace RavenDBExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var documentStore = new DocumentStore
            {
                Url = "http://joey1-pc:8082",
                DefaultDatabase = "Northwind"
            };
            documentStore.Initialize();

            using (var session = documentStore.OpenSession())
            {
                /* Save
                var entity = new Menu
                {
                    Name = "Breakfast Menu",
                    Courses = {
                                new Course {
                                     Name = "Waffle",
                                     Cost = 2.3m
                                  },
                                new Course {
                                     Name = "Cereal",
                                     Cost = 1.3m,
                                     Allergenics = { "Peanuts" }
                                  }
                               }
                };

                session.Store(entity);
                session.SaveChanges();
                */

                /* Get
                var menu = session.Load<Menu>("menus/1");
                Console.WriteLine(menu.Name);
                foreach (var course in menu.Courses)
                {
                    Console.WriteLine("\t{0} - {1}", course.Name, course.Cost);
                    Console.WriteLine("\t\t{0}", string.Join(",", course.Allergenics));
                }
                 *  */

                /* Query
                var menus = from menu in session.Query<Menu>()
                            where menu.Courses.Any(x => x.Cost < 2)
                            select menu;

                var m = menus.ToList();
                 */

                //session.Store(new Customer
                //{
                //    Name = "Joe Smith",
                //    Attributes =
                //                  {
                //                     {"IsAnnoyingCustomer", true},
                //                     {"SatisfactionLevel", 8.7},
                //                     {"LicensePlate", "B7D-12JA"}
                //                   }
                //});

                //session.SaveChanges();

                var get = session.Query<Customer>().Where(x => x.Attributes["IsAnnoyingCustomer"].Equals(true)).ToList();
            }
        }

        public class Menu
        {
            public Menu()
            {
                Courses = new List<Course>();
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public List<Course> Courses { get; set; }
        }

        public class Course
        {
            public Course()
            {
                Allergenics = new List<string>(10);
            }
            public string Name { get; set; }
            public decimal Cost { get; set; }
            public List<string> Allergenics { get; set; }
        }

        public class Customer
        {
            public Customer()
            {
                this.Attributes = new Dictionary<string, object>();
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public Dictionary<string, object> Attributes { get; set; }
        }
    }
}
