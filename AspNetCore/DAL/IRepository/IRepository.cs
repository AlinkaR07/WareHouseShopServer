using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetItem(int cod);
        T GetItem(string cod);
        List<T> GetList(); // получение всех объектов
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int cod); // удаление объекта
        int Save();
    }
}
