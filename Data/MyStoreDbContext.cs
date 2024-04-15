using Microsoft.EntityFrameworkCore;
using StoreDb.Models;

namespace StoreDb.Data
{
    public class MyStoreDbContext : DbContext
    {
        public MyStoreDbContext(DbContextOptions<MyStoreDbContext> options) :
            base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public void AddData()
        {
            //kollar ifall det redan finns data i tabellerna annars läggs det till mer, Unvika att det läggs in fler varje gång-
            if (!Employees.Any())
            {
                // Skapa och lägg till en anställd
                var employee1 = new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Birthdate = new DateTime(1990, 1, 1),
                    
                };
                Employees.Add(employee1);
                //
                var employee2 = new Employee
                {
                    FirstName = "Tilda",
                    LastName = "Turner",
                    Birthdate = new DateTime(1991, 2, 12),
                    
                };
                Employees.Add(employee2);
                //
                var employee3 = new Employee
                {
                    FirstName = "Harry",
                    LastName = "Drömland",
                    Birthdate = new DateTime(1990, 4, 5),
                   
                };
                Employees.Add(employee3);

                // Spara ändringar i databasen
                SaveChanges();
            }
        }
    }
}
