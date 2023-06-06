using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreApp.DAL.Models;
using AspNetCoreApp.DAL.Models.DTO;

namespace AspNetCoreApp.BLL
{
    public interface IDbCrud
    {
        Order GetOrder(int number);
        IEnumerable<Order> AllGetOrders();
        LineOrder GetLineOrders(int id);
        IEnumerable<LineOrder> AllGetLineOrders();
        Write GetWrites(int number);
        IEnumerable<Write> AllGetWrites();
        LineWrite GetLineWrites(int id);
        IEnumerable<LineWrite> AllGetLinesWrites();
        Tovar GetTovars(int cod);
        IEnumerable<Tovar> AllGetTovars();
        void CreateOrder(Order order);
        void CreateLineOrder(LineOrder lineOrder);
        void CreateWrite(Write write);
        void CreateLineWrite(LineWrite lineWrite);
        void CreateTovar(Tovar tovar);
        void UpdateOrder(Order order);
        void UpdateLineOrder(LineOrder lineOrder);
        void UpdateWrite(Write write);
        void UpdateLineWrite(LineWrite lineWrite);
        void UpdateTovar(Tovar tovar);
        void DeleteOrder(int number);
        void DeleteLineOrder(int id);
        void DeleteWrite(int number);
        void DeleteLineWrite(int id);
        void DeleteTovar(int cod);
        bool Save();

    }
}
