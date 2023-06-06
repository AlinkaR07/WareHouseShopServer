using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IRepository;
using DAL.Models;


namespace DAL.IRepository
{
    public interface IDbRepos
    {
        IRepository<Tovar> tovar { get; }
        IRepository<Order> order { get; }
        IRepository<LineOrder> lineOrder { get; }
        IRepository<Write> write { get; }
        IRepository<LineWrite> lineWrite { get; }
        int Save();
    }
}
