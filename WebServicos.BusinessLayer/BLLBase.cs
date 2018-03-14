using System.Collections.Generic;

namespace WebServicos.BusinessLayer
{
    public abstract class BLLBase<T>
    {
        public virtual T Get(int id)
        {
            return (T)(object)this;
        }

        public virtual List<T> List()
        {
            return (List<T>)(object)this;
        }

        public virtual int Add(T model)
        {
            return (int)(object)this;
        }

        public virtual int Remove(T model)
        {
            return (int)(object)this;
        }
    }
}