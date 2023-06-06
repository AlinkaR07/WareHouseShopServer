using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreApp.DAL.Models;
using AspNetCoreApp.DAL.Repository;

namespace DAL.Repository
{
    public class DbRepos : IDbRepos
    {
        private Context db = new Context();
        private GeneralRepository<Tovar> tovarRepository;
        private GeneralRepository<Order> orderRepository;
        private GeneralRepository<Write> writeRepository;
        private GeneralRepository<LineOrder> lineorderRepository;
        private GeneralRepository<LineWrite> linewriteRepository;

        public GeneralRepository<Tovar> TovarRepository
        {
            get
            {
                if (this.tovarRepository == null)
                {
                    this.tovarRepository = new GeneralRepository<Tovar>(db);
                }
                  return tovarRepository;
            }
        }

        public GeneralRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new GeneralRepository<Order>(db);
                }
                 return orderRepository;
            }
        }

        public GeneralRepository<Write> WriteRepository
        {
            get
            {
                if (this.writeRepository == null)
                {
                    this.writeRepository = new GeneralRepository<Write>(db);
                }
                return writeRepository;
            }
        }
        public GeneralRepository<LineOrder> LineOrderRepository
        {
            get
            {
                if (this.lineorderRepository == null)
                {
                    this.lineorderRepository = new GeneralRepository<LineOrder>(db);
                }
                return lineorderRepository;
            }
        }

        public GeneralRepository<LineWrite> LineWriteRepository
        {
            get
            {
                if (this.linewriteRepository == null)
                {
                    this.linewriteRepository = new GeneralRepository<LineWrite>(db);
                }
                return linewriteRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}

