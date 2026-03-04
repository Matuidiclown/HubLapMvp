using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAPS.Data.Core
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> LoadData<T>(string storedProcedure, object parameters, string connectionId = "DefaultConnection");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "DefaultConnection");
    }
}
