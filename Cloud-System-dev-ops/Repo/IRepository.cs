using Cloud_System_dev_ops.Models;
using System.Collections.Generic;

namespace Cloud_System_dev_ops.Repo
{
    public interface IRepository<T>
    {
        T UpdateObject(T Object);

        T CreateObject(T Object);

        IEnumerable<T> GetObject();
        T DeleteObject(T Object);
    }
}