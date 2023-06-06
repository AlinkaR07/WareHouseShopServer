using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IRepository;
using DAL.Models;

namespace DAL.Repository
{
    public class DbRepos : IDbRepos
    {
        private Context db;
        private TovarRepository tovarRepository;
        private OrderRepository orderRepository;
        private WriteRepository writeRepository;
        private LineOrderRepository lineorderRepository;
        private LineWriteRepository linewriteRepository;
        public DbRepos()
        {
            db = new Context();
        }

        public IRepository<Tovar> tovar
        {
            get
            {
                if (tovarRepository == null)
                    tovarRepository = new TovarRepository(db);
                return tovarRepository;
            }
        }

        public IRepository<Order> order
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }

        public IRepository<Write> write
        {
            get
            {
                if (writeRepository == null)
                    writeRepository = new WriteRepository(db);
                return writeRepository;
            }
        }
        public IRepository<LineOrder> lineOrder
        {
            get
            {
                if (lineorderRepository == null)
                    lineorderRepository = new LineOrderRepository(db);
                return lineorderRepository;
            }
        }

        public IRepository<LineWrite> lineWrite
        {
            get
            {
                if (linewriteRepository == null)
                    linewriteRepository = new LineWriteRepository(db);
                return linewriteRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}

