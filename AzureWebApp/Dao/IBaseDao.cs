using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureWebApp.Dao
{
    public interface IBaseDao<T1, T2>
    {
        T2 ExecuteSQL(T1 input);
    }
}
