using AspNetCoreApp.DAL.Models;

namespace AspNetCoreApp.DAL.Repository
{
    public interface IDbRepos
    {
        GeneralRepository<Order> OrderRepository { get; }
        GeneralRepository<LineOrder> LineOrderRepository { get; }
        GeneralRepository<Write> WriteRepository { get; }
        GeneralRepository<LineWrite> LineWriteRepository { get; }
        GeneralRepository<Tovar> TovarRepository { get; }
        int Save();
    }
}
