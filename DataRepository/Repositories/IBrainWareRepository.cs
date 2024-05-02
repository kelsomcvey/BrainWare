using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Repositories
{
    public interface IBrainWareRepository
    {
        public abstract Task<IEnumerable<T>> ExecuteStoredProcedure<T>(string storedProcedure, object parameters);
      
    }
}
