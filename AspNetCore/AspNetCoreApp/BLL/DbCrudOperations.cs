using AspNetCoreApp.DAL.Models;
using AspNetCoreApp.DAL.Models.DTO;
using AspNetCoreApp.DAL.Repository;

namespace AspNetCoreApp.BLL
{
    public class DbCrudOperations : IDbCrud
    {
        IDbRepos db;

        public DbCrudOperations(IDbRepos repos)
        {
            this.db = repos;
        }

        public Order GetOrder(int number)
        {
            return db.OrderRepository.GetByID(number);
             
        }
        public IEnumerable<Order> AllGetOrders()
        {
            return db.OrderRepository.Get();
        }
        public LineOrder GetLineOrders(int id)
        {
            return db.LineOrderRepository.GetByID(id);
        }
        public IEnumerable<LineOrder> AllGetLineOrders()
        {
            return db.LineOrderRepository.Get();
        }
        public Write GetWrites(int number)
        {
            return db.WriteRepository.GetByID(number);
        }
        public IEnumerable<Write> AllGetWrites()
        {
            return db.WriteRepository.Get();
        }
        public LineWrite GetLineWrites(int id)
        {
            return db.LineWriteRepository.GetByID(id);
        }
        public IEnumerable<LineWrite> AllGetLinesWrites()
        {
            return db.LineWriteRepository.Get();
        }
        public Tovar GetTovars(int cod)
        {
            return db.TovarRepository.GetByID(cod);
        }
        public IEnumerable<Tovar> AllGetTovars()
        {
            return db.TovarRepository.Get();
        }
        public void CreateOrder(Order order)
        {
            db.OrderRepository.Insert(order);
            Save();
        }
        public void CreateLineOrder(LineOrder lineOrder)
        {
            db.LineOrderRepository.Insert(lineOrder); 
            Save();
        }
        public void CreateWrite(Write write)
        {
            db.WriteRepository.Insert(write);
            Save();
        }
        public void CreateLineWrite(LineWrite lineWrite)
        {
            db.LineWriteRepository.Insert(lineWrite);
            Save();
        }
        public void CreateTovar(Tovar tovar)
        {
            db.TovarRepository.Insert(tovar);
            Save();
        }
        public void UpdateOrder(Order order)
        {
            db.OrderRepository.Update(order);
            Save();
        }
        public void UpdateLineOrder(LineOrder lineOrder)
        {
            db.LineOrderRepository.Update(lineOrder); 
            Save();
        }
        public void UpdateWrite(Write write)
        {
            db.WriteRepository.Update(write);
        }
        public void UpdateLineWrite(LineWrite lineWrite)
        {
            db.LineWriteRepository.Update(lineWrite);
            Save();
        }
        public void UpdateTovar(Tovar tovar)
        {
            db.TovarRepository.Update(tovar);
            Save();
        }
        public void DeleteOrder(int number)
        {
            db.OrderRepository.Delete(number);
            Save();
        }
        public void DeleteLineOrder(int id)
        {
            db.LineOrderRepository.Delete(id);
            Save();
        }

        public void DeleteWrite(int number)
        {
            db.WriteRepository.Delete(number);
            Save();
        }

        public void DeleteLineWrite(int id)
        {
            db.LineWriteRepository.Delete(id);
            Save();
        }
        public void DeleteTovar(int cod)
        {
            db.TovarRepository.Delete(cod);
            Save();
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }
    }
}
