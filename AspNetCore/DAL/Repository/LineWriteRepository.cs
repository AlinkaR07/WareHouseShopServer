using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
//using System.Data.Entity;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    class LineWriteRepository : IRepository<LineWrite>
    {
        private Context db;
        public LineWriteRepository(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public LineWrite GetItem(int cod)
        {
            return db.LineWrite.Find(cod);
        }
        public LineWrite GetItem(string cod)
        {
            return db.LineWrite.Find(cod);
        }
        public LineWrite GetItem(DateTime date, string name)
        {
            return db.LineWrite.Find(date, name);
        }
        public List<LineWrite> GetList()
        {
            return db.LineWrite.ToList();
        }

        public void Create(LineWrite lineWrite)
        {
            db.LineWrite.Add(lineWrite);
        }

        public void Update(LineWrite lineWrite)
        {
            db.Entry(lineWrite).State = EntityState.Modified;
        }

        public void Delete(int cod)
        {
            LineWrite Linewrite = db.LineWrite.Find(cod);
            if (Linewrite != null)
                db.LineWrite.Remove(Linewrite);
        }
        public void Delete(string cod)
        {
            LineWrite Linewrite = db.LineWrite.Find(cod);
            if (Linewrite != null)
                db.LineWrite.Remove(Linewrite);
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}

