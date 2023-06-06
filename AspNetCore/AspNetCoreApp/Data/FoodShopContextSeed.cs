using Microsoft.Extensions.Hosting;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using AspNetCoreApp.DAL.Models;

namespace AspNetCoreApp.Data
{
    public class FoodShopContextSeed
    {
        public static async Task SeedAsync(Context context)
        {
            try
            {
                context.Database.EnsureCreated();
                if (context.Order.Any())
                {
                    return;
                }
                var orders = new Order[]
                {
                  new Order{ DataOrder= DateTime.Today, FIOworker_FK_="Смирнова А.Л.", NameOrganizationPostavshik_FK_="Фруто-Няня", StatusOrder="заказан"},
                };
                foreach (Order o in orders)
                {
                    context.Order.Add(o);
                }
                await context.SaveChangesAsync();
                var lineorders = new LineOrder[]
                {
                             new LineOrder { NumberOrder_FK_=1, CodTovara_FK_=1, Name="Мармелад Осьминожка", CountOrder=35, PurchasePrice=27.ToString() }
            };
                foreach (LineOrder lo in lineorders)
                {
                    context.LineOrder.Add(lo);
                }
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            try
            {
                context.Database.EnsureCreated();
                if (context.Write.Any())
                {
                    return;
                }
                var writes = new Write[]
                   {
                  new Write{ DataWrite= DateTime.Today, FIOworker_FK_="Смирнова А.Л."}
                   };
                foreach (Write w in writes)
                {
                    context.Write.Add(w);
                }
                await context.SaveChangesAsync();
                var linewrites = new LineWrite[]
                {
                             new LineWrite { CodTovara_FK_=3, Name="Сок яблочный", Count=10, Summa=300 }
            };
                foreach (LineWrite lw in linewrites)
                {
                    context.LineWrite.Add(lw);
                }
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            try
            {
                context.Database.EnsureCreated();
                if (context.Tovar.Any())
                {
                    return;
                }
                var tovars = new Tovar[]
                   {
                  new Tovar{  Name="Молоко Домик в деревне", Category="Молочные проудкты", Count=10, Price=10}
                   };
                foreach (Tovar t in tovars)
                {
                    context.Tovar.Add(t);
                }
                await context.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }
     }
}
