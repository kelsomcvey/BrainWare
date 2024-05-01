using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Repositories
{
    public interface IBrainWareRepository
    {
        public abstract Task<IEnumerable<T>> GetOrders<T>(string sql, object parameters = null);
    }
}
