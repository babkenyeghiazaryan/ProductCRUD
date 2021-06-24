using InterviewTask.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace InterviewTask.Data.Extensions
{
    /// <summary>
    /// Extension method for "ModelBuilder" which can be used in "OnModelCreating" method in ApplicationDBContext
    /// </summary>
    public static class ModelBuilderExtensions
    {
        private static Random gen = new Random();
        private static string[] CascadeDeletePermittedFK = new string[]{
            typeof(Product).FullName
        };
        
        public static void DataSeed(this ModelBuilder builder)
        {
            var fakeData = new List<Product>();
            for (int i = 0; i < 3000; i++)
            {
                var product = new Product();
                product.Id = i + 1;
                product.Name = Faker.Company.Name();
                product.Description = Faker.Lorem.Paragraph(1);
                product.Available = Faker.RandomNumber.Next() > (Int32.MaxValue / 2);
                product.DateCreatedUTC = RandomDay();
                product.Price = RandomDecimal();
                fakeData.Add(product);
            }

            builder.Entity<Product>().HasData(fakeData.ToArray());
        }

        //Gathers the types, for which cascadeDelete is permitted.
        public static string[] CascadeDeletePermitted_FK_Types(this ModelBuilder builder)
        {
            return CascadeDeletePermittedFK;
        }

        
        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        private static decimal RandomDecimal()
        {
            var rnd = new Random();
            int randomInt = rnd.Next(0, 100);

            double randomDouble = rnd.Next(0, 999999999);
            decimal randomDec = Convert.ToDecimal(randomInt) + Convert.ToDecimal((randomDouble / 1000000000));

            return randomDec;
        }
    }
}
