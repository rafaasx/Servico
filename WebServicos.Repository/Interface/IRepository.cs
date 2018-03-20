using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServicos.Repository.Interface
{
    public interface IRepository<T>
    {
        void Add(T obj);
        void AddRange(List<T> obj);
        T Get(int id);
        List<T> List();
        void Update(T obj);
        void Delete(int id);
    }
}
