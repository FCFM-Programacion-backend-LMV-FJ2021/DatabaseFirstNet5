using DatabaseFirstDWB_LMV.DataAccess;
using System;
using System.Linq;
using DatabaseFirstDWB_LMV.NorthwindData;

namespace DatabaseFirstDWB_LMV
{
    class Program
    {

        public static void InitialTest()
        {

            DWBLMVContext dbContext = new DWBLMVContext();

            //SELECT *FROM PRODUCTS
            var productsQry = dbContext.Products;
            var productList = productsQry.ToList();

            //select Title as Titulo, Cost as Costo from Products : //Este es un objeto anonimo
            var productSelectQry = dbContext.Products.Select(s => new
            {   //Proyección lambda
                Titulo = s.Title,
                Costo = s.Cost
            }).ToList();

            productSelectQry.ForEach(f => Console.WriteLine(f.Titulo));


        }

        public static void Excercise1()
        {
            //SELECT *FROM Employees; 
            var employeeQry = new NORTHWNDContext().Employees.AsQueryable();
            var output = employeeQry.ToList();
        }

        public static void Excercise2()
        {
            // Objeto NO anonimo
            //var x = new ObjectClass{ Id = 2, Title = "jkhsfjs" };
            //Objeto ANONIMO
            //var x = new { Id = 2, Title = "jkhsfjs" };

            //SELECT Title, FirstName, LastName FROM Employees WHERE Title = 'Sales Representative';
            var employeeQry = new NORTHWNDContext()
                .Employees.Select(s => new 
                {
                     s.Title,
                     s.FirstName,
                     s.LastName
                }).Where(w=> w.Title == "Sales Representative").AsQueryable();

            //Aqui materializamos el query
            var output = employeeQry.ToList();

            output.ForEach(fe => Console.WriteLine("Name: " + fe.FirstName + " Title: " + fe.Title));

        }

        public static void Excercise3()
        {

            //SELECT FirstName as Nombre, LastName as Apellido  FROM Employees WHERE Title<> 'Sales Representative
            var employeeQry = new NORTHWNDContext()
                .Employees.Select(s => new
                {
                    Nombre = s.FirstName,
                    Apellido = s.LastName,
                    s.Title,
                }).Where(w => w.Title != "Sales Representative");

            var output = employeeQry.ToList();

        }
        static void Main(string[] args)
        {
            Excercise1();
            Excercise2();
            Excercise3();
            //ejemplo insert
            Employee employee = new Employee();
            employee.FirstName = "Rolando";
            employee.City = "Monterrey";
            //...

            var dbContext = new NORTHWNDContext();
            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();


            //ejemplo delete

            var product = dbContext.Products.Where(w => w.ProductId == 1).First();
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

        }
    }
}
